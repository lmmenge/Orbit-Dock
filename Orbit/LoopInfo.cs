namespace Orbit
{
	/// <summary>
	/// Definition of a loop information object
	/// </summary>
	public struct LoopInfo
	{
		/// <summary>
		/// Index of the first item in this loop that is shown
		/// </summary>
		public int StartIndex;
		/// <summary>
		/// Amount of degrees that this loop is rotated in relation to the origin
		/// </summary>
		public int RotatedDegrees;
		/// <summary>
		/// Indicates if this loop has a Scroll Up/Down indicator
		/// </summary>
		public bool ShowsMoreIndicator;
		/// <summary>
		/// Precalculated radius of this Loop
		/// </summary>
		public double AbsoluteRadius;
		/// <summary>
		/// Precaulculates radius of this loop relative to the first loop
		/// </summary>
		public double RelativeRadius;
		/// <summary>
		/// Maximum amount of items that can be visible at one time
		/// </summary>
		public int MaxVisibleItems;
		/// <summary>
		/// Amount of items that are, by rule, visible at one time
		/// </summary>
		public int VisibleItems;
		/// <summary>
		/// Amount if items
		/// </summary>
		public int Count;
		/// <summary>
		/// The ThumbnailSync object for this loop
		/// </summary>
		public Orbit.Core.ThumbnailSync TS;
		/// <summary>
		/// Path to this level on the hard disk
		/// </summary>
		public string LevelPath;
	}

}