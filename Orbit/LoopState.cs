namespace Orbit
{
	/// <summary>
	/// directx drawing states: (should draw, animate - what directx is doing)
	/// </summary>
	public enum LoopState
	{
		/// <summary>
		/// The Program Start State. to restart the program set the state to this
		/// </summary>
		Nothing,
		/// <summary>
		/// Checking keys and veritication times
		/// </summary>
		Checking,
		/// <summary>
		/// Loading state. will display a loading message onscreen.
		/// </summary>
		Loading,
		/// <summary>
		/// Main loop. Drawing and setting up a frame of the scene and presenting it to the screen
		/// </summary>
		Loop,
		/// <summary>
		/// Setting up a frame.
		/// </summary>
		AnimatingFrame,
		/// <summary>
		/// Rendering a frame.
		/// </summary>
		DrawingFrame,
		/// <summary>
		/// Configuration Dialog is open.
		/// </summary>
		Configuring,
		/// <summary>
		/// Direct3D hasn't been initialized yet.
		/// </summary>
		NotInit
	}

}