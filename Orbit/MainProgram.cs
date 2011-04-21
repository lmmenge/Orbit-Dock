using System;
using System.Windows.Forms;
using Orbit.OrbitServices.OrbitServicesClient;

namespace Orbit
{
	/// <summary>
	/// Summary description for MainProgram.
	/// </summary>
	public class MainProgram
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			using(OrbitWindow myForm = new OrbitWindow())
			{
				try
				{
					// set some flags
					myForm.ShowFPS=false;
					myForm.ShowOnTop=true;
					// parse the command line arguments
					int i=0;
					while(i<args.Length)
					{
						switch(args[i].ToLower())
						{
							case "showfps":
								myForm.ShowFPS=true;
								break;
							case "dontshowontop":
								myForm.ShowOnTop=false;;
								break;
						}
						i++;
					}
					// actually run
					if(!myForm.Disposing) Application.Run(myForm);
				}
				catch(Exception e)
				{
					// hide all traces of Orbit's existence
					try
					{
						Win32.User32.User32API.UnregisterHotKey(myForm.Handle, 100);
						Win32.User32.User32API.UnregisterHotKey(myForm.Handle, 101);
						myForm.ShowTrayIcon=false;
						myForm.Hide();
						if(myForm.Renderer.TransparentResourceManager!=null && myForm.Renderer.TransparentResourceManager.DisplayForm.Visible)
							myForm.Renderer.TransparentResourceManager.DisplayForm.Hide();
						myForm.Renderer.Stop();
					}
					catch(Exception){}

					// create the error information
					string ErrorReportData=e.GetType().ToString()+"\r\n"+e.StackTrace;
					if(!ErrorReport.WasReported(ErrorReportData))
					{
						// report only if it wasn't reported already
						ErrorReport ErrorForm=new ErrorReport(ErrorReportData);
						using(ErrorForm)
						{
							ErrorForm.ShowDialog();
						}
					}
					else
					{
						MessageBox.Show("Orbit has encountered an unhandled exception.\nThis error has already been reported by another user.\nOrbit will now exit.", "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					//System.Diagnostics.Process.GetCurrentProcess().Kill();
				}
			}
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}
	}
}
