using System;

namespace Orbit.Core
{
	/// <summary>
	/// Represents all the possible outcomes of the initialization process of the Direct3DManager class
	/// </summary>
	public enum InitializationResult
	{
		/// <summary>
		/// Initialization was successful
		/// </summary>
		Successful,
		/// <summary>
		/// Initialization failed due to a Direct3D Device creation error
		/// </summary>
		Failed,
		/// <summary>
		/// Initialization failed because the Video card can't cope with requirements to start Orbit
		/// </summary>
		VideoCardNotSupported
	}
}