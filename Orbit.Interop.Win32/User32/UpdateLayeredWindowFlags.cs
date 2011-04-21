using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// UpdateLayeredWindow API flags
	/// </summary>
	public enum UpdateLayeredWindowFlags
	{
		/// <summary>
		/// Use pblend as the blend function. If the display mode is 256 colors or less, the effect of this value is the same as the effect of
		/// </summary>
		Alpha=0x00000002,
		/// <summary>
		/// Use crKey as the transparency color.
		/// </summary>
		ColorKey=0x1,
		/// <summary>
		/// Draw an opaque layered window.
		/// </summary>
		Opaque=0x4
	}

}