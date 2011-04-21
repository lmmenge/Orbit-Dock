namespace Win32.User32
{
	/// <summary>
	/// Specifies the relationship between the specified window and the window whose handle is to be retrieved. This parameter can be one of the following values.
	/// </summary>
	public enum GetWindowCommand
	{
		/// <summary>
		/// The retrieved handle identifies the child window at the top of the Z order, if the specified window is a parent window; otherwise, the retrieved handle is NULL. The function examines only child windows of the specified window. It does not examine descendant windows.
		/// </summary>
		Child = 5,
		/// <summary>
		/// The retrieved handle identifies the window of the same type that is highest in the Z order. If the specified window is a topmost window, the handle identifies the topmost window that is highest in the Z order. If the specified window is a top-level window, the handle identifies the top-level window that is highest in the Z order. If the specified window is a child window, the handle identifies the sibling window that is highest in the Z order.
		/// </summary>
		First = 0,
		/// <summary>
		/// The retrieved handle identifies the window of the same type that is lowest in the Z order. If the specified window is a topmost window, the handle identifies the topmost window that is lowest in the Z order. If the specified window is a top-level window, the handle identifies the top-level window that is lowest in the Z order. If the specified window is a child window, the handle identifies the sibling window that is lowest in the Z order.
		/// </summary>
		Last = 1,
		/// <summary>
		/// The retrieved handle identifies the window below the specified window in the Z order. If the specified window is a topmost window, the handle identifies the topmost window below the specified window. If the specified window is a top-level window, the handle identifies the top-level window below the specified window. If the specified window is a child window, the handle identifies the sibling window below the specified window.
		/// </summary>
		Next = 2,
		/// <summary>
		/// The retrieved handle identifies the window above the specified window in the Z order. If the specified window is a topmost window, the handle identifies the topmost window above the specified window. If the specified window is a top-level window, the handle identifies the top-level window above the specified window. If the specified window is a child window, the handle identifies the sibling window above the specified window.
		/// </summary>
		Previous = 3,
		/// <summary>
		/// Windows 2000/XP: The retrieved handle identifies the enabled popup window owned by the specified window (the search uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled popup windows, the retrieved handle is that of the specified window.
		/// </summary>
		EnabledPopUp = 5,
		/// <summary>
		/// The retrieved handle identifies the specified window's owner window, if any.
		/// </summary>
		Owner = 4
	}
}