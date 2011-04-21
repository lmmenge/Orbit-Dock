using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Orbit.Configuration;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// Future base class for Orbit Items
	/// </summary>
	public abstract class OrbitItem:IDisposable
	{
		#region Internal variables
		#region Properties variables
		private string _Name;
		private string _Description;
		private string _Parent;
		private string _ImagePath;
		private string _ToggledImagePath;
		private bool _Enabled;
		private bool _IsShown;
		private bool _IsMouseOver;
		private bool _IsToggled;
		private bool _RunAndLeave;
		private RectangleF _Rectangle;
		private int _Line;
		private double _RotationOffset;
		private Color _ColorKey;
		private float _AnimationState;
		/// <summary>
		/// Sets which context menu items are shown for this item
		/// </summary>
		protected ItemMenuFlags _MenuFlags;
		#endregion

		#region External resources variables
		/// <summary>
		/// Represents the Direct3D Device used by this item to load resources into
		/// </summary>
		protected Device display;
		#endregion

		#region Managed resources variables
		/// <summary>
		/// Icon that represents this item
		/// </summary>
		protected OrbitTexture _Icon;
		/// <summary>
		/// Icon that represents this item when it's hovered
		/// </summary>
		protected OrbitTexture _HoverIcon;
		/// <summary>
		/// Represents the vertex buffer used by this item
		/// </summary>
		protected VertexBuffer SpriteVertexBuffer;
		#endregion

		#region Other internal variables
		//private bool UseAccurateHitTesting;
		//protected float LoadedPercentage;
		#endregion
		#endregion

		#region Constructors and Destructors
		#region Main Class Constructor
		/// <summary>
		/// Creates a new instance of the OrbitItem class
		/// </summary>
		public OrbitItem(){}
		#endregion

		#region Constructors Validators
		/// <summary>
		/// Validates the device passed on by a creator
		/// </summary>
		/// <param name="D3DDevice">Direct3D Device to be validated</param>
		protected void ValidateDevice(Device D3DDevice)
		{
			if(D3DDevice==null || D3DDevice.Disposed)
				throw new ArgumentNullException("D3DDevice");
		}
		/// <summary>
		/// Validates the Images path passed on by a creator
		/// </summary>
		/// <param name="DefaultImagesPath">Path to validate</param>
		protected void ValidatePath(string DefaultImagesPath)
		{
			if(DefaultImagesPath==null)
				throw new ArgumentNullException("DefaultImagesPath");
		}
		#endregion

		#region Resource Creation
		/// <summary>
		/// Initializes common resources for this item
		/// </summary>
		protected void InitializeResources()
		{
			// create the default values for some stuff
			try
			{
				this.SpriteVertexBuffer=new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 4, display, Usage.Dynamic, CustomVertex.TransformedColoredTextured.Format, Pool.Default);
				UpdateBufferData(this.SpriteVertexBuffer, Color.FromArgb(0x00, Color.White));
			}
			catch(Exception)
			{
				throw;
			}
			this.IsMouseOver=false;
			this.IsToggled=false;
			this.RunAndLeave=false;
			this.IsShown=true;
			this.Enabled=true;
			this._AnimationState=0.0f;
			this._ColorKey=Color.Transparent;
			this._MenuFlags=ItemMenuFlags.None;
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the OrbitItem object
		/// </summary>
		public virtual void Dispose()
		{
			// disposable stuff
			UnloadTexture(ref this._Icon);
			UnloadTexture(ref this._HoverIcon);
			UnloadVertexBuffer();
		}
		#endregion

		#region Texture Unloading
		/// <summary>
		/// Unloads the specified texture from memory
		/// </summary>
		/// <param name="texture">texture to unload</param>
		protected void UnloadTexture(Texture texture)
		{
			try
			{
				if(texture!=null)
				{
					texture.Dispose();
				}
			}
			catch(Exception){}
		}
		/// <summary>
		/// Unloads the specified texture from memory
		/// </summary>
		/// <param name="texture">texture to unload</param>
		protected void UnloadTexture(ref OrbitTexture texture)
		{
			if(texture!=null)
			{
				texture.FreeReference();
				texture = null;
			}
		}
		private void UnloadVertexBuffer()
		{
			try
			{
				if(this.SpriteVertexBuffer!=null)
					this.SpriteVertexBuffer.Dispose();
			}
			catch(Exception){}
		}
		#endregion
		#endregion

		#region Direct3D Helper Methods
		#region Texture Loading
		/// <summary>
		/// Loads the icon for the item from a path
		/// </summary>
		/// <param name="texture">Texture to set</param>
		/// <returns>True if succeeded</returns>
		public bool SetIcon(OrbitTexture texture)
		{
			OrbitTexture oldIcon = this._Icon;
			this._Icon = texture;
			UnloadTexture(ref oldIcon);
			this._ImagePath = this._Icon.Path;

			return true;
		}
		/// <summary>
		/// Loads the icon for the item from a path
		/// </summary>
		/// <param name="Path">Path to load the icon from</param>
		/// <returns>True if succeeded</returns>
		public bool SetIcon(string Path)
		{
			// TODO: possibly join this path validation thing and the SetToggledIcon one
			// checking if i have the needed info
			if((Global.Configuration.Locations.ImagesPath=="" || Global.Configuration.Locations.ImagesPath==null || Path==null) && !System.IO.File.Exists(Path))
			{
				CannotLoadIcon();
				this._ImagePath=null;
				return false;
			}

			// checking for relative path
			string PathToLoad;
			if(Path.IndexOf(":\\")>0)
				PathToLoad=Path;
			else
				PathToLoad=System.IO.Path.Combine(Global.Configuration.Locations.ImagesPath, Path);

			// bail out if this doesn't exist
			if(!System.IO.File.Exists(PathToLoad))
			{
				System.Diagnostics.Debug.WriteLine("Cannot load texture: "+PathToLoad);
				CannotLoadIcon();
				this._ImagePath=null;
				return false;
			}

			// dispose an old one
			//this.UnloadTexture();

			// and load new
			// Use Loader to load texture
			OrbitTexture oldIcon = this._Icon;
			this._Icon=Orbit.Utilities.OrbitTextureLoader.Load(display, PathToLoad);
			this.UnloadTexture(ref oldIcon);
			
			// generate the sprite information
			if(this._Icon!=null)
			{
				this._ImagePath=PathToLoad;
			}
			else
			{
				CannotLoadIcon();
				this._ImagePath=null;
			}
			return true;
		}

		/// <summary>
		/// Loads the icon for the item from a bitmap
		/// </summary>
		/// <param name="NewImage">Bitmap to load the icon from</param>
		/// <returns>True if succeeded</returns>
		public bool SetIcon(Image NewImage)
		{
			// any result of this function implies that the item no longer has an image path
			this._ImagePath=null;

			// checking if i have the needed info
			if(NewImage==null)
			{
				CannotLoadIcon();
				return false;
			}

			try
			{
				//using(Bitmap IconThumb=(Bitmap)ImageHelper.GetAspectThumbnail((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
				using(Bitmap IconThumb=(Bitmap)ImageHelper.GetBestSizeFor((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
				{
					// load
					//OrbitTexture newIcon=Texture.FromBitmap(display, IconThumb, Usage.Dynamic, Pool.Default);
					OrbitTexture newIcon = OrbitTextureLoader.Load(display, IconThumb);
					if(this._Icon!=null)
					{
						OrbitTexture oldIcon=this._Icon;
						this._Icon=newIcon;
						UnloadTexture(ref oldIcon);
					}
					else
					{
						//this.UnloadTexture();
						this._Icon=newIcon;
					}
				}
				return true;
			}
			catch(Exception)
			{
				try
				{
					UnloadTexture(ref this._Icon);
				}
				catch(Exception){}
				CannotLoadIcon();
				return false;
			}
		}

		/// <summary>
		/// Loads the hover icon for the item
		/// </summary>
		/// <param name="Path">Path to load the icon from</param>
		/// <returns>True if succeeded</returns>
		public bool SetHoverIcon(string Path)
		{
			// checking if i have the needed info
			if(Global.Configuration.Locations.ImagesPath=="" || Global.Configuration.Locations.ImagesPath==null || Path==null)
			{
				CannotLoadHoverIcon();
				return false;
			}
			// checking for relative path
			string PathToLoad;
			if(Path.IndexOf(":\\")>0)
				PathToLoad=Path;
			else
				PathToLoad=System.IO.Path.Combine(Global.Configuration.Locations.ImagesPath, Path);

			// bail out if this doesn't exist
			if(!System.IO.File.Exists(PathToLoad))
			{
				System.Diagnostics.Debug.WriteLine("Cannot load texture: "+PathToLoad+". Path not found.");
				CannotLoadHoverIcon();
				return false;
			}

			// dispose an old one
			//this.UnloadHoverTexture();

			// or else load
			//this._HoverTexture=Orbit.Utilities.OrbitTextureLoader.Load(display, PathToLoad);
			OrbitTexture oldIcon = this._HoverIcon;
			this._HoverIcon=Orbit.Utilities.OrbitTextureLoader.Load(display, PathToLoad);
			this.UnloadTexture(ref oldIcon);
			
			// generate the sprite information
			if(this._HoverIcon==null)
			{
				CannotLoadHoverIcon();
			}
			return true;
		}
		/// <summary>
		/// Sets the path to be loaded when this element is set to toggled
		/// </summary>
		/// <param name="Path">Path to load the icon from</param>
		/// <returns>True if verified path exists</returns>
		public bool SetToggledIcon(string Path)
		{
			if(System.IO.File.Exists(Path))
			{
				_ToggledImagePath=Path;
				return true;
			}
			else
			{
				_ToggledImagePath=null;
				return false;
			}
		}
		#endregion

		#region Texture Loading Exception Handlers
		/// <summary>
		/// Loads the embedded texture loading error texture.
		/// </summary>
		protected void CannotLoadIcon()
		{
			// TODO: See how we can best join this and the CannotLoadHoverIcon() methods to avoid repeating code
			try
			{
				// load
				this._Icon = OrbitTextureLoader.Load(display, System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.ExecutablePath).GetManifestResourceStream("Orbit.Images.ImageErrorIcon.png"));
			}
			catch(Exception)
			{
				UnloadTexture(ref _Icon);
			}
		}
		/// <summary>
		/// Loads the embedded texture loading error texture.
		/// </summary>
		protected void CannotLoadHoverIcon()
		{
			try
			{
				// load
				this._HoverIcon = OrbitTextureLoader.Load(display, System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.ExecutablePath).GetManifestResourceStream("Orbit.Images.ImageErrorIcon.png"));
			}
			catch(Exception)
			{
				UnloadTexture(ref _HoverIcon);
			}
		}
		#endregion
		#endregion

		#region Protected Event Raisers
		/// <summary>
		/// Raises the Paint event
		/// </summary>
		protected virtual void OnPaint()
		{
			if(Paint!=null) Paint(this, new EventArgs());
		}
		/// <summary>
		/// Occurs when the ColorKey is changed
		/// </summary>
		protected virtual void OnColorKeyChange()
		{
			UpdateBufferData(this.SpriteVertexBuffer, ColorKey);
		}
		#endregion

		#region Rendering methods
		/// <summary>
		/// Draws this element once the scene has begun
		/// </summary>
		/// <param name="XOffset">Offset on the X axis</param>
		/// <param name="YOffset">Offset on the Y axis</param>
		public virtual void Draw(float XOffset, float YOffset)
		{
			if(this._Icon.Texture==null)
				return;

			try
			{
				DrawTextureOnBuffer(this.SpriteVertexBuffer, this._Icon.Texture, new RectangleF(new PointF(this.Rectangle.X+XOffset, this.Rectangle.Y+YOffset), this.Rectangle.Size), this.ColorKey);
			}
			catch(Exception){}
		}

		/// <summary>
		/// Draws another texture using this element's vertex buffer
		/// </summary>
		/// <param name="overrideTexture">Texture to be drawn on this item's vertex buffer</param>
		public void Draw(OrbitTexture overrideTexture)
		{
			// TODO: add position OFFSET support to this call
			DrawTextureOnBuffer(this.SpriteVertexBuffer, overrideTexture.Texture, this.Rectangle, Color.White);
			/*return;
			try
			{
				display.SamplerState[0].MagFilter = TextureFilter.Linear;
				display.SamplerState[0].MinFilter = TextureFilter.Linear;
				display.SamplerState[0].MipFilter = TextureFilter.Linear;
				display.SamplerState[0].MipMapLevelOfDetailBias = -1f;
				
				display.SetTexture(0, overrideTexture);
			
				//display.VertexFormat = CustomVertex.TransformedColoredTextured.Format;
				display.VertexFormat = CustomVertex.PositionColoredTextured.Format;

				Matrix Scaling=Matrix.Scaling(this.Rectangle.Width+1, this.Rectangle.Height+1, 1f);
				Matrix Translation=Matrix.Translation(this.Rectangle.X+this.Rectangle.Width/2, this.Rectangle.Y+this.Rectangle.Height/2, 0f);
				display.Transform.World=Scaling * Translation;
			
				display.SetStreamSource(0, this.SpriteVertexBuffer, 0);
				display.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			}
			catch(Exception){}*/
		}

		/// <summary>
		/// Draws a Texture on a VertexBuffer moving it to the specified RectangleF and using the vertex Colors specified
		/// </summary>
		/// <param name="vertexBuffer">VertexBuffer to use when drawing</param>
		/// <param name="texture">Texture to draw to the VertexBuffer</param>
		/// <param name="rectangle">RectangleF with the new position for the VertexBuffer's quad</param>
		/// <param name="colorKey">Color for the Vertexes of the VertexBuffer</param>
		protected void DrawTextureOnBuffer(VertexBuffer vertexBuffer, Texture texture, RectangleF rectangle, Color colorKey)
		{
			try
			{
				display.SetTexture(0, texture);
				// set our texture filters
				display.SamplerState[0].MagFilter = TextureFilter.Linear;
				display.SamplerState[0].MinFilter = TextureFilter.Linear;
				display.SamplerState[0].MipFilter = TextureFilter.Linear;
				display.SamplerState[0].MipMapLevelOfDetailBias = 0f;
			
				display.VertexFormat = CustomVertex.PositionColoredTextured.Format;

				//Matrix Scaling=Matrix.Scaling(rectangle.Width+1, rectangle.Height+1, 1f);
				Matrix Scaling=Matrix.Scaling(rectangle.Width, rectangle.Height, 1f);
				Matrix Translation=Matrix.Translation(rectangle.X+rectangle.Width/2, rectangle.Y+rectangle.Height/2, 0f);
				display.Transform.World=Scaling * Translation;
			
				display.SetStreamSource(0, vertexBuffer, 0);
				display.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			}
			catch(Exception)
			{
				try
				{
					vertexBuffer.Unlock();
				}
				catch(Exception){}
			}
		}

		/// <summary>
		/// Updates the vertex buffer to be a quad with 1pt in width/height and with the pivot in the center of it
		/// </summary>
		/// <param name="vertexBuffer">Vertex Buffer to update</param>
		/// <param name="colorKey">Color Key to apply to the buffer</param>
		protected void UpdateBufferData(VertexBuffer vertexBuffer, Color colorKey)
		{
			int color = colorKey.ToArgb();
			try
			{			
				Microsoft.DirectX.GraphicsStream stm = vertexBuffer.Lock(0, 0, 0);
				CustomVertex.PositionColoredTextured[] verts = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, LockFlags.Discard);
				// Bottom left
				verts[0] = new CustomVertex.PositionColoredTextured(
					new Microsoft.DirectX.Vector3(
					-0.5f, 
					0.5f,
					0), 
					color, 
					0.0f, 
					1.0f);
		
				// Top left
				verts[1] = new CustomVertex.PositionColoredTextured(
					new Microsoft.DirectX.Vector3(
					-0.5f, 
					-0.5f,
					0), 
					color, 
					0.0f, 
					0.0f);

				// Bottom right
				verts[2] = new CustomVertex.PositionColoredTextured(
					new Microsoft.DirectX.Vector3(
					0.5f, 
					0.5f,
					0), 
					color, 
					1.0f, 
					1.0f);

				// Top right
				verts[3] = new CustomVertex.PositionColoredTextured(
					new Microsoft.DirectX.Vector3(
					0.5f,
					-0.5f,
					0), 
					color, 
					1.0f, 
					0.0f);
				stm.Write(verts);
				vertexBuffer.Unlock();
			}
			catch(Exception)
			{
				try
				{
					vertexBuffer.Unlock();
				}
				catch(Exception){}
			}
		}
		#endregion

		#region Public overriden methods
		/// <summary>
		/// Verifies if an item is equal to another
		/// </summary>
		/// <param name="Item">Item to compare with</param>
		/// <returns>True if this item equals the one specified</returns>
		public virtual bool Equals(OrbitItem Item)
		{
			if(this.Name==Item.Name
				&& this.Description==Item.Description)
				return true;

			return false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of this item
		/// </summary>
		public virtual string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if(_Name!=value)
					_Name=value;
			}
		}
		/// <summary>
		/// Gets or sets the descrption of this item
		/// </summary>
		public virtual string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if(_Description!=value)
					_Description=value;
			}
		}
		/// <summary>
		/// Gets or sets the parent item for this item
		/// </summary>
		public string Parent
		{
			get
			{
				return _Parent;
			}
			set
			{
				if(_Parent!=value)
					_Parent=value;
			}
		}
		/// <summary>
		/// Enables/Disables this item
		/// </summary>
		/// <remarks>Disabled items still take up a position in a loop but will not be shown</remarks>
		public bool Enabled
		{
			get
			{
				return _Enabled;
			}
			set
			{
				if(_Enabled!=value)
					_Enabled=value;
				if(!_Enabled)
				{
					_IsMouseOver=false;
				}
			}
		}
		/// <summary>
		/// Shows/Hides this item
		/// </summary>
		public bool IsShown
		{
			get
			{
				return _IsShown;
			}
			set
			{
				if(_IsShown!=value)
					_IsShown=value;
				if(!_IsShown)
				{
					_IsMouseOver=false;
				}
			}
		}
		/// <summary>
		/// Sets the item state as having or not the mouse over
		/// </summary>
		public bool IsMouseOver
		{
			get
			{
				return _IsMouseOver;
			}
			set
			{
				if(_IsMouseOver!=value)
					_IsMouseOver=value;
			}
		}
		/// <summary>
		/// Sets the item to toggled mode or not
		/// </summary>
		public bool IsToggled
		{
			get
			{
				return _IsToggled;
			}
			set
			{
				if(_IsToggled!=value)
				{
					_IsToggled=value;
					if(value)
					{
						// if is toggled, try to set the icon to the toggled image
						if(_ToggledImagePath!=null && _ToggledImagePath!="")
						{
							// TODO: temporary fix for toggle image
							// this doesn't store the new ImagePath when switching to the toggled image
							// i could possibly add a new variable (string) for the toggled image path.
							string ImagePath=_ImagePath;
							SetIcon(_ToggledImagePath);
							_ImagePath=ImagePath;
						}
					}
					else
					{
						// if is untoggled, try to set the icon to the default image
						if(_ToggledImagePath!=null && _ToggledImagePath!="")
						{
							SetIcon(_ImagePath);
						}
					}
				}
			}
		}
		/// <summary>
		/// Determines if this item causes Orbit to leave after it's run
		/// </summary>
		public bool RunAndLeave
		{
			get
			{
				return _RunAndLeave;
			}
			set
			{
				if(_RunAndLeave!=value)
					_RunAndLeave=value;
			}
		}
		/// <summary>
		/// Gets or sets the boundaries of this item
		/// </summary>
		public RectangleF Rectangle
		{
			get
			{
				return _Rectangle;
			}
			set
			{
				if(_Rectangle!=value)
					_Rectangle=value;
			}
		}
		/// <summary>
		/// Gets or sets the Loop index in which this item is
		/// </summary>
		public int Line
		{
			get
			{
				return _Line;
			}
			set
			{
				if(_Line!=value)
				{
					//int old=_Line;
					_Line=value;
					//if(LineChanged!=null)LineChanged(this, old, value);
				}
			}
		}
		/// <summary>
		/// Gets or sets the rotation offset for this item in the loop
		/// </summary>
		public double RotationOffset
		{
			get
			{
				return _RotationOffset;
			}
			set
			{
				if(_RotationOffset!=value)
					_RotationOffset=value;
			}
		}
		/// <summary>
		/// Gets or sets the color key for this item
		/// </summary>
		public Color ColorKey
		{
			get
			{
				return _ColorKey;
			}
			/*set
			{
				if(_ColorKey!=value)
				{
					_ColorKey=value;
					OnColorKeyChange();
				}
			}*/
		}
		/// <summary>
		/// Gets or sets the animation state for this item
		/// </summary>
		public float AnimationState
		{
			get
			{
				return _AnimationState;
			}
			set
			{
				if(_AnimationState!=value)
				{
					_AnimationState=value;
					/*if(value<=Global.Configuration.Appearance.IconAlpha)
						_ColorKey=Color.FromArgb((byte)(Math.Sin(value*Math.PI/2.0f/Global.Configuration.Appearance.IconAlpha)*Global.Configuration.Appearance.IconAlpha), _ColorKey);
					else
						_ColorKey=Color.FromArgb((byte)(Math.Sin(((value-Global.Configuration.Appearance.IconAlpha)/(255-Global.Configuration.Appearance.IconAlpha))*Math.PI/2.0f)*(255-Global.Configuration.Appearance.IconAlpha)+Global.Configuration.Appearance.IconAlpha), _ColorKey);*/
					_ColorKey=Color.FromArgb((byte)value, _ColorKey);
					OnColorKeyChange();
				}
			}
		}
		/// <summary>
		/// Gets the Vertex Buffer for ths item
		/// </summary>
		public VertexBuffer VertexBuffer
		{
			get
			{
				return this.SpriteVertexBuffer;
			}
		}
		/// <summary>
		/// Gets which context menu items are shown for this item
		/// </summary>
		public ItemMenuFlags MenuFlags
		{
			get
			{
				return _MenuFlags;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when an item requests a repaint
		/// </summary>
		public event EventHandler Paint;
		/*/// <summary>
		/// Occurs when the item's line has changed
		/// </summary>
		public event LineChangedEventHandler LineChanged;*/
		#endregion
	}
}