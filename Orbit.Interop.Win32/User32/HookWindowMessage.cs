using System;

namespace Win32.User32
{
	/// <summary>
	/// Specifies the identifier of the mouse message.
	/// </summary>
	public enum HookWindowMessage
	{
		/// <summary>
		/// Left mouse button down
		/// </summary>
		WM_LBUTTONDOWN=0x0201,
		/// <summary>
		/// Left mouse button up
		/// </summary>
		WM_LBUTTONUP=0x0202,
		/// <summary>
		/// Mouse move
		/// </summary>
		WM_MOUSEMOVE=0x0200,
		/// <summary>
		/// Mouse wheel scroll
		/// </summary>
		WM_MOUSEWHEEL=0x020A,
		/// <summary>
		/// Right mouse button down
		/// </summary>
		WM_RBUTTONDOWN= 0x0204,
		/// <summary>
		/// Right mouse button up
		/// </summary>
		WM_RBUTTONUP=0x0205,
		/// <summary>
		/// Middle mouse button down
		/// </summary>
		WM_MBUTTONDOWN = 0x207,
		/// <summary>
		/// Middle mouse button up
		/// </summary>
		WM_MBUTTONUP=0x208
	}
}
