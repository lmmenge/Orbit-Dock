using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Threading;
using System.Runtime.InteropServices;

using Orbit.Core;

namespace Orbit.Utilities
{
	/// <summary>
	/// Class specialized in capturing screenshots
	/// </summary>
	public class ScreenGrabber:IDisposable
	{
		#region Direct3D Resources
		private Device display;
		private Rectangle SrcRect;
		private Sprite SpritePainter;

		private OrbitTexture ScreenCapture;
		#endregion

		#region Threading
		private Thread RetrieveThread;
		#endregion

		#region Control Resources
		private byte Alpha;
		private bool CanDraw;
		#endregion

		#region Clipboard Resources
		private object OldClipboard;
		#endregion

		#region Creator
		/// <summary>
		/// Initiates a new instance of the ScreenGrabber class
		/// </summary>
		/// <param name="D3DDevice">Direct3D Device to load the screens to</param>
		/// <param name="D3DSprite">Direct3D Sprite to use to paint the screens</param>
		/// <param name="SourceRectangle">Area of the screen that the ScreenGrabber class should capture</param>
		public ScreenGrabber(Device D3DDevice, Sprite D3DSprite, Rectangle SourceRectangle)
		{
			// verifying values
			if(D3DDevice==null)
				throw new ArgumentNullException("D3DDevice");
			if(D3DSprite==null)
				throw new ArgumentNullException("D3DSprite");

			// gather data
			display=D3DDevice;
			SrcRect=SourceRectangle;
			SpritePainter=D3DSprite;

			//b=new Bitmap(SrcRect.Width, SrcRect.Height);
		}

		#endregion

		#region Public Functions
		#region Render Loop
		/// <summary>
		/// Sets the next frame in the animation of the Screenshot
		/// </summary>
		/// <returns>The recommended LoopState for the next loop</returns>
		public LoopState Animate()
		{
			// only animate if can draw and if hasn't already animated to the end
			if(CanDraw && Alpha<255)
			{
				Alpha+=15;
				/*if(Alpha+Orbit.Configuration.Global.Configuration.Runtime.LastFrameSpeed<255)
					Alpha+=Orbit.Configuration.Global.Configuration.Runtime.LastFrameSpeed;
				else
					Alpha=255;*/
				return LoopState.Loop;
			}
			return LoopState.Nothing;
		}
		
		/// <summary>
		/// Renders the Screenshot
		/// </summary>
		/// <param name="InitSprite">Tells the render method if it should Begin() the Sprite object or not</param>
		public void Render(bool InitSprite)
		{
			try
			{
				// only draw if ready
				if(CanDraw)
				{
					// init if needed
					if(InitSprite)
						SpritePainter.Begin(SpriteFlags.AlphaBlend);

					// draw
					SpritePainter.Draw2D(ScreenCapture.Texture,
										Rectangle.Empty,
										new SizeF(ScreenCapture.Description.Width, ScreenCapture.Description.Height),
										new Point(0, 0),
										Color.FromArgb(Alpha, Color.White));

					// stop if needed
					if(InitSprite)
						SpritePainter.End();
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}

		#endregion
		
		#region Utilities Functions
		/// <summary>
		/// Captures the screenshot and readies for rendering
		/// </summary>
		public void Capture(CaptureMode CapMode)
		{
			// ready states
			CanDraw=false;
			Alpha=0;

			switch(CapMode)
			{
				case CaptureMode.Clipboard:
					//CaptureFromBitBlt();
					CaptureFromClipboard();
					break;
				case CaptureMode.BitBlt:
					CaptureFromBitBlt();
					break;
				default:
					CaptureFromClipboard();
					break;
			}
		}

		#endregion
		#endregion

		#region Properties
		/// <summary>
		/// Gets the captured Texture
		/// </summary>
		public OrbitTexture ScreenGrab
		{
			get
			{
				return this.ScreenCapture;
			}
		}
		/// <summary>
		/// Gets or sets the area of the screen to be captured
		/// </summary>
		public Rectangle CaptureRegion
		{
			get
			{
				return this.SrcRect;
			}
			set
			{
				if(this.SrcRect!=value)
					this.SrcRect=value;
			}
		}
		/// <summary>
		/// Gets the blend-in stage of this background provider
		/// </summary>
		public byte AlphaStage
		{
			get
			{
				return this.Alpha;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// UNKNOWN
		/// </summary>
		public event EventHandler Paint;
		#endregion

		#region Private Capture Processes
		#region Clipboard
		#region Helper Functions
		private void CaptureOldClipboard()
		{
			// try to get the clipboard, if fails, set to null
			try
			{
				this.OldClipboard=Clipboard.GetDataObject().GetData(typeof(String));
			}
			catch(Exception)
			{
				this.OldClipboard=null;
			}
		}


		private void RestoreOldClipboard()
		{
			// set the item back if there was one or clear it if there wasn't
			try
			{
				if(this.OldClipboard!=null)
					Clipboard.SetDataObject(this.OldClipboard, true);
				else
					Clipboard.SetDataObject(null);
			}
			catch(Exception){}
		}


		private void CaptureThread()
		{
			// leave if not supported
			if(display.DeviceCaps.MaxTextureHeight<Screen.PrimaryScreen.Bounds.Height && display.DeviceCaps.MaxTextureWidth<Screen.PrimaryScreen.Bounds.Width)
			{
				return;
			}

			// destroy previous texture
			if(this.ScreenCapture!=null)
			{
				this.ScreenCapture.FreeReference();
				this.ScreenCapture = null;
			}

			// capture
			using(Bitmap MyBg=new Bitmap(SrcRect.Width, SrcRect.Height))
			{
				try
				{
					// getting from clipboard
					using(Bitmap screen=(Bitmap)Clipboard.GetDataObject().GetData(typeof(Bitmap)))
					{
						// drawing on final file
						using(Graphics g=Graphics.FromImage((Image)MyBg))
						{
							g.DrawImage(screen, new Rectangle(new Point(0,0), SrcRect.Size), SrcRect.X, SrcRect.Y, SrcRect.Width, SrcRect.Height, System.Drawing.GraphicsUnit.Pixel);
						}
					}
					// creating texture
					this.ScreenCapture=OrbitTextureLoader.Load(display, MyBg);
					// validating
					this.CanDraw=true;
					// request update
					if(this.Paint!=null)this.Paint(this, new EventArgs());
				}
				catch(Exception)
				{
					// failed somewhere, so can't draw
					this.CanDraw=false;
					// delete vestigies
					if(ScreenCapture!=null)
					{
						ScreenCapture.FreeReference();
						ScreenCapture = null;
					}
				}
				// restoring the old clipboard
				RestoreOldClipboard();
			}
		}
		#endregion

		private void CaptureFromClipboard()
		{
			// backup the old clipboard
			CaptureOldClipboard();			

			// send my data to the clipboard
			SendKeys.SendWait("+{PRTSC}");

			// start the retrieval thread
			if(RetrieveThread!=null && RetrieveThread.IsAlive)
				RetrieveThread.Abort();
			RetrieveThread=new System.Threading.Thread(new System.Threading.ThreadStart(CaptureThread));
			RetrieveThread.Priority=System.Threading.ThreadPriority.BelowNormal;
			RetrieveThread.Start();
		}
		#endregion

		#region BitBlt
		//Bitmap b;
		private void CaptureFromBitBlt()
		{
			try
			{
				// creating the bitmap
				using(Bitmap b=new Bitmap(SrcRect.Width, SrcRect.Height))
				{
					// capturing
					using(Graphics g=Graphics.FromImage(b))
					{
						IntPtr hdcScreen = Win32.GDI.GDIAPI.CreateDC("DISPLAY", null, null, IntPtr.Zero);
						IntPtr hdcG=g.GetHdc();

						Win32.GDI.GDIAPI.BitBlt(hdcG, 0, 0, b.Width, b.Height, hdcScreen, SrcRect.X, SrcRect.Y, Win32.GDI.RasterOperation.SourceCopy);

						g.ReleaseHdc(hdcG);
						Win32.GDI.GDIAPI.DeleteDC(hdcScreen);
					}
					// destroy previous texture
					if(this.ScreenCapture!=null)
					{
						this.ScreenCapture.FreeReference();
						this.ScreenCapture = null;
					}

					// creating texture
					this.ScreenCapture=OrbitTextureLoader.Load(display, b);
					// validating
					this.CanDraw=true;
					this.Alpha=255;
					// request update
					if(this.Paint!=null)this.Paint(this, new EventArgs());
				}
			}
			catch(Exception)
			{
				// failed somewhere, so can't draw
				this.CanDraw=false;
				this.Alpha=0;
				// delete vestigies
				if(ScreenCapture!=null)
				{
					ScreenCapture.FreeReference();
					ScreenCapture = null;
				}
			}
		}
		#endregion
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the ScreenGrabber class
		/// </summary>
		public void Dispose()
		{
			if(this.ScreenCapture!=null)
			{
				this.ScreenCapture.FreeReference();
				this.ScreenCapture = null;
			}

			if(this.RetrieveThread!=null && this.RetrieveThread.IsAlive)
				this.RetrieveThread.Abort();
		}

		#endregion
	}
}
