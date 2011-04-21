using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Utilities
{
	/// <summary>
	/// Template class for background providers
	/// </summary>
	public class BackgroundProvider:IDisposable
	{
		#region Private Members
		#region External Resources
		/// <summary>
		/// Direct3D Device
		/// </summary>
		protected Device display;
		/// <summary>
		/// Sprite object that paints the background
		/// </summary>
		protected Sprite SpritePainter;
		#endregion

		#region Managed Resources
		/// <summary>
		/// Texture
		/// </summary>
		protected OrbitTexture BG;
		/// <summary>
		/// Texture Description
		/// </summary>
		//protected SurfaceDescription BGDesc;
		#endregion

		#region Internal Property Members
		/// <summary>
		/// Defines how the background is stretched
		/// </summary>
		protected Orbit.Configuration.BackgroundStretchMode _StretchMode;
		/// <summary>
		/// Defines the size of the background image
		/// </summary>
		protected Size _BackgroundSize;
		/// <summary>
		/// Defines the path to the background image
		/// </summary>
		protected string _BackgroundPath;
		/// <summary>
		/// Defines the background color
		/// </summary>
		protected Color _BackgroundColor;
		#endregion
		#endregion

		#region Creator
		/// <summary>
		/// Initiates a new instance of the BackgroundProvider class
		/// </summary>
		protected BackgroundProvider()
		{
		}
		/// <summary>
		/// Initiates a new instance of the BackgroundProvider class
		/// </summary>
		/// <param name="D3DDevice">Direct3D Device to load the background to</param>
		/// <param name="D3DSprite">Direct3D Sprite to use to paint the background</param>
		public BackgroundProvider(Device D3DDevice, Sprite D3DSprite)
		{
			// verifying values
			if(D3DDevice==null)
				throw new ArgumentNullException("D3DDevice");
			if(D3DSprite==null)
				throw new ArgumentNullException("D3DSprite");

			// gather data
			display=D3DDevice;
			SpritePainter=D3DSprite;

			this._StretchMode=Orbit.Configuration.BackgroundStretchMode.StretchAspect;
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the BackgroundProvider object
		/// </summary>
		public void Dispose()
		{
			DisposeBg();
		}

		private void DisposeBg()
		{
			// background
			if(this.BG!=null)
			{
				this.BG.FreeReference();
				this.BG = null;
			}
		}

		#endregion

		#region Rendering Functions
		/// <summary>
		/// Draws the Non Transparent Mode background Texture
		/// </summary>
		/// <param name="InitSprite">Indicates whether to initialize the Sprite object or not (if it's already initialized)</param>
		/// <param name="ReferenceSize">Size to compare and scale the image to</param>
		/// <param name="Offset">Offset from the top left corner of the screen</param>
		public void Draw(bool InitSprite, Size ReferenceSize, Point Offset)
		{
			try
			{
				// draw image if there is a background image
				if(BG!=null)
				{
					if(InitSprite)
						SpritePainter.Begin(SpriteFlags.AlphaBlend);

					float ScaleRatioX=1;
					float ScaleRatioY=1;
					float SizeX=_BackgroundSize.Width;
					float SizeY=_BackgroundSize.Height;
					float TranslateX=0;
					float TranslateY=0;
					switch(_StretchMode)
					{
						case Orbit.Configuration.BackgroundStretchMode.None:
							ScaleRatioX=(float)_BackgroundSize.Width/(float)BG.Description.Width;
							ScaleRatioY=(float)_BackgroundSize.Height/(float)BG.Description.Height;
							SizeX=_BackgroundSize.Width;
							SizeY=_BackgroundSize.Height;
							TranslateX=((float)ReferenceSize.Width/2f-(float)_BackgroundSize.Width/2f)-Offset.X;
							TranslateY=((float)ReferenceSize.Height/2f-(float)_BackgroundSize.Height/2f)-Offset.Y;
							break;
						case Orbit.Configuration.BackgroundStretchMode.Stretch:
							TranslateX=0-Offset.X;
							TranslateY=0-Offset.Y;
							ScaleRatioX=(float)ReferenceSize.Width/(float)BG.Description.Width;
							ScaleRatioY=(float)ReferenceSize.Height/(float)BG.Description.Height;
							SizeX=ReferenceSize.Width;
							SizeY=ReferenceSize.Height;
							break;
						case Orbit.Configuration.BackgroundStretchMode.StretchAspect:
							SizeF NewSizeOriginal=ImageHelper.GetAspectSizeThatFits(_BackgroundSize, ReferenceSize);
							
							ScaleRatioX=(float)NewSizeOriginal.Width/(float)BG.Description.Width;
							ScaleRatioY=(float)NewSizeOriginal.Height/(float)BG.Description.Height;
							SizeX=NewSizeOriginal.Width;
							SizeY=NewSizeOriginal.Height;
							TranslateX=(ReferenceSize.Width-NewSizeOriginal.Width)/2;
							TranslateY=(ReferenceSize.Height-NewSizeOriginal.Height)/2;
							break;
						case Orbit.Configuration.BackgroundStretchMode.Tile:
							ScaleRatioX=(float)_BackgroundSize.Width/(float)BG.Description.Width;
							ScaleRatioY=(float)_BackgroundSize.Height/(float)BG.Description.Height;
							SizeX=_BackgroundSize.Width;
							SizeY=_BackgroundSize.Height;
							TranslateX=0;
							TranslateY=0;

							// render multiple times.
							// doing this here to avoid bloat for other methods :)
							while(true)
							{
								SpritePainter.Draw2D(BG.Texture,
									Rectangle.Empty,
									new SizeF(SizeX, SizeY),
									new Point((int)TranslateX, (int)TranslateY),
									Color.White);

								TranslateX+=_BackgroundSize.Width;
								if(TranslateX>ReferenceSize.Width)
								{
									TranslateX=0;
									TranslateY+=_BackgroundSize.Height;
								}
								if(TranslateY>ReferenceSize.Height)
								{
									break;
								}
							}
							break;
					}

					SpritePainter.Draw2D(BG.Texture,
						Rectangle.Empty,
						new SizeF(SizeX, SizeY),
						new Point((int)TranslateX, (int)TranslateY),
						Color.White);

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
		#endregion

		#region Utility Functions
		/// <summary>
		/// Tells the BackgroundProvider to check all its resources
		/// </summary>
		public virtual void Prepare()
		{
		}

		/// <summary>
		/// Sets the Non Transparent background Texture
		/// </summary>
		/// <param name="Path">Path to load the Texture from</param>
		/// <returns>True if succeeded</returns>
		protected bool SetBg(string Path)
		{
			// dispose old background
			DisposeBg();

			try
			{
				// use custom loader to load
				BG=OrbitTextureLoader.Load(display, Path);
				// only loaded if not null
				if(BG!=null)
				{
					// get description and set path properly
					_BackgroundPath=Path;
					return true;
				}
			}
			catch(Exception){}

			// code will only reach this if failed loading
			_BackgroundPath="";
			return false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the BackgroundStretchMode for this background
		/// </summary>
		public Orbit.Configuration.BackgroundStretchMode StretchMode
		{
			get
			{
				return _StretchMode;
			}
		}
		/// <summary>
		/// Gets the Size of the background image
		/// </summary>
		public virtual Size BackgroundSize
		{
			get
			{
				return BackgroundSize;
			}
			set
			{
				if(_BackgroundSize!=value)
					_BackgroundSize=value;
			}
		}
		/// <summary>
		/// Gets or Sets the background image path
		/// </summary>
		public virtual string BackgroundPath
		{
			get
			{
				return _BackgroundPath;
			}
			set
			{
				if(_BackgroundPath!=value)
				{
					this.SetBg(value);
				}
			}
		}
		/// <summary>
		/// Gets or sets the background color
		/// </summary>
		public virtual Color BackgroundColor
		{
			get
			{
				return _BackgroundColor;
			}
			set
			{
				_BackgroundColor=value;
			}
		}
		#endregion
	}
}
