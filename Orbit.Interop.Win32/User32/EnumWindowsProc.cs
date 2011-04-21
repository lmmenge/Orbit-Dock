using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// The EnumWindowsProc function is an application-defined callback function used with the EnumWindows or EnumDesktopWindows function. It receives top-level window handles. The WNDENUMPROC type defines a pointer to this callback function. EnumWindowsProc is a placeholder for the application-defined function name.
	/// </summary>
	/// <param name="hWnd">Handle to a top-level window.</param>
	/// <param name="lParam">Specifies the application-defined value given in EnumWindows or EnumDesktopWindows.</param>
	/// <returns>To continue enumeration, the callback function must return TRUE; to stop enumeration, it must return FALSE.</returns>
	public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
}
