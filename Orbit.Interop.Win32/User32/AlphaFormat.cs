using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Controls the way the source and destination bitmaps are interpreted.
	/// </summary>
	public enum AlphaFormat:byte
	{
		/// <summary>
		/// This flag is set when the bitmap has an Alpha channel (that is, per-pixel alpha). Note that the APIs use premultiplied alpha, which means that the red, green and blue channel values in the bitmap must be premultiplied with the alpha channel value. For example, if the alpha channel value is x, the red, green and blue channels must be multiplied by x and divided by 0xff prior to the call.
		/// </summary>
		SourceAlpha=0x01
	}

}