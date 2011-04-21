using System;

namespace Orbit.Core
{
	/// <summary>
	/// Represents the possible reasons why the Direct3DManager class switched modes
	/// </summary>
	public enum ModeSwitchReason
	{
		/// <summary>
		/// Transparent mode is not supported. Attempting to switch to Non-Transparent mode
		/// </summary>
		TransparentModeNotSupported,
		/// <summary>
		/// Transparent mode failed to start. Attempting to switch to Non-Transparent mode
		/// </summary>
		UnexpectedError
	}
}
