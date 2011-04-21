namespace Orbit
{
	/// <summary>
	/// program state: (should fade out, just carry normal operation - what the program should do/appear as)
	/// </summary>
	public enum ProgramState
	{
		/// <summary>
		/// Program is about to hide
		/// </summary>
		ToHide,
		/// <summary>
		/// Program is hidden
		/// </summary>
		Hidden,
		/// <summary>
		/// Program is started and has nothing special to do
		/// </summary>
		Nothing,
		/// <summary>
		/// program is fading out
		/// </summary>
		FadeOut
	}
}