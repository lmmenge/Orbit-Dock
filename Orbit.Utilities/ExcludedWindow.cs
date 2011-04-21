using System;

namespace Orbit.Utilities
{
	/// <summary>
	/// Information required for an excluded window
	/// </summary>
	public class ExcludedWindow
	{
		private string _ClassName;
		private string _ProcessName;

		/// <summary>
		/// Creates a new instance of the ExcludedWindow class
		/// </summary>
		public ExcludedWindow(){}
		/// <summary>
		/// Creates a new instance of the ExcludedWindow class
		/// </summary>
		/// <param name="className">Class name of the window to be excluded</param>
		/// <param name="processName">Process name of the window to be excluded</param>
		public ExcludedWindow(string className, string processName)
		{
			if(className==null || processName==null)
				throw new ArgumentNullException();

			_ClassName=className;
			_ProcessName=processName;
		}

		
		/// <summary>
		/// Gets/Sets the window's Class name
		/// </summary>
		public string ClassName
		{
			get
			{
				return _ClassName;
			}
			set
			{
				if(_ClassName!=value)
					_ClassName=value;
			}
		}

		/// <summary>
		/// Gets/Sets the window's Process name
		/// </summary>
		public string ProcessName
		{
			get
			{
				return _ProcessName;
			}
			set
			{
				if(_ProcessName!=value)
					_ProcessName=value;
			}
		}
	}
}
