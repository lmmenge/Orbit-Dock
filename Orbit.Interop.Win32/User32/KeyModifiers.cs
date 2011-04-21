using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Specifies keys that must be pressed in combination with the key specified.
	/// </summary>
	[Flags()]
	public enum KeyModifiers
	{  
		/// <summary>
		/// No Key.
		/// </summary>
		None = 0,
		/// <summary>
		/// Either ALT key must be held down.
		/// </summary>
		Alt = 1,
		/// <summary>
		/// Either CTRL key must be held down.
		/// </summary>
		Control = 2,
		/// <summary>
		/// Either SHIFT key must be held down.
		/// </summary>
		Shift = 4,
		/// <summary>
		/// Either WINDOWS key was held down. These keys are labeled with the Microsoft® Windows® logo.
		/// </summary>
		Windows = 8
	}

}