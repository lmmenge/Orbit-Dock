using System;
using System.Runtime.InteropServices;

namespace Win32
{
	/// <summary>
	/// Represents a Win32 RECT structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Rectangle 
	{
		/// <summary>
		/// The X coordinate from the top left point
		/// </summary>
		public int left;
		/// <summary>
		/// The Y coordinate from the top left point
		/// </summary>
		public int top;
		/// <summary>
		/// The X coordinate from the bottom right point
		/// </summary>
		public int right;
		/// <summary>
		/// The Y coordinate from the bottom right point
		/// </summary>
		public int bottom;
	}
}