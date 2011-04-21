using System;
using System.Runtime.InteropServices;

namespace Win32
{
	/// <summary>
	/// Represents a Win32 POINT structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Point 
	{
		/// <summary>
		/// The X coordinate
		/// </summary>
		public System.Int32 x;
		/// <summary>
		/// The Y coordinate
		/// </summary>
		public System.Int32 y;

		/// <summary>
		/// Creates a new instance of the Point structure from two given coordinates
		/// </summary>
		/// <param name="x">The X coordinate</param>
		/// <param name="y">The Y coordinate</param>
		public Point(System.Int32 x, System.Int32 y) { this.x= x; this.y= y; }
	}

}