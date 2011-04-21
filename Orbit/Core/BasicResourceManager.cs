using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Orbit;
using Orbit.Utilities;
using Orbit.Configuration;

namespace Orbit.Core
{
	/// <summary>
	/// Manages the common resources in the Orbit environment
	/// </summary>
	public class OrbitResourceManager : IDisposable
	{
		#region Private Members
		#region Owner Created
		Device display;
		#endregion

		#region Managed Resources
		private ScreenGrabber ScreenG;

		private VertexBuffer GenericQuad;

		private Sprite SpritePainter;

		private Microsoft.DirectX.Direct3D.Font fntOut;
		private Microsoft.DirectX.Direct3D.Font descOut;
#if DEBUG
		private Microsoft.DirectX.Direct3D.Font dbo;
#endif

		private Microsoft.DirectX.Direct3D.Surface back;
		private Microsoft.DirectX.Direct3D.Surface composite;

		private BackgroundProvider BGProvider;
		private OrbitTexture IconBg;
		private OrbitTexture IconSelected;
		private OrbitTexture ScrollUp;
		private OrbitTexture ScrollDown;
		private OrbitTexture CompositeScreen;

		//private Microsoft.DirectX.Direct3D.SurfaceDescription CompositeScreenDesc;
		#endregion
		#endregion

		#region Creator
		/// <summary>
		/// Creates a new instance of the OrbitResourceManager class
		/// </summary>
		/// <param name="D3DDevice">The Direct3D Device that this class should load the resources onto</param>
		public OrbitResourceManager(Device D3DDevice)
		{
			// verifying values
			if(D3DDevice==null)
				throw new ArgumentNullException("D3DDevice");

			display=D3DDevice;

			InitDeviceMem();
		}
		#endregion

		#region Private Functions
		/// <summary>
		/// Initializes all the Textures based on the configuration settings
		/// </summary>
		private void InitDeviceMem()
		{
			// generic quad vertex buffer
			try
			{
				int color = Color.White.ToArgb();
				this.GenericQuad=new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 4, display, Usage.Dynamic, CustomVertex.TransformedColoredTextured.Format, Pool.Default);
				Microsoft.DirectX.GraphicsStream stm = GenericQuad.Lock(0, 0, 0);
				CustomVertex.PositionColoredTextured[] verts = (CustomVertex.PositionColoredTextured[])GenericQuad.Lock(0, LockFlags.None);
				// Bottom left
				verts[0] = new CustomVertex.PositionColoredTextured(
					new Vector3(
					-0.5f, 
					0.5f,
					0), 
					color, 
					0.0f, 
					1.0f);
		
				// Top left
				verts[1] = new CustomVertex.PositionColoredTextured(
					new Vector3(
					-0.5f, 
					-0.5f,
					0), 
					color, 
					0.0f, 
					0.0f);

				// Bottom right
				verts[2] = new CustomVertex.PositionColoredTextured(
					new Vector3(
					0.5f, 
					0.5f,
					0), 
					color, 
					1.0f, 
					1.0f);

				// Top right
				verts[3] = new CustomVertex.PositionColoredTextured(
					new Vector3(
					0.5f,
					-0.5f,
					0), 
					color, 
					1.0f, 
					0.0f);
				stm.Write(verts);
				GenericQuad.Unlock();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				try
				{
					GenericQuad.Unlock();
				}
				catch(Exception){}
			}

			// font objects
			fntOut=new Microsoft.DirectX.Direct3D.Font(display, new System.Drawing.Font(Global.Configuration.Fonts.LabelFont, Global.Configuration.Fonts.LabelSize, Global.Configuration.Fonts.LabelStyle));
			descOut=new Microsoft.DirectX.Direct3D.Font(display, new System.Drawing.Font(Global.Configuration.Fonts.DescriptionFont, Global.Configuration.Fonts.DescriptionSize, Global.Configuration.Fonts.DescriptionStyle));
#if DEBUG
			dbo=new Microsoft.DirectX.Direct3D.Font(display, new System.Drawing.Font("Tahoma", 10));
#endif

			// init Sprite painter
			SpritePainter=new Sprite(display);

			// get a pointer to the back buffer
			back=display.GetBackBuffer(0, 0, BackBufferType.Mono);

			// init composite texture
			InitCompositeTexture();

			// load bg only if is not using transparency
			if(Global.Configuration.Appearance.Transparency!=Orbit.Configuration.TransparencyMode.Real)
			{
				// load the proper background provider
				if(Global.Configuration.Images.UseWindowsWallpaper)
				{
					// init the wallpaper sync background provider
					BGProvider=new WindowsBackgroundProvider(display, SpritePainter);
					BGProvider.BackgroundColor=Global.Configuration.Images.BackgroundColor;
				}
				else
				{
					// init the background provider
					BGProvider=new BackgroundProvider(display, SpritePainter);
					// assing bg image
					if(Global.Configuration.Images.BackgroundImagePath!="")
					{
						BGProvider.BackgroundPath=Global.Configuration.Images.BackgroundImagePath;
						// pass on the backgorund size
						ImageInformation ii=Microsoft.DirectX.Direct3D.TextureLoader.ImageInformationFromFile(Global.Configuration.Images.BackgroundImagePath);
						BGProvider.BackgroundSize=new Size(ii.Width, ii.Height);
					}
					// pass the background color
					BGProvider.BackgroundColor=Color.FromArgb(0xFF, Global.Configuration.Images.BackgroundColor);
				}
			}

			// Load Icon Background
			if(Global.Configuration.Images.IconBackgroundImagePath!="")
				SetIconBg(Global.Configuration.Images.IconBackgroundImagePath);

			// Load Selected Icon overlay
			if(Global.Configuration.Images.IconSelectedImagePath!="")
				SetIconSelected(Global.Configuration.Images.IconSelectedImagePath);

			// load the scroll up image
			if(Global.Configuration.Images.ScrollUpImagePath!="")
				SetScrollUp(Global.Configuration.Images.ScrollUpImagePath);

			// load the scroll down image
			if(Global.Configuration.Images.ScrollDownImagePath!="")
				SetScrollDown(Global.Configuration.Images.ScrollDownImagePath);
		}

		/// <summary>
		/// Disposes all the Textures based on the configuration settings
		/// </summary>
		private void DeInitDeviceMem()
		{
			// generic quad
			DisposeGenericQuad();

			// text stuff
			this.fntOut.Dispose();
			this.descOut.Dispose();
#if DEBUG
			this.dbo.Dispose();
#endif

			// the composition screen
			DisposeComposite();

			// pointer to the backbuffer
			DisposeBackBuffer();

			// background
			DisposeBg();

			// icon background
			DisposeIconBg();

			// Icon selected indicator
			DisposeIconSelected();

			// scroll up and down
			DisposeScrollUp();
			DisposeScrollDown();

			// dispose the screen grabber
			DisposeScreenGrabber();
		}

		#endregion

		#region Public Functions
		#region Individual Loading Functions
		/// <summary>
		/// Sets the icon background Texture
		/// </summary>
		/// <param name="Path">Path to load the Texture from</param>
		/// <returns>True if succeeded</returns>
		public bool SetIconBg(string Path)
		{
			// dispose old background
			DisposeIconBg();

			try
			{
				// use custom loader to load
				IconBg=OrbitTextureLoader.Load(display, Path);
				// only loaded if not null
				if(IconBg!=null)
				{
					// get description and set path properly
					Global.Configuration.Images.IconBackgroundImagePath=Path;
					return true;
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}

			// code will only reach this if failed loading
			Global.Configuration.Images.IconBackgroundImagePath="";
			return false;
		}
		/// <summary>
		/// Sets the selected item indicator Texture
		/// </summary>
		/// <param name="Path">Path to load the Texture from</param>
		/// <returns>True if succeeded</returns>
		public bool SetIconSelected(string Path)
		{
			// dispose old background
			DisposeIconSelected();

			try
			{
				// use custom loader to load
				IconSelected=OrbitTextureLoader.Load(display, Path);
				// only loaded if not null
				if(IconSelected!=null)
				{
					// get description and set path properly
					Global.Configuration.Images.IconSelectedImagePath=Path;
					return true;
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}

			// code will only reach this if failed loading
			Global.Configuration.Images.IconSelectedImagePath="";
			return false;
		}
		/// <summary>
		/// Sets the Scroll Up indicator Texture
		/// </summary>
		/// <param name="Path">Path to load the Texture from</param>
		/// <returns>True if succeeded</returns>
		public bool SetScrollUp(string Path)
		{
			// dispose old background
			DisposeScrollUp();

			try
			{
				// use custom loader to load
				ScrollUp=OrbitTextureLoader.Load(display, Path);
				// only loaded if not null
				if(ScrollUp!=null)
				{
					// get description and set path properly
					Global.Configuration.Images.ScrollUpImagePath=Path;
					return true;
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}

			// code will only reach this if failed loading
			Global.Configuration.Images.ScrollUpImagePath="";
			return false;
		}
		/// <summary>
		/// Sets the Scroll Down indicator Texture
		/// </summary>
		/// <param name="Path">Path to load the Texture from</param>
		/// <returns>True if succeeded</returns>
		public bool SetScrollDown(string Path)
		{
			// dispose old background
			DisposeScrollDown();

			try
			{
				// use custom loader to load
				ScrollDown=OrbitTextureLoader.Load(display, Path);
				// only loaded if not null
				if(ScrollDown!=null)
				{
					// get description and set path properly
					Global.Configuration.Images.ScrollDownImagePath=Path;
					return true;
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}

			// code will only reach this if failed loading
			Global.Configuration.Images.ScrollDownImagePath="";
			return false;
		}
		/// <summary>
		/// Initializes the Composite buffer
		/// </summary>
		public void InitCompositeTexture()
		{
			DisposeComposite();

			try
			{
				Texture compositeScreen=new Texture(display, 512, Convert.ToInt32((Global.Configuration.Fonts.DescriptionSize+Global.Configuration.Fonts.LabelSize+5)*3f/2f), 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
				this.CompositeScreen = new OrbitTexture(compositeScreen);
				this.CompositeScreen.GetReference();
				composite=CompositeScreen.Texture.GetSurfaceLevel(0);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				try
				{
					CompositeScreen.FreeReference();
					CompositeScreen = null;
				}
				catch(Exception){}
			}
		}

		/// <summary>
		/// Initializes a new managed instance of the ScreenGrabber object
		/// </summary>
		/// <param name="SourceRectangle">Area of the screen that this grabber should capture</param>
		[Obsolete("Use Direct3DManager.ScreenGrabber instead", true)]
		public void InitScreenGrabber(Rectangle SourceRectangle)
		{
			ScreenG=new ScreenGrabber(display, SpritePainter, SourceRectangle);
		}
		#endregion

		#region Rendering Functions
		/// <summary>
		/// Draws a given texture on our GenericQuad onto the screen
		/// </summary>
		/// <param name="texture">Texture to draw</param>
		/// <param name="rectangle">Destination Rectangle</param>
		/// <param name="colorKey">Optional Color Keying</param>
		public void DrawTextureOnGenericQuad(Texture texture, RectangleF rectangle, Color colorKey)
		{
			DrawTextureOnVertexBuffer(texture, GenericQuad, rectangle, colorKey);
		}
		/// <summary>
		/// Draws a given texture on a VertexBuffer onto the screen
		/// </summary>
		/// <param name="texture">Texture to draw</param>
		/// <param name="buffer">VertexBuffer to draw the texture in</param>
		/// <param name="rectangle">Destination Rectangle</param>
		/// <param name="colorKey">Optional Color Keying</param>
		public void DrawTextureOnVertexBuffer(Texture texture, VertexBuffer buffer, RectangleF rectangle, Color colorKey)
		{
			// TODO: in the future, enable materials so that this method supports the Alpha component in the colorKey parameter (keep in mind this is a SHARED buffer)
			try
			{
				display.SetTexture(0, texture);
				// set our texture filters
				display.SamplerState[0].MagFilter = TextureFilter.Linear;
				display.SamplerState[0].MinFilter = TextureFilter.Linear;
				display.SamplerState[0].MipFilter = TextureFilter.Linear;
				display.SamplerState[0].MipMapLevelOfDetailBias = 0f;
			
				display.VertexFormat = CustomVertex.PositionColoredTextured.Format;

				Matrix Scaling=Matrix.Scaling(rectangle.Width, rectangle.Height, 1f);
				Matrix Translation=Matrix.Translation(rectangle.X+rectangle.Width/2, rectangle.Y+rectangle.Height/2, 0f);
				display.Transform.World=Scaling * Translation;
			
				display.SetStreamSource(0, GenericQuad, 0);
				display.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				try
				{
					GenericQuad.Unlock();
				}
				catch(Exception){}
			}
		}
		/// <summary>
		/// Draws the Icon Background Texture
		/// </summary>
		/// <param name="InitSprite">Indicates whether to initialize the Sprite object or not (if it's already initialized)</param>
		/// <param name="Size">Size of the destination rectangle</param>
		/// <param name="Center">Point of the center of the background</param>
		public void DrawIconBg(bool InitSprite, Size Size, Point Center)
		{
			try
			{
				if(Global.Configuration.Images.IconBackgroundImagePath!="" && IconBg!=null)
				{
					if(InitSprite)
						SpritePainter.Begin(SpriteFlags.AlphaBlend);

					SpritePainter.Draw2D(IconBg.Texture,
										Rectangle.Empty,
										//new Size(IconBgDesc.Width,IconBgDesc.Height),
										new SizeF(Size.Width, Size.Height),
										new Point(Center.X-Size.Width/2, Center.Y-Size.Height/2),
										Color.FromArgb(Global.Configuration.Appearance.IconBackgroundAlpha, Color.White).ToArgb());

					if(InitSprite)
						SpritePainter.End();
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				if(InitSprite)
					SpritePainter.End();
			}
		}

		/// <summary>
		/// Draws the Icon Selected overlay Texture
		/// </summary>
		/// <param name="InitSprite">Indicates whether to initialize the Sprite object or not (if it's already initialized)</param>
		/// <param name="Bounds">Destination rectangle of the overlay</param>
		/// <param name="Alpha">Alpha value</param>
		public void DrawSelectedOverlay(bool InitSprite, RectangleF Bounds, byte Alpha)
		{
			try
			{
				if(InitSprite)
					SpritePainter.Begin(SpriteFlags.AlphaBlend);

				if(IconSelected!=null)
					SpritePainter.Draw2D(IconSelected.Texture,
										Rectangle.Empty,
										new SizeF(Bounds.Width, Bounds.Height),
										new Point((int)Bounds.Left, (int)Bounds.Top),
										Color.FromArgb(Alpha, Color.White));

				if(InitSprite)
					SpritePainter.End();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// Draws the Scroll Down indicator Texture
		/// </summary>
		/// <param name="InitSprite">Indicates whether to initialize the Sprite object or not (if it's already initialized)</param>
		/// <param name="Bounds">Destination rectangle of the indicator</param>
		public void DrawScrollDownIndicator(bool InitSprite, RectangleF Bounds)
		{
			try
			{
				if(InitSprite)
					SpritePainter.Begin(SpriteFlags.AlphaBlend);

				DrawTextureOnGenericQuad(ScrollDown.Texture,
										new RectangleF(new PointF(Bounds.Left, Bounds.Top), new SizeF((float)Global.Configuration.Runtime.IconSizeAverage*Global.Scale/2.0f, (float)Global.Configuration.Runtime.IconSizeAverage*Global.Scale/2.0f)),
										Color.White);

				if(InitSprite)
					SpritePainter.End();	
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}
		/// <summary>
		/// Draws the Scroll Up indicator Texture
		/// </summary>
		/// <param name="InitSprite">Indicates whether to initialize the Sprite object or not (if it's already initialized)</param>
		/// <param name="Bounds">Destination rectangle of the indicator</param>
		public void DrawScrollUpIndicator(bool InitSprite, RectangleF Bounds)
		{
			try
			{
				if(InitSprite)
					SpritePainter.Begin(SpriteFlags.None);

				DrawTextureOnGenericQuad(ScrollUp.Texture,
					new RectangleF(new PointF(Bounds.Left, Bounds.Top), new SizeF((float)Global.Configuration.Runtime.IconSizeAverage*Global.Scale/2.0f, (float)Global.Configuration.Runtime.IconSizeAverage*Global.Scale/2.0f)),
					Color.White);

				if(InitSprite)
					SpritePainter.End();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}
		#endregion
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the OrbitResourceManager class
		/// </summary>
		public void Dispose()
		{
			DeInitDeviceMem();
		}

		#region Disposal functions
		private void DisposeGenericQuad()
		{
			// generic quad sprite buffer
			if(this.GenericQuad!=null)
				this.GenericQuad.Dispose();
		}
		private void DisposeComposite()
		{
			// the composition screen
			if(this.CompositeScreen!=null)
			{
				this.CompositeScreen.FreeReference();
				this.CompositeScreen = null;
			}
			if(this.composite!=null)
				this.composite.Dispose();
		}

		
		private void DisposeBackBuffer()
		{
			// pointer to the backbuffer
			if(this.back!=null)
				this.back.Dispose();
		}

		private void DisposeBg()
		{
			// dispose background provider
			if(this.BGProvider!=null)
				this.BGProvider.Dispose();
		}

		private void DisposeIconBg()
		{
			// icon background
			if(this.IconBg!=null)
			{
				this.IconBg.FreeReference();
				this.IconBg = null;
			}
		}

		private void DisposeIconSelected()
		{
			// Icon selected indicator
			if(this.IconSelected!=null)
			{
				this.IconSelected.FreeReference();
				this.IconSelected = null;
			}
		}

		private void DisposeScrollUp()
		{
			// scroll up
			if(this.ScrollUp!=null)
			{
				this.ScrollUp.FreeReference();
				this.ScrollUp = null;
			}
		}

		private void DisposeScrollDown()
		{
			// scroll down
			if(this.ScrollDown!=null)
			{
				this.ScrollDown.FreeReference();
				this.ScrollDown = null;
			}
		}

		private void DisposeScreenGrabber()
		{
			if(ScreenG!=null)
				ScreenG.Dispose();
		}
		#endregion
		#endregion

		#region Properties
		/// <summary>
		/// Gets the ScreenGrabber object
		/// </summary>
		public ScreenGrabber ScreenGrabber
		{
			get
			{
				return ScreenG;
			}
		}
		/// <summary>
		/// Gets the Direct3D Sprite object
		/// </summary>
		public Sprite Sprite
		{
			get
			{
				return SpritePainter;
			}
		}


		/// <summary>
		/// Gets the object for the Direct3D object that draws the item's labels
		/// </summary>
		public Microsoft.DirectX.Direct3D.Font LabelFont
		{
			get
			{
				return fntOut;
			}
		}
		/// <summary>
		/// Gets the object for the Direct3D object that draws the item's descriptions
		/// </summary>
		public Microsoft.DirectX.Direct3D.Font DescriptionFont
		{
			get
			{
				return descOut;
			}
		}

#if DEBUG
		/// <summary>
		/// Gets the object for the Direct3D object that draws the program debug information
		/// </summary>
		public Microsoft.DirectX.Direct3D.Font DebugFont
		{
			get
			{
				return dbo;
			}
		}
#endif

		/// <summary>
		/// Gets the Back Buffer Surface
		/// </summary>
		public Surface BackBuffer
		{
			get
			{
				return back;
			}
		}
		/// <summary>
		/// Gets the Composite Buffer Surface (used to draw the labels)
		/// </summary>
		public Surface CompositeBuffer
		{
			get
			{
				return composite;
			}
		}

		/// <summary>
		/// Gets the loaded background provider
		/// </summary>
		public Orbit.Utilities.BackgroundProvider BackgroundProvider
		{
			get
			{
				return BGProvider;
			}
		}
		/// <summary>
		/// Gets the Icon Background Texture
		/// </summary>
		public OrbitTexture IconBackground
		{
			get
			{
				return IconBg;
			}
		}
		/// <summary>
		/// Gets the Texture for the selected icon indicator
		/// </summary>
		public OrbitTexture IconSelectedIndicator
		{
			get
			{
				return IconSelected;
			}
		}
		/// <summary>
		/// Gets the Texture for the scroll up indicator
		/// </summary>
		public OrbitTexture ScrollUpIndicator
		{
			get
			{
				return ScrollUp;
			}
		}
		/// <summary>
		/// Gets the texture for the scroll down indicator
		/// </summary>
		public OrbitTexture ScrollDownIndicator
		{
			get
			{
				return ScrollDown;
			}
		}
		/// <summary>
		/// Gets the Composite Buffer Texture (used to draw the labels)
		/// </summary>
		/// <remarks>This Texture contains the CompositeBuffer Surface</remarks>
		public OrbitTexture Composite
		{
			get
			{
				return CompositeScreen;
			}
		}
		#endregion
	}
}