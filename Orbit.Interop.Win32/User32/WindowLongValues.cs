using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Attribute to change.
	/// </summary>
	public enum WindowLongValues
	{
		/// <summary>
		/// Sets a new extended window style. For more information, see CreateWindowEx.
		/// </summary>
		ExtendedStyle=(-20),
		/// <summary>
		/// Sets a new window style.
		/// </summary>
		Style=(-16),
		/// <summary>
		/// Sets a new address for the window procedure.
		/// </summary>
		WindowProcedure=(-4),
		/// <summary>
		/// Sets a new application instance handle.
		/// </summary>
		Handle=(-6),
		/// <summary>
		/// Sets a new identifier of the window.
		/// </summary>
		Id=(-12),
		/// <summary>
		/// Sets the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
		/// </summary>
		UserData=(-21),
		/// <summary>
		/// Sets the new pointer to the dialog box procedure.
		/// </summary>
		DialogProcedure=4,
		/// <summary>
		/// Sets the return value of a message processed in the dialog box procedure.
		/// </summary>
		MessageResult=0,
		/// <summary>
		/// Sets new extra information that is private to the application, such as handles or pointers.
		/// </summary>
		User=8
	}

}