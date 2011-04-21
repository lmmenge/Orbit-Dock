using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32.User32;

namespace Orbit.Utilities
{
	/// <summary>
	/// Hooks the Mouse
	/// </summary>
	//[Obsolete("Using the C++ DLL, instead", true)]
	public class MouseHook:IDisposable
	{
		#region Internal Variables
		private int HookHandle=0;
		#endregion

		#region Creator
		/// <summary>
		/// Creates a new instance of the MouseHook class
		/// </summary>
		public MouseHook()
		{
			// Create an instance of HookProc.
			HookProc MouseHookProcedure = new HookProc(MouseHookProc);

			// setting the hook
			//HookHandle = User32API.SetWindowsHookEx(HookType.WH_MOUSE, MouseHookProcedure, (IntPtr)0, AppDomain.GetCurrentThreadId());
			HookHandle = User32API.SetWindowsHookEx(HookType.WH_MOUSE_LL, MouseHookProcedure,  Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
			//If SetWindowsHookEx fails.
			if(HookHandle == 0 )
			{
				System.Windows.Forms.MessageBox.Show("hook failed");
				//return;
			}
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the MouseHook object and undoes the hook
		/// </summary>
		public void Dispose()
		{
			// attempting unhook
			bool ret = Win32.User32.User32API.UnhookWindowsHookEx(HookHandle);
			//If UnhookWindowsHookEx fails.
			if(ret == false)
			{
				System.Windows.Forms.MessageBox.Show("unhook failed");
				return;
			}
			HookHandle=0;
		}
		#endregion

		#region Hook Process
		private int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			//Marshall the data from callback.
			MouseHookStruct MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

			// this code requires us to return the next hook
			if (nCode < 0)
				return Win32.User32.User32API.CallNextHookEx(HookHandle, nCode, wParam, lParam);

			//Create a string variable with shows current mouse. coordinates
			switch((HookWindowMessage)wParam.ToInt32())
			{
				case HookWindowMessage.WM_LBUTTONDOWN:
					System.Windows.Forms.MessageBox.Show("left down");
					break;
				case HookWindowMessage.WM_LBUTTONUP:
					System.Windows.Forms.MessageBox.Show("left up");
					break;
				case HookWindowMessage.WM_RBUTTONDOWN:
					System.Windows.Forms.MessageBox.Show("right down");
					break;
				case HookWindowMessage.WM_RBUTTONUP:
					System.Windows.Forms.MessageBox.Show("right up");
					break;
				case HookWindowMessage.WM_MOUSEWHEEL:
					System.Windows.Forms.MessageBox.Show("wheel up");
					break;
				case HookWindowMessage.WM_MOUSEMOVE:
					System.Windows.Forms.MessageBox.Show("mouse move");
					break;
			}

			// pass the message on to the next hook
			return Win32.User32.User32API.CallNextHookEx(HookHandle, nCode, wParam, lParam);
		}
		#endregion

		#region Interop
		/// <summary>
		/// Starts the Mouse hook process
		/// </summary>
		/// <param name="hWnd">Handle to bind the hook to</param>
		/// <returns></returns>
		[DllImport("Orbit.Hook.dll")]
		public static extern bool StartHook(IntPtr hWnd);
		/// <summary>
		/// Stops the Mouse hook process
		/// </summary>
		/// <param name="hWnd">Handle to unbind the hook from</param>
		/// <returns></returns>
		[DllImport("Orbit.Hook.dll")]
		public static extern bool StopHook(IntPtr hWnd);
		/// <summary>
		/// Sets the shortcut key
		/// </summary>
		/// <param name="vKey">Mouse button to hook to</param>
		[DllImport("Orbit.Hook.dll")]
		public static extern void SetShortcutKey(Keys vKey);
		#endregion
	}
}
