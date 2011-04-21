using System;

namespace Orbit.Utilities
{
	/// <summary>
	/// Delegate that receives a window when enumerating all windows
	/// </summary>
	public delegate void WindowListedEventHandler(object sender, WindowListedEventArgs e);

	/// <summary>
	/// Argument class for the WindowListedEventHandler delegate
	/// </summary>
	public class WindowListedEventArgs:EventArgs
	{
		private WindowInformation _Wi;

		/// <summary>
		/// Creates a new instance of the WindowListedEventArgs class
		/// </summary>
		/// <param name="name">Name of the window</param>
		/// <param name="handle">Handle to the window</param>
		public WindowListedEventArgs(string name, IntPtr handle)
		{
			_Wi=new WindowInformation(name, handle);
		}

		/// <summary>
		/// Gets the window information
		/// </summary>
		public WindowInformation WindowInformation
		{
			get
			{
				return _Wi;
			}
		}
	}
}