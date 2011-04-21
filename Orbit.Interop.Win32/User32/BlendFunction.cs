using System;
using System.Runtime.InteropServices;

namespace Win32.User32
{
	/// <summary>
	/// Represents the Win32 BLENDFUNCTION structure which controls blending by specifying the blending functions for source and destination bitmaps.
	/// </summary>
	public struct BlendFunction 
	{
		/// <summary>
		/// Specifies the source blend operation. Currently, the only source and destination blend operation that has been defined is AC_SRC_OVER. For details, see the following Remarks section.
		/// </summary>
		public BlendOperation BlendOp;
		/// <summary>
		/// Must be zero.
		/// </summary>
		public byte BlendFlags;
		/// <summary>
		/// Specifies an alpha transparency value to be used on the entire source bitmap. The SourceConstantAlpha value is combined with any per-pixel alpha values in the source bitmap. If you set SourceConstantAlpha to 0, it is assumed that your image is transparent. Set the SourceConstantAlpha value to 255 (opaque) when you only want to use per-pixel alpha values.
		/// </summary>
		public byte SourceConstantAlpha;
		/// <summary>
		/// This member controls the way the source and destination bitmaps are interpreted. AlphaFormat has the following value.
		/// </summary>
		public AlphaFormat AlphaFormat;
	}

}