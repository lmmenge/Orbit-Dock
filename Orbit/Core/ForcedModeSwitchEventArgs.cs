using System;

namespace Orbit.Core
{
	/// <summary>
	/// Represents the arguments for the ForcedModeSwitch event
	/// </summary>
	public class ForcedModeSwitchEventArgs
	{
		ModeSwitchReason _ModeSwitchReason;

		/// <summary>
		/// Creates a new instance of the ForcedModeSwitchEventArgs class
		/// </summary>
		public ForcedModeSwitchEventArgs(ModeSwitchReason modeSwitchReason)
		{
			_ModeSwitchReason=modeSwitchReason;
		}

		/// <summary>
		/// Gets the reason why the mode was changed
		/// </summary>
		public ModeSwitchReason ModeSwitchReason
		{
			get
			{
				return _ModeSwitchReason;
			}
		}
	}
}
