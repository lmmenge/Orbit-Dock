using System;
using System.Runtime.InteropServices;

namespace Win32
{
	/// <summary>
	/// Represents a Win32 SIZE structure
	/// </summary>
	public struct Size 
	{
		/// <summary>
		/// The Width parameter
		/// </summary>
		public System.Int32 cx;
		/// <summary>
		/// The Height parameter
		/// </summary>
		public System.Int32 cy;

		/// <summary>
		/// Creates a new instance of the size structure from two parameters
		/// </summary>
		/// <param name="cx">The Width parameter</param>
		/// <param name="cy">The Height parameter</param>
		public Size(System.Int32 cx, System.Int32 cy) { this.cx= cx; this.cy= cy; }
	}		
}