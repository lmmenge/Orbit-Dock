using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Specifies the window sizing and positioning flags. This parameter can be a combination of the following values
	/// </summary>
	public enum WindowPositionSizeFlags
	{
		/// <summary>
		/// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
		/// </summary>
		AsyncWindowPositioning=0x4000,
		/// <summary>
		/// Prevents generation of the WM_SYNCPAINT message.
		/// </summary>
		DeferErase=0x2000,
		/// <summary>
		/// Draws a frame (defined in the window's class description) around the window.
		/// </summary>
		DrawFrame=0x20,
		/// <summary>
		/// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
		/// </summary>
		FrameChanged=0x20,
		/// <summary>
		/// Hides the window.
		/// </summary>
		HideWindow=0x80,
		/// <summary>
		/// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
		/// </summary>
		NoActivate=0x10,
		/// <summary>
		/// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
		/// </summary>
		NoCopyBits=0x100,
		/// <summary>
		/// Retains the current position (ignores X and Y parameters).
		/// </summary>
		NoMove=0x2,
		/// <summary>
		/// Does not change the owner window's position in the Z order.
		/// </summary>
		NoOwnerZOrder=0x200,
		/// <summary>
		/// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
		/// </summary>
		NoRedraw=0x8,
		/// <summary>
		/// Same as the SWP_NOOWNERZORDER flag.
		/// </summary>
		NoRePosition=0x200,
		/// <summary>
		/// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
		/// </summary>
		NoSendChanging=0x400,
		/// <summary>
		/// Retains the current size (ignores the cx and cy parameters).
		/// </summary>
		NoSize=0x1,
		/// <summary>
		/// Retains the current Z order (ignores the hWndInsertAfter parameter).
		/// </summary>
		NoZOrder=0x4,
		/// <summary>
		/// Displays the window.
		/// </summary>
		ShowWindow=0x40
	}
}