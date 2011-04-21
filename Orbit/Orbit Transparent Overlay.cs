using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Orbit.Utilities;

namespace Orbit
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class OrbitTransparentWindow : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private static System.Threading.Mutex mutex = new System.Threading.Mutex();

		#region Constructor
		/// <summary>
		/// Creates a new instance of the OrbitTransparentWindow class
		/// </summary>
		/// <param name="ClickThrough">Indicates if the window should be Click-Through</param>
		public OrbitTransparentWindow(bool ClickThrough)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			if(ClickThrough)
			{
				Win32.User32.User32API.SetWindowLong(this.Handle, Win32.User32.WindowLongValues.ExtendedStyle, Win32.User32.WindowExtendedStyles.Layered | Win32.User32.WindowExtendedStyles.Transparent);
			}
			else 
			{
				Win32.User32.User32API.SetWindowLong(this.Handle, Win32.User32.WindowLongValues.ExtendedStyle, Win32.User32.WindowExtendedStyles.Layered);
			}
		}

		#endregion

		#region Destructor
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// OrbitTransparentWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "OrbitTransparentWindow";
			this.ShowInTaskbar = false;
			this.Text = "Orbit";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
		}
		#endregion

		#region Event Handling
		private void Form2_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Close if esc is pressed
			if(e.KeyChar==(char)27)
			{
				this.Close();
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Updates the content of this window with a Bitmap
		/// </summary>
		/// <param name="Bmp">Bitmap to update this window with</param>
		/// <param name="Rect">Rectangle where this window should be located</param>
		public void Update(Bitmap Bmp, Rectangle Rect)
		{
			if(Bmp==null)
				return;

			// wait until it's safe to proceed
			//mutex.WaitOne();
			/*
			 * This function blits a Bitmap to a Layered Window.
			 * ALL drawing functions that are for the transparent
			 * mode use this function.
			*/
			//System.Diagnostics.Debug.WriteLine("started getting hbitmap from bitmap");
			IntPtr Bmap=Bmp.GetHbitmap(Color.FromArgb(0));
			//System.Diagnostics.Debug.WriteLine("done!");
			IntPtr memDc = Win32.GDI.GDIAPI.CreateCompatibleDC(Win32.User32.User32API.GetDC(IntPtr.Zero));
			IntPtr oldBitmap = Win32.GDI.GDIAPI.SelectObject(memDc, Bmap);

			try
			{
				Win32.User32.BlendFunction blend = new Win32.User32.BlendFunction();
				blend.BlendOp             = Win32.User32.BlendOperation.SourceOver;
				blend.BlendFlags          = 0;
				blend.SourceConstantAlpha = 255;
				blend.AlphaFormat         = Win32.User32.AlphaFormat.SourceAlpha;

				Win32.Size size = new Win32.Size(Rect.Width, Rect.Height);
				Win32.Point topPos = new Win32.Point(Rect.X, Rect.Y);
				Win32.Point pointSource = new Win32.Point(0, 0);

				Win32.User32.User32API.UpdateLayeredWindow(this.Handle, IntPtr.Zero, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.User32.UpdateLayeredWindowFlags.Alpha);
			}
			finally
			{
				try
				{
					if (Bmap != IntPtr.Zero) 
					{
						Win32.GDI.GDIAPI.SelectObject(memDc, oldBitmap);
						Win32.GDI.GDIAPI.DeleteObject(Bmap);
					}
					Win32.GDI.GDIAPI.DeleteDC(memDc);
				}
				catch(Exception){}
				//Bmp.Dispose();
			}
			// release our mutex
			//mutex.ReleaseMutex();
		}

		/// <summary>
		/// Updates the layered window with a transparent bitmap (essentially clears the contents of that window)
		/// </summary>
		public void Clear()
		{
			using(Bitmap b=new Bitmap(1,1))
			{
				Update(b, new Rectangle(0,0,1,1));
			}
		}
		#endregion
	}
}
