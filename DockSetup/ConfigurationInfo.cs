using System;
using System.Xml.Serialization;

namespace Orbit.Configuration
{
	/// <summary>
	/// Encapsulates Orbit's configuration
	/// </summary>
	public sealed class ConfigurationInfo
	{
		#region Configuration Categories Classes
		public class RuntimeConfig
		{
			#region Private Members
			private int _IconSizeAverage;
			private string _CurrentLevel;
			private bool _AllowLabels;
			private int _MaxItemsShownInFirstLoop;
			private int _LoopDistance;
			private int _TransparentBlitFix;
			private BackgroundStretchMode _BgStretch;
			private byte _CurrentIconBgAlpha;
			private bool _CanPop;
			private byte _LastFrameSpeed;
			#endregion

			#region Constructors
			public RuntimeConfig()
			{
				InitializeComponent();
			}
			private void InitializeComponent()
			{
				_MaxItemsShownInFirstLoop=15;
				_CanPop=true;
			}
			#endregion
            
			#region Properties
			public int IconSizeAverage
			{
				get
				{
					return _IconSizeAverage;
				}
				set
				{
					if(_IconSizeAverage!=value)
						_IconSizeAverage=value;
				}
			}
			public string CurrentLevel
			{
				get
				{
					return _CurrentLevel;
				}
				set
				{
					if(_CurrentLevel!=value)
						_CurrentLevel=value;
				}
			}
			public bool AllowLabels
			{
				get
				{
					return _AllowLabels;
				}
				set
				{
					if(_AllowLabels!=value)
						_AllowLabels=value;
				}
			}
			
			public int MaxItemsShownInFirstLoop
			{
				get
				{
					return _MaxItemsShownInFirstLoop;
				}
				set
				{
					if(_MaxItemsShownInFirstLoop!=value)
						_MaxItemsShownInFirstLoop=value;
				}
			}
			
			public int LoopDistance
			{
				get
				{
					return _LoopDistance;
				}
				set
				{
					if(_LoopDistance!=value)
						_LoopDistance=value;
				}
			}
			
			public int TransparentBlitFix
			{
				get
				{
					return _TransparentBlitFix;
				}
				set
				{
					if(_TransparentBlitFix!=value)
						_TransparentBlitFix=value;
				}
			}
			
			public BackgroundStretchMode BgStretch
			{
				get
				{
					return _BgStretch;
				}
				set
				{
					if(_BgStretch!=value)
						_BgStretch=value;
				}
			}
			
			public byte CurrentIconBgAlpha
			{
				get
				{
					return _CurrentIconBgAlpha;
				}
				set
				{
					if(_CurrentIconBgAlpha!=value)
						_CurrentIconBgAlpha=value;
				}
			}
			
			public bool CanPop
			{
				get
				{
					return _CanPop;
				}
				set
				{
					if(_CanPop!=value)
						_CanPop=value;
				}
			}
			public byte LastFrameSpeed
			{
				get
				{
					return _LastFrameSpeed;
				}
				set
				{
					_LastFrameSpeed=value;
				}
			}
			#endregion
		}
		public class LocationsConfig
		{
			#region Private Members
			private string _ItemsPath;
			private string _ImagesPath;
			private string _OrbitDataPath;
			#endregion

			#region Constructor
			public LocationsConfig()
			{
				InitializeComponent();
			}
			private void InitializeComponent()
			{
				UpdateRuntimeValues();
				_ItemsPath=System.IO.Path.Combine(_OrbitDataPath, "myitems\\");
				_ImagesPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "images");
			}
			#endregion

			public void UpdateRuntimeValues()
			{
				_OrbitDataPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), @"profiles\"+System.Environment.UserName);
			}

			#region Methods
			public static string GetExcludedTasksFilePath()
			{
				return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), @"profiles\"+System.Environment.UserName+@"\ExcludedTasks.xml");
			}
			public static string GetApplicationPath()
			{
				//return System.Windows.Forms.Application.ExecutablePath.Substring(0, System.Windows.Forms.Application.ExecutablePath.Length-(System.Windows.Forms.Application.ExecutablePath.Length-System.Windows.Forms.Application.ExecutablePath.LastIndexOf("\\")))+"\\";
				return System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath)+"\\";
			}
			#endregion

			#region Properties
			public string ItemsPath
			{
				get
				{
					return _ItemsPath;
				}
				set
				{
					if(_ItemsPath!=value)
						_ItemsPath=value;
				}
			}
			public string ImagesPath
			{
				get
				{
					return _ImagesPath;
				}
				set
				{
					if(_ImagesPath!=value)
						_ImagesPath=value;
				}
			}
			public string OrbitDataPath
			{
				get
				{
					return _OrbitDataPath;
				}
				set
				{
					if(_OrbitDataPath!=value)
						_OrbitDataPath=value;
				}
			}
			#endregion
		}
		public class ImagesConfig
		{
			#region Private Members
			private string _BackgroundImagePath;
			private string _IconBackgroundImagePath;
			private string _IconSelectedImagePath;
			private System.Drawing.Color _BackgroundColor;
			private string _ScrollUpImagePath;
			private string _ScrollDownImagePath;
			private bool _UseWindowsWallpaper;

			// obsolete properties
			private System.Drawing.Size _BackgroundSize;
			private System.Drawing.Size _IconBackgroundSize;
			#endregion

			#region Constructor
			public ImagesConfig()
			{
				InitializeComponent();
			}
			private void InitializeComponent()
			{
				string App=System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
				_BackgroundImagePath="";
				_IconBackgroundImagePath=System.IO.Path.Combine(App, @"background\ring.png");
				_IconSelectedImagePath=System.IO.Path.Combine(App, @"background\select.png");
				_ScrollUpImagePath=System.IO.Path.Combine(App, @"background\up.png");
				_ScrollDownImagePath=System.IO.Path.Combine(App, @"background\down.png");
				_UseWindowsWallpaper=true;
				_BackgroundColor=System.Drawing.Color.FromArgb(0x00, System.Drawing.Color.Black);
			}
			#endregion

			#region Properties
			public string BackgroundImagePath
			{
				get
				{
					return _BackgroundImagePath;
				}
				set
				{
					if(_BackgroundImagePath!=value)
						_BackgroundImagePath=value;
				}
			}
			public string IconBackgroundImagePath
			{
				get
				{
					return _IconBackgroundImagePath;
				}
				set
				{
					if(_IconBackgroundImagePath!=value)
						_IconBackgroundImagePath=value;
				}
			}
			public string IconSelectedImagePath
			{
				get
				{
					return _IconSelectedImagePath;
				}
				set
				{
					if(_IconSelectedImagePath!=value)
						_IconSelectedImagePath=value;
				}
			}
			public string ScrollUpImagePath
			{
				get
				{
					return _ScrollUpImagePath;
				}
				set
				{
					if(_ScrollUpImagePath!=value)
						_ScrollUpImagePath=value;
				}
			}
			public string ScrollDownImagePath
			{
				get
				{
					return _ScrollDownImagePath;
				}
				set
				{
					if(_ScrollDownImagePath!=value)
						_ScrollDownImagePath=value;
				}
			}
			public bool UseWindowsWallpaper
			{
				get
				{
					return _UseWindowsWallpaper;
				}
				set
				{
					if(_UseWindowsWallpaper!=value)
						_UseWindowsWallpaper=value;
				}
			}
			public System.Drawing.Color BackgroundColor
			{
				get
				{
					return _BackgroundColor;
				}
				set
				{
					if(_BackgroundColor!=value)
						_BackgroundColor=value;
				}
			}
			[XmlIgnore]
			public CustomColor BgColor
			{
				get
				{
					return new CustomColor(_BackgroundColor.R, _BackgroundColor.G, _BackgroundColor.B);
				}
				set
				{
					_BackgroundColor=System.Drawing.Color.FromArgb(value.R, value.G, value.B);
				}
			}
			public System.Drawing.Size BackgroundSize
			{
				get
				{
					return _BackgroundSize;
				}
				set
				{
					if(_BackgroundSize!=value)
						_BackgroundSize=value;
				}
			}
			public System.Drawing.Size IconBackgroundSize
			{
				get
				{
					return _IconBackgroundSize;
				}
				set
				{
					if(_IconBackgroundSize!=value)
						_IconBackgroundSize=value;
				}
			}
			#endregion
		}
		public class FontsConfig
		{
			#region Private Members
			private System.Drawing.Color _LabelColor;
			private System.Drawing.Color _LabelBorderColor;
			private System.Drawing.Color _DescriptionColor;
			private System.Drawing.Color _DescriptionBorderColor;
			private bool _ShowLabelBorder;
			private bool _ShowDescriptionBorder;
			private string _LabelFont;
			private string _DescriptionFont;
			private float _LabelSize;
			private float _DescriptionSize;
			private System.Drawing.FontStyle _LabelStyle;
			private System.Drawing.FontStyle _DescriptionStyle;
			#endregion

			#region Constructor
			public FontsConfig()
			{
				InitializeComponent();
			}
			private void InitializeComponent()
			{
				_LabelColor=System.Drawing.Color.White;
				_LabelBorderColor=System.Drawing.Color.Black;
				_DescriptionColor=System.Drawing.Color.FromArgb(217,223,234);
				_DescriptionBorderColor=System.Drawing.Color.Black;
				_ShowLabelBorder=true;
				_ShowDescriptionBorder=true;
				_LabelFont="trebuchet ms";
				_DescriptionFont="trebuchet ms";
				_LabelSize=22;
				_DescriptionSize=14;
				_LabelStyle=System.Drawing.FontStyle.Bold;
				_DescriptionStyle=System.Drawing.FontStyle.Bold;
			}
			#endregion

			#region Properties
			[XmlIgnore]
			public System.Drawing.Color LabelColor
			{
				get
				{
					return _LabelColor;
				}
				set
				{
					if(_LabelColor!=value)
						_LabelColor=value;
				}
			}
			[XmlIgnore]
			public System.Drawing.Color LabelBorderColor
			{
				get
				{
					return _LabelBorderColor;
				}
				set
				{
					if(_LabelBorderColor!=value)
						_LabelBorderColor=value;
				}
			}
			[XmlIgnore]
			public System.Drawing.Color DescriptionColor
			{
				get
				{
					return _DescriptionColor;
				}
				set
				{
					if(_DescriptionColor!=value)
						_DescriptionColor=value;
				}
			}
			[XmlIgnore]
			public System.Drawing.Color DescriptionBorderColor
			{
				get
				{
					return _DescriptionBorderColor;
				}
				set
				{
					if(_DescriptionBorderColor!=value)
						_DescriptionBorderColor=value;
				}
			}
			public CustomColor LabelFontColor
			{
				get
				{
					return new CustomColor(_LabelColor.R, _LabelColor.G, _LabelColor.B);
				}
				set
				{
					_LabelColor=System.Drawing.Color.FromArgb(value.R, value.G, value.B);
				}
			}
			public CustomColor LabelBorderFontColor
			{
				get
				{
					return new CustomColor(_LabelBorderColor.R, _LabelBorderColor.G, _LabelBorderColor.B);
				}
				set
				{
					_LabelBorderColor=System.Drawing.Color.FromArgb(value.R, value.G, value.B);
				}
			}
			public CustomColor DescriptionFontColor
			{
				get
				{
					return new CustomColor(_DescriptionColor.R, _DescriptionColor.G, _DescriptionColor.B);
				}
				set
				{
					_DescriptionColor=System.Drawing.Color.FromArgb(value.R, value.G, value.B);
				}
			}
			public CustomColor DescriptionBorderFontColor
			{
				get
				{
					return new CustomColor(_DescriptionBorderColor.R, _DescriptionBorderColor.G, _DescriptionBorderColor.B);
				}
				set
				{
					_DescriptionBorderColor=System.Drawing.Color.FromArgb(value.R, value.G, value.B);
				}
			}
			public bool ShowLabelBorder
			{
				get
				{
					return _ShowLabelBorder;
				}
				set
				{
					if(_ShowLabelBorder!=value)
						_ShowLabelBorder=value;
				}
			}
			public bool ShowDescriptionBorder
			{
				get
				{
					return _ShowDescriptionBorder;
				}
				set
				{
					if(_ShowDescriptionBorder!=value)
						_ShowDescriptionBorder=value;
				}
			}
			public string LabelFont
			{
				get
				{
					return _LabelFont;
				}
				set
				{
					if(_LabelFont!=value)
						_LabelFont=value;
				}
			}
			public string DescriptionFont
			{
				get
				{
					return _DescriptionFont;
				}
				set
				{
					if(_DescriptionFont!=value)
						_DescriptionFont=value;
				}
			}
			public float LabelSize
			{
				get
				{
					return _LabelSize;
				}
				set
				{
					if(_LabelSize!=value)
						_LabelSize=value;
				}
			}
			public float DescriptionSize
			{
				get
				{
					return _DescriptionSize;
				}
				set
				{
					if(_DescriptionSize!=value)
						_DescriptionSize=value;
				}
			}
			public System.Drawing.FontStyle LabelStyle
			{
				get
				{
					return _LabelStyle;
				}
				set
				{
					if(_LabelStyle!=value)
						_LabelStyle=value;
				}
			}
			public System.Drawing.FontStyle DescriptionStyle
			{
				get
				{
					return _DescriptionStyle;
				}
				set
				{
					if(_DescriptionStyle!=value)
						_DescriptionStyle=value;
				}
			}
			#endregion
		}
		public class AppearanceConfig
		{
			#region Private Members
			private int _IconMinifiedSize;
			private int _IconMagnifiedSize;
			private TransparencyMode _Transparency;
			private bool _ShowLabels;
			private byte _IconAlpha;
			private byte _IconBackgroundAlpha;
			private byte _AnimationSpeed;
			private int _ItemsShownPerLine;
			private int _MouseWheelSensitivity;
			private bool _ShowImageThumbnails;
			private bool _GroupIcons;

			// obsolete properties
			//private Microsoft.DirectX.Direct3D.Format _CompressionFormat;
			//private bool _UseMultipleWindows;
			#endregion

			#region Constructor
			public AppearanceConfig()
			{
				InitializeComponent();
			}
			private void InitializeComponent()
			{
				_IconMinifiedSize=32;
				_IconMagnifiedSize=128;
				_Transparency=TransparencyMode.Fake;
				_ShowLabels=true;
				_IconAlpha=150;
				_IconBackgroundAlpha=180;
				_AnimationSpeed=13;
				_ItemsShownPerLine=0;
				_MouseWheelSensitivity=8;
				_ShowImageThumbnails=true;
				_GroupIcons=true;
			}
			#endregion

			#region Properties
			public int IconMinifiedSize
			{
				get
				{
					//return _IconMinifiedSize*Global.Scale;
					return _IconMinifiedSize;
				}
				set
				{
					if(_IconMinifiedSize!=value)
						_IconMinifiedSize=value;
				}
			}
			public int IconMagnifiedSize
			{
				get
				{
					//return _IconMagnifiedSize*Global.Scale;
					return _IconMagnifiedSize;
				}
				set
				{
					if(_IconMagnifiedSize!=value)
						_IconMagnifiedSize=value;
				}
			}

			public TransparencyMode Transparency
			{
				get
				{
					return _Transparency;
				}
				set
				{
					if(_Transparency!=value)
						_Transparency=value;
				}
			}
			public bool ShowLabels
			{
				get
				{
					return _ShowLabels;
				}
				set
				{
					if(_ShowLabels!=value)
						_ShowLabels=value;
				}
			}

			public byte IconAlpha
			{
				get
				{
					return _IconAlpha;
				}
				set
				{
					if(_IconAlpha!=value)
						_IconAlpha=value;
				}
			}
			public byte IconBackgroundAlpha
			{
				get
				{
					return _IconBackgroundAlpha;
				}
				set
				{
					if(_IconBackgroundAlpha!=value)
						_IconBackgroundAlpha=value;
				}
			}
			public byte AnimationSpeed
			{
				get
				{
					return _AnimationSpeed;
				}
				set
				{
					if(_AnimationSpeed!=value)
						_AnimationSpeed=value;
				}
			}

			public int ItemsShownPerLine
			{
				get
				{
					return _ItemsShownPerLine;
				}
				set
				{
					if(_ItemsShownPerLine!=value)
						_ItemsShownPerLine=value;
				}
			}

			public int MouseWheelSensitivity
			{
				get
				{
					return _MouseWheelSensitivity;
				}
				set
				{
					if(_MouseWheelSensitivity!=value)
						_MouseWheelSensitivity=value;
				}
			}

			public bool ShowImageThumbnails
			{
				get
				{
					return _ShowImageThumbnails;
				}
				set
				{
					if(_ShowImageThumbnails!=value)
						_ShowImageThumbnails=value;
				}
			}

			public bool GroupIcons
			{
				get
				{
					return _GroupIcons;
				}
				set
				{
					if(_GroupIcons!=value)
						_GroupIcons=value;
				}
			}

			#endregion			
		}
		public class BehaviorConfig
		{
			#region Private Members
			private bool _FolderPopOnHover;
			private System.Windows.Forms.Keys _PopKey;
			private bool _AlwaysOpenInStart;
			private bool _TransitionSpin;
			private bool _TransitionZoom;
			private bool _TransitionSlide;
			private bool _ShowTasksOnly;
			private bool _AlwaysPopInCenter;
			private bool _ForceNoPreviewAnimation;
			private bool _ForceLowQualityLabels;
			#endregion

			#region Constructor
			public BehaviorConfig()
			{
				InitializeComponent();
			}
			private void InitializeComponent()
			{
				_FolderPopOnHover=false;
				_PopKey=System.Windows.Forms.Keys.MButton;
				_AlwaysOpenInStart=false;
				_TransitionSpin=false;
				_TransitionZoom=true;
				_TransitionSlide=true;
				_ShowTasksOnly=false;
				_AlwaysPopInCenter=false;
				_ForceNoPreviewAnimation=false;
				_ForceLowQualityLabels=false;
			}
			#endregion

			#region Properties
			public bool FolderPopOnHover
			{
				get
				{
					return _FolderPopOnHover;
				}
				set
				{
					if(_FolderPopOnHover!=value)
						_FolderPopOnHover=value;
				}
			}
			public System.Windows.Forms.Keys PopKey
			{
				get
				{
					return _PopKey;
				}
				set
				{
					if(_PopKey!=value)
						_PopKey=value;
				}
			}
			public bool AlwaysOpenInStart
			{
				get
				{
					return _AlwaysOpenInStart;
				}
				set
				{
					if(_AlwaysOpenInStart!=value)
						_AlwaysOpenInStart=value;
				}
			}
			public bool TransitionSpin
			{
				get
				{
					return _TransitionSpin;
				}
				set
				{
					if(_TransitionSpin!=value)
						_TransitionSpin=value;
				}
			}
			public bool TransitionZoom
			{
				get
				{
					return _TransitionZoom;
				}
				set
				{
					if(_TransitionZoom!=value)
						_TransitionZoom=value;
				}
			}
			public bool TransitionSlide
			{
				get
				{
					return _TransitionSlide;
				}
				set
				{
					if(_TransitionSlide!=value)
						_TransitionSlide=value;
				}
			}
			public bool ShowTasksOnly
			{
				get
				{
					return _ShowTasksOnly;
				}
				set
				{
					if(_ShowTasksOnly!=value)
						_ShowTasksOnly=value;
				}
			}
			public bool AlwaysPopInCenter
			{
				get
				{
					return _AlwaysPopInCenter;
				}
				set
				{
					if(_AlwaysPopInCenter!=value)
						_AlwaysPopInCenter=value;
				}
			}
			public bool ForceNoPreviewAnimation
			{
				get
				{
					return _ForceNoPreviewAnimation;
				}
				set
				{
					if(_ForceNoPreviewAnimation!=value)
					{
						_ForceNoPreviewAnimation=value;
					}
				}
			}
			public bool ForceLowQualityLabels
			{
				get
				{
					return _ForceLowQualityLabels;
				}
				set
				{
					if(_ForceLowQualityLabels!=value)
					{
						_ForceLowQualityLabels=value;
					}
				}
			}
			#endregion
		}

		#endregion

		#region Private Members
		private LocationsConfig _Locations;
		private ImagesConfig _Images;
		private FontsConfig _Fonts;
		private AppearanceConfig _Appearance;
		private BehaviorConfig _Behavior;
		private RuntimeConfig _Runtime;
		#endregion

		#region Constructors
		public ConfigurationInfo()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			// instancing
			Locations=new LocationsConfig();
			Images=new ImagesConfig();
			Fonts=new FontsConfig();
			Appearance=new AppearanceConfig();
			Behavior=new BehaviorConfig();
			Runtime=new RuntimeConfig();

			UpdateRuntimeValues();
		}

		public static ConfigurationInfo FromXML()
		{
			// create a default config
			ConfigurationInfo conf=new ConfigurationInfo();
			// find out where the config file should be
			string ConfigPath=System.IO.Path.Combine(conf.Locations.OrbitDataPath, "config.xml");
			// throw exception if file doesn't exist
			if(!System.IO.File.Exists(ConfigPath))
			{
				conf=FromINI();
				return conf;
			}

			// deserializer
			System.Xml.Serialization.XmlSerializer xs=new System.Xml.Serialization.XmlSerializer(typeof(ConfigurationInfo));
			// open XML
			System.IO.StreamReader sr=new System.IO.StreamReader(ConfigPath);
			// deserialize
			conf=(ConfigurationInfo)xs.Deserialize(sr);
			// close XML
			sr.Close();

			conf.UpdateRuntimeValues();
			// return new config
			return conf;
		}
		public static ConfigurationInfo FromINI()
		{
			// some globals
			string ApplicationPath=System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
			// create a default config
			ConfigurationInfo conf=new ConfigurationInfo();
			// find out where the config file should be
			string ConfigPath=System.IO.Path.Combine(conf.Locations.OrbitDataPath, "config.ini");
			// throw exception if file doesn't exist
			if(!System.IO.File.Exists(ConfigPath))
			{
				//throw new System.IO.FileNotFoundException("The configuration file did not exist.", ConfigPath);
				return conf;
			}
			conf.Appearance.Transparency=TransparencyMode.None;

			// opening configuration file if it exists
			System.IO.StreamReader iFile=new System.IO.StreamReader(ConfigPath);
			while(iFile.Peek()>=0)
			{
				string[] Param=iFile.ReadLine().Split(char.Parse("="));
				string[] ColorVals;
				switch(Param[0].ToLower())
				{
					case "iconmagsize":
						conf.Appearance.IconMagnifiedSize=int.Parse(Param[1]);
						break;
					case "iconminsize":
						conf.Appearance.IconMinifiedSize=int.Parse(Param[1]);
						break;
					case "popkey":
						conf.Behavior.PopKey=(System.Windows.Forms.Keys)int.Parse(Param[1]);
						break;
					case "usepopkey":
						bool Use=bool.Parse(Param[1]);
						if(!Use)
							conf.Behavior.PopKey=System.Windows.Forms.Keys.None;
						break;
					case "itemspath":
						if(System.IO.Directory.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							//conf.ItemsPath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						} 
						else 
						{
							//conf.ItemsPath=ApplicationPath+"items\\";
						}
						break;
					case "imagespath":
						if(System.IO.Directory.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							conf.Locations.ImagesPath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						}
						else
						{
							conf.Locations.ImagesPath=ApplicationPath+"images\\";
						}
						break;
					case "bgimage":
						if(System.IO.File.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							conf.Images.BackgroundImagePath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						} 
						else {conf.Images.BackgroundImagePath="";}
						break;
					case "iconbg":
						if(System.IO.File.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							conf.Images.IconBackgroundImagePath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						}
						else {conf.Images.IconBackgroundImagePath="";}
						break;
					case "iconselected":
						if(System.IO.File.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							conf.Images.IconSelectedImagePath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						}
						else {conf.Images.IconSelectedImagePath="";}
						break;
					case "usetransparency":
						bool UseReal=bool.Parse(Param[1]);
						if(UseReal && conf.Appearance.Transparency==TransparencyMode.None)
							conf.Appearance.Transparency=TransparencyMode.Real;
						break;
					case "showlabels":
						conf.Appearance.ShowLabels=bool.Parse(Param[1]);
						break;
					case "iconalpha":
						conf.Appearance.IconAlpha=byte.Parse(Param[1]);
						break;
					case "bgalpha":
						conf.Appearance.IconBackgroundAlpha=byte.Parse(Param[1]);
						break;
					case "folderpoponhover":
						conf.Behavior.FolderPopOnHover=bool.Parse(Param[1]);
						break;
					case "bgimagex":
						conf.Images.BackgroundSize=new System.Drawing.Size(int.Parse(Param[1]), conf.Images.BackgroundSize.Height);
						break;
					case "bgimagey":
						conf.Images.BackgroundSize=new System.Drawing.Size(conf.Images.BackgroundSize.Width, int.Parse(Param[1]));
						break;
					case "iconbgx":
						conf.Images.IconBackgroundSize=new System.Drawing.Size(int.Parse(Param[1]), conf.Images.IconBackgroundSize.Height);
						break;
					case "iconbgy":
						conf.Images.IconBackgroundSize=new System.Drawing.Size(conf.Images.IconBackgroundSize.Width, int.Parse(Param[1]));
						break;
					case "blendrate":
						conf.Appearance.AnimationSpeed=byte.Parse(Param[1]);
						break;
					case "itemsshowninfirstloop":
						conf.Runtime.MaxItemsShownInFirstLoop=int.Parse(Param[1]);
						break;
					case "itemsshownperline":
						conf.Appearance.ItemsShownPerLine=int.Parse(Param[1]);
						break;
					case "loopdistance":
						conf.Runtime.LoopDistance=int.Parse(Param[1]);
						break;
					case "openinstart":
						conf.Behavior.AlwaysOpenInStart=bool.Parse(Param[1]);
						break;
					case "spinsensitivity":
						conf.Appearance.MouseWheelSensitivity=int.Parse(Param[1]);
						break;
					case "slideout":
						conf.Behavior.TransitionSlide=bool.Parse(Param[1]);
						break;
					case "zoomout":
						conf.Behavior.TransitionZoom=bool.Parse(Param[1]);
						break;
					case "spinout":
						conf.Behavior.TransitionSpin=bool.Parse(Param[1]);
						break;
					case "bmpfix":
						conf.Runtime.TransparentBlitFix=int.Parse(Param[1]);
						break;
					case "bgcolor":
						ColorVals=Param[1].Split(';');
						conf.Images.BackgroundColor=System.Drawing.Color.FromArgb(0x00, int.Parse(ColorVals[0]), int.Parse(ColorVals[1]), int.Parse(ColorVals[2]));
						break;
					case "labelcolor":
						ColorVals=Param[1].Split(';');
						conf.Fonts.LabelColor=System.Drawing.Color.FromArgb(0xFF, int.Parse(ColorVals[0]), int.Parse(ColorVals[1]), int.Parse(ColorVals[2]));
						break;
					case "labelbordercolor":
						ColorVals=Param[1].Split(';');
						conf.Fonts.LabelBorderColor=System.Drawing.Color.FromArgb(0xFF, int.Parse(ColorVals[0]), int.Parse(ColorVals[1]), int.Parse(ColorVals[2]));
						break;
					case "descriptioncolor":
						ColorVals=Param[1].Split(';');
						conf.Fonts.DescriptionColor=System.Drawing.Color.FromArgb(0xFF, int.Parse(ColorVals[0]), int.Parse(ColorVals[1]), int.Parse(ColorVals[2]));
						break;
					case "descriptionbordercolor":
						ColorVals=Param[1].Split(';');
						conf.Fonts.DescriptionBorderColor=System.Drawing.Color.FromArgb(0xFF, int.Parse(ColorVals[0]), int.Parse(ColorVals[1]), int.Parse(ColorVals[2]));
						break;
					case "showthumbnails":
						conf.Appearance.ShowImageThumbnails=bool.Parse(Param[1]);
						break;
					case "labelsize":
						conf.Fonts.LabelSize=float.Parse(Param[1]);
						break;
					case "descriptionsize":
						conf.Fonts.DescriptionSize=float.Parse(Param[1]);
						break;
					case "descriptionfont":
						conf.Fonts.DescriptionFont=Param[1];
						break;
					case "labelfont":
						conf.Fonts.LabelFont=Param[1];
						break;
					case "labelstyle":
					switch(Param[1].ToLower())
					{
						case "regular":
							conf.Fonts.LabelStyle=System.Drawing.FontStyle.Regular;
							break;
						case "bold":
							conf.Fonts.LabelStyle=System.Drawing.FontStyle.Bold;
							break;
						case "italic":
							conf.Fonts.LabelStyle=System.Drawing.FontStyle.Italic;
							break;
						case "underline":
							conf.Fonts.LabelStyle=System.Drawing.FontStyle.Underline;
							break;
					}
						break;
					case "descriptionstyle":
					switch(Param[1].ToLower())
					{
						case "regular":
							conf.Fonts.DescriptionStyle=System.Drawing.FontStyle.Regular;
							break;
						case "bold":
							conf.Fonts.DescriptionStyle=System.Drawing.FontStyle.Bold;
							break;
						case "italic":
							conf.Fonts.DescriptionStyle=System.Drawing.FontStyle.Italic;
							break;
						case "underline":
							conf.Fonts.DescriptionStyle=System.Drawing.FontStyle.Underline;
							break;
					}
						break;
					case "labelborder":
						conf.Fonts.ShowLabelBorder=bool.Parse(Param[1]);
						break;
					case "descriptionborder":
						conf.Fonts.ShowDescriptionBorder=bool.Parse(Param[1]);
						break;
					case "groupicons":
						conf.Appearance.GroupIcons=bool.Parse(Param[1]);
						break;
					case "scrollupimage":
						if(System.IO.File.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							conf.Images.ScrollUpImagePath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						} 
						else {conf.Images.ScrollUpImagePath="";}
						break;
					case "scrolldownimage":
						if(System.IO.File.Exists(Param[1].Replace("[AppPath]\\", ApplicationPath)))
						{
							conf.Images.ScrollDownImagePath=Param[1].Replace("[AppPath]\\", ApplicationPath);
						} 
						else {conf.Images.ScrollDownImagePath="";}
						break;
					case "capturebackground":
						bool UseFake=bool.Parse(Param[1]);
						if(UseFake && conf.Appearance.Transparency==TransparencyMode.None)
							conf.Appearance.Transparency=TransparencyMode.Fake;
						break;
					case "usewindowswallpaper":
						conf.Images.UseWindowsWallpaper=bool.Parse(Param[1]);
						break;
					case "showtasksonly":
						conf.Behavior.ShowTasksOnly=bool.Parse(Param[1]);
						break;
					case "hideminimizedtasks":
						//conf.Behavior.HideMinimizedTasks=bool.Parse(Param[1]);
						Orbit.Utilities.WindowsTaskManager.HideMinimizedWindows=bool.Parse(Param[1]);
						break;
					case "alwayspopincenter":
						conf.Behavior.AlwaysPopInCenter=bool.Parse(Param[1]);
						break;
					case "forcenopreviewanimation":
						conf.Behavior.ForceNoPreviewAnimation=bool.Parse(Param[1]);
						break;
					case "forcelowqualitylabels":
						conf.Behavior.ForceLowQualityLabels=bool.Parse(Param[1]);
						break;
				}
			}
			iFile.Close();

			conf.UpdateRuntimeValues();
			return conf;
		}
		#endregion

		#region Public Functions
		public void SaveToXML()
		{
			// find out where the config file should be
			string ConfigPath=System.IO.Path.Combine(this.Locations.OrbitDataPath, "config.xml");
			// throw exception if file doesn't exist
//			if(!System.IO.File.Exists(ConfigPath))
//				throw new System.IO.FileNotFoundException("The configuration file did not exist.", ConfigPath);

			// deserializer
			System.Xml.Serialization.XmlSerializer xs=new System.Xml.Serialization.XmlSerializer(typeof(ConfigurationInfo));
			// open XML
			System.IO.StreamWriter sw=new System.IO.StreamWriter(ConfigPath);
			// deserialize
			xs.Serialize(sw, this);
			// close XML
			sw.Close();
		}
		public void SaveToINI()
		{
			System.IO.StreamWriter iFile=new System.IO.StreamWriter(System.IO.Path.Combine(this.Locations.OrbitDataPath, "config.ini"));
			iFile.WriteLine("## THIS FILE IS CREATED BY ORBIT. ##");
			iFile.WriteLine("## DO NOT EDIT THIS FILE (ONLY IF YOU REALLY KNOW WHAT YOU'RE DOING) ##");
			iFile.WriteLine("");

			iFile.WriteLine("IconMinSize="+this.Appearance.IconMinifiedSize);
			iFile.WriteLine("IconMagSize="+this.Appearance.IconMagnifiedSize);
			iFile.WriteLine("PopKey="+(int)this.Behavior.PopKey);
			iFile.WriteLine("UsePopKey="+(this.Behavior.PopKey!=System.Windows.Forms.Keys.None));
			iFile.WriteLine("IconAlpha="+this.Appearance.IconAlpha);

			iFile.WriteLine("");

			iFile.WriteLine("FolderPopOnHover="+this.Behavior.FolderPopOnHover);
			iFile.WriteLine("UseTransparency="+(this.Appearance.Transparency==TransparencyMode.Real));
			iFile.WriteLine("ShowLabels="+this.Appearance.ShowLabels);
			iFile.WriteLine("OpenInStart="+this.Behavior.AlwaysOpenInStart);
			iFile.WriteLine("BlendRate="+this.Appearance.AnimationSpeed);
			iFile.WriteLine("SpinSensitivity="+this.Appearance.MouseWheelSensitivity);
			iFile.WriteLine("SlideOut="+this.Behavior.TransitionSlide);
			iFile.WriteLine("ZoomOut="+this.Behavior.TransitionZoom);
			iFile.WriteLine("SpinOut="+this.Behavior.TransitionSpin);
			iFile.WriteLine("ShowThumbnails="+this.Appearance.ShowImageThumbnails);
			iFile.WriteLine("GroupIcons="+this.Appearance.GroupIcons);
			iFile.WriteLine("CaptureBackground="+(this.Appearance.Transparency==TransparencyMode.Fake));
			iFile.WriteLine("ItemsShownPerLine="+this.Appearance.ItemsShownPerLine);
			iFile.WriteLine("UseWindowsWallpaper="+this.Images.UseWindowsWallpaper);

			iFile.WriteLine("");

			iFile.WriteLine("LabelColor="+this.Fonts.LabelColor.R+";"+this.Fonts.LabelColor.G+";"+this.Fonts.LabelColor.B);
			iFile.WriteLine("LabelBorderColor="+this.Fonts.LabelBorderColor.R+";"+this.Fonts.LabelBorderColor.G+";"+this.Fonts.LabelBorderColor.B);
			iFile.WriteLine("LabelFont="+this.Fonts.LabelFont);
			iFile.WriteLine("LabelSize="+this.Fonts.LabelSize);
			iFile.WriteLine("LabelStyle="+this.Fonts.LabelStyle);
			iFile.WriteLine("LabelBorder="+this.Fonts.ShowLabelBorder);
			iFile.WriteLine("DescriptionBorderColor="+this.Fonts.DescriptionBorderColor.R+";"+this.Fonts.DescriptionBorderColor.G+";"+this.Fonts.DescriptionBorderColor.B);
			iFile.WriteLine("DescriptionColor="+this.Fonts.DescriptionColor.R+";"+this.Fonts.DescriptionColor.G+";"+this.Fonts.DescriptionColor.B);
			iFile.WriteLine("DescriptionFont="+this.Fonts.DescriptionFont);
			iFile.WriteLine("DescriptionSize="+this.Fonts.DescriptionSize);
			iFile.WriteLine("DescriptionStyle="+this.Fonts.DescriptionStyle);
			iFile.WriteLine("DescriptionBorder="+this.Fonts.ShowDescriptionBorder);

			iFile.WriteLine("");
			
			//iFile.WriteLine("ItemsPath="+ProgramConfig.ItemsPath);
			//System.Windows.Forms.MessageBox.Show(this.Locations.ImagesPath);
			iFile.WriteLine("ImagesPath="+this.Locations.ImagesPath);

			iFile.WriteLine("");

			iFile.WriteLine("BgColor="+this.Images.BackgroundColor.R+";"+this.Images.BackgroundColor.G+";"+this.Images.BackgroundColor.B);
			iFile.WriteLine("BgImage="+this.Images.BackgroundImagePath);
			iFile.WriteLine("BgImageX="+this.Images.BackgroundSize.Width);
			iFile.WriteLine("BgImageY="+this.Images.BackgroundSize.Height);

			iFile.WriteLine("");

			iFile.WriteLine("IconBg="+this.Images.IconBackgroundImagePath);
			iFile.WriteLine("IconBgX="+this.Images.IconBackgroundSize.Width);
			iFile.WriteLine("IconBgY="+this.Images.IconBackgroundSize.Height);
			iFile.WriteLine("BgAlpha="+this.Appearance.IconBackgroundAlpha);

			iFile.WriteLine("");
			iFile.WriteLine("IconSelected="+this.Images.IconSelectedImagePath);
			iFile.WriteLine("ScrollUpImage="+this.Images.ScrollUpImagePath);
			iFile.WriteLine("ScrollDownImage="+this.Images.ScrollDownImagePath);

			iFile.WriteLine("");
			iFile.WriteLine("## The following commands are switches that are not available on the configuraiton dialog ##");
			iFile.WriteLine("# Forces Orbit to open with only tasks showing");
			iFile.WriteLine("ShowTasksOnly="+this.Behavior.ShowTasksOnly);
			iFile.WriteLine("");
			iFile.WriteLine("# Forces Orbit to always open the loop in the center, regardless of where the mouse is");
			iFile.WriteLine("AlwaysPopInCenter="+this.Behavior.AlwaysPopInCenter);
			iFile.WriteLine("");
			iFile.WriteLine("# Forces Orbit to hide minimized windows in the tasks folder");
			iFile.WriteLine("HideMinimizedTasks="+Orbit.Utilities.WindowsTaskManager.HideMinimizedWindows);
			iFile.WriteLine("");
			iFile.WriteLine("# Forces Orbit to render low quality labels, which are known to work on 99% of the video cards");
			iFile.WriteLine("ForceLowQualityLabels="+this.Behavior.ForceLowQualityLabels);
			iFile.WriteLine("");
			iFile.WriteLine("# Forces Orbit not to use the fancy preview animation. This speeds things up a bit.");
			iFile.WriteLine("ForceNoPreviewAnimation="+this.Behavior.ForceNoPreviewAnimation);
			iFile.Close();
		}
		public void UpdateRuntimeValues()
		{
			// updating locations
			Locations.UpdateRuntimeValues();

			// the runtime items depend on the other vars
			Runtime.AllowLabels=true;
			if(!Images.UseWindowsWallpaper)
				Runtime.BgStretch=BackgroundStretchMode.StretchAspect;
			Runtime.CurrentLevel="";
			Runtime.IconSizeAverage=(Appearance.IconMinifiedSize+Appearance.IconMagnifiedSize)/2;
			Runtime.LoopDistance=10;
			//Runtime.MaxItemsShownInFirstLoop=15;
			Runtime.TransparentBlitFix=4;
			Runtime.CanPop=true;
		}
		public void StripInvalidValues()
		{
			#region Checking if images exist
			if(this.Images.BackgroundImagePath==null
				|| !System.IO.File.Exists(this.Images.BackgroundImagePath))
				this.Images.BackgroundImagePath="";
			if(this.Images.IconBackgroundImagePath==null
				|| !System.IO.File.Exists(this.Images.IconBackgroundImagePath))
				this.Images.IconBackgroundImagePath="";
			if(this.Images.IconSelectedImagePath==null
				|| !System.IO.File.Exists(this.Images.IconSelectedImagePath))
				this.Images.IconSelectedImagePath="";
			if(this.Images.ScrollDownImagePath==null
				|| !System.IO.File.Exists(this.Images.ScrollDownImagePath))
				this.Images.ScrollDownImagePath="";
			if(this.Images.ScrollUpImagePath==null
				|| !System.IO.File.Exists(this.Images.ScrollUpImagePath))
				this.Images.ScrollUpImagePath="";
			#endregion
		}
		#endregion

		#region Properties
		public LocationsConfig Locations
		{
			get
			{
				return _Locations;
			}
			set
			{
				if(value!=_Locations)
				{
					_Locations=value;
					if(this.RequestMemoryReset!=null)this.RequestMemoryReset(this, new EventArgs());
				}
			}
		}
		public ImagesConfig Images
		{
			get
			{
				return _Images;
			}
			set
			{
				if(value!=_Images)
				{
					_Images=value;
					if(this.RequestMemoryReset!=null)this.RequestMemoryReset(this, new EventArgs());
				}
			}
		}
		public FontsConfig Fonts
		{
			get
			{
				return _Fonts;
			}
			set
			{
				if(value!=_Fonts)
				{
					_Fonts=value;
					if(this.RequestDeviceReset!=null)this.RequestDeviceReset(this, new EventArgs());
				}
			}
		}
		public AppearanceConfig Appearance
		{
			get
			{
				return _Appearance;
			}
			set
			{
				if(value!=_Appearance)
				{
					_Appearance=value;
					if(this.RequestDeviceReset!=null)this.RequestDeviceReset(this, new EventArgs());
				}
			}
		}
		public BehaviorConfig Behavior
		{
			get
			{
				return _Behavior;
			}
			set
			{
				if(value!=_Behavior)
					_Behavior=value;
			}
		}
		public RuntimeConfig Runtime
		{
			get
			{
				return _Runtime;
			}
			set
			{
				if(value!=_Runtime)
					_Runtime=value;
			}
		}
		#endregion

		#region Events
		public event EventHandler RequestDeviceReset;
		public event EventHandler RequestMemoryReset;
		#endregion
	}
}
