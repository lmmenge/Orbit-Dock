using System;

namespace Orbit.Core
{
	/// <summary>
	/// Describes the possible feature sets of Orbit that should be available
	/// </summary>
	public class FeatureSets
	{
		/// <summary>
		/// Indicates if Orbit should use High Quality rendering for labels
		/// </summary>
		public static bool UseHQLabels=true;
		/// <summary>
		/// Indicates if Orbit should use High Quality rendering for previews. 
		/// This enables a transition animation when items acquire their previews
		/// </summary>
		public static bool UseHQPreviews=true;
		/// <summary>
		/// Hides Minimized windows in the task manager
		/// </summary>
		public static bool HideMinimizedWindows=false;
	}
}
