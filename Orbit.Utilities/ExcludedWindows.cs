using System;

namespace Orbit.Utilities
{
	/// <summary>
	/// Summary description for ExcludedTasks.
	/// </summary>
	public class ExcludedWindows
	{
		public ExcludedWindows(){}

		[System.Xml.Serialization.XmlElement("Window")]
		public ExcludedWindow[] WindowList;
	}
}
