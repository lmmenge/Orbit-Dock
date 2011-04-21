using System;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Orbit.Utilities
{
	/// <summary>
	/// Windows shots Capturing
	/// </summary>
	public sealed class WindowsTaskManager
	{
		#region Internal Variables
		IntPtr[] _Ignore;
		#endregion

		#region Public Static Variables
		/// <summary>
		/// Indicates whether the task manager should hide minimized tasks
		/// </summary>
		public static bool HideMinimizedWindows;
		#endregion

		#region Creator
		/// <summary>
		/// Creates a new instance of the WindowsTaskManager class
		/// </summary>
		/// <param name="handlesToIgnore">Array with window handles to ignore</param>
		public WindowsTaskManager(IntPtr[] handlesToIgnore)
		{
			//if(handlesToIgnore!=null)
			_Ignore=handlesToIgnore;
		}
		#endregion

		#region Public Functions
		#region Window Enumeration
		/// <summary>
		/// Gets the handles to all windows in their appearance order
		/// </summary>
		[Obsolete("Use the IntPtr[] GetWindowHandles()")]
		public void EnumWindows()
		{
			Win32.User32.User32API.EnumWindows(new Win32.User32.EnumWindowsProc(WindowReceived), 0);
		}


		/// <summary>
		/// Gets the handles to all windows in their appearance order
		/// </summary>
		/// <returns>Returns handles to all open windows</returns>
		public static IntPtr[] GetWindowHandles()
		{
			return GetWindowHandles(null);
		}

		
		/// <summary>
		/// Gets the handles to all windows in their appearance order
		/// </summary>
		/// <param name="excludedWindowsList">List with all the excluded windows</param>
		/// <returns>Returns handles to all open windows</returns>
		public static IntPtr[] GetWindowHandles(ExcludedWindow[] excludedWindowsList)
		{
			// count how many windows are there
			int i=0;
			IntPtr Handle=Win32.User32.User32API.GetForegroundWindow();
			do
			{
				if(WindowsTaskManager.IsTaskWindow(Handle)
					&& !IsTaskExcluded(Handle, excludedWindowsList))
					i++;
				Handle=Win32.User32.User32API.GetWindow(Handle, Win32.User32.GetWindowCommand.Next);
			} while (Handle!=IntPtr.Zero);

			// if no tasks are available
			if(i==0)
				return null;

			// actually catalog them
			IntPtr[] ItemRegistry=new IntPtr[i];
			int a=0;

			Handle=Win32.User32.User32API.GetForegroundWindow();
			do
			{
				if(WindowsTaskManager.IsTaskWindow(Handle)
					&& !IsTaskExcluded(Handle, excludedWindowsList))
				{
					ItemRegistry[a]=Handle;
					a++;
				}
				Handle=Win32.User32.User32API.GetWindow(Handle, Win32.User32.GetWindowCommand.Next);
			} while(Handle!=IntPtr.Zero);

			return ItemRegistry;
		}

		
		#region Window Exclusion Methods
		/// <summary>
		/// Tells if a window is in the provided ExcludedWindowList
		/// </summary>
		/// <param name="handle">Handle to the window to verify</param>
		/// <param name="excludedWindowsList">List with the excluded windows</param>
		/// <returns>True if window belongs in the excluded window list</returns>
		public static bool IsTaskExcluded(IntPtr handle, ExcludedWindow[] excludedWindowsList)
		{
			if(excludedWindowsList==null)
				return false;

			foreach(ExcludedWindow Excluded in excludedWindowsList)
			{
				//System.Diagnostics.Debug.WriteLine(Excluded.ProcessName.ToLower()+" and "+System.IO.Path.GetFileName(WindowsTaskManager.GetExecutableName(Handle)).ToLower());
				//System.Diagnostics.Debug.WriteLine(Excluded.ClassName+" and "+WindowsTaskManager.GetWindowClass(Handle));

				//if(Excluded.ProcessName.ToLower()==System.IO.Path.GetFileName(WindowsTaskManager.GetExecutableName(handle)).ToLower()
				if(String.Compare(Excluded.ProcessName, System.IO.Path.GetFileName(WindowsTaskManager.GetExecutableName(handle)), true, System.Globalization.CultureInfo.InvariantCulture)==0
					&& Excluded.ClassName==WindowsTaskManager.GetWindowClass(handle))
					return true;
			}
			return false;
		}
		/// <summary>
		/// Loads the window exclusion list from a file
		/// </summary>
		/// <param name="path">Path to the file containing the exclusion list</param>
		/// <returns>The exclusion list. Null if failed.</returns>
		public static ExcludedWindow[] LoadExcludedClassWindows(string path)
		{
			// bail out if doesn't exist
			if(!System.IO.File.Exists(path))
				return null;

			// load
			ExcludedWindow[] CustomExcludeList;
			try
			{
				// opens the file
				System.IO.StreamReader sr=new System.IO.StreamReader(path);
				// deserializes
				System.Xml.Serialization.XmlSerializer xs=new System.Xml.Serialization.XmlSerializer(typeof(ExcludedWindow[]));
				CustomExcludeList=(ExcludedWindow[])xs.Deserialize(sr);

				/*System.Xml.Serialization.XmlSerializer xs=new System.Xml.Serialization.XmlSerializer(typeof(ExcludedWindows));
				CustomExcludeList=((ExcludedWindows)xs.Deserialize(sr)).WindowList;*/
				// close
				sr.Close();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				// return
				return null;
			}

			return CustomExcludeList;
		}
		/// <summary>
		/// Saves the window exclusion list to a file
		/// </summary>
		/// <param name="path">Path to the file to save the exclusion list to</param>
		/// <param name="excludedWindowsList">Exclusion list to save</param>
		public static void SaveExcludedClassWindows(string path, ExcludedWindow[] excludedWindowsList)
		{
			// save
			try
			{
				// opens the file
				System.IO.StreamWriter sw=new System.IO.StreamWriter(path, false, System.Text.Encoding.Unicode);
				// serialize
				try
				{
					// serialize using generic array
					System.Xml.Serialization.XmlSerializer xs=new System.Xml.Serialization.XmlSerializer(typeof(ExcludedWindow[]));
					xs.Serialize(sw, excludedWindowsList);

					// serialize using custom class
					/*System.Xml.Serialization.XmlSerializer xs=new System.Xml.Serialization.XmlSerializer(typeof(ExcludedWindows));
					ExcludedWindows ex=new ExcludedWindows();
					ex.WindowList=excludedWindowsList;
					xs.Serialize(sw, ex);*/
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				}
				// close
				sw.Close();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}
		#endregion
		#endregion

		#region Window Information
		/// <summary>
		/// Gets the title text on a window
		/// </summary>
		/// <param name="handle">Handle to the window you want to get the title from</param>
		/// <returns>A string with the window title</returns>
		public static string GetWindowText(IntPtr handle)
		{
			System.Text.StringBuilder title=new System.Text.StringBuilder(256);
			Win32.User32.User32API.GetWindowText(handle, title, 255);
			return title.ToString().Trim();
		}
		/// <summary>
		/// Gets the class name of a window
		/// </summary>
		/// <param name="handle">Handle to the window you want to get the title from</param>
		/// <returns>A string with the window title</returns>
		public static string GetWindowClass(IntPtr handle)
		{
			System.Text.StringBuilder name=new System.Text.StringBuilder(256);
			Win32.User32.User32API.GetClassName(handle, name, 255);
			return name.ToString().Trim();
		}

		/// <summary>
		/// Gets the bounds of a window
		/// </summary>
		/// <param name="handle">Handle to the window</param>
		/// <returns>A Rectangle with the window bounds</returns>
		public static Rectangle GetWindowBounds(IntPtr handle)
		{
			Win32.Rectangle tmpRect;
			Win32.User32.User32API.GetWindowRect(handle, out tmpRect);

			Rectangle Rectangle = new Rectangle(tmpRect.left, tmpRect.top, tmpRect.right-tmpRect.left, tmpRect.bottom-tmpRect.top);
			return Rectangle;
		}
		/// <summary>
		/// Gets the icon to a window
		/// </summary>
		/// <param name="handle">Handle to a window to retrieve the icon from</param>
		/// <returns>An Icon object with the windows' icon</returns>
		public static Icon GetWindowIcon(IntPtr handle)
		{
			// try to get large icon
			IntPtr IconHandle=Win32.User32.User32API.GetClassLong(handle, Win32.User32.ClassLongValues.LargeIcon);
			// if fail, try to get small icon
			if(IconHandle==IntPtr.Zero)
				IconHandle=Win32.User32.User32API.GetClassLong(handle, Win32.User32.ClassLongValues.SmallIcon);

			// create icon if available
			Icon WindowIcon=null;
			if(IconHandle!=IntPtr.Zero)
				WindowIcon=Icon.FromHandle((IntPtr)IconHandle);

			// return
			return WindowIcon;
		}
		/// <summary>
		/// Determines if a window is a task window or not
		/// </summary>
		/// <param name="handle">Handle to the window</param>
		/// <returns>True if it is a task window</returns>
		public static bool IsTaskWindow(IntPtr handle)
		{
			if (Win32.User32.User32API.IsWindowVisible(handle) && WindowsTaskManager.GetWindowText(handle).Length>0)
			{
				System.Text.StringBuilder buf = new System.Text.StringBuilder(255);
				Rectangle rWin=GetWindowBounds(handle);
				//if (rWin.Width > 0 && rWin.Height > 0 && rWin.IntersectsWith(System.Windows.Forms.Screen.PrimaryScreen.Bounds)) // the intersect command hides minimized windows
				if (rWin.Width > 0 && rWin.Height > 0)
				{
					if(HideMinimizedWindows)
					{
						if(!rWin.IntersectsWith(System.Windows.Forms.Screen.PrimaryScreen.Bounds))
							return false;
					}

					Win32.User32.User32API.GetClassName(handle, buf, buf.MaxCapacity);
					if (buf.ToString() != "Progman" && buf.ToString() != "SysListView32")
						return true;
				}
			}

			return (false);
		}
		/// <summary>
		/// Gets the name of the executable that owns this window
		/// </summary>
		/// <param name="handle">Handle to the window</param>
		/// <returns>String with the process executable</returns>
		public static string GetExecutableName(IntPtr handle)
		{
			StringBuilder filename = new StringBuilder(256);
			int pid = 0;
			uint size = 0;
			IntPtr hModule = (IntPtr)0;
			IntPtr hProcess = (IntPtr)0;

			Win32.User32.User32API.GetWindowThreadProcessId(handle, out pid);
			hProcess = (IntPtr)Win32.Kernel32.Kernel32API.OpenProcess(Win32.Kernel32.DesiredAccess.QueryInformation | Win32.Kernel32.DesiredAccess.VMRead, 0, pid);
			Win32.PSAPI.PSAPI.EnumProcessModules(hProcess, ref hModule, (uint)Marshal.SizeOf(hModule), ref size);
			Win32.PSAPI.PSAPI.GetModuleFileNameExW(hProcess, hModule, filename, (uint)filename.MaxCapacity);
			Win32.Kernel32.Kernel32API.CloseHandle(hProcess);
			return filename.ToString();
		}
		#endregion

		#region Window Capture
		/// <summary>
		/// Captures a screenshot of a window
		/// </summary>
		/// <param name="windowName">The Title of the window to capture</param>
		/// <returns>A Bitmap with the image of the window</returns>
		public static Bitmap GetWindowBitmap(string windowName)
		{
			Process[] pl=Process.GetProcesses(Environment.MachineName);
			foreach(Process proc in pl)
			{
				if(proc.MainWindowTitle==windowName)
				{
					Bitmap shot=GetWindowBitmap(proc.MainWindowHandle);
					Win32.User32.User32API.UpdateWindow(proc.MainWindowHandle);
					return shot;
				}
			}
			return null;
		}
		/// <summary>
		/// Captures a screenshot of a window
		/// </summary>
		/// <param name="windowHandle">The Handle of the window to capture</param>
		/// <returns>A Bitmap with the image of the window</returns>
		public static Bitmap GetWindowBitmap(IntPtr windowHandle)
		{
			Win32.Rectangle r;
			Win32.User32.User32API.GetWindowRect(windowHandle, out r);
			if(r.right-r.left<=0 || r.bottom-r.top<=0)
				return null;
			Bitmap b=new Bitmap(r.right-r.left, r.bottom-r.top);
			using(Graphics g=Graphics.FromImage(b))
			{
				IntPtr hdc=g.GetHdc();

				if(!Win32.User32.User32API.PrintWindow(windowHandle, hdc, 0))
				{
					//Win32.User32.User32API.
					if(b!=null)
						b.Dispose();
					return null;
				}

				g.ReleaseHdc(hdc);
			}
			return b;
		}
		#endregion

		#region Window Switching
		/// <summary>
		/// Switches the active Window
		/// </summary>
		/// <param name="handle">Handle to the Window to switch to</param>
		public static void SwitchTo(IntPtr handle)
		{
			//SetForegroundWindow(Handle);
			Win32.User32.User32API.SwitchToThisWindow(handle, true);
		}
		#endregion
		#endregion

		#region Private Helper Functions
		private bool WindowReceived(IntPtr hWnd, int lParam)
		{
			string WndTitle=GetWindowText(hWnd);
			string WndClass=GetWindowClass(hWnd);

			Win32.Rectangle r;
			Win32.User32.User32API.GetWindowRect(hWnd, out r);
			if(r.right-r.left<=0 || r.bottom-r.top<=0)
				return true;

			//if(WndTitle!="" && Win32.User32.User32API.IsWindowVisible(hWnd) && hWnd!=IntPtr.Zero && WndClass!="Progman")
			if(WndTitle.Length!=0 && Win32.User32.User32API.IsWindowVisible(hWnd) && hWnd!=IntPtr.Zero && WndClass!="Progman")
			{
				if(_Ignore!=null)
				{
					foreach(IntPtr IgnoredHandle in _Ignore)
					{
						if(IgnoredHandle==hWnd)
							return true;
					}
				}
				WindowListedEventArgs wi=new WindowListedEventArgs(WndTitle, hWnd);
				if(WindowListed!=null)WindowListed(this, wi);
			}
			return true;
		}

		#endregion

		#region Events
		/// <summary>
		/// Occurs when a window's properties are received
		/// </summary>
		public event WindowListedEventHandler WindowListed;
		#endregion
	}
}
