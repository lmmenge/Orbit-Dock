using System;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.Direct3D;

using Orbit.Configuration;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// An item that represents a running task
	/// </summary>
	public class TaskItem:PreviewableItem
	{
		#region Private variables
		#region Internal Control variables
		private IntPtr Handle;
		#endregion

		#region Properties
		private string _WindowName;
		#endregion
		#endregion

		#region Constructor
		private TaskItem(){}

		/// <summary>
		/// Creates a new instance of a Task Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="WindowInformation">Information about the window that this object represents</param>
		public TaskItem(Device Device, Orbit.Utilities.WindowInformation WindowInformation)
		{
			// checking the values
			ValidateDevice(Device);

			// settng the basic properties
			display=Device;

			try
			{
				// initialize common resources
				InitializeResources();

				// initialize needed HQ task preview resources
				InitializePreviewResources();

				// set the context menu flags
				this._MenuFlags=ItemMenuFlags.IgnoreWindow;

				// get information from window
				_WindowName=WindowInformation.Name;
				//base.Name=WindowInformation.Name;
				//base.Description="Running Task";
				this.Handle=WindowInformation.Handle;

				// load icon from window
				try
				{
					using(Icon ico=WindowsTaskManager.GetWindowIcon(WindowInformation.Handle))
					{
						if(ico!=null && ico.Width>=32) // we don't want 16x16 icons here (we can get 32x32 icons on the other page for sure)
						{
							// got icon for the window
							using(Bitmap b=ImageHelper.GetBitmapFromIcon(ico))
							{
								SetIcon(b);
								//SetOverlay(b); // overlay not needed anymore. will use standard icon
							}
						}
						else
						{
							try
							{
								// try to acquire icon from the process module
								string modulePath=WindowsTaskManager.GetExecutableName(WindowInformation.Handle);
								// or use the "C:\Windows\System32\more.com" file for dummy icon
								if(!System.IO.File.Exists(modulePath))
									modulePath=System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System), "more.com");

								Win32.Shell32.SHFileInfo FileInfo=new Win32.Shell32.SHFileInfo();
								Win32.Shell32.Shell32API.SHGetFileInfo(modulePath, 0, ref FileInfo, (uint)System.Runtime.InteropServices.Marshal.SizeOf(FileInfo), Win32.Shell32.ShellGetFileInfoFlags.LargeIcon | Win32.Shell32.ShellGetFileInfoFlags.Icon);

								try
								{
									// getting the handle to the icon from the SHFileInfo structure
									using(Icon IconO=Icon.FromHandle(FileInfo.hIcon))
									{
										// create the biggest possible icon
										using(Icon icon=new Icon(IconO, 128, 128))
										{
											//System.Diagnostics.Debug.WriteLine("Icon size"+icon.Width);
											// convert to bitmap
											using(Bitmap IconPic=Orbit.Utilities.ImageHelper.GetBitmapFromIcon(icon))
											{
												// setting it to be the icon
												this.SetIcon(IconPic);
												//this.SetOverlay(IconPic); // overlay not needed anymore. will use standard icon
											}
										}
									}
								}
								catch(Exception){}
								// don't forget to destroy the handle to the other icon
								Win32.User32.User32API.DestroyIcon(FileInfo.hIcon);
							}
							catch(Exception)
							{
								CannotLoadIcon();
							}
						}
					}
				}
				catch(Exception)
				{
					System.Diagnostics.Debug.WriteLine("failed to load icon");
				}
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// ALT+TABs to the window represented by this item
		/// </summary>
		public void SwitchTo()
		{
			WindowsTaskManager.SwitchTo(this.Handle);
		}
		/// <summary>
		/// Adds a window class name to the exclusion list
		/// </summary>
		public void IgnoreWindow()
		{
			// create the new excluded window
			ExcludedWindow me=new ExcludedWindow(WindowsTaskManager.GetWindowClass(Handle), System.IO.Path.GetFileName(WindowsTaskManager.GetExecutableName(Handle)));
			//System.Diagnostics.Debug.WriteLine("ignoring "+me.ClassName+" from "+me.ProcessName);

			// load the custom list
			ExcludedWindow[] CustomExcludeList=WindowsTaskManager.LoadExcludedClassWindows(Orbit.Configuration.ConfigurationInfo.LocationsConfig.GetExcludedTasksFilePath());

			if(CustomExcludeList==null)
			{
				ExcludedWindow[] NewExcludedList=new ExcludedWindow[1];
				NewExcludedList[0]=me;
				WindowsTaskManager.SaveExcludedClassWindows(Orbit.Configuration.ConfigurationInfo.LocationsConfig.GetExcludedTasksFilePath(), NewExcludedList);
				return;
			}

			// set our new list
			ExcludedWindow[] ExcludeList=new ExcludedWindow[CustomExcludeList.Length+1];
			// set our mandatory excluded tasks
			
			// add our new class
			ExcludeList[0]=me;

			// copy our custom exclusion list
			int i=0;
			while(i<CustomExcludeList.Length)
			{
				ExcludeList[i+1]=CustomExcludeList[i];
				i++;
			}

			WindowsTaskManager.SaveExcludedClassWindows(Orbit.Configuration.ConfigurationInfo.LocationsConfig.GetExcludedTasksFilePath(), ExcludeList);
		}
		#endregion

		#region Inherited Methods
		/// <summary>
		/// Gets the task preview image
		/// </summary>
		protected override void GetPreview()
		{
			// if window is minimized, then don't get it's thumbnail
			if(false && !WindowsTaskManager.GetWindowBounds(Handle).IntersectsWith(System.Windows.Forms.Screen.PrimaryScreen.Bounds))
				return;

			// getting the screenshot
			try
			{
				using(Bitmap Shot=WindowsTaskManager.GetWindowBitmap(Handle))
				{
					// thumbnailing not needed
					/*using(Bitmap ShotThumb=(Bitmap)ImageHelper.GetAspectThumbnail(Shot, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
					{
						SetBackground(ShotThumb);
					}*/
					SetBackground(Shot);
				}
				Compose();
			}
			catch(Exception){}
		}

		#endregion

		#region Overriden/Hidden Properties
		/// <summary>
		/// Gets the title of the window
		/// </summary>
		public override string Name
		{
			get
			{
				return _WindowName;
			}
		}

		/// <summary>
		/// Gets the description of this window
		/// </summary>
		public override string Description
		{
			get
			{
				return "Running Task";
			}
		}

		#endregion
	}
}