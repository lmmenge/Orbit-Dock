using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.DirectX.Direct3D;

using Orbit.Utilities;

namespace Orbit.Core
{
	/// <summary>
	/// Manages the resources used in Transparent Mode
	/// </summary>
	public class TransparentResourceManager : IDisposable
	{
		#region Managed Resources
		#region Owner created
		private Device display;
		#endregion

		#region Managed
		private Microsoft.DirectX.Direct3D.Surface front;
		private Microsoft.DirectX.Direct3D.Surface blit;

		private OrbitTransparentWindow Disp;
		private OrbitTransparentWindow BgDisp;
		private OrbitTransparentWindow LabelDisp;
		private OrbitTransparentWindow OverlayDisp;

		/// <summary>
		/// Size of the surface
		/// </summary>
		public System.Drawing.Size ProjectionSize;
		#endregion
		#endregion

		#region Creator
		/// <summary>
		/// Creates a new instance of the TransparentResourceManager class
		/// </summary>
		/// <param name="D3DDevice">Direct3D Device that this class should use</param>
		public TransparentResourceManager(Device D3DDevice)
		{
			if(D3DDevice==null)
				throw new ArgumentNullException("D3DDevice");

			display=D3DDevice;

			InitializeTransparentWindows();
		}


		/// <summary>
		/// Initialized new instances of the managed transparent windows
		/// </summary>
		private void InitializeTransparentWindows()
		{
			// dispose if they existed b4
			DisposeForms();
			
			// create
			Disp=new OrbitTransparentWindow(false);
			BgDisp=new OrbitTransparentWindow(true);
			LabelDisp=new OrbitTransparentWindow(true);
			OverlayDisp=new OrbitTransparentWindow(true);

			// set always on top
			Win32.User32.WindowPositionSizeFlags SWPFlags=Win32.User32.WindowPositionSizeFlags.NoMove | Win32.User32.WindowPositionSizeFlags.NoSize;
			Win32.User32.User32API.SetWindowPos(Disp.Handle, -1, 0, 0, 0, 0, SWPFlags);
			Win32.User32.User32API.SetWindowPos(BgDisp.Handle, -1, 0, 0, 0, 0, SWPFlags);
			Win32.User32.User32API.SetWindowPos(LabelDisp.Handle, -1, 0, 0, 0, 0, SWPFlags);
			Win32.User32.User32API.SetWindowPos(OverlayDisp.Handle, -1, 0, 0, 0, 0, SWPFlags);
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the TransparentResourceManager class
		/// </summary>
		public void Dispose()
		{
			// front buffer
			DisposeFrontBuffer();

			// blit buffer
			DisposeBlitBuffer();

			// forms
			DisposeForms();
		}


		#region Individual disposal
		private void DisposeFrontBuffer()
		{
			// front buffer
			if(this.front!=null)
				this.front.Dispose();
		}

		private void DisposeBlitBuffer()
		{
			if(this.blit!=null)
				this.blit.Dispose();
		}
		private void DisposeForms()
		{
			if(Disp!=null)
				Disp.Dispose();
			if(BgDisp!=null)
				BgDisp.Dispose();
			if(LabelDisp!=null)
				LabelDisp.Dispose();
			if(OverlayDisp!=null)
				OverlayDisp.Dispose();
		}
		#endregion
		#endregion

		#region Public Functions
		/// <summary>
		/// Blits the front buffer to a system buffer and returns a Bitmap from that buffer
		/// </summary>
		/// <param name="Rectangle">Region to capture</param>
		/// <returns>Returns a Bitmap with the front buffer contents</returns>
		public System.Drawing.Bitmap GetCropFrontBuffer(ref System.Drawing.Rectangle Rectangle)
		{
			// crop the rectangle if it's too large, correcting it
			if(Rectangle.Width>blit.Description.Width)
				Rectangle.Width=blit.Description.Width;
			if(Rectangle.Height>blit.Description.Height)
				Rectangle.Height=blit.Description.Height;
			try
			{
				// update the system surface
				display.GetRenderTargetData(front, blit);

				// do the normal stuff but this time from the systemSurface
				int Pitch;
				// lock it
				Microsoft.DirectX.GraphicsStream gs = blit.LockRectangle(Rectangle,LockFlags.None,out Pitch);
				// create bitmap (this might be the slow call for this process)
				Bitmap b=new Bitmap(Rectangle.Width, Rectangle.Height, Pitch,System.Drawing.Imaging.PixelFormat.Format32bppArgb,gs.InternalData);
				//Bitmap b=null;
				// unlock
				blit.UnlockRectangle();
				// return our bitmap
				return b;
			}
			catch(Exception)
			{
				try
				{
					blit.UnlockRectangle();
				}
				catch(Exception){}
				return null;
			}
		}
		/// <summary>
		/// Sets up the front buffer
		/// </summary>
		/// <param name="BufferSize">Size of the buffer</param>
		public void SetUpFrontBuffer(Size BufferSize)
		{
			try
			{
				DisposeFrontBuffer();
				this.front = display.CreateRenderTarget(BufferSize.Width, BufferSize.Height, Format.A8R8G8B8, MultiSampleType.None, 0, true);
				display.SetRenderTarget(0, front);
			}
			catch(Exception)
			{
				System.Windows.Forms.MessageBox.Show("Error creating Front Buffer");
				throw;
			}
		}

		/// <summary>
		/// Sets up the secondary blit buffer
		/// </summary>
		/// <param name="BufferSize">Size of the secondary buffer</param>
		/// <remarks>It is recommended that the Size be the same as the Front Buffer one's</remarks>
		public void SetUpBlitBuffer(Size BufferSize)
		{
			try
			{
				DisposeBlitBuffer();
				blit = display.CreateOffscreenPlainSurface(BufferSize.Width, BufferSize.Height,Format.A8R8G8B8, Pool.SystemMemory);
			}
			catch(Exception)
			{
				System.Windows.Forms.MessageBox.Show("Error creating Blit Buffer");
				throw;
			}
		}
		/// <summary>
		/// Hooks the transparent forms's events
		/// </summary>
		/// <param name="Closed">Callback to the Closed event</param>
		/// <param name="Deactivate">Callback to the Deactivate event</param>
		/// <param name="MouseMove">Callback to the MouseMove event</param>
		/// <param name="MouseUp">Callback to the MouseUp event</param>
		/// <param name="MouseWheel">Callback to the MouseWheel event</param>
		/// <param name="KeyDown">Callback to the KeyDown event</param>
		/// <param name="KeyUp">Callback to the KeyUp event</param>
		public void HookFormEvents(
			EventHandler Closed,
			EventHandler Deactivate,
			MouseEventHandler MouseMove,
			MouseEventHandler MouseUp,
			MouseEventHandler MouseWheel,
			KeyEventHandler KeyDown,
			KeyEventHandler KeyUp)
		{
			// hook the drawing form to me
			Disp.Closed+=Closed;
			Disp.MouseMove+=MouseMove;
			Disp.MouseUp+=MouseUp;
			Disp.Deactivate+=Deactivate;
			Disp.MouseWheel+=MouseWheel;
			Disp.KeyDown+=KeyDown;
			Disp.KeyUp+=KeyUp;

			// hook the background form to me
			BgDisp.Closed+=Closed;

			// hook the label window to me
			LabelDisp.Closed+=Closed;
			LabelDisp.MouseWheel+=MouseWheel;
			LabelDisp.KeyDown+=KeyDown;
			LabelDisp.KeyUp+=KeyUp;

			// hook the overlays form to me
			OverlayDisp.Closed+=Closed;
			OverlayDisp.MouseMove+=MouseMove;
			OverlayDisp.MouseUp+=MouseUp;
			OverlayDisp.Deactivate+=Deactivate;
			OverlayDisp.MouseWheel+=MouseWheel;
			OverlayDisp.KeyDown+=KeyDown;
			OverlayDisp.KeyUp+=KeyUp;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the Front Buffer
		/// </summary>
		public Surface FrontBuffer
		{
			get
			{
				return front;
			}
		}
		/// <summary>
		/// Gets the Secondary Blit Buffer
		/// </summary>
		public Surface BlitBuffer
		{
			get
			{
				return blit;
			}
		}
		/// <summary>
		/// Gets the Display Window
		/// </summary>
		public OrbitTransparentWindow DisplayForm
		{
			get
			{
				return Disp;
			}
		}
		/// <summary>
		/// Gets the Overlay Window
		/// </summary>
		public OrbitTransparentWindow OverlayForm
		{
			get
			{
				return OverlayDisp;
			}
		}
		/// <summary>
		/// Gets the Background Window
		/// </summary>
		public OrbitTransparentWindow BackForm
		{
			get
			{
				return BgDisp;
			}
		}
		/// <summary>
		/// Gets the Label Window
		/// </summary>
		public OrbitTransparentWindow LabelForm
		{
			get
			{
				return LabelDisp;
			}
		}
		#endregion
	}
}
