using System;

namespace Orbit
{
	/// <summary>
	/// Information regarding the build of Orbit
	/// </summary>
	public class BuildInformation
	{
		/// <summary>
		/// Date in which Orbit was built
		/// </summary>
		public static int BuildDate=080207;
		/// <summary>
		/// Time in which Orbit was built
		/// </summary>
		public static int BuildTime=1909;
		/// <summary>
		/// Full Orbit build number
		/// </summary>
		public static string BuildNumber = "0.4.0.0-"+BuildDate+"-"+BuildTime;
	}
}
