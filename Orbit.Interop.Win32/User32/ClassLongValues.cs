using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Specifies the 32-bit value to retrieve. To retrieve a 32-bit value from the extra class memory, specify the positive, zero-based byte offset of the value to be retrieved. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To retrieve any other value from the WNDCLASSEX structure, specify one of the following values.
	/// </summary>
	public enum ClassLongValues
	{
		/// <summary>
		/// Retrieves an ATOM value that uniquely identifies the window class. This is the same atom that the RegisterClassEx function returns.
		/// </summary>
		Atom=(-32),
		/// <summary>
		/// Retrieves the size, in bytes, of the extra memory associated with the class.
		/// </summary>
		ExtraClassMemory=(-20),
		/// <summary>
		/// Retrieves the size, in bytes, of the extra window memory associated with each window in the class. For information on how to access this memory, see GetWindowLong.
		/// </summary>
		ExtraWindowMemory=(-18),
		/// <summary>
		/// Retrieves a handle to the background brush associated with the class.
		/// </summary>
		BackgroundBrush=(-10),
		/// <summary>
		/// Retrieves a handle to the cursor associated with the class.
		/// </summary>
		Cursor=(-12),
		/// <summary>
		/// Retrieves a handle to the icon associated with the class.
		/// </summary>
		LargeIcon=(-14),
		/// <summary>
		/// Retrieves a handle to the small icon associated with the class.
		/// </summary>
		SmallIcon=(-34),
		/// <summary>
		/// Retrieves a handle to the module that registered the class.
		/// </summary>
		RegistrantModule=(-16),
		/// <summary>
		/// Retrieves the address of the menu name string. The string identifies the menu resource associated with the class.
		/// </summary>
		MenuName=(-8),
		/// <summary>
		/// Retrieves the window-class style bits.
		/// </summary>
		Style=(-26),
		/// <summary>
		/// Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.
		/// </summary>
		WindowProcedure=(-24)
	}

}