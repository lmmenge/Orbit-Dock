namespace Orbit.Utilities
{
	/// <summary>
	/// Selects the capture mode for the ScreenGrabber class
	/// </summary>
	public enum CaptureMode
	{
		/// <summary>
		/// Uses PrintScreen to capture the screen to the clipboard and then copies it to memory. 
		/// Can be used in threaded capturing (loading Bitmap while screen is already hidden)
		/// </summary>
		Clipboard,
		/// <summary>
		/// Uses BitBlt() to capture the screen to memory directly. 
		/// Can't be used in threaded capturing or else will result in the current screen being captured
		/// </summary>
		BitBlt
	}
}