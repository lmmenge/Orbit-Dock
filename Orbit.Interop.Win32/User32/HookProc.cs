using System;

namespace Win32.User32
{
	/// <summary>
	/// Delegate for the hook process
	/// </summary>
	public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
}
