using System;
using System.Runtime.InteropServices;

namespace Win32.GDI
{
	/// <summary>
	/// Provides declarations for the GDI API Calls
	/// </summary>
	public sealed class GDIAPI
	{
		#region Declarations
		/// <summary>
		/// Copies the Contents of a Bitmap to a buffer
		/// </summary>
		/// <param name="hbmp">Handle to bitmap</param>
		/// <param name="cbBuffer">Number of bytes to copy</param>
		/// <param name="lpvBits">Buffer to receive bits</param>
		/// <returns></returns>
		[DllImport("gdi32.dll")]
		public static extern long GetBitmapBits(IntPtr hbmp, long cbBuffer, IntPtr lpvBits);

		/// <summary>
		/// Deletes a GDI object
		/// </summary>
		/// <param name="hObject">GDI object to delete</param>
		/// <returns>True if succeeded.</returns>
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		/// <summary>
		/// Selects a GDI object into memory
		/// </summary>
		/// <param name="hDC">Device context to insert the object</param>
		/// <param name="hObject">The GDI object to insert</param>
		/// <returns>Handle to the object being replaced if succeeds.</returns>
		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		/// <summary>
		/// Creates a compatible device context
		/// </summary>
		/// <param name="hDC">Device context to be compatible to</param>
		/// <returns>A device context</returns>
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		/// <summary>
		/// Copies a region from a device context into another
		/// </summary>
		/// <param name="hdcDest">handle to destination DC</param>
		/// <param name="nXDest">x-coord of destination upper-left corner</param>
		/// <param name="nYDest">y-coord of destination upper-left corner</param>
		/// <param name="nWidth">width of destination rectangle</param>
		/// <param name="nHeight">height of destination rectangle</param>
		/// <param name="hdcSrc">handle to source DC</param>
		/// <param name="nXSrc">x-coordinate of source upper-left corner</param>
		/// <param name="nYSrc">y-coordinate of source upper-left corner</param>
		/// <param name="dwRop">raster operation code</param>
		/// <returns>True if succeeded.</returns>
		[DllImport("gdi32.DLL")]
		public static extern bool BitBlt(IntPtr hdcDest,int nXDest,int nYDest,int nWidth,int nHeight,IntPtr hdcSrc,int nXSrc,int nYSrc,RasterOperation dwRop);

		/// <summary>
		/// Creates a device context
		/// </summary>
		/// <param name="lpszDriver">driver name</param>
		/// <param name="lpszDevice">device name</param>
		/// <param name="lpszOutput">not used; should be NULL</param>
		/// <param name="lpInitData">optional printer data</param>
		/// <returns>A device context</returns>
		[DllImport("gdi32.DLL")]
		public static extern IntPtr CreateDC(string lpszDriver,string lpszDevice,string lpszOutput,IntPtr lpInitData);

		/// <summary>
		/// Deletes a device context
		/// </summary>
		/// <param name="hdc">The DC to delete</param>
		/// <returns>True if succeeded</returns>
		[DllImport("gdi32.DLL")]
		public static extern bool DeleteDC(IntPtr hdc);
		#endregion
	}
}