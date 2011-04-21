using System;

namespace Orbit.Configuration
{
	/// <summary>
	/// Global non-static Objects
	/// </summary>
	public sealed class Global
	{
		#region Internal Members
		private static Orbit.Configuration.ConfigurationInfo _Configuration;
		private static Orbit.Language.LanguageLoader _LanguageLoader;
		private static float _Scale=1f;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets the global Configuration settings
		/// </summary>
		public static Orbit.Configuration.ConfigurationInfo Configuration
		{
			get
			{
				return _Configuration;
			}
			set
			{
				if(_Configuration!=value)
					_Configuration=value;
			}
		}
		/// <summary>
		/// Gets/Sets the localization settings
		/// </summary>
		public static Orbit.Language.LanguageLoader LanguageLoader
		{
			get
			{
				return _LanguageLoader;
			}
			set
			{
				if(_LanguageLoader!=value)
					_LanguageLoader=value;
			}
		}
		/// <summary>
		/// Gets/Sets the scaling factor for Orbit
		/// </summary>
		public static float Scale
		{
			get
			{
				return _Scale;
			}
			set
			{
				if(_Scale!=value)
				{
					_Scale=value;
				}
			}
		}
		#endregion
	}
}
