using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Provides declarations for the User32 API Calls
	/// </summary>
	public sealed class User32API
	{
		/// <summary>
		/// Draws an icon onto a Device Context
		/// </summary>
		/// <param name="hDC">Handle to the DC to draw to</param>
		/// <param name="X">X position</param>
		/// <param name="Y">Y position</param>
		/// <param name="hIcon">Handle to the icon to draw</param>
		/// <returns>True if succeeded</returns>
		[DllImport("user32.dll")]
		public static extern bool DrawIcon(IntPtr hDC,int X,int Y,IntPtr hIcon);

		/// <summary>
		/// Gets information on an icon
		/// </summary>
		/// <param name="hIcon">HICON to extract information from</param>
		/// <param name="piconinfo">Pointer to a IconInformation object to receive the information</param>
		/// <returns>True if succeeded</returns>
		[DllImport("user32.dll")]
		public static extern bool GetIconInfo(IntPtr hIcon, out IconInformation piconinfo);

		/// <summary>
		/// Registers a hotkey
		/// </summary>
		/// <param name="hWnd">Handle to the window to receive WM_HOTKEY messages</param>
		/// <param name="id">The hotkey identifier returned by the GlobalAddAtom</param>
		/// <param name="fsModifiers">Combination to be checked along with the hotkey</param>
		/// <param name="vk">Virtual key code for the key</param>
		/// <returns>True if succeeded</returns>
		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd,int id,KeyModifiers fsModifiers,System.Windows.Forms.Keys vk);

		/// <summary>
		/// Unregisters a hotkey
		/// </summary>
		/// <param name="hWnd">Handle to the window which registered</param>
		/// <param name="id">Hotkey id</param>
		/// <returns>True if succeeded</returns>
		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd,int id);
				
		/// <summary>
		/// Switches to a window
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <param name="fAltTab">Switch using Alt+Tab</param>
		[DllImport("user32.dll")]
		public static extern void SwitchToThisWindow(IntPtr hWnd,bool fAltTab);

		/// <summary>
		/// Enumerates all the windows
		/// </summary>
		/// <param name="ewp">Callback function</param>
		/// <param name="lParam">Optional parameter to pass to the callback</param>
		/// <returns>Nonzero if successful</returns>
		[DllImport("user32.dll")]
		public static extern int EnumWindows(EnumWindowsProc ewp,int lParam); 

		/// <summary>
		/// Prints a windows' contents into a device context
		/// </summary>
		/// <param name="hwnd">Handle to the window</param>
		/// <param name="hdcBlt">Device context to copy to</param>
		/// <param name="nFlags">Optional flags</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool PrintWindow(IntPtr hwnd,IntPtr hdcBlt,long nFlags);

		/// <summary>
		/// Gets information from a window class
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <param name="nIndex">Information to retrieve</param>
		/// <returns>Pointer to the information requested</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetClassLong(IntPtr hWnd,ClassLongValues nIndex);

		/// <summary>
		/// Gets the class name from a window
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <param name="lpClassName">StringBuilder where to store the class name</param>
		/// <param name="nMaxCount">Max count of characters to retrieve from the name</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int GetClassName(IntPtr hWnd,System.Text.StringBuilder lpClassName,int nMaxCount);

		/// <summary>
		/// Gets the window bounds
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <param name="lpRect">Out to the rectangle structure to receive the bounds</param>
		/// <returns>True if succeeded</returns>
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hWnd,out Win32.Rectangle lpRect);

		/// <summary>
		/// Finds out if window is visible
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <returns>True if window is visible. False if not.</returns>
		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		/// <summary>
		/// Gets the title text of a window
		/// </summary>
		/// <param name="h">Handle to the window to get the text from</param>
		/// <param name="s">StringBuilder where to store the title</param>
		/// <param name="nMaxCount">Max count of characters to retrieve from the name</param>
		[DllImport("user32.Dll")]
		public static extern void GetWindowText(IntPtr h,System.Text.StringBuilder s,int nMaxCount);

		/// <summary>
		/// Set the size, position and Z ordering of a window
		/// </summary>
		/// <param name="hwnd">Handle to the window</param>
		/// <param name="hWndInsertAfter">Z Ordering flag or handle to a window</param>
		/// <param name="X">X position</param>
		/// <param name="Y">Y position</param>
		/// <param name="cx">Width</param>
		/// <param name="cy">Height</param>
		/// <param name="wFlags">Window sizing and positioning flags</param>
		/// <returns>Nonzero if succeeds</returns>
		[DllImport("user32.dll")]
		public static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int X, int Y, int cx, int cy, WindowPositionSizeFlags wFlags);

		/// <summary>
		/// Gets the device context to a window
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <returns>A device context</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		/// <summary>
		/// Brings a window to the foreground
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		[DllImport("user32.dll")]
		public static extern void SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// Activates a window
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		[DllImport("user32.dll")]
		public static extern void SetActiveWindow(IntPtr hWnd);

		/// <summary>
		/// Gets the state of a key
		/// </summary>
		/// <param name="vKey">Virtual key code for the key</param>
		/// <returns>Most significant bit on: Key is pressed. Least significant bit on: Key was pressed between this call and the previous call to this function</returns>
		[DllImport("user32.dll")]
		public static extern int GetAsyncKeyState(int vKey);

		/// <summary>
		/// Sets the properties of a window
		/// </summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <param name="nIndex">Property to set</param>
		/// <param name="dwNewLong">Value</param>
		/// <returns>0 if failed</returns>
		[DllImport("user32.dll")]
		public static extern long SetWindowLong(IntPtr hWnd, WindowLongValues nIndex, WindowExtendedStyles dwNewLong);

		/// <summary>
		/// Updates the contents of a layered window
		/// </summary>
		/// <param name="hwnd">Handle to the window</param>
		/// <param name="hdcDst">Destination device context</param>
		/// <param name="pptDst">Destination Point</param>
		/// <param name="psize">Destination Size</param>
		/// <param name="hdcSrc">Source device context</param>
		/// <param name="pprSrc">Source Point</param>
		/// <param name="crKey">Color Key for the destination window</param>
		/// <param name="pblend">Blend information</param>
		/// <param name="dwFlags">Blend method</param>
		/// <returns>True if succeeded. False if not.</returns>
		[DllImport("user32.dll")]
		public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BlendFunction pblend, UpdateLayeredWindowFlags dwFlags);

		/// <summary>
		/// Destroys an instance of an icon
		/// </summary>
		/// <param name="hIcon">Handle to the icon to destroy</param>
		/// <returns>True if succeeded.</returns>
		[DllImport("user32.dll")]
		public static extern bool DestroyIcon(IntPtr hIcon);

		/// <summary>
		/// Gets the process id of a thread
		/// </summary>
		/// <param name="hwnd">Handle to the thread</param>
		/// <param name="lpdwProcessId">Int where the processid will be outputted</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);
		
		/// <summary>
		/// Gets the handle to the foreground window
		/// </summary>
		/// <returns>The handle to the foreground window</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();
		
		/// <summary>
		/// Gets the the handle to a window according to a the wCmd issued
		/// </summary>
		/// <param name="hWnd">Handle to the reference window</param>
		/// <param name="wCmd">Command to use based on the reference window</param>
		/// <returns>The handle to a window. IntPtr.Zero when done</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCommand wCmd);

		/// <summary>
		/// Updates the client area of a window
		/// </summary>
		/// <param name="hWnd">Handle to the window to repaint</param>
		/// <returns>True if succeeded.</returns>
		[DllImport("user32.dll")]
		public static extern bool UpdateWindow(IntPtr hWnd);

		//Import for SetWindowsHookEx function.
		//Use this function to install thread-specific hook.
		/// <summary>
		/// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events. These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
		/// </summary>
		/// <param name="idHook">[in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values.</param>
		/// <param name="lpfn">[in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a dynamic-link library (DLL). Otherwise, lpfn can point to a hook procedure in the code associated with the current process.</param>
		/// <param name="hInstance">[in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter. The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by the current process and if the hook procedure is within the code associated with the current process.</param>
		/// <param name="threadId">[in] Specifies the identifier of the thread with which the hook procedure is to be associated. If this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread.</param>
		/// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
		/// <remarks>An error may occur if the hMod parameter is NULL and the dwThreadId parameter is zero or specifies the identifier of a thread created by another process. Calling the CallNextHookEx function to chain to the next hook procedure is optional, but it is highly recommended; otherwise, other applications that have installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call CallNextHookEx unless you absolutely need to prevent the notification from being seen by other applications. Before terminating, an application must call the UnhookWindowsHookEx function to free system resources associated with the hook.</remarks>
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern int SetWindowsHookEx(HookType idHook, HookProc lpfn, IntPtr hInstance, int threadId);

		//Import for UnhookWindowsHookEx.
		//Call this function to uninstall the hook.
		/// <summary>
		/// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
		/// </summary>
		/// <param name="idHook">[in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx.</param>
		/// <returns>If the function succeeds, the return value is nonzero.</returns>
		/// <remarks>The hook procedure can be in the state of being called by another thread even after UnhookWindowsHookEx returns. If the hook procedure is not being called concurrently, the hook procedure is removed immediately before UnhookWindowsHookEx returns.</remarks>
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern bool UnhookWindowsHookEx(int idHook);

		//Import for CallNextHookEx.
		//Use this function to pass the hook information to next hook procedure in chain.
		/// <summary>
		/// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information.
		/// </summary>
		/// <param name="idHook">Ignored.</param>
		/// <param name="nCode">[in] Specifies the hook code passed to the current hook procedure. The next hook procedure uses this code to determine how to process the hook information.</param>
		/// <param name="wParam">[in] Specifies the wParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
		/// <param name="lParam">[in] Specifies the lParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
		/// <returns>This value is returned by the next hook procedure in the chain. The current hook procedure must also return this value. The meaning of the return value depends on the hook type. For more information, see the descriptions of the individual hook procedures.</returns>
		/// <remarks>Hook procedures are installed in chains for particular hook types. CallNextHookEx calls the next hook in the chain. Calling CallNextHookEx is optional, but it is highly recommended; otherwise, other applications that have installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call CallNextHookEx unless you absolutely need to prevent the notification from being seen by other applications.</remarks>
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
	}
}