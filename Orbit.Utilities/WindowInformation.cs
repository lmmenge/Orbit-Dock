using System;

namespace Orbit.Utilities
{
	/// <summary>
	/// Contains information on an enumerated window
	/// </summary>
	public class WindowInformation
	{
		private string _Name;
		private IntPtr _Handle;

		/// <summary>
		/// Creates a new instance of the WindowInformation class
		/// </summary>
		/// <param name="name">Name of the window</param>
		/// <param name="handle">Handle to the window</param>
		public WindowInformation(string name, IntPtr handle)
		{
			_Name=name;
			_Handle=handle;
		}


		/// <summary>
		/// Gets the Title bar text for the window
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
		}
		/// <summary>
		/// Gets the handle to the window
		/// </summary>
		public IntPtr Handle
		{
			get
			{
				return _Handle;
			}
		}
	}
}