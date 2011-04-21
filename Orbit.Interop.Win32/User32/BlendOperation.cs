using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Specifies the source blend operation.
	/// </summary>
	public enum BlendOperation:byte
	{
		/// <summary>
		/// The source bitmap is placed over the destination bitmap based on the alpha values of the source pixels.
		/// </summary>
		SourceOver=0x00,
	}

}