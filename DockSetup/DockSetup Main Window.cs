using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Orbit.Language;

namespace Orbit.Configuration
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class DockSetup : System.Windows.Forms.Form
	{
		//private ConfigTypes.Config ProgramConfig;
		private Orbit.Configuration.ConfigurationInfo ProgramConfig;
		private string ApplicationPath;
		private string OrbitDataPath;
		private int AppearanceValueToChange;

		#region Windows Forms Items
		private System.Windows.Forms.PictureBox ClosePictureBox;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Panel LocationsPanel;
		private System.Windows.Forms.Panel ImagesPanel;
		private System.Windows.Forms.Panel AppearancePanel;
		private System.Windows.Forms.Panel BehaviorPanel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.PictureBox LocationsPicture;
		private System.Windows.Forms.PictureBox ImagesPicture;
		private System.Windows.Forms.PictureBox AppearancePicture;
		private System.Windows.Forms.PictureBox BehaviorPicture;
		private System.Windows.Forms.PictureBox ItemPicture;
		private System.Windows.Forms.PictureBox ItemSelectedPicture;
		private System.Windows.Forms.Label LocationsLabel;
		private System.Windows.Forms.Label ImagesLabel;
		private System.Windows.Forms.Label AppearanceLabel;
		private System.Windows.Forms.Label BehaviorLabel;
		private System.Windows.Forms.Button ItemsDirBrowse;
		private System.Windows.Forms.Button ImagesDirBrowse;
		private System.Windows.Forms.Label ItemsDirDisplay;
		private System.Windows.Forms.Label ImagesDirDisplay;
		private System.Windows.Forms.PictureBox IconBgDisplay;
		private System.Windows.Forms.PictureBox BgDisplay;
		private System.Windows.Forms.CheckBox SpinOut;
		private System.Windows.Forms.CheckBox SlideOut;
		private System.Windows.Forms.CheckBox ZoomOut;
		private System.Windows.Forms.CheckBox OpenInStart;
		private System.Windows.Forms.CheckBox OpenOnMouseOver;
		private System.Windows.Forms.CheckBox StartWithWindows;
		private System.Windows.Forms.CheckBox ShowItemLabels;
		private System.Windows.Forms.CheckBox UseTransparency;
		private System.Windows.Forms.ComboBox PopKey;
		private System.Windows.Forms.LinkLabel BgImageBrowse;
		private System.Windows.Forms.LinkLabel BgImageDisable;
		private System.Windows.Forms.LinkLabel IconBgImageDisable;
		private System.Windows.Forms.LinkLabel IconBgImageBrowse;
		private System.Windows.Forms.TrackBar AppearanceBar;
		private System.Windows.Forms.Label AppearanceDisplay;
		private System.Windows.Forms.LinkLabel IconSizeLink;
		private System.Windows.Forms.LinkLabel IconOpacityLink;
		private System.Windows.Forms.LinkLabel BgOpacityLink;
		private System.Windows.Forms.LinkLabel AnimSpeedLink;
		private System.Windows.Forms.LinkLabel MouseWheelLink;
		private System.Windows.Forms.Label AppearanceDescription;
		private System.Windows.Forms.PictureBox SavePictureBox;
		private System.Windows.Forms.Panel AboutPanel;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label AboutLabel;
		private System.Windows.Forms.PictureBox AboutPicture;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel HomepageLink;
		private System.Windows.Forms.LinkLabel AuthorLink;
		private System.Windows.Forms.LinkLabel BugLink;
		private System.Windows.Forms.LinkLabel ImproveLink;
		private System.Windows.Forms.Label VersionInfo;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.CheckBox UseMultipleWindows;
		private System.Windows.Forms.PictureBox BgColorDisplay;
		private System.Windows.Forms.LinkLabel PickColor;
		private System.Windows.Forms.CheckBox ShowThumbnails;
		private System.Windows.Forms.PictureBox IconSelectedDisplay;
		private System.Windows.Forms.LinkLabel IconSelectedImageDisable;
		private System.Windows.Forms.LinkLabel IconSelectedImageBrowse;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel FontsPanel;
		private System.Windows.Forms.PictureBox FontsPicture;
		private System.Windows.Forms.Label FontsLabel;
		private System.Windows.Forms.LinkLabel LabelFontChangeLink;
		private System.Windows.Forms.Label LabelFontFamilyDisplay;
		private System.Windows.Forms.PictureBox LabelBorderColor;
		private System.Windows.Forms.PictureBox LabelColor;
		private System.Windows.Forms.LinkLabel LabelBorderColorChange;
		private System.Windows.Forms.LinkLabel LabelColorChange;
		private System.Windows.Forms.LinkLabel DescriptionColorChange;
		private System.Windows.Forms.LinkLabel DescriptionBorderColorChange;
		private System.Windows.Forms.LinkLabel DescriptionFontChangeLink;
		private System.Windows.Forms.Label DescriptionFontFamilyDisplay;
		private System.Windows.Forms.PictureBox DescriptionColor;
		private System.Windows.Forms.PictureBox DescriptionBorderColor;
		private System.Windows.Forms.CheckBox LabelBorder;
		private System.Windows.Forms.CheckBox DescriptionBorder;
		private System.Windows.Forms.LinkLabel ItemsShownPerLineLink;
		private System.Windows.Forms.LinkLabel ScrollUpBrowse;
		private System.Windows.Forms.LinkLabel ScrollDownBrowse;
		private System.Windows.Forms.PictureBox ScrollDownDisplay;
		private System.Windows.Forms.PictureBox ScrollUpDisplay;
		private System.Windows.Forms.CheckBox GroupIcons;
		private System.Windows.Forms.CheckBox FakeTransparency;
		private System.Windows.Forms.Label ProjectName;
		private System.Windows.Forms.LinkLabel IconMagnifiedSize;
		private System.Windows.Forms.Label VersionInformationLabel;
		private System.Windows.Forms.Label HelpTheProjectLabel;
		private System.Windows.Forms.Label AuthorLabel;
		private System.Windows.Forms.Label PreferredImagesLocationLabel;
		private System.Windows.Forms.Label ItemsStructureLocationLabel;
		private System.Windows.Forms.Label NonTransparentBackgroundLabel;
		private System.Windows.Forms.Label IconAreaBackgroundLabel;
		private System.Windows.Forms.Label SelectedItemLabel;
		private System.Windows.Forms.Label ScrollUpLabel;
		private System.Windows.Forms.Label ScrollDownLabel;
		private System.Windows.Forms.Label NonTransparentColorLabel;
		private System.Windows.Forms.Label OrbitConfigurationLabel;
		private System.Windows.Forms.Label OrbitConfigurationDescriptionLabel;
		private System.Windows.Forms.Label DescriptionFontLabel;
		private System.Windows.Forms.Label LabelFontLabel;
		private System.Windows.Forms.Label OrbitDescriptionLabel;
		private System.Windows.Forms.Label UseSliderLabel;
		private System.Windows.Forms.Label SelectASettingLabel;
		private System.Windows.Forms.Label OtherSettingsLabel;
		private System.Windows.Forms.Label BehaviorOtherSettingsLabel;
		private System.Windows.Forms.Label ClickAndMouseResponseLabel;
		private System.Windows.Forms.Label TransitionEffectsLabel;
		private System.Windows.Forms.Label LabelColorsLabel;
		private System.Windows.Forms.Label DescriptionFillLabel;
		private System.Windows.Forms.Label DescriptionBorderLabel;
		private System.Windows.Forms.Label DescriptionColorsLabel;
		private System.Windows.Forms.Label LabelFillLabel;
		private System.Windows.Forms.Label LabelBorderLabel;
		private System.Windows.Forms.LinkLabel BgImageUseWindowsLink;
		private System.Windows.Forms.Label PopUpKeyLabel;
		private System.Windows.Forms.Label ExcludedTasksLabel;
		private System.Windows.Forms.LinkLabel ManageExcludedTasksLink;
		#endregion
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DockSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ApplicationPath=Application.ExecutablePath.Substring(0,Application.ExecutablePath.Length-(Application.ExecutablePath.Length-Application.ExecutablePath.LastIndexOf("\\")))+"\\";
			OrbitDataPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), @"profiles\"+System.Environment.UserName);
			this.ItemsDirDisplay.Text=System.IO.Path.Combine(OrbitDataPath, "myitems");

			//LoadConfig();
			ProgramConfig=Orbit.Configuration.ConfigurationInfo.FromINI();
			ProgramConfig.StripInvalidValues();
			UpdateControlsFromConfig();

			UpdateUILanguage();
			Global.LanguageLoader.LanguageLoaded+=new EventHandler(Language_LanguageLoaded);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(BgDisplay.Image!=null)
				try
				{
					BgDisplay.Image=null;
					BgDisplay.Image.Dispose();
				}
				catch(Exception){}
			if(IconBgDisplay.Image!=null)
				try
				{
					IconBgDisplay.Image=null;
					IconBgDisplay.Image.Dispose();
				}
				catch(Exception){}
			if(IconSelectedDisplay.Image!=null)
				try
				{
					IconSelectedDisplay.Image=null;
					IconSelectedDisplay.Image.Dispose();
				}
				catch(Exception){}
			if(IconSelectedDisplay.Image!=null)
				try
				{
					IconSelectedDisplay.Image=null;
					IconSelectedDisplay.Image.Dispose();
				}
				catch(Exception){}
			if(ScrollDownDisplay!=null)
				try
				{
					ScrollDownDisplay.Image=null;
					ScrollDownDisplay.Image.Dispose();
				}
				catch(Exception){}
			if(ScrollUpDisplay!=null)
				try
				{
					ScrollUpDisplay.Image=null;
					ScrollUpDisplay.Image.Dispose();
				}
				catch(Exception){}

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DockSetup));
			this.LocationsPanel = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ImagesDirBrowse = new System.Windows.Forms.Button();
			this.ImagesDirDisplay = new System.Windows.Forms.Label();
			this.PreferredImagesLocationLabel = new System.Windows.Forms.Label();
			this.ItemsDirBrowse = new System.Windows.Forms.Button();
			this.ItemsDirDisplay = new System.Windows.Forms.Label();
			this.ItemsStructureLocationLabel = new System.Windows.Forms.Label();
			this.LocationsLabel = new System.Windows.Forms.Label();
			this.LocationsPicture = new System.Windows.Forms.PictureBox();
			this.ClosePictureBox = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.OrbitConfigurationLabel = new System.Windows.Forms.Label();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.ImagesPanel = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.BgImageUseWindowsLink = new System.Windows.Forms.LinkLabel();
			this.ScrollDownLabel = new System.Windows.Forms.Label();
			this.ScrollDownBrowse = new System.Windows.Forms.LinkLabel();
			this.ScrollDownDisplay = new System.Windows.Forms.PictureBox();
			this.ScrollUpLabel = new System.Windows.Forms.Label();
			this.ScrollUpBrowse = new System.Windows.Forms.LinkLabel();
			this.ScrollUpDisplay = new System.Windows.Forms.PictureBox();
			this.SelectedItemLabel = new System.Windows.Forms.Label();
			this.IconSelectedImageDisable = new System.Windows.Forms.LinkLabel();
			this.IconSelectedImageBrowse = new System.Windows.Forms.LinkLabel();
			this.IconSelectedDisplay = new System.Windows.Forms.PictureBox();
			this.PickColor = new System.Windows.Forms.LinkLabel();
			this.BgColorDisplay = new System.Windows.Forms.PictureBox();
			this.NonTransparentColorLabel = new System.Windows.Forms.Label();
			this.IconAreaBackgroundLabel = new System.Windows.Forms.Label();
			this.IconBgImageDisable = new System.Windows.Forms.LinkLabel();
			this.IconBgImageBrowse = new System.Windows.Forms.LinkLabel();
			this.IconBgDisplay = new System.Windows.Forms.PictureBox();
			this.BgImageDisable = new System.Windows.Forms.LinkLabel();
			this.BgImageBrowse = new System.Windows.Forms.LinkLabel();
			this.BgDisplay = new System.Windows.Forms.PictureBox();
			this.NonTransparentBackgroundLabel = new System.Windows.Forms.Label();
			this.ImagesLabel = new System.Windows.Forms.Label();
			this.ImagesPicture = new System.Windows.Forms.PictureBox();
			this.AppearancePanel = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.IconMagnifiedSize = new System.Windows.Forms.LinkLabel();
			this.FakeTransparency = new System.Windows.Forms.CheckBox();
			this.GroupIcons = new System.Windows.Forms.CheckBox();
			this.ItemsShownPerLineLink = new System.Windows.Forms.LinkLabel();
			this.ShowThumbnails = new System.Windows.Forms.CheckBox();
			this.UseMultipleWindows = new System.Windows.Forms.CheckBox();
			this.UseTransparency = new System.Windows.Forms.CheckBox();
			this.ShowItemLabels = new System.Windows.Forms.CheckBox();
			this.OtherSettingsLabel = new System.Windows.Forms.Label();
			this.AppearanceDisplay = new System.Windows.Forms.Label();
			this.AppearanceDescription = new System.Windows.Forms.Label();
			this.UseSliderLabel = new System.Windows.Forms.Label();
			this.MouseWheelLink = new System.Windows.Forms.LinkLabel();
			this.AnimSpeedLink = new System.Windows.Forms.LinkLabel();
			this.BgOpacityLink = new System.Windows.Forms.LinkLabel();
			this.IconOpacityLink = new System.Windows.Forms.LinkLabel();
			this.IconSizeLink = new System.Windows.Forms.LinkLabel();
			this.SelectASettingLabel = new System.Windows.Forms.Label();
			this.AppearanceBar = new System.Windows.Forms.TrackBar();
			this.AppearanceLabel = new System.Windows.Forms.Label();
			this.AppearancePicture = new System.Windows.Forms.PictureBox();
			this.BehaviorPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ManageExcludedTasksLink = new System.Windows.Forms.LinkLabel();
			this.ExcludedTasksLabel = new System.Windows.Forms.Label();
			this.PopUpKeyLabel = new System.Windows.Forms.Label();
			this.PopKey = new System.Windows.Forms.ComboBox();
			this.StartWithWindows = new System.Windows.Forms.CheckBox();
			this.BehaviorOtherSettingsLabel = new System.Windows.Forms.Label();
			this.OpenOnMouseOver = new System.Windows.Forms.CheckBox();
			this.OpenInStart = new System.Windows.Forms.CheckBox();
			this.ClickAndMouseResponseLabel = new System.Windows.Forms.Label();
			this.ZoomOut = new System.Windows.Forms.CheckBox();
			this.SlideOut = new System.Windows.Forms.CheckBox();
			this.SpinOut = new System.Windows.Forms.CheckBox();
			this.TransitionEffectsLabel = new System.Windows.Forms.Label();
			this.BehaviorLabel = new System.Windows.Forms.Label();
			this.BehaviorPicture = new System.Windows.Forms.PictureBox();
			this.OrbitConfigurationDescriptionLabel = new System.Windows.Forms.Label();
			this.ItemPicture = new System.Windows.Forms.PictureBox();
			this.ItemSelectedPicture = new System.Windows.Forms.PictureBox();
			this.SavePictureBox = new System.Windows.Forms.PictureBox();
			this.AboutPanel = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.label19 = new System.Windows.Forms.Label();
			this.VersionInfo = new System.Windows.Forms.Label();
			this.VersionInformationLabel = new System.Windows.Forms.Label();
			this.ImproveLink = new System.Windows.Forms.LinkLabel();
			this.BugLink = new System.Windows.Forms.LinkLabel();
			this.HelpTheProjectLabel = new System.Windows.Forms.Label();
			this.AuthorLink = new System.Windows.Forms.LinkLabel();
			this.HomepageLink = new System.Windows.Forms.LinkLabel();
			this.AuthorLabel = new System.Windows.Forms.Label();
			this.OrbitDescriptionLabel = new System.Windows.Forms.Label();
			this.ProjectName = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.AboutLabel = new System.Windows.Forms.Label();
			this.AboutPicture = new System.Windows.Forms.PictureBox();
			this.FontsPanel = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.DescriptionBorder = new System.Windows.Forms.CheckBox();
			this.LabelBorder = new System.Windows.Forms.CheckBox();
			this.DescriptionColorChange = new System.Windows.Forms.LinkLabel();
			this.DescriptionBorderColorChange = new System.Windows.Forms.LinkLabel();
			this.DescriptionColor = new System.Windows.Forms.PictureBox();
			this.DescriptionBorderColor = new System.Windows.Forms.PictureBox();
			this.DescriptionFillLabel = new System.Windows.Forms.Label();
			this.DescriptionBorderLabel = new System.Windows.Forms.Label();
			this.DescriptionColorsLabel = new System.Windows.Forms.Label();
			this.DescriptionFontChangeLink = new System.Windows.Forms.LinkLabel();
			this.DescriptionFontFamilyDisplay = new System.Windows.Forms.Label();
			this.DescriptionFontLabel = new System.Windows.Forms.Label();
			this.LabelColorChange = new System.Windows.Forms.LinkLabel();
			this.LabelBorderColorChange = new System.Windows.Forms.LinkLabel();
			this.LabelColor = new System.Windows.Forms.PictureBox();
			this.LabelBorderColor = new System.Windows.Forms.PictureBox();
			this.LabelFillLabel = new System.Windows.Forms.Label();
			this.LabelBorderLabel = new System.Windows.Forms.Label();
			this.LabelColorsLabel = new System.Windows.Forms.Label();
			this.LabelFontChangeLink = new System.Windows.Forms.LinkLabel();
			this.LabelFontFamilyDisplay = new System.Windows.Forms.Label();
			this.LabelFontLabel = new System.Windows.Forms.Label();
			this.FontsLabel = new System.Windows.Forms.Label();
			this.FontsPicture = new System.Windows.Forms.PictureBox();
			this.LocationsPanel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.ImagesPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			this.AppearancePanel.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AppearanceBar)).BeginInit();
			this.BehaviorPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.AboutPanel.SuspendLayout();
			this.panel6.SuspendLayout();
			this.FontsPanel.SuspendLayout();
			this.panel7.SuspendLayout();
			this.SuspendLayout();
			// 
			// LocationsPanel
			// 
			this.LocationsPanel.BackColor = System.Drawing.SystemColors.Control;
			this.LocationsPanel.Controls.Add(this.panel2);
			this.LocationsPanel.Controls.Add(this.LocationsLabel);
			this.LocationsPanel.Controls.Add(this.LocationsPicture);
			this.LocationsPanel.ForeColor = System.Drawing.Color.Black;
			this.LocationsPanel.Location = new System.Drawing.Point(4, 60);
			this.LocationsPanel.Name = "LocationsPanel";
			this.LocationsPanel.Size = new System.Drawing.Size(596, 24);
			this.LocationsPanel.TabIndex = 0;
			this.LocationsPanel.Tag = "120";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.ImagesDirBrowse);
			this.panel2.Controls.Add(this.ImagesDirDisplay);
			this.panel2.Controls.Add(this.PreferredImagesLocationLabel);
			this.panel2.Controls.Add(this.ItemsDirBrowse);
			this.panel2.Controls.Add(this.ItemsDirDisplay);
			this.panel2.Controls.Add(this.ItemsStructureLocationLabel);
			this.panel2.Location = new System.Drawing.Point(4, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(588, 92);
			this.panel2.TabIndex = 2;
			// 
			// ImagesDirBrowse
			// 
			this.ImagesDirBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.ImagesDirBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ImagesDirBrowse.Location = new System.Drawing.Point(556, 64);
			this.ImagesDirBrowse.Name = "ImagesDirBrowse";
			this.ImagesDirBrowse.Size = new System.Drawing.Size(24, 20);
			this.ImagesDirBrowse.TabIndex = 1;
			this.ImagesDirBrowse.Text = "...";
			this.ImagesDirBrowse.Click += new System.EventHandler(this.ImagesDirBrowse_Click);
			// 
			// ImagesDirDisplay
			// 
			this.ImagesDirDisplay.Location = new System.Drawing.Point(8, 68);
			this.ImagesDirDisplay.Name = "ImagesDirDisplay";
			this.ImagesDirDisplay.Size = new System.Drawing.Size(544, 16);
			this.ImagesDirDisplay.TabIndex = 4;
			this.ImagesDirDisplay.Text = "C:\\Program Files\\Orbit\\My Images";
			// 
			// PreferredImagesLocationLabel
			// 
			this.PreferredImagesLocationLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PreferredImagesLocationLabel.Location = new System.Drawing.Point(4, 48);
			this.PreferredImagesLocationLabel.Name = "PreferredImagesLocationLabel";
			this.PreferredImagesLocationLabel.Size = new System.Drawing.Size(576, 16);
			this.PreferredImagesLocationLabel.TabIndex = 0;
			this.PreferredImagesLocationLabel.Text = "Preferred Images Location";
			// 
			// ItemsDirBrowse
			// 
			this.ItemsDirBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.ItemsDirBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ItemsDirBrowse.Location = new System.Drawing.Point(556, 20);
			this.ItemsDirBrowse.Name = "ItemsDirBrowse";
			this.ItemsDirBrowse.Size = new System.Drawing.Size(24, 20);
			this.ItemsDirBrowse.TabIndex = 0;
			this.ItemsDirBrowse.Text = "...";
			this.ItemsDirBrowse.Visible = false;
			this.ItemsDirBrowse.Click += new System.EventHandler(this.ItemsDirBrowse_Click);
			// 
			// ItemsDirDisplay
			// 
			this.ItemsDirDisplay.Location = new System.Drawing.Point(8, 24);
			this.ItemsDirDisplay.Name = "ItemsDirDisplay";
			this.ItemsDirDisplay.Size = new System.Drawing.Size(544, 16);
			this.ItemsDirDisplay.TabIndex = 1;
			this.ItemsDirDisplay.Text = "C:\\Program Files\\Orbit\\My Items";
			// 
			// ItemsStructureLocationLabel
			// 
			this.ItemsStructureLocationLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ItemsStructureLocationLabel.Location = new System.Drawing.Point(4, 4);
			this.ItemsStructureLocationLabel.Name = "ItemsStructureLocationLabel";
			this.ItemsStructureLocationLabel.Size = new System.Drawing.Size(576, 16);
			this.ItemsStructureLocationLabel.TabIndex = 0;
			this.ItemsStructureLocationLabel.Text = "Items Structure Location";
			// 
			// LocationsLabel
			// 
			this.LocationsLabel.BackColor = System.Drawing.Color.Transparent;
			this.LocationsLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LocationsLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.LocationsLabel.Location = new System.Drawing.Point(28, 4);
			this.LocationsLabel.Name = "LocationsLabel";
			this.LocationsLabel.Size = new System.Drawing.Size(552, 16);
			this.LocationsLabel.TabIndex = 1;
			this.LocationsLabel.Text = "Locations";
			// 
			// LocationsPicture
			// 
			this.LocationsPicture.BackColor = System.Drawing.SystemColors.Control;
			this.LocationsPicture.Location = new System.Drawing.Point(4, 0);
			this.LocationsPicture.Name = "LocationsPicture";
			this.LocationsPicture.Size = new System.Drawing.Size(588, 24);
			this.LocationsPicture.TabIndex = 0;
			this.LocationsPicture.TabStop = false;
			this.LocationsPicture.Click += new System.EventHandler(this.LocationsPicture_Click);
			// 
			// ClosePictureBox
			// 
			this.ClosePictureBox.BackColor = System.Drawing.Color.White;
			this.ClosePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ClosePictureBox.Image")));
			this.ClosePictureBox.Location = new System.Drawing.Point(580, 28);
			this.ClosePictureBox.Name = "ClosePictureBox";
			this.ClosePictureBox.Size = new System.Drawing.Size(24, 24);
			this.ClosePictureBox.TabIndex = 1;
			this.ClosePictureBox.TabStop = false;
			this.ClosePictureBox.Click += new System.EventHandler(this.ClosePictureBox_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.White;
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(608, 56);
			this.pictureBox2.TabIndex = 2;
			this.pictureBox2.TabStop = false;
			// 
			// OrbitConfigurationLabel
			// 
			this.OrbitConfigurationLabel.BackColor = System.Drawing.Color.White;
			this.OrbitConfigurationLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OrbitConfigurationLabel.Location = new System.Drawing.Point(52, 4);
			this.OrbitConfigurationLabel.Name = "OrbitConfigurationLabel";
			this.OrbitConfigurationLabel.Size = new System.Drawing.Size(476, 32);
			this.OrbitConfigurationLabel.TabIndex = 3;
			this.OrbitConfigurationLabel.Text = "Orbit Configuration";
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackColor = System.Drawing.Color.White;
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(4, 4);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(48, 48);
			this.pictureBox3.TabIndex = 4;
			this.pictureBox3.TabStop = false;
			// 
			// ImagesPanel
			// 
			this.ImagesPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ImagesPanel.Controls.Add(this.panel3);
			this.ImagesPanel.Controls.Add(this.ImagesLabel);
			this.ImagesPanel.Controls.Add(this.ImagesPicture);
			this.ImagesPanel.ForeColor = System.Drawing.Color.Black;
			this.ImagesPanel.Location = new System.Drawing.Point(4, 88);
			this.ImagesPanel.Name = "ImagesPanel";
			this.ImagesPanel.Size = new System.Drawing.Size(596, 232);
			this.ImagesPanel.TabIndex = 1;
			this.ImagesPanel.Tag = "220";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.White;
			this.panel3.Controls.Add(this.BgImageUseWindowsLink);
			this.panel3.Controls.Add(this.ScrollDownLabel);
			this.panel3.Controls.Add(this.ScrollDownBrowse);
			this.panel3.Controls.Add(this.ScrollDownDisplay);
			this.panel3.Controls.Add(this.ScrollUpLabel);
			this.panel3.Controls.Add(this.ScrollUpBrowse);
			this.panel3.Controls.Add(this.ScrollUpDisplay);
			this.panel3.Controls.Add(this.SelectedItemLabel);
			this.panel3.Controls.Add(this.IconSelectedImageDisable);
			this.panel3.Controls.Add(this.IconSelectedImageBrowse);
			this.panel3.Controls.Add(this.IconSelectedDisplay);
			this.panel3.Controls.Add(this.PickColor);
			this.panel3.Controls.Add(this.BgColorDisplay);
			this.panel3.Controls.Add(this.NonTransparentColorLabel);
			this.panel3.Controls.Add(this.IconAreaBackgroundLabel);
			this.panel3.Controls.Add(this.IconBgImageDisable);
			this.panel3.Controls.Add(this.IconBgImageBrowse);
			this.panel3.Controls.Add(this.IconBgDisplay);
			this.panel3.Controls.Add(this.BgImageDisable);
			this.panel3.Controls.Add(this.BgImageBrowse);
			this.panel3.Controls.Add(this.BgDisplay);
			this.panel3.Controls.Add(this.NonTransparentBackgroundLabel);
			this.panel3.Location = new System.Drawing.Point(4, 24);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(588, 192);
			this.panel3.TabIndex = 2;
			// 
			// BgImageUseWindowsLink
			// 
			this.BgImageUseWindowsLink.ActiveLinkColor = System.Drawing.Color.Red;
			this.BgImageUseWindowsLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgImageUseWindowsLink.Location = new System.Drawing.Point(88, 60);
			this.BgImageUseWindowsLink.Name = "BgImageUseWindowsLink";
			this.BgImageUseWindowsLink.Size = new System.Drawing.Size(160, 28);
			this.BgImageUseWindowsLink.TabIndex = 2;
			this.BgImageUseWindowsLink.TabStop = true;
			this.BgImageUseWindowsLink.Text = "Use Windows wallpaper";
			this.BgImageUseWindowsLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgImageUseWindowsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BgImageUseWindowsLink_LinkClicked);
			// 
			// ScrollDownLabel
			// 
			this.ScrollDownLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ScrollDownLabel.Location = new System.Drawing.Point(404, 148);
			this.ScrollDownLabel.Name = "ScrollDownLabel";
			this.ScrollDownLabel.Size = new System.Drawing.Size(180, 16);
			this.ScrollDownLabel.TabIndex = 22;
			this.ScrollDownLabel.Text = "Scroll Down Indicator";
			// 
			// ScrollDownBrowse
			// 
			this.ScrollDownBrowse.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ScrollDownBrowse.Location = new System.Drawing.Point(440, 168);
			this.ScrollDownBrowse.Name = "ScrollDownBrowse";
			this.ScrollDownBrowse.Size = new System.Drawing.Size(108, 16);
			this.ScrollDownBrowse.TabIndex = 8;
			this.ScrollDownBrowse.TabStop = true;
			this.ScrollDownBrowse.Text = "Change...";
			this.ScrollDownBrowse.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ScrollDownBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ScrollDownBrowse_LinkClicked);
			// 
			// ScrollDownDisplay
			// 
			this.ScrollDownDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ScrollDownDisplay.Location = new System.Drawing.Point(408, 164);
			this.ScrollDownDisplay.Name = "ScrollDownDisplay";
			this.ScrollDownDisplay.Size = new System.Drawing.Size(24, 24);
			this.ScrollDownDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ScrollDownDisplay.TabIndex = 19;
			this.ScrollDownDisplay.TabStop = false;
			// 
			// ScrollUpLabel
			// 
			this.ScrollUpLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ScrollUpLabel.Location = new System.Drawing.Point(404, 100);
			this.ScrollUpLabel.Name = "ScrollUpLabel";
			this.ScrollUpLabel.Size = new System.Drawing.Size(180, 16);
			this.ScrollUpLabel.TabIndex = 18;
			this.ScrollUpLabel.Text = "Scroll Up Indicator";
			// 
			// ScrollUpBrowse
			// 
			this.ScrollUpBrowse.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ScrollUpBrowse.Location = new System.Drawing.Point(440, 120);
			this.ScrollUpBrowse.Name = "ScrollUpBrowse";
			this.ScrollUpBrowse.Size = new System.Drawing.Size(108, 16);
			this.ScrollUpBrowse.TabIndex = 7;
			this.ScrollUpBrowse.TabStop = true;
			this.ScrollUpBrowse.Text = "Change...";
			this.ScrollUpBrowse.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ScrollUpBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ScrollUpBrowse_LinkClicked);
			// 
			// ScrollUpDisplay
			// 
			this.ScrollUpDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ScrollUpDisplay.Location = new System.Drawing.Point(408, 116);
			this.ScrollUpDisplay.Name = "ScrollUpDisplay";
			this.ScrollUpDisplay.Size = new System.Drawing.Size(24, 24);
			this.ScrollUpDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ScrollUpDisplay.TabIndex = 15;
			this.ScrollUpDisplay.TabStop = false;
			// 
			// SelectedItemLabel
			// 
			this.SelectedItemLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SelectedItemLabel.Location = new System.Drawing.Point(204, 100);
			this.SelectedItemLabel.Name = "SelectedItemLabel";
			this.SelectedItemLabel.Size = new System.Drawing.Size(196, 16);
			this.SelectedItemLabel.TabIndex = 14;
			this.SelectedItemLabel.Text = "Selected Item Indicator";
			// 
			// IconSelectedImageDisable
			// 
			this.IconSelectedImageDisable.ActiveLinkColor = System.Drawing.Color.Red;
			this.IconSelectedImageDisable.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconSelectedImageDisable.Location = new System.Drawing.Point(288, 152);
			this.IconSelectedImageDisable.Name = "IconSelectedImageDisable";
			this.IconSelectedImageDisable.Size = new System.Drawing.Size(112, 16);
			this.IconSelectedImageDisable.TabIndex = 6;
			this.IconSelectedImageDisable.TabStop = true;
			this.IconSelectedImageDisable.Text = "Disable";
			this.IconSelectedImageDisable.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconSelectedImageDisable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconSelectedImageDisable_LinkClicked);
			// 
			// IconSelectedImageBrowse
			// 
			this.IconSelectedImageBrowse.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconSelectedImageBrowse.Location = new System.Drawing.Point(288, 136);
			this.IconSelectedImageBrowse.Name = "IconSelectedImageBrowse";
			this.IconSelectedImageBrowse.Size = new System.Drawing.Size(112, 16);
			this.IconSelectedImageBrowse.TabIndex = 5;
			this.IconSelectedImageBrowse.TabStop = true;
			this.IconSelectedImageBrowse.Text = "Change...";
			this.IconSelectedImageBrowse.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconSelectedImageBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconSelectedImageBrowse_LinkClicked);
			// 
			// IconSelectedDisplay
			// 
			this.IconSelectedDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.IconSelectedDisplay.Location = new System.Drawing.Point(208, 116);
			this.IconSelectedDisplay.Name = "IconSelectedDisplay";
			this.IconSelectedDisplay.Size = new System.Drawing.Size(72, 72);
			this.IconSelectedDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.IconSelectedDisplay.TabIndex = 11;
			this.IconSelectedDisplay.TabStop = false;
			// 
			// PickColor
			// 
			this.PickColor.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.PickColor.Location = new System.Drawing.Point(292, 24);
			this.PickColor.Name = "PickColor";
			this.PickColor.Size = new System.Drawing.Size(292, 16);
			this.PickColor.TabIndex = 10;
			this.PickColor.TabStop = true;
			this.PickColor.Text = "Pick another color...";
			this.PickColor.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.PickColor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PickColor_LinkClicked);
			// 
			// BgColorDisplay
			// 
			this.BgColorDisplay.BackColor = System.Drawing.Color.Black;
			this.BgColorDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BgColorDisplay.Location = new System.Drawing.Point(256, 20);
			this.BgColorDisplay.Name = "BgColorDisplay";
			this.BgColorDisplay.Size = new System.Drawing.Size(28, 24);
			this.BgColorDisplay.TabIndex = 9;
			this.BgColorDisplay.TabStop = false;
			// 
			// NonTransparentColorLabel
			// 
			this.NonTransparentColorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.NonTransparentColorLabel.Location = new System.Drawing.Point(252, 4);
			this.NonTransparentColorLabel.Name = "NonTransparentColorLabel";
			this.NonTransparentColorLabel.Size = new System.Drawing.Size(332, 16);
			this.NonTransparentColorLabel.TabIndex = 8;
			this.NonTransparentColorLabel.Text = "Non-Transparent Mode Background Color";
			// 
			// IconAreaBackgroundLabel
			// 
			this.IconAreaBackgroundLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.IconAreaBackgroundLabel.Location = new System.Drawing.Point(4, 100);
			this.IconAreaBackgroundLabel.Name = "IconAreaBackgroundLabel";
			this.IconAreaBackgroundLabel.Size = new System.Drawing.Size(196, 16);
			this.IconAreaBackgroundLabel.TabIndex = 7;
			this.IconAreaBackgroundLabel.Text = "Icon Area Background";
			// 
			// IconBgImageDisable
			// 
			this.IconBgImageDisable.ActiveLinkColor = System.Drawing.Color.Red;
			this.IconBgImageDisable.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconBgImageDisable.Location = new System.Drawing.Point(88, 152);
			this.IconBgImageDisable.Name = "IconBgImageDisable";
			this.IconBgImageDisable.Size = new System.Drawing.Size(112, 16);
			this.IconBgImageDisable.TabIndex = 4;
			this.IconBgImageDisable.TabStop = true;
			this.IconBgImageDisable.Text = "Disable";
			this.IconBgImageDisable.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconBgImageDisable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconBgImageDisable_LinkClicked);
			// 
			// IconBgImageBrowse
			// 
			this.IconBgImageBrowse.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconBgImageBrowse.Location = new System.Drawing.Point(88, 136);
			this.IconBgImageBrowse.Name = "IconBgImageBrowse";
			this.IconBgImageBrowse.Size = new System.Drawing.Size(112, 16);
			this.IconBgImageBrowse.TabIndex = 3;
			this.IconBgImageBrowse.TabStop = true;
			this.IconBgImageBrowse.Text = "Change...";
			this.IconBgImageBrowse.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconBgImageBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconBgImageBrowse_LinkClicked);
			// 
			// IconBgDisplay
			// 
			this.IconBgDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.IconBgDisplay.Location = new System.Drawing.Point(8, 116);
			this.IconBgDisplay.Name = "IconBgDisplay";
			this.IconBgDisplay.Size = new System.Drawing.Size(72, 72);
			this.IconBgDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.IconBgDisplay.TabIndex = 4;
			this.IconBgDisplay.TabStop = false;
			// 
			// BgImageDisable
			// 
			this.BgImageDisable.ActiveLinkColor = System.Drawing.Color.Red;
			this.BgImageDisable.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgImageDisable.Location = new System.Drawing.Point(88, 44);
			this.BgImageDisable.Name = "BgImageDisable";
			this.BgImageDisable.Size = new System.Drawing.Size(160, 16);
			this.BgImageDisable.TabIndex = 1;
			this.BgImageDisable.TabStop = true;
			this.BgImageDisable.Text = "Disable";
			this.BgImageDisable.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgImageDisable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BgImageDisable_LinkClicked);
			// 
			// BgImageBrowse
			// 
			this.BgImageBrowse.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgImageBrowse.Location = new System.Drawing.Point(88, 28);
			this.BgImageBrowse.Name = "BgImageBrowse";
			this.BgImageBrowse.Size = new System.Drawing.Size(160, 16);
			this.BgImageBrowse.TabIndex = 0;
			this.BgImageBrowse.TabStop = true;
			this.BgImageBrowse.Text = "Change...";
			this.BgImageBrowse.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgImageBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BgImageBrowse_LinkClicked);
			// 
			// BgDisplay
			// 
			this.BgDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BgDisplay.Location = new System.Drawing.Point(8, 20);
			this.BgDisplay.Name = "BgDisplay";
			this.BgDisplay.Size = new System.Drawing.Size(72, 72);
			this.BgDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.BgDisplay.TabIndex = 1;
			this.BgDisplay.TabStop = false;
			// 
			// NonTransparentBackgroundLabel
			// 
			this.NonTransparentBackgroundLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.NonTransparentBackgroundLabel.Location = new System.Drawing.Point(4, 4);
			this.NonTransparentBackgroundLabel.Name = "NonTransparentBackgroundLabel";
			this.NonTransparentBackgroundLabel.Size = new System.Drawing.Size(244, 16);
			this.NonTransparentBackgroundLabel.TabIndex = 0;
			this.NonTransparentBackgroundLabel.Text = "Non-Transparent Mode Background";
			// 
			// ImagesLabel
			// 
			this.ImagesLabel.BackColor = System.Drawing.Color.Transparent;
			this.ImagesLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ImagesLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.ImagesLabel.Location = new System.Drawing.Point(28, 4);
			this.ImagesLabel.Name = "ImagesLabel";
			this.ImagesLabel.Size = new System.Drawing.Size(552, 16);
			this.ImagesLabel.TabIndex = 1;
			this.ImagesLabel.Text = "Images";
			// 
			// ImagesPicture
			// 
			this.ImagesPicture.BackColor = System.Drawing.SystemColors.Control;
			this.ImagesPicture.Location = new System.Drawing.Point(4, 0);
			this.ImagesPicture.Name = "ImagesPicture";
			this.ImagesPicture.Size = new System.Drawing.Size(588, 24);
			this.ImagesPicture.TabIndex = 0;
			this.ImagesPicture.TabStop = false;
			this.ImagesPicture.Click += new System.EventHandler(this.ImagesPicture_Click);
			// 
			// AppearancePanel
			// 
			this.AppearancePanel.BackColor = System.Drawing.SystemColors.Control;
			this.AppearancePanel.Controls.Add(this.panel4);
			this.AppearancePanel.Controls.Add(this.AppearanceLabel);
			this.AppearancePanel.Controls.Add(this.AppearancePicture);
			this.AppearancePanel.ForeColor = System.Drawing.Color.Black;
			this.AppearancePanel.Location = new System.Drawing.Point(4, 144);
			this.AppearancePanel.Name = "AppearancePanel";
			this.AppearancePanel.Size = new System.Drawing.Size(596, 24);
			this.AppearancePanel.TabIndex = 3;
			this.AppearancePanel.Tag = "252";
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.White;
			this.panel4.Controls.Add(this.IconMagnifiedSize);
			this.panel4.Controls.Add(this.FakeTransparency);
			this.panel4.Controls.Add(this.GroupIcons);
			this.panel4.Controls.Add(this.ItemsShownPerLineLink);
			this.panel4.Controls.Add(this.ShowThumbnails);
			this.panel4.Controls.Add(this.UseMultipleWindows);
			this.panel4.Controls.Add(this.UseTransparency);
			this.panel4.Controls.Add(this.ShowItemLabels);
			this.panel4.Controls.Add(this.OtherSettingsLabel);
			this.panel4.Controls.Add(this.AppearanceDisplay);
			this.panel4.Controls.Add(this.AppearanceDescription);
			this.panel4.Controls.Add(this.UseSliderLabel);
			this.panel4.Controls.Add(this.MouseWheelLink);
			this.panel4.Controls.Add(this.AnimSpeedLink);
			this.panel4.Controls.Add(this.BgOpacityLink);
			this.panel4.Controls.Add(this.IconOpacityLink);
			this.panel4.Controls.Add(this.IconSizeLink);
			this.panel4.Controls.Add(this.SelectASettingLabel);
			this.panel4.Controls.Add(this.AppearanceBar);
			this.panel4.Location = new System.Drawing.Point(4, 24);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(588, 224);
			this.panel4.TabIndex = 2;
			// 
			// IconMagnifiedSize
			// 
			this.IconMagnifiedSize.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconMagnifiedSize.Location = new System.Drawing.Point(8, 56);
			this.IconMagnifiedSize.Name = "IconMagnifiedSize";
			this.IconMagnifiedSize.Size = new System.Drawing.Size(256, 16);
			this.IconMagnifiedSize.TabIndex = 1;
			this.IconMagnifiedSize.TabStop = true;
			this.IconMagnifiedSize.Text = "Magnified Icon Size...";
			this.IconMagnifiedSize.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconMagnifiedSize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconMagnifiedSize_LinkClicked);
			// 
			// FakeTransparency
			// 
			this.FakeTransparency.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.FakeTransparency.Location = new System.Drawing.Point(272, 204);
			this.FakeTransparency.Name = "FakeTransparency";
			this.FakeTransparency.Size = new System.Drawing.Size(256, 16);
			this.FakeTransparency.TabIndex = 13;
			this.FakeTransparency.Text = "Use fake transparency";
			this.FakeTransparency.CheckedChanged += new System.EventHandler(this.FakeTransparency_CheckedChanged);
			// 
			// GroupIcons
			// 
			this.GroupIcons.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GroupIcons.Location = new System.Drawing.Point(272, 188);
			this.GroupIcons.Name = "GroupIcons";
			this.GroupIcons.Size = new System.Drawing.Size(256, 16);
			this.GroupIcons.TabIndex = 12;
			this.GroupIcons.Text = "Group icons together";
			this.GroupIcons.CheckedChanged += new System.EventHandler(this.GroupIcons_CheckedChanged);
			// 
			// ItemsShownPerLineLink
			// 
			this.ItemsShownPerLineLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ItemsShownPerLineLink.Location = new System.Drawing.Point(8, 136);
			this.ItemsShownPerLineLink.Name = "ItemsShownPerLineLink";
			this.ItemsShownPerLineLink.Size = new System.Drawing.Size(256, 16);
			this.ItemsShownPerLineLink.TabIndex = 6;
			this.ItemsShownPerLineLink.TabStop = true;
			this.ItemsShownPerLineLink.Text = "Items Shown Per Line...";
			this.ItemsShownPerLineLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ItemsShownPerLineLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ItemsShownPerLineLink_LinkClicked);
			// 
			// ShowThumbnails
			// 
			this.ShowThumbnails.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ShowThumbnails.Location = new System.Drawing.Point(272, 172);
			this.ShowThumbnails.Name = "ShowThumbnails";
			this.ShowThumbnails.Size = new System.Drawing.Size(256, 16);
			this.ShowThumbnails.TabIndex = 11;
			this.ShowThumbnails.Text = "Show image file\'s thumbnails";
			this.ShowThumbnails.CheckedChanged += new System.EventHandler(this.ShowThumbnails_CheckedChanged);
			// 
			// UseMultipleWindows
			// 
			this.UseMultipleWindows.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.UseMultipleWindows.Location = new System.Drawing.Point(8, 204);
			this.UseMultipleWindows.Name = "UseMultipleWindows";
			this.UseMultipleWindows.Size = new System.Drawing.Size(256, 16);
			this.UseMultipleWindows.TabIndex = 10;
			this.UseMultipleWindows.Text = "Make each item a separate window";
			this.UseMultipleWindows.Visible = false;
			this.UseMultipleWindows.CheckedChanged += new System.EventHandler(this.UseMultipleWindows_CheckedChanged);
			// 
			// UseTransparency
			// 
			this.UseTransparency.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.UseTransparency.Location = new System.Drawing.Point(8, 188);
			this.UseTransparency.Name = "UseTransparency";
			this.UseTransparency.Size = new System.Drawing.Size(256, 16);
			this.UseTransparency.TabIndex = 9;
			this.UseTransparency.Text = "Use Transparent Mode";
			this.UseTransparency.CheckedChanged += new System.EventHandler(this.UseTransparency_CheckedChanged);
			// 
			// ShowItemLabels
			// 
			this.ShowItemLabels.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ShowItemLabels.Location = new System.Drawing.Point(8, 172);
			this.ShowItemLabels.Name = "ShowItemLabels";
			this.ShowItemLabels.Size = new System.Drawing.Size(256, 16);
			this.ShowItemLabels.TabIndex = 8;
			this.ShowItemLabels.Text = "Show Item Labels";
			this.ShowItemLabels.CheckedChanged += new System.EventHandler(this.ShowItemLabels_CheckedChanged);
			// 
			// OtherSettingsLabel
			// 
			this.OtherSettingsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OtherSettingsLabel.Location = new System.Drawing.Point(4, 156);
			this.OtherSettingsLabel.Name = "OtherSettingsLabel";
			this.OtherSettingsLabel.Size = new System.Drawing.Size(260, 16);
			this.OtherSettingsLabel.TabIndex = 10;
			this.OtherSettingsLabel.Text = "Other Settings";
			// 
			// AppearanceDisplay
			// 
			this.AppearanceDisplay.Location = new System.Drawing.Point(272, 100);
			this.AppearanceDisplay.Name = "AppearanceDisplay";
			this.AppearanceDisplay.Size = new System.Drawing.Size(220, 16);
			this.AppearanceDisplay.TabIndex = 9;
			this.AppearanceDisplay.Text = "250";
			this.AppearanceDisplay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// AppearanceDescription
			// 
			this.AppearanceDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AppearanceDescription.Location = new System.Drawing.Point(272, 40);
			this.AppearanceDescription.Name = "AppearanceDescription";
			this.AppearanceDescription.Size = new System.Drawing.Size(312, 16);
			this.AppearanceDescription.TabIndex = 8;
			this.AppearanceDescription.Text = "Now Setting Up Normal Icon Size";
			// 
			// UseSliderLabel
			// 
			this.UseSliderLabel.Location = new System.Drawing.Point(8, 20);
			this.UseSliderLabel.Name = "UseSliderLabel";
			this.UseSliderLabel.Size = new System.Drawing.Size(256, 16);
			this.UseSliderLabel.TabIndex = 7;
			this.UseSliderLabel.Text = "And use the slider to adjust its value";
			// 
			// MouseWheelLink
			// 
			this.MouseWheelLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.MouseWheelLink.Location = new System.Drawing.Point(8, 120);
			this.MouseWheelLink.Name = "MouseWheelLink";
			this.MouseWheelLink.Size = new System.Drawing.Size(256, 16);
			this.MouseWheelLink.TabIndex = 5;
			this.MouseWheelLink.TabStop = true;
			this.MouseWheelLink.Text = "Mouse Wheel Sensitivity...";
			this.MouseWheelLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.MouseWheelLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MouseWheelLink_LinkClicked);
			// 
			// AnimSpeedLink
			// 
			this.AnimSpeedLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.AnimSpeedLink.Location = new System.Drawing.Point(8, 104);
			this.AnimSpeedLink.Name = "AnimSpeedLink";
			this.AnimSpeedLink.Size = new System.Drawing.Size(256, 16);
			this.AnimSpeedLink.TabIndex = 4;
			this.AnimSpeedLink.TabStop = true;
			this.AnimSpeedLink.Text = "Animation Speed...";
			this.AnimSpeedLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.AnimSpeedLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AnimSpeedLink_LinkClicked);
			// 
			// BgOpacityLink
			// 
			this.BgOpacityLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgOpacityLink.Location = new System.Drawing.Point(8, 88);
			this.BgOpacityLink.Name = "BgOpacityLink";
			this.BgOpacityLink.Size = new System.Drawing.Size(256, 16);
			this.BgOpacityLink.TabIndex = 3;
			this.BgOpacityLink.TabStop = true;
			this.BgOpacityLink.Text = "Icon Area Background Opacity...";
			this.BgOpacityLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BgOpacityLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BgOpacityLink_LinkClicked);
			// 
			// IconOpacityLink
			// 
			this.IconOpacityLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconOpacityLink.Location = new System.Drawing.Point(8, 72);
			this.IconOpacityLink.Name = "IconOpacityLink";
			this.IconOpacityLink.Size = new System.Drawing.Size(256, 16);
			this.IconOpacityLink.TabIndex = 2;
			this.IconOpacityLink.TabStop = true;
			this.IconOpacityLink.Text = "Icon Opacity...";
			this.IconOpacityLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconOpacityLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconOpacityLink_LinkClicked);
			// 
			// IconSizeLink
			// 
			this.IconSizeLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconSizeLink.Location = new System.Drawing.Point(8, 40);
			this.IconSizeLink.Name = "IconSizeLink";
			this.IconSizeLink.Size = new System.Drawing.Size(256, 16);
			this.IconSizeLink.TabIndex = 0;
			this.IconSizeLink.TabStop = true;
			this.IconSizeLink.Text = "Normal Icon Size...";
			this.IconSizeLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.IconSizeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconSizeLink_LinkClicked);
			// 
			// SelectASettingLabel
			// 
			this.SelectASettingLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SelectASettingLabel.Location = new System.Drawing.Point(4, 4);
			this.SelectASettingLabel.Name = "SelectASettingLabel";
			this.SelectASettingLabel.Size = new System.Drawing.Size(260, 16);
			this.SelectASettingLabel.TabIndex = 1;
			this.SelectASettingLabel.Text = "Select an Appearance Setting";
			// 
			// AppearanceBar
			// 
			this.AppearanceBar.Location = new System.Drawing.Point(272, 56);
			this.AppearanceBar.Name = "AppearanceBar";
			this.AppearanceBar.Size = new System.Drawing.Size(220, 45);
			this.AppearanceBar.TabIndex = 7;
			this.AppearanceBar.Scroll += new System.EventHandler(this.AppearanceBar_Scroll);
			// 
			// AppearanceLabel
			// 
			this.AppearanceLabel.BackColor = System.Drawing.Color.Transparent;
			this.AppearanceLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AppearanceLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.AppearanceLabel.Location = new System.Drawing.Point(28, 4);
			this.AppearanceLabel.Name = "AppearanceLabel";
			this.AppearanceLabel.Size = new System.Drawing.Size(552, 16);
			this.AppearanceLabel.TabIndex = 1;
			this.AppearanceLabel.Text = "Appearance";
			// 
			// AppearancePicture
			// 
			this.AppearancePicture.BackColor = System.Drawing.SystemColors.Control;
			this.AppearancePicture.Location = new System.Drawing.Point(4, 0);
			this.AppearancePicture.Name = "AppearancePicture";
			this.AppearancePicture.Size = new System.Drawing.Size(588, 24);
			this.AppearancePicture.TabIndex = 0;
			this.AppearancePicture.TabStop = false;
			this.AppearancePicture.Click += new System.EventHandler(this.AppearancePicture_Click);
			// 
			// BehaviorPanel
			// 
			this.BehaviorPanel.BackColor = System.Drawing.SystemColors.Control;
			this.BehaviorPanel.Controls.Add(this.panel1);
			this.BehaviorPanel.Controls.Add(this.BehaviorLabel);
			this.BehaviorPanel.Controls.Add(this.BehaviorPicture);
			this.BehaviorPanel.ForeColor = System.Drawing.Color.Black;
			this.BehaviorPanel.Location = new System.Drawing.Point(5, 172);
			this.BehaviorPanel.Name = "BehaviorPanel";
			this.BehaviorPanel.Size = new System.Drawing.Size(596, 24);
			this.BehaviorPanel.TabIndex = 4;
			this.BehaviorPanel.Tag = "232";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.ManageExcludedTasksLink);
			this.panel1.Controls.Add(this.ExcludedTasksLabel);
			this.panel1.Controls.Add(this.PopUpKeyLabel);
			this.panel1.Controls.Add(this.PopKey);
			this.panel1.Controls.Add(this.StartWithWindows);
			this.panel1.Controls.Add(this.BehaviorOtherSettingsLabel);
			this.panel1.Controls.Add(this.OpenOnMouseOver);
			this.panel1.Controls.Add(this.OpenInStart);
			this.panel1.Controls.Add(this.ClickAndMouseResponseLabel);
			this.panel1.Controls.Add(this.ZoomOut);
			this.panel1.Controls.Add(this.SlideOut);
			this.panel1.Controls.Add(this.SpinOut);
			this.panel1.Controls.Add(this.TransitionEffectsLabel);
			this.panel1.Location = new System.Drawing.Point(4, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(588, 204);
			this.panel1.TabIndex = 2;
			// 
			// ManageExcludedTasksLink
			// 
			this.ManageExcludedTasksLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ManageExcludedTasksLink.Location = new System.Drawing.Point(288, 20);
			this.ManageExcludedTasksLink.Name = "ManageExcludedTasksLink";
			this.ManageExcludedTasksLink.Size = new System.Drawing.Size(272, 16);
			this.ManageExcludedTasksLink.TabIndex = 11;
			this.ManageExcludedTasksLink.TabStop = true;
			this.ManageExcludedTasksLink.Text = "Manage Excluded Tasks...";
			this.ManageExcludedTasksLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ManageExcludedTasksLink_LinkClicked);
			// 
			// ExcludedTasksLabel
			// 
			this.ExcludedTasksLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ExcludedTasksLabel.Location = new System.Drawing.Point(284, 4);
			this.ExcludedTasksLabel.Name = "ExcludedTasksLabel";
			this.ExcludedTasksLabel.Size = new System.Drawing.Size(276, 16);
			this.ExcludedTasksLabel.TabIndex = 10;
			this.ExcludedTasksLabel.Text = "Excluded Tasks";
			// 
			// PopUpKeyLabel
			// 
			this.PopUpKeyLabel.Location = new System.Drawing.Point(4, 180);
			this.PopUpKeyLabel.Name = "PopUpKeyLabel";
			this.PopUpKeyLabel.Size = new System.Drawing.Size(120, 16);
			this.PopUpKeyLabel.TabIndex = 9;
			this.PopUpKeyLabel.Text = "Pop Up Key:";
			// 
			// PopKey
			// 
			this.PopKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PopKey.Items.AddRange(new object[] {
														"Win+W Only",
														"Left Mouse Button",
														"Right Mouse Button",
														"Middle Mouse Button",
														"Action 1 Mouse Button",
														"Action 2 Mouse Button"});
			this.PopKey.Location = new System.Drawing.Point(124, 176);
			this.PopKey.Name = "PopKey";
			this.PopKey.Size = new System.Drawing.Size(156, 21);
			this.PopKey.TabIndex = 7;
			this.PopKey.SelectedIndexChanged += new System.EventHandler(this.PopKey_SelectedIndexChanged);
			// 
			// StartWithWindows
			// 
			this.StartWithWindows.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.StartWithWindows.Location = new System.Drawing.Point(8, 160);
			this.StartWithWindows.Name = "StartWithWindows";
			this.StartWithWindows.Size = new System.Drawing.Size(272, 16);
			this.StartWithWindows.TabIndex = 6;
			this.StartWithWindows.Text = "Start with Windows";
			this.StartWithWindows.CheckedChanged += new System.EventHandler(this.StartWithWindows_CheckedChanged);
			// 
			// BehaviorOtherSettingsLabel
			// 
			this.BehaviorOtherSettingsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BehaviorOtherSettingsLabel.Location = new System.Drawing.Point(4, 140);
			this.BehaviorOtherSettingsLabel.Name = "BehaviorOtherSettingsLabel";
			this.BehaviorOtherSettingsLabel.Size = new System.Drawing.Size(276, 16);
			this.BehaviorOtherSettingsLabel.TabIndex = 7;
			this.BehaviorOtherSettingsLabel.Text = "Other Settings";
			// 
			// OpenOnMouseOver
			// 
			this.OpenOnMouseOver.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.OpenOnMouseOver.Location = new System.Drawing.Point(8, 116);
			this.OpenOnMouseOver.Name = "OpenOnMouseOver";
			this.OpenOnMouseOver.Size = new System.Drawing.Size(272, 16);
			this.OpenOnMouseOver.TabIndex = 5;
			this.OpenOnMouseOver.Text = "Open folders on Mouse-Over";
			this.OpenOnMouseOver.CheckedChanged += new System.EventHandler(this.OpenOnMouseOver_CheckedChanged);
			// 
			// OpenInStart
			// 
			this.OpenInStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.OpenInStart.Location = new System.Drawing.Point(8, 100);
			this.OpenInStart.Name = "OpenInStart";
			this.OpenInStart.Size = new System.Drawing.Size(272, 16);
			this.OpenInStart.TabIndex = 3;
			this.OpenInStart.Text = "Always open folders in the same loop";
			this.OpenInStart.CheckedChanged += new System.EventHandler(this.OpenInStart_CheckedChanged);
			// 
			// ClickAndMouseResponseLabel
			// 
			this.ClickAndMouseResponseLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ClickAndMouseResponseLabel.Location = new System.Drawing.Point(4, 80);
			this.ClickAndMouseResponseLabel.Name = "ClickAndMouseResponseLabel";
			this.ClickAndMouseResponseLabel.Size = new System.Drawing.Size(276, 16);
			this.ClickAndMouseResponseLabel.TabIndex = 4;
			this.ClickAndMouseResponseLabel.Text = "Click and Mouse Response";
			// 
			// ZoomOut
			// 
			this.ZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ZoomOut.Location = new System.Drawing.Point(8, 56);
			this.ZoomOut.Name = "ZoomOut";
			this.ZoomOut.Size = new System.Drawing.Size(272, 16);
			this.ZoomOut.TabIndex = 2;
			this.ZoomOut.Text = "Zoom";
			this.ZoomOut.CheckedChanged += new System.EventHandler(this.ZoomOut_CheckedChanged);
			// 
			// SlideOut
			// 
			this.SlideOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SlideOut.Location = new System.Drawing.Point(8, 40);
			this.SlideOut.Name = "SlideOut";
			this.SlideOut.Size = new System.Drawing.Size(272, 16);
			this.SlideOut.TabIndex = 1;
			this.SlideOut.Text = "Slide";
			this.SlideOut.CheckedChanged += new System.EventHandler(this.SlideOut_CheckedChanged);
			// 
			// SpinOut
			// 
			this.SpinOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SpinOut.Location = new System.Drawing.Point(8, 24);
			this.SpinOut.Name = "SpinOut";
			this.SpinOut.Size = new System.Drawing.Size(272, 16);
			this.SpinOut.TabIndex = 0;
			this.SpinOut.Text = "Spin";
			this.SpinOut.CheckedChanged += new System.EventHandler(this.SpinOut_CheckedChanged);
			// 
			// TransitionEffectsLabel
			// 
			this.TransitionEffectsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TransitionEffectsLabel.Location = new System.Drawing.Point(4, 4);
			this.TransitionEffectsLabel.Name = "TransitionEffectsLabel";
			this.TransitionEffectsLabel.Size = new System.Drawing.Size(276, 16);
			this.TransitionEffectsLabel.TabIndex = 0;
			this.TransitionEffectsLabel.Text = "Transition Effects";
			// 
			// BehaviorLabel
			// 
			this.BehaviorLabel.BackColor = System.Drawing.Color.Transparent;
			this.BehaviorLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BehaviorLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BehaviorLabel.Location = new System.Drawing.Point(28, 4);
			this.BehaviorLabel.Name = "BehaviorLabel";
			this.BehaviorLabel.Size = new System.Drawing.Size(552, 16);
			this.BehaviorLabel.TabIndex = 1;
			this.BehaviorLabel.Text = "Behavior";
			// 
			// BehaviorPicture
			// 
			this.BehaviorPicture.BackColor = System.Drawing.SystemColors.Control;
			this.BehaviorPicture.Location = new System.Drawing.Point(4, 0);
			this.BehaviorPicture.Name = "BehaviorPicture";
			this.BehaviorPicture.Size = new System.Drawing.Size(588, 24);
			this.BehaviorPicture.TabIndex = 0;
			this.BehaviorPicture.TabStop = false;
			this.BehaviorPicture.Click += new System.EventHandler(this.BehaviorPicture_Click);
			// 
			// OrbitConfigurationDescriptionLabel
			// 
			this.OrbitConfigurationDescriptionLabel.BackColor = System.Drawing.Color.White;
			this.OrbitConfigurationDescriptionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OrbitConfigurationDescriptionLabel.Location = new System.Drawing.Point(56, 36);
			this.OrbitConfigurationDescriptionLabel.Name = "OrbitConfigurationDescriptionLabel";
			this.OrbitConfigurationDescriptionLabel.Size = new System.Drawing.Size(472, 16);
			this.OrbitConfigurationDescriptionLabel.TabIndex = 8;
			this.OrbitConfigurationDescriptionLabel.Text = "Customize the way that Orbit shows up on your screen";
			// 
			// ItemPicture
			// 
			this.ItemPicture.Image = ((System.Drawing.Image)(resources.GetObject("ItemPicture.Image")));
			this.ItemPicture.Location = new System.Drawing.Point(532, 4);
			this.ItemPicture.Name = "ItemPicture";
			this.ItemPicture.Size = new System.Drawing.Size(24, 24);
			this.ItemPicture.TabIndex = 9;
			this.ItemPicture.TabStop = false;
			this.ItemPicture.Visible = false;
			// 
			// ItemSelectedPicture
			// 
			this.ItemSelectedPicture.Image = ((System.Drawing.Image)(resources.GetObject("ItemSelectedPicture.Image")));
			this.ItemSelectedPicture.Location = new System.Drawing.Point(556, 4);
			this.ItemSelectedPicture.Name = "ItemSelectedPicture";
			this.ItemSelectedPicture.Size = new System.Drawing.Size(24, 24);
			this.ItemSelectedPicture.TabIndex = 10;
			this.ItemSelectedPicture.TabStop = false;
			this.ItemSelectedPicture.Visible = false;
			// 
			// SavePictureBox
			// 
			this.SavePictureBox.BackColor = System.Drawing.Color.White;
			this.SavePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SavePictureBox.Image")));
			this.SavePictureBox.Location = new System.Drawing.Point(580, 4);
			this.SavePictureBox.Name = "SavePictureBox";
			this.SavePictureBox.Size = new System.Drawing.Size(24, 24);
			this.SavePictureBox.TabIndex = 11;
			this.SavePictureBox.TabStop = false;
			this.SavePictureBox.Click += new System.EventHandler(this.SavePictureBox_Click);
			// 
			// AboutPanel
			// 
			this.AboutPanel.BackColor = System.Drawing.SystemColors.Control;
			this.AboutPanel.Controls.Add(this.panel6);
			this.AboutPanel.Controls.Add(this.AboutLabel);
			this.AboutPanel.Controls.Add(this.AboutPicture);
			this.AboutPanel.ForeColor = System.Drawing.Color.Black;
			this.AboutPanel.Location = new System.Drawing.Point(5, 200);
			this.AboutPanel.Name = "AboutPanel";
			this.AboutPanel.Size = new System.Drawing.Size(596, 24);
			this.AboutPanel.TabIndex = 5;
			this.AboutPanel.Tag = "132";
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.Color.White;
			this.panel6.Controls.Add(this.label19);
			this.panel6.Controls.Add(this.VersionInfo);
			this.panel6.Controls.Add(this.VersionInformationLabel);
			this.panel6.Controls.Add(this.ImproveLink);
			this.panel6.Controls.Add(this.BugLink);
			this.panel6.Controls.Add(this.HelpTheProjectLabel);
			this.panel6.Controls.Add(this.AuthorLink);
			this.panel6.Controls.Add(this.HomepageLink);
			this.panel6.Controls.Add(this.AuthorLabel);
			this.panel6.Controls.Add(this.OrbitDescriptionLabel);
			this.panel6.Controls.Add(this.ProjectName);
			this.panel6.Controls.Add(this.pictureBox1);
			this.panel6.Location = new System.Drawing.Point(4, 24);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(588, 104);
			this.panel6.TabIndex = 2;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(416, 44);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(104, 56);
			this.label19.TabIndex = 12;
			this.label19.Text = "Orbit:\nOrbit Configuration:\nItem Configuration:\nOrbit Services:";
			// 
			// VersionInfo
			// 
			this.VersionInfo.Location = new System.Drawing.Point(520, 44);
			this.VersionInfo.Name = "VersionInfo";
			this.VersionInfo.Size = new System.Drawing.Size(64, 56);
			this.VersionInfo.TabIndex = 11;
			this.VersionInfo.Text = "Not present";
			// 
			// VersionInformationLabel
			// 
			this.VersionInformationLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.VersionInformationLabel.Location = new System.Drawing.Point(412, 28);
			this.VersionInformationLabel.Name = "VersionInformationLabel";
			this.VersionInformationLabel.Size = new System.Drawing.Size(168, 16);
			this.VersionInformationLabel.TabIndex = 10;
			this.VersionInformationLabel.Text = "Version Information";
			// 
			// ImproveLink
			// 
			this.ImproveLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ImproveLink.Location = new System.Drawing.Point(236, 84);
			this.ImproveLink.Name = "ImproveLink";
			this.ImproveLink.Size = new System.Drawing.Size(172, 16);
			this.ImproveLink.TabIndex = 3;
			this.ImproveLink.TabStop = true;
			this.ImproveLink.Text = "Suggest an improvement";
			this.ImproveLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ImproveLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ImproveLink_LinkClicked);
			// 
			// BugLink
			// 
			this.BugLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BugLink.Location = new System.Drawing.Point(236, 68);
			this.BugLink.Name = "BugLink";
			this.BugLink.Size = new System.Drawing.Size(172, 16);
			this.BugLink.TabIndex = 2;
			this.BugLink.TabStop = true;
			this.BugLink.Text = "Report a bug";
			this.BugLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BugLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BugLink_LinkClicked);
			// 
			// HelpTheProjectLabel
			// 
			this.HelpTheProjectLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HelpTheProjectLabel.Location = new System.Drawing.Point(232, 52);
			this.HelpTheProjectLabel.Name = "HelpTheProjectLabel";
			this.HelpTheProjectLabel.Size = new System.Drawing.Size(172, 16);
			this.HelpTheProjectLabel.TabIndex = 7;
			this.HelpTheProjectLabel.Text = "Help the Project";
			// 
			// AuthorLink
			// 
			this.AuthorLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.AuthorLink.Location = new System.Drawing.Point(100, 68);
			this.AuthorLink.Name = "AuthorLink";
			this.AuthorLink.Size = new System.Drawing.Size(112, 16);
			this.AuthorLink.TabIndex = 0;
			this.AuthorLink.TabStop = true;
			this.AuthorLink.Text = "Lucas Mendes Menge";
			this.AuthorLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.AuthorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuthorLink_LinkClicked);
			// 
			// HomepageLink
			// 
			this.HomepageLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.HomepageLink.Location = new System.Drawing.Point(100, 84);
			this.HomepageLink.Name = "HomepageLink";
			this.HomepageLink.Size = new System.Drawing.Size(132, 16);
			this.HomepageLink.TabIndex = 1;
			this.HomepageLink.TabStop = true;
			this.HomepageLink.Text = "Official Homepage";
			this.HomepageLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.HomepageLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HomepageLink_LinkClicked);
			// 
			// AuthorLabel
			// 
			this.AuthorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AuthorLabel.Location = new System.Drawing.Point(96, 52);
			this.AuthorLabel.Name = "AuthorLabel";
			this.AuthorLabel.Size = new System.Drawing.Size(132, 16);
			this.AuthorLabel.TabIndex = 3;
			this.AuthorLabel.Text = "Author";
			// 
			// OrbitDescriptionLabel
			// 
			this.OrbitDescriptionLabel.Location = new System.Drawing.Point(96, 32);
			this.OrbitDescriptionLabel.Name = "OrbitDescriptionLabel";
			this.OrbitDescriptionLabel.Size = new System.Drawing.Size(312, 16);
			this.OrbitDescriptionLabel.TabIndex = 2;
			this.OrbitDescriptionLabel.Text = "The foating round dock for Windows";
			// 
			// ProjectName
			// 
			this.ProjectName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ProjectName.Location = new System.Drawing.Point(92, 0);
			this.ProjectName.Name = "ProjectName";
			this.ProjectName.Size = new System.Drawing.Size(316, 32);
			this.ProjectName.TabIndex = 1;
			this.ProjectName.Text = "Orbit";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(4, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 96);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// AboutLabel
			// 
			this.AboutLabel.BackColor = System.Drawing.Color.Transparent;
			this.AboutLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AboutLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.AboutLabel.Location = new System.Drawing.Point(28, 4);
			this.AboutLabel.Name = "AboutLabel";
			this.AboutLabel.Size = new System.Drawing.Size(552, 16);
			this.AboutLabel.TabIndex = 1;
			this.AboutLabel.Text = "About";
			// 
			// AboutPicture
			// 
			this.AboutPicture.BackColor = System.Drawing.SystemColors.Control;
			this.AboutPicture.Location = new System.Drawing.Point(4, 0);
			this.AboutPicture.Name = "AboutPicture";
			this.AboutPicture.Size = new System.Drawing.Size(588, 24);
			this.AboutPicture.TabIndex = 0;
			this.AboutPicture.TabStop = false;
			this.AboutPicture.Click += new System.EventHandler(this.AboutPicture_Click);
			// 
			// FontsPanel
			// 
			this.FontsPanel.BackColor = System.Drawing.SystemColors.Control;
			this.FontsPanel.Controls.Add(this.panel7);
			this.FontsPanel.Controls.Add(this.FontsLabel);
			this.FontsPanel.Controls.Add(this.FontsPicture);
			this.FontsPanel.ForeColor = System.Drawing.Color.Black;
			this.FontsPanel.Location = new System.Drawing.Point(4, 116);
			this.FontsPanel.Name = "FontsPanel";
			this.FontsPanel.Size = new System.Drawing.Size(596, 24);
			this.FontsPanel.TabIndex = 2;
			this.FontsPanel.Tag = "188";
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.Color.White;
			this.panel7.Controls.Add(this.DescriptionBorder);
			this.panel7.Controls.Add(this.LabelBorder);
			this.panel7.Controls.Add(this.DescriptionColorChange);
			this.panel7.Controls.Add(this.DescriptionBorderColorChange);
			this.panel7.Controls.Add(this.DescriptionColor);
			this.panel7.Controls.Add(this.DescriptionBorderColor);
			this.panel7.Controls.Add(this.DescriptionFillLabel);
			this.panel7.Controls.Add(this.DescriptionBorderLabel);
			this.panel7.Controls.Add(this.DescriptionColorsLabel);
			this.panel7.Controls.Add(this.DescriptionFontChangeLink);
			this.panel7.Controls.Add(this.DescriptionFontFamilyDisplay);
			this.panel7.Controls.Add(this.DescriptionFontLabel);
			this.panel7.Controls.Add(this.LabelColorChange);
			this.panel7.Controls.Add(this.LabelBorderColorChange);
			this.panel7.Controls.Add(this.LabelColor);
			this.panel7.Controls.Add(this.LabelBorderColor);
			this.panel7.Controls.Add(this.LabelFillLabel);
			this.panel7.Controls.Add(this.LabelBorderLabel);
			this.panel7.Controls.Add(this.LabelColorsLabel);
			this.panel7.Controls.Add(this.LabelFontChangeLink);
			this.panel7.Controls.Add(this.LabelFontFamilyDisplay);
			this.panel7.Controls.Add(this.LabelFontLabel);
			this.panel7.Location = new System.Drawing.Point(4, 24);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(588, 160);
			this.panel7.TabIndex = 2;
			// 
			// DescriptionBorder
			// 
			this.DescriptionBorder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.DescriptionBorder.Location = new System.Drawing.Point(240, 140);
			this.DescriptionBorder.Name = "DescriptionBorder";
			this.DescriptionBorder.Size = new System.Drawing.Size(204, 16);
			this.DescriptionBorder.TabIndex = 7;
			this.DescriptionBorder.Text = "Display border";
			this.DescriptionBorder.CheckedChanged += new System.EventHandler(this.DescriptionBorder_CheckedChanged);
			// 
			// LabelBorder
			// 
			this.LabelBorder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.LabelBorder.Location = new System.Drawing.Point(240, 60);
			this.LabelBorder.Name = "LabelBorder";
			this.LabelBorder.Size = new System.Drawing.Size(208, 16);
			this.LabelBorder.TabIndex = 3;
			this.LabelBorder.Text = "Display border";
			this.LabelBorder.CheckedChanged += new System.EventHandler(this.LabelBorder_CheckedChanged);
			// 
			// DescriptionColorChange
			// 
			this.DescriptionColorChange.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.DescriptionColorChange.Location = new System.Drawing.Point(356, 120);
			this.DescriptionColorChange.Name = "DescriptionColorChange";
			this.DescriptionColorChange.Size = new System.Drawing.Size(228, 16);
			this.DescriptionColorChange.TabIndex = 6;
			this.DescriptionColorChange.TabStop = true;
			this.DescriptionColorChange.Text = "Change fill color...";
			this.DescriptionColorChange.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.DescriptionColorChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DescriptionColorChange_LinkClicked);
			// 
			// DescriptionBorderColorChange
			// 
			this.DescriptionBorderColorChange.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.DescriptionBorderColorChange.Location = new System.Drawing.Point(356, 100);
			this.DescriptionBorderColorChange.Name = "DescriptionBorderColorChange";
			this.DescriptionBorderColorChange.Size = new System.Drawing.Size(228, 16);
			this.DescriptionBorderColorChange.TabIndex = 5;
			this.DescriptionBorderColorChange.TabStop = true;
			this.DescriptionBorderColorChange.Text = "Change border color...";
			this.DescriptionBorderColorChange.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.DescriptionBorderColorChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DescriptionBorderColorChange_LinkClicked);
			// 
			// DescriptionColor
			// 
			this.DescriptionColor.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(50)), ((System.Byte)(50)), ((System.Byte)(50)));
			this.DescriptionColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DescriptionColor.Location = new System.Drawing.Point(324, 120);
			this.DescriptionColor.Name = "DescriptionColor";
			this.DescriptionColor.Size = new System.Drawing.Size(28, 16);
			this.DescriptionColor.TabIndex = 17;
			this.DescriptionColor.TabStop = false;
			// 
			// DescriptionBorderColor
			// 
			this.DescriptionBorderColor.BackColor = System.Drawing.Color.DarkGray;
			this.DescriptionBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DescriptionBorderColor.Location = new System.Drawing.Point(324, 100);
			this.DescriptionBorderColor.Name = "DescriptionBorderColor";
			this.DescriptionBorderColor.Size = new System.Drawing.Size(28, 16);
			this.DescriptionBorderColor.TabIndex = 16;
			this.DescriptionBorderColor.TabStop = false;
			// 
			// DescriptionFillLabel
			// 
			this.DescriptionFillLabel.Location = new System.Drawing.Point(240, 120);
			this.DescriptionFillLabel.Name = "DescriptionFillLabel";
			this.DescriptionFillLabel.Size = new System.Drawing.Size(80, 16);
			this.DescriptionFillLabel.TabIndex = 15;
			this.DescriptionFillLabel.Text = "Fill";
			// 
			// DescriptionBorderLabel
			// 
			this.DescriptionBorderLabel.Location = new System.Drawing.Point(240, 100);
			this.DescriptionBorderLabel.Name = "DescriptionBorderLabel";
			this.DescriptionBorderLabel.Size = new System.Drawing.Size(80, 16);
			this.DescriptionBorderLabel.TabIndex = 14;
			this.DescriptionBorderLabel.Text = "Border";
			// 
			// DescriptionColorsLabel
			// 
			this.DescriptionColorsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DescriptionColorsLabel.Location = new System.Drawing.Point(236, 84);
			this.DescriptionColorsLabel.Name = "DescriptionColorsLabel";
			this.DescriptionColorsLabel.Size = new System.Drawing.Size(212, 16);
			this.DescriptionColorsLabel.TabIndex = 13;
			this.DescriptionColorsLabel.Text = "Description Colors";
			// 
			// DescriptionFontChangeLink
			// 
			this.DescriptionFontChangeLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.DescriptionFontChangeLink.Location = new System.Drawing.Point(8, 116);
			this.DescriptionFontChangeLink.Name = "DescriptionFontChangeLink";
			this.DescriptionFontChangeLink.Size = new System.Drawing.Size(224, 16);
			this.DescriptionFontChangeLink.TabIndex = 4;
			this.DescriptionFontChangeLink.TabStop = true;
			this.DescriptionFontChangeLink.Text = "Change this font family...";
			this.DescriptionFontChangeLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.DescriptionFontChangeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DescriptionFontChangeLink_LinkClicked);
			// 
			// DescriptionFontFamilyDisplay
			// 
			this.DescriptionFontFamilyDisplay.Location = new System.Drawing.Point(8, 100);
			this.DescriptionFontFamilyDisplay.Name = "DescriptionFontFamilyDisplay";
			this.DescriptionFontFamilyDisplay.Size = new System.Drawing.Size(224, 16);
			this.DescriptionFontFamilyDisplay.TabIndex = 11;
			this.DescriptionFontFamilyDisplay.Text = "Tahoma, 10";
			// 
			// DescriptionFontLabel
			// 
			this.DescriptionFontLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DescriptionFontLabel.Location = new System.Drawing.Point(4, 84);
			this.DescriptionFontLabel.Name = "DescriptionFontLabel";
			this.DescriptionFontLabel.Size = new System.Drawing.Size(228, 16);
			this.DescriptionFontLabel.TabIndex = 10;
			this.DescriptionFontLabel.Text = "Description Font";
			// 
			// LabelColorChange
			// 
			this.LabelColorChange.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.LabelColorChange.Location = new System.Drawing.Point(356, 40);
			this.LabelColorChange.Name = "LabelColorChange";
			this.LabelColorChange.Size = new System.Drawing.Size(228, 16);
			this.LabelColorChange.TabIndex = 2;
			this.LabelColorChange.TabStop = true;
			this.LabelColorChange.Text = "Change fill color...";
			this.LabelColorChange.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.LabelColorChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LabelColorChange_LinkClicked);
			// 
			// LabelBorderColorChange
			// 
			this.LabelBorderColorChange.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.LabelBorderColorChange.Location = new System.Drawing.Point(356, 20);
			this.LabelBorderColorChange.Name = "LabelBorderColorChange";
			this.LabelBorderColorChange.Size = new System.Drawing.Size(228, 16);
			this.LabelBorderColorChange.TabIndex = 1;
			this.LabelBorderColorChange.TabStop = true;
			this.LabelBorderColorChange.Text = "Change border color...";
			this.LabelBorderColorChange.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.LabelBorderColorChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LabelBorderColorChange_LinkClicked);
			// 
			// LabelColor
			// 
			this.LabelColor.BackColor = System.Drawing.Color.Black;
			this.LabelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LabelColor.Location = new System.Drawing.Point(324, 40);
			this.LabelColor.Name = "LabelColor";
			this.LabelColor.Size = new System.Drawing.Size(28, 16);
			this.LabelColor.TabIndex = 7;
			this.LabelColor.TabStop = false;
			// 
			// LabelBorderColor
			// 
			this.LabelBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LabelBorderColor.Location = new System.Drawing.Point(324, 20);
			this.LabelBorderColor.Name = "LabelBorderColor";
			this.LabelBorderColor.Size = new System.Drawing.Size(28, 16);
			this.LabelBorderColor.TabIndex = 6;
			this.LabelBorderColor.TabStop = false;
			// 
			// LabelFillLabel
			// 
			this.LabelFillLabel.Location = new System.Drawing.Point(240, 40);
			this.LabelFillLabel.Name = "LabelFillLabel";
			this.LabelFillLabel.Size = new System.Drawing.Size(80, 16);
			this.LabelFillLabel.TabIndex = 5;
			this.LabelFillLabel.Text = "Fill";
			// 
			// LabelBorderLabel
			// 
			this.LabelBorderLabel.Location = new System.Drawing.Point(240, 20);
			this.LabelBorderLabel.Name = "LabelBorderLabel";
			this.LabelBorderLabel.Size = new System.Drawing.Size(80, 16);
			this.LabelBorderLabel.TabIndex = 4;
			this.LabelBorderLabel.Text = "Border";
			// 
			// LabelColorsLabel
			// 
			this.LabelColorsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LabelColorsLabel.Location = new System.Drawing.Point(236, 4);
			this.LabelColorsLabel.Name = "LabelColorsLabel";
			this.LabelColorsLabel.Size = new System.Drawing.Size(212, 16);
			this.LabelColorsLabel.TabIndex = 3;
			this.LabelColorsLabel.Text = "Label Colors";
			// 
			// LabelFontChangeLink
			// 
			this.LabelFontChangeLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.LabelFontChangeLink.Location = new System.Drawing.Point(8, 36);
			this.LabelFontChangeLink.Name = "LabelFontChangeLink";
			this.LabelFontChangeLink.Size = new System.Drawing.Size(224, 16);
			this.LabelFontChangeLink.TabIndex = 0;
			this.LabelFontChangeLink.TabStop = true;
			this.LabelFontChangeLink.Text = "Change this font family...";
			this.LabelFontChangeLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.LabelFontChangeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LabelFontChangeLink_LinkClicked);
			// 
			// LabelFontFamilyDisplay
			// 
			this.LabelFontFamilyDisplay.Location = new System.Drawing.Point(8, 20);
			this.LabelFontFamilyDisplay.Name = "LabelFontFamilyDisplay";
			this.LabelFontFamilyDisplay.Size = new System.Drawing.Size(224, 16);
			this.LabelFontFamilyDisplay.TabIndex = 1;
			this.LabelFontFamilyDisplay.Text = "Tahoma, 15";
			// 
			// LabelFontLabel
			// 
			this.LabelFontLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LabelFontLabel.Location = new System.Drawing.Point(4, 4);
			this.LabelFontLabel.Name = "LabelFontLabel";
			this.LabelFontLabel.Size = new System.Drawing.Size(228, 16);
			this.LabelFontLabel.TabIndex = 0;
			this.LabelFontLabel.Text = "Label Font";
			// 
			// FontsLabel
			// 
			this.FontsLabel.BackColor = System.Drawing.Color.Transparent;
			this.FontsLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FontsLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.FontsLabel.Location = new System.Drawing.Point(28, 4);
			this.FontsLabel.Name = "FontsLabel";
			this.FontsLabel.Size = new System.Drawing.Size(552, 16);
			this.FontsLabel.TabIndex = 1;
			this.FontsLabel.Text = "Fonts";
			// 
			// FontsPicture
			// 
			this.FontsPicture.BackColor = System.Drawing.SystemColors.Control;
			this.FontsPicture.Location = new System.Drawing.Point(4, 0);
			this.FontsPicture.Name = "FontsPicture";
			this.FontsPicture.Size = new System.Drawing.Size(588, 24);
			this.FontsPicture.TabIndex = 0;
			this.FontsPicture.TabStop = false;
			this.FontsPicture.Click += new System.EventHandler(this.FontsPicture_Click);
			// 
			// DockSetup
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(606, 455);
			this.ControlBox = false;
			this.Controls.Add(this.LocationsPanel);
			this.Controls.Add(this.ImagesPanel);
			this.Controls.Add(this.FontsPanel);
			this.Controls.Add(this.AppearancePanel);
			this.Controls.Add(this.BehaviorPanel);
			this.Controls.Add(this.AboutPanel);
			this.Controls.Add(this.SavePictureBox);
			this.Controls.Add(this.ItemSelectedPicture);
			this.Controls.Add(this.ItemPicture);
			this.Controls.Add(this.OrbitConfigurationDescriptionLabel);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.OrbitConfigurationLabel);
			this.Controls.Add(this.ClosePictureBox);
			this.Controls.Add(this.pictureBox2);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "DockSetup";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Orbit Configuration";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.LocationsPanel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ImagesPanel.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.AppearancePanel.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AppearanceBar)).EndInit();
			this.BehaviorPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.AboutPanel.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.FontsPanel.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Form Event Handlers
		#region Form
		private void Form2_Load(object sender, System.EventArgs e)
		{
			this.LocationsPicture.Image=this.ItemPicture.Image;
			this.ImagesPicture.Image=this.ItemPicture.Image;
			this.FontsPicture.Image=this.ItemPicture.Image;
			this.AppearancePicture.Image=this.ItemPicture.Image;
			this.BehaviorPicture.Image=this.ItemPicture.Image;
			this.AboutPicture.Image=this.ItemPicture.Image;

			this.LocationsLabel.Click+=new EventHandler(LocationsPicture_Click);
			this.ImagesLabel.Click+=new EventHandler(ImagesPicture_Click);
			this.FontsLabel.Click+=new EventHandler(FontsPicture_Click);
			this.AppearanceLabel.Click+=new EventHandler(AppearancePicture_Click);
			this.BehaviorLabel.Click+=new EventHandler(BehaviorPicture_Click);
			this.AboutLabel.Click+=new EventHandler(AboutPicture_Click);

			try
			{
				System.Reflection.AssemblyName OrbitName = System.Reflection.AssemblyName.GetAssemblyName(ApplicationPath+"Orbit.exe");
				this.VersionInfo.Text=OrbitName.Version.ToString();
			}
			catch(Exception)
			{
				this.VersionInfo.Text="Not present";
			}
			try
			{
				System.Reflection.AssemblyName SetupName = System.Reflection.AssemblyName.GetAssemblyName(ApplicationPath+"DockSetup.dll");
				this.VersionInfo.Text+="\n"+SetupName.Version.ToString();
			}
			catch(Exception)
			{
				this.VersionInfo.Text+="\nNot present";
			}
			try
			{
				System.Reflection.AssemblyName ItemName = System.Reflection.AssemblyName.GetAssemblyName(ApplicationPath+"ItemSetup.dll");
				this.VersionInfo.Text+="\n"+ItemName.Version.ToString();
			}
			catch(Exception)
			{
				this.VersionInfo.Text+="\nNot present";
			}
			try
			{
				System.Reflection.AssemblyName ServicesName = System.Reflection.AssemblyName.GetAssemblyName(ApplicationPath+"OrbitServices.dll");
				this.VersionInfo.Text+="\n"+ServicesName.Version.ToString();
			}
			catch(Exception)
			{
				this.VersionInfo.Text+="\nNot present";
			}

			this.ProjectName.Text="Orbit Agora Project";
			this.ShowPanel(this.AboutPanel);
			this.AboutPicture.Image=this.ItemSelectedPicture.Image;

			// disable true transparency if not supported by OS
			if(System.Environment.OSVersion.Platform != System.PlatformID.Win32NT)
			{
				this.UseTransparency.Enabled=false;
				this.UseTransparency.Checked=false;
			}
		}

		private void SavePictureBox_Click(object sender, System.EventArgs e)
		{
			bool CanSave=true;
			//string Message="Cannot save configuration.";
			string Message="";
			if(!System.IO.Directory.Exists(ProgramConfig.Locations.ImagesPath))
			{
				CanSave=false;
				//Message+="\nYou must specify an image directory.";
				Message+=Global.LanguageLoader.Language.DockSetup.Messages.NeedsImageDirectory;
			}
			/*
			if(!System.IO.Directory.Exists(ProgramConfig.ItemsPath))
			{
				CanSave=false;
				Message+="\nYou must specify an item directory.";
			}
			*/
			if(CanSave)
			{
				//this.SaveConfig();
				//ProgramConfig.UpdateRuntimeValues();
				ProgramConfig.SaveToINI();
				Global.Configuration=ProgramConfig;
				Global.Configuration.UpdateRuntimeValues();
				//ProgramConfig.SaveToXML();
				this.DialogResult=DialogResult.OK;
			}
			else
			{
				MessageBox.Show(Message, Global.LanguageLoader.Language.DockSetup.OrbitConfigurationLabel, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void ClosePictureBox_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
		}

		#endregion

		#region Panel Code
		private void LocationsPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.LocationsPanel);
			if(this.LocationsPanel.Height!=24)
			{
				this.LocationsPicture.Image=this.ItemSelectedPicture.Image;
			}
		}

		private void ImagesPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.ImagesPanel);
			if(this.ImagesPanel.Height!=24)
			{
				this.ImagesPicture.Image=this.ItemSelectedPicture.Image;
			}
		}

		private void FontsPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.FontsPanel);
			if(this.FontsPanel.Height!=24)
			{
				this.FontsPicture.Image=this.ItemSelectedPicture.Image;
			}
			this.LabelFontFamilyDisplay.Text=ProgramConfig.Fonts.LabelFont+" - "+ProgramConfig.Fonts.LabelSize+" - "+ProgramConfig.Fonts.LabelStyle;
			this.DescriptionFontFamilyDisplay.Text=ProgramConfig.Fonts.DescriptionFont+" - "+ProgramConfig.Fonts.DescriptionSize+" - "+ProgramConfig.Fonts.DescriptionStyle;
		}
		private void AppearancePicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.AppearancePanel);
			if(this.AppearancePanel.Height!=24)
			{
				this.AppearancePicture.Image=this.ItemSelectedPicture.Image;
			}
		}

		private void BehaviorPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.BehaviorPanel);
			if(this.BehaviorPanel.Height!=24)
			{
				this.BehaviorPicture.Image=this.ItemSelectedPicture.Image;
			}
		}

		private void AboutPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.AboutPanel);
			if(this.AboutPanel.Height!=24)
			{
				this.AboutPicture.Image=this.ItemSelectedPicture.Image;
			}
		}
		#endregion

		#region Locations
		private void ItemsDirBrowse_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog FolderBrowse=new FolderBrowserDialog();
			FolderBrowse.Description="Browse for the Items Structure Location";
			FolderBrowse.SelectedPath=ApplicationPath;
			FolderBrowse.ShowDialog();
			if(System.IO.Directory.Exists(FolderBrowse.SelectedPath) && FolderBrowse.SelectedPath!=ApplicationPath)
			{
				this.ItemsDirDisplay.Text=FolderBrowse.SelectedPath+"\\";
				ProgramConfig.Locations.ItemsPath=FolderBrowse.SelectedPath+"\\";
			}
		}

		private void ImagesDirBrowse_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog FolderBrowse=new FolderBrowserDialog();
			FolderBrowse.Description="Browse for the Preferred Images Location";
			FolderBrowse.SelectedPath=ApplicationPath;
			FolderBrowse.ShowDialog();
			if(System.IO.Directory.Exists(FolderBrowse.SelectedPath) && FolderBrowse.SelectedPath!=ApplicationPath)
			{
				this.ImagesDirDisplay.Text=FolderBrowse.SelectedPath+"\\";
				ProgramConfig.Locations.ImagesPath=FolderBrowse.SelectedPath+"\\";
				//MessageBox.Show(FolderBrowse.SelectedPath+"\\\n"+ProgramConfig.Locations.ImagesPath);
			}
		}
		#endregion

		#region Images
		private void BgImageDisable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ProgramConfig.Images.BackgroundImagePath="";
			ProgramConfig.Images.UseWindowsWallpaper=false;
			this.BgImageUseWindowsLink.Enabled=true;
			this.BgImageDisable.Enabled=false;
			this.BgColorDisplay.BackColor=Color.FromArgb(0xFF, ProgramConfig.Images.BackgroundColor);

			Image buff=BgDisplay.Image;
			this.BgDisplay.Image=new Bitmap(1,1);
			if(buff!=null)
				buff.Dispose();
		}

		private void BgImageBrowse_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog FileBrowse=new OpenFileDialog();
			FileBrowse.Title="Browse for Background Image";
			FileBrowse.Multiselect=false;
			FileBrowse.ValidateNames=true;
			FileBrowse.CheckFileExists=true;
			FileBrowse.Filter="Supported Image Files|*.jpg;*.gif;*.png;*.tga;*.bmp";
			if(ProgramConfig.Images.BackgroundImagePath!="" && ProgramConfig.Images.BackgroundImagePath!=null)
				if(ProgramConfig.Images.BackgroundImagePath.IndexOf(":\\")>-1)
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(ProgramConfig.Images.BackgroundImagePath);
				else
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(System.IO.Path.Combine(ProgramConfig.Locations.ImagesPath, ProgramConfig.Images.BackgroundImagePath));
			else
				FileBrowse.InitialDirectory=ProgramConfig.Locations.ImagesPath;
			FileBrowse.ShowDialog();
			if(FileBrowse.FileName!=null && System.IO.File.Exists(FileBrowse.FileName))
			{
				/*
				if(FileBrowse.FileName.ToLower().IndexOf(".png")>=0 || FileBrowse.FileName.ToLower().IndexOf(".jpg")>=0 || FileBrowse.FileName.ToLower().IndexOf(".jpeg")>=0 || FileBrowse.FileName.ToLower().IndexOf(".bmp")>=0 || FileBrowse.FileName.ToLower().IndexOf(".gif")>=0 || FileBrowse.FileName.ToLower().IndexOf(".tga")>=0)
				{
					this.BgDisplay.Image=Image.FromFile(FileBrowse.FileName);
					ProgramConfig.BgImageSize=this.BgDisplay.Image.Size;
					ProgramConfig.BgImage=FileBrowse.FileName;
				}
				*/
				try
				{
					Image buff=BgDisplay.Image;
					using(Image wallbig=Image.FromFile(FileBrowse.FileName))
					{
						this.BgDisplay.Image=Orbit.Utilities.ImageHelper.GetAspectThumbnail(wallbig, this.BgDisplay.Size);
					}
					if(buff!=null)
						buff.Dispose();
					ProgramConfig.Images.BackgroundSize=this.BgDisplay.Image.Size;
					ProgramConfig.Images.BackgroundImagePath=FileBrowse.FileName;
					ProgramConfig.Images.UseWindowsWallpaper=false;
					this.BgImageUseWindowsLink.Enabled=true;
					this.BgImageDisable.Enabled=true;
				}
				catch(Exception)
				{
					this.BgDisplay.Image=Image.FromFile(ProgramConfig.Images.BackgroundImagePath);
					ProgramConfig.Images.BackgroundSize=this.BgDisplay.Image.Size;
				}
			}
		}

		private void BgImageUseWindowsLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.BgColorDisplay.BackColor=Color.FromKnownColor(KnownColor.Desktop);
			try
			{
				Microsoft.Win32.RegistryKey WallpaperKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop");
				string UsingWallpaper=(string)WallpaperKey.GetValue("Wallpaper");
				if(UsingWallpaper!="" && UsingWallpaper!=null)
				{
					string WallpaperPath=(string)WallpaperKey.GetValue("ConvertedWallpaper");
					//string WallpaperStyle=(string)WallpaperKey.GetValue("WallpaperStyle");
					Image buff=BgDisplay.Image;
					using(Image wallbig=Image.FromFile(UsingWallpaper))
					{
						this.BgDisplay.Image=Orbit.Utilities.ImageHelper.GetAspectThumbnail(wallbig, this.BgDisplay.Size);
					}
					if(buff!=null)
						buff.Dispose();
					ProgramConfig.Images.UseWindowsWallpaper=true;
					this.BgImageUseWindowsLink.Enabled=false;
					this.BgImageDisable.Enabled=true;
					ProgramConfig.Images.BackgroundImagePath="";
					ProgramConfig.Images.BackgroundSize=new Size(0,0);
				}
				else
				{
					this.BgImageUseWindowsLink.Enabled=true;
					this.ProgramConfig.Images.UseWindowsWallpaper=false;
					if(this.BgDisplay.Image!=null)
						this.BgDisplay.Image.Dispose();
				}
			}
			catch(Exception)
			{
				//MessageBox.Show(e.Message);
			}
		}
		private void IconBgImageDisable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ProgramConfig.Images.IconBackgroundImagePath="";
			Image buff=this.IconBgDisplay.Image;
			this.IconBgDisplay.Image=new Bitmap(1,1);
			if(buff!=null)
				buff.Dispose();
		}

		private void IconBgImageBrowse_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog FileBrowse=new OpenFileDialog();
			FileBrowse.Title="Browse for Icon Background Image";
			FileBrowse.Multiselect=false;
			FileBrowse.ValidateNames=true;
			FileBrowse.CheckFileExists=true;
			FileBrowse.Filter="Supported Image Files|*.jpg;*.gif;*.png;*.tga;*.bmp";
			if(ProgramConfig.Images.IconBackgroundImagePath!="" && ProgramConfig.Images.IconBackgroundImagePath!=null)
				if(ProgramConfig.Images.BackgroundImagePath.IndexOf(":\\")>-1)
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(ProgramConfig.Images.IconBackgroundImagePath);
				else
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(System.IO.Path.Combine(ProgramConfig.Locations.ImagesPath, ProgramConfig.Images.IconBackgroundImagePath));
			else
				FileBrowse.InitialDirectory=ProgramConfig.Locations.ImagesPath;
			FileBrowse.ShowDialog();
			if(FileBrowse.FileName!=null && System.IO.File.Exists(FileBrowse.FileName))
			{
				/*
				if(FileBrowse.FileName.ToLower().IndexOf(".png")>=0 || FileBrowse.FileName.ToLower().IndexOf(".jpg")>=0 || FileBrowse.FileName.ToLower().IndexOf(".jpeg")>=0 || FileBrowse.FileName.ToLower().IndexOf(".bmp")>=0 || FileBrowse.FileName.ToLower().IndexOf(".gif")>=0 || FileBrowse.FileName.ToLower().IndexOf(".tga")>=0)
				{
					this.BgDisplay.Image=Image.FromFile(FileBrowse.FileName);
					ProgramConfig.BgImageSize=this.BgDisplay.Image.Size;
					ProgramConfig.BgImage=FileBrowse.FileName;
				}
				*/
				try
				{
					Image buff=this.IconBgDisplay.Image;
					this.IconBgDisplay.Image=Image.FromFile(FileBrowse.FileName);
					if(buff!=null)
						buff.Dispose();
					ProgramConfig.Images.IconBackgroundSize=this.IconBgDisplay.Image.Size;
					ProgramConfig.Images.IconBackgroundImagePath=FileBrowse.FileName;
				}
				catch(Exception)
				{
					this.IconBgDisplay.Image=Image.FromFile(ProgramConfig.Images.IconBackgroundImagePath);
					ProgramConfig.Images.IconBackgroundSize=this.IconBgDisplay.Image.Size;
				}
			}
		}
		private void IconSelectedImageDisable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ProgramConfig.Images.IconSelectedImagePath="";
			Image buff=IconSelectedDisplay.Image;
			this.IconSelectedDisplay.Image=new Bitmap(1,1);
			if(buff!=null)
				buff.Dispose();
		}
		private void IconSelectedImageBrowse_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog FileBrowse=new OpenFileDialog();
			FileBrowse.Title="Browse for Selected Item Mark Image";
			FileBrowse.Multiselect=false;
			FileBrowse.ValidateNames=true;
			FileBrowse.CheckFileExists=true;
			FileBrowse.Filter="Supported Image Files|*.jpg;*.gif;*.png;*.tga;*.bmp";
			if(ProgramConfig.Images.IconSelectedImagePath!="" && ProgramConfig.Images.IconSelectedImagePath!=null)
				if(ProgramConfig.Images.BackgroundImagePath.IndexOf(":\\")>-1)
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(ProgramConfig.Images.IconSelectedImagePath);
				else
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(System.IO.Path.Combine(ProgramConfig.Locations.ImagesPath, ProgramConfig.Images.IconSelectedImagePath));
			else
				FileBrowse.InitialDirectory=ProgramConfig.Locations.ImagesPath;
			FileBrowse.ShowDialog();
			if(FileBrowse.FileName!=null && System.IO.File.Exists(FileBrowse.FileName))
			{
				try
				{
					Image buff=IconSelectedDisplay.Image;
					this.IconSelectedDisplay.Image=Image.FromFile(FileBrowse.FileName);
					if(buff!=null)
						buff.Dispose();
					ProgramConfig.Images.IconSelectedImagePath=FileBrowse.FileName;
				}
				catch(Exception)
				{
					this.IconSelectedDisplay.Image=Image.FromFile(ProgramConfig.Images.IconSelectedImagePath);
				}
			}
		}
		private void PickColor_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.ColorDialog ColorPicker=new ColorDialog();
			ColorPicker.AllowFullOpen=true;
			ColorPicker.AnyColor=true;
			ColorPicker.Color=ProgramConfig.Images.BackgroundColor;
			if(ColorPicker.ShowDialog()==DialogResult.OK)
			{
				this.BgColorDisplay.BackColor=ColorPicker.Color;
				ProgramConfig.Images.BackgroundColor=ColorPicker.Color;
			}
		}
		private void ScrollUpBrowse_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog FileBrowse=new OpenFileDialog();
			FileBrowse.Title="Browse for Scroll Up Image";
			FileBrowse.Multiselect=false;
			FileBrowse.ValidateNames=true;
			FileBrowse.CheckFileExists=true;
			FileBrowse.Filter="Supported Image Files|*.jpg;*.gif;*.png;*.tga;*.bmp";
			if(ProgramConfig.Images.ScrollUpImagePath!="" && ProgramConfig.Images.ScrollUpImagePath!=null)
				if(ProgramConfig.Images.BackgroundImagePath.IndexOf(":\\")>-1)
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(ProgramConfig.Images.ScrollUpImagePath);
				else
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(System.IO.Path.Combine(ProgramConfig.Locations.ImagesPath, ProgramConfig.Images.ScrollUpImagePath));
			else
				FileBrowse.InitialDirectory=ProgramConfig.Locations.ImagesPath;
			FileBrowse.ShowDialog();
			if(FileBrowse.FileName!=null && System.IO.File.Exists(FileBrowse.FileName))
			{
				try
				{
					Image buff=ScrollUpDisplay.Image;
					this.ScrollUpDisplay.Image=Image.FromFile(FileBrowse.FileName);
					if(buff!=null)
						buff.Dispose();
					ProgramConfig.Images.ScrollUpImagePath=FileBrowse.FileName;
				}
				catch(Exception)
				{
					this.ScrollUpDisplay.Image=Image.FromFile(ProgramConfig.Images.ScrollUpImagePath);
				}
			}
		}
		private void ScrollDownBrowse_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog FileBrowse=new OpenFileDialog();
			FileBrowse.Title="Browse for Scroll Down Image";
			FileBrowse.Multiselect=false;
			FileBrowse.ValidateNames=true;
			FileBrowse.CheckFileExists=true;
			FileBrowse.Filter="Supported Image Files|*.jpg;*.gif;*.png;*.tga;*.bmp";
			if(ProgramConfig.Images.ScrollDownImagePath!="" && ProgramConfig.Images.ScrollDownImagePath!=null)
				if(ProgramConfig.Images.BackgroundImagePath.IndexOf(":\\")>-1)
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(ProgramConfig.Images.ScrollDownImagePath);
				else
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(System.IO.Path.Combine(ProgramConfig.Locations.ImagesPath, ProgramConfig.Images.ScrollDownImagePath));
			else
				FileBrowse.InitialDirectory=ProgramConfig.Locations.ImagesPath;
			FileBrowse.ShowDialog();
			if(FileBrowse.FileName!=null && System.IO.File.Exists(FileBrowse.FileName))
			{
				try
				{
					Image buff=ScrollDownDisplay.Image;
					this.ScrollDownDisplay.Image=Image.FromFile(FileBrowse.FileName);
					if(buff!=null)
						buff.Dispose();
					ProgramConfig.Images.ScrollDownImagePath=FileBrowse.FileName;
				}
				catch(Exception)
				{
					this.ScrollDownDisplay.Image=Image.FromFile(ProgramConfig.Images.ScrollDownImagePath);
				}
			}
		}
		
		#endregion

		#region Fonts
		private void LabelFontChangeLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			FontDialog FontBrowse=new FontDialog();
			FontBrowse.AllowVectorFonts=true;
			FontBrowse.AllowVerticalFonts=false;
			FontBrowse.FixedPitchOnly=false;
			FontBrowse.FontMustExist=true;
			FontBrowse.ShowApply=false;
			FontBrowse.ShowColor=true;
			FontBrowse.ShowEffects=false;
			FontBrowse.ShowHelp=false;
			FontBrowse.Font=new Font(ProgramConfig.Fonts.LabelFont, ProgramConfig.Fonts.LabelSize, ProgramConfig.Fonts.LabelStyle);
			if(FontBrowse.ShowDialog()==DialogResult.OK)
			{
				ProgramConfig.Fonts.LabelSize=FontBrowse.Font.Size;
				ProgramConfig.Fonts.LabelFont=FontBrowse.Font.FontFamily.Name;
				ProgramConfig.Fonts.LabelStyle=FontBrowse.Font.Style;
				this.LabelFontFamilyDisplay.Text=FontBrowse.Font.Name+" - "+FontBrowse.Font.Size+" - "+ProgramConfig.Fonts.LabelStyle;
			}
		}
		private void DescriptionFontChangeLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			FontDialog FontBrowse=new FontDialog();
			FontBrowse.AllowVectorFonts=true;
			FontBrowse.AllowVerticalFonts=false;
			FontBrowse.FixedPitchOnly=false;
			FontBrowse.FontMustExist=true;
			FontBrowse.ShowApply=false;
			FontBrowse.ShowColor=true;
			FontBrowse.ShowEffects=false;
			FontBrowse.ShowHelp=false;
			FontBrowse.Font=new Font(ProgramConfig.Fonts.DescriptionFont, ProgramConfig.Fonts.DescriptionSize, ProgramConfig.Fonts.DescriptionStyle);
			if(FontBrowse.ShowDialog()==DialogResult.OK)
			{
				ProgramConfig.Fonts.DescriptionSize=FontBrowse.Font.Size;
				ProgramConfig.Fonts.DescriptionFont=FontBrowse.Font.FontFamily.Name;
				ProgramConfig.Fonts.DescriptionStyle=FontBrowse.Font.Style;
				this.DescriptionFontFamilyDisplay.Text=FontBrowse.Font.Name+" - "+FontBrowse.Font.Size+" - "+ProgramConfig.Fonts.DescriptionStyle;
			}
		}
		private void LabelBorderColorChange_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.ColorDialog ColorPicker=new ColorDialog();
			ColorPicker.AllowFullOpen=true;
			ColorPicker.AnyColor=true;
			ColorPicker.Color=ProgramConfig.Fonts.LabelBorderColor;
			if(ColorPicker.ShowDialog()==DialogResult.OK)
			{
				this.LabelBorderColor.BackColor=ColorPicker.Color;
				ProgramConfig.Fonts.LabelBorderColor=ColorPicker.Color;
			}
		}
		private void LabelColorChange_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.ColorDialog ColorPicker=new ColorDialog();
			ColorPicker.AllowFullOpen=true;
			ColorPicker.AnyColor=true;
			ColorPicker.Color=ProgramConfig.Fonts.LabelColor;
			if(ColorPicker.ShowDialog()==DialogResult.OK)
			{
				this.LabelColor.BackColor=ColorPicker.Color;
				ProgramConfig.Fonts.LabelColor=ColorPicker.Color;
			}
		}
		private void DescriptionBorderColorChange_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.ColorDialog ColorPicker=new ColorDialog();
			ColorPicker.AllowFullOpen=true;
			ColorPicker.AnyColor=true;
			ColorPicker.Color=ProgramConfig.Fonts.DescriptionBorderColor;
			if(ColorPicker.ShowDialog()==DialogResult.OK)
			{
				this.DescriptionBorderColor.BackColor=ColorPicker.Color;
				ProgramConfig.Fonts.DescriptionBorderColor=ColorPicker.Color;
			}
		}

		private void DescriptionColorChange_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.ColorDialog ColorPicker=new ColorDialog();
			ColorPicker.AllowFullOpen=true;
			ColorPicker.AnyColor=true;
			ColorPicker.Color=ProgramConfig.Fonts.DescriptionColor;
			if(ColorPicker.ShowDialog()==DialogResult.OK)
			{
				this.DescriptionColor.BackColor=ColorPicker.Color;
				ProgramConfig.Fonts.DescriptionColor=ColorPicker.Color;
			}
		}
		private void LabelBorder_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Fonts.ShowLabelBorder=this.LabelBorder.Checked;
		}

		private void DescriptionBorder_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Fonts.ShowDescriptionBorder=this.DescriptionBorder.Checked;
		}
		#endregion

		#region Appearance
		private void ShowItemLabels_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Appearance.ShowLabels=this.ShowItemLabels.Checked;
		}

		private void UseTransparency_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.UseTransparency.Checked)
			{
				ProgramConfig.Appearance.Transparency=Orbit.Configuration.TransparencyMode.Real;
			}
			else
			{
				if(this.FakeTransparency.Checked)
					ProgramConfig.Appearance.Transparency=Orbit.Configuration.TransparencyMode.Fake;
				else
					ProgramConfig.Appearance.Transparency=Orbit.Configuration.TransparencyMode.None;
			}

			this.UseMultipleWindows.Enabled=this.UseTransparency.Checked;
			this.FakeTransparency.Enabled=!this.UseTransparency.Checked;
		}
		private void UseMultipleWindows_CheckedChanged(object sender, System.EventArgs e)
		{
			//ProgramConfig.UseMultipleWindows=UseMultipleWindows.Checked;
		}
		private void ShowThumbnails_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Appearance.ShowImageThumbnails=this.ShowThumbnails.Checked;
		}
		private void GroupIcons_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Appearance.GroupIcons=this.GroupIcons.Checked;
		}
		private void FakeTransparency_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.FakeTransparency.Checked)
				ProgramConfig.Appearance.Transparency=Orbit.Configuration.TransparencyMode.Fake;
			else
				ProgramConfig.Appearance.Transparency=Orbit.Configuration.TransparencyMode.None;
		}
		private void IconSizeLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=0;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconSizeLink;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.IconMinifiedSize.ToString();
			this.AppearanceBar.Minimum=1;
			this.AppearanceBar.Maximum=256;
			this.AppearanceBar.TickFrequency=32;
			this.AppearanceBar.LargeChange=5;
			this.AppearanceBar.Value=ProgramConfig.Appearance.IconMinifiedSize;
		}

		private void IconMagnifiedSize_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=6;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconMagnifiedLink;
			this.AppearanceBar.Minimum=ProgramConfig.Appearance.IconMinifiedSize;
			this.AppearanceBar.Maximum=256;
			this.AppearanceBar.TickFrequency=32;
			this.AppearanceBar.LargeChange=5;
			if(ProgramConfig.Appearance.IconMagnifiedSize<ProgramConfig.Appearance.IconMinifiedSize)
				ProgramConfig.Appearance.IconMagnifiedSize=ProgramConfig.Appearance.IconMinifiedSize;
			this.AppearanceBar.Value=ProgramConfig.Appearance.IconMagnifiedSize;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.IconMagnifiedSize.ToString();
		}
		private void IconOpacityLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=1;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconOpacityLink;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.IconAlpha.ToString();
			this.AppearanceBar.Minimum=1;
			this.AppearanceBar.Maximum=255;
			this.AppearanceBar.TickFrequency=32;
			this.AppearanceBar.LargeChange=5;
			this.AppearanceBar.Value=ProgramConfig.Appearance.IconAlpha;
		}

		private void BgOpacityLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=2;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.BgOpacityLink;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.IconBackgroundAlpha.ToString();
			this.AppearanceBar.Minimum=0;
			this.AppearanceBar.Maximum=255;
			this.AppearanceBar.TickFrequency=32;
			this.AppearanceBar.LargeChange=5;
			this.AppearanceBar.Value=ProgramConfig.Appearance.IconBackgroundAlpha;
		}

		private void AnimSpeedLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=3;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.AnimSpeedLink;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.AnimationSpeed.ToString();
			this.AppearanceBar.Minimum=1;
			this.AppearanceBar.Maximum=255;
			this.AppearanceBar.TickFrequency=32;
			this.AppearanceBar.LargeChange=5;
			this.AppearanceBar.Value=ProgramConfig.Appearance.AnimationSpeed;
		}

		private void MouseWheelLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=4;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.MouseWheelLink;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.MouseWheelSensitivity.ToString();
			this.AppearanceBar.Minimum=1;
			this.AppearanceBar.Maximum=10;
			this.AppearanceBar.TickFrequency=1;
			this.AppearanceBar.LargeChange=1;
			this.AppearanceBar.Value=ProgramConfig.Appearance.MouseWheelSensitivity;
		}

		private void ItemsShownPerLineLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.AppearanceValueToChange=5;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.ItemsShownPerLineLink;
			if(ProgramConfig.Appearance.ItemsShownPerLine!=0)
				this.AppearanceDisplay.Text=ProgramConfig.Appearance.ItemsShownPerLine.ToString();
			else
				this.AppearanceDisplay.Text=Global.LanguageLoader.Language.DockSetup.Appearance.NoLimitLabel;
			this.AppearanceBar.Minimum=0;
			this.AppearanceBar.Maximum=30;
			this.AppearanceBar.TickFrequency=5;
			this.AppearanceBar.LargeChange=1;
			this.AppearanceBar.Value=ProgramConfig.Appearance.ItemsShownPerLine;
		}
		private void AppearanceBar_Scroll(object sender, System.EventArgs e)
		{
			this.AppearanceDisplay.Text=this.AppearanceBar.Value.ToString();
			switch(this.AppearanceValueToChange)
			{
				case 0:
					ProgramConfig.Appearance.IconMinifiedSize=this.AppearanceBar.Value;
					if(ProgramConfig.Appearance.IconMinifiedSize>ProgramConfig.Appearance.IconMagnifiedSize)
						ProgramConfig.Appearance.IconMagnifiedSize=ProgramConfig.Appearance.IconMinifiedSize;
					break;
				case 1:
					ProgramConfig.Appearance.IconAlpha=(byte)this.AppearanceBar.Value;
					break;
				case 2:
					ProgramConfig.Appearance.IconBackgroundAlpha=(byte)this.AppearanceBar.Value;
					break;
				case 3:
					ProgramConfig.Appearance.AnimationSpeed=(byte)this.AppearanceBar.Value;
					break;
				case 4:
					ProgramConfig.Appearance.MouseWheelSensitivity=this.AppearanceBar.Value;
					break;
				case 5:
					ProgramConfig.Appearance.ItemsShownPerLine=this.AppearanceBar.Value;
					if(ProgramConfig.Appearance.ItemsShownPerLine==0)
					{
						this.AppearanceDisplay.Text=Global.LanguageLoader.Language.DockSetup.Appearance.NoLimitLabel;
						this.GroupIcons.Enabled=true;
					}
					else
					{
						this.GroupIcons.Enabled=false;
					}
					break;
				case 6:
					ProgramConfig.Appearance.IconMagnifiedSize=this.AppearanceBar.Value;
					break;
			}
		}
		#endregion

		#region Behavior
		private void SpinOut_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Behavior.TransitionSpin=this.SpinOut.Checked;
		}

		private void SlideOut_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Behavior.TransitionSlide=this.SlideOut.Checked;
		}

		private void ZoomOut_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Behavior.TransitionZoom=this.ZoomOut.Checked;
		}

		private void OpenInStart_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Behavior.AlwaysOpenInStart=this.OpenInStart.Checked;
		}

		private void OpenOnMouseOver_CheckedChanged(object sender, System.EventArgs e)
		{
			ProgramConfig.Behavior.FolderPopOnHover=this.OpenOnMouseOver.Checked;
			this.OpenInStart.Enabled=!this.OpenOnMouseOver.Checked;
		}
		private void StartWithWindows_CheckedChanged(object sender, System.EventArgs e)
		{
			Microsoft.Win32.RegistryKey RunKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if(this.StartWithWindows.Checked)
			{
				try
				{
					RunKey.SetValue("Orbit", Application.ExecutablePath);
				}
				catch(Exception){}
			} 
			else 
			{
				try
				{
					RunKey.DeleteValue("Orbit", false);
				}
				catch(Exception){}
			}
		}

		private void PopKey_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(PopKey.SelectedIndex)
			{
				case 0:
					ProgramConfig.Behavior.PopKey=Keys.None;
					break;
				case 1:
					ProgramConfig.Behavior.PopKey=(Keys)1;
					break;
				case 2:
					ProgramConfig.Behavior.PopKey=(Keys)2;
					break;
				case 3:
					ProgramConfig.Behavior.PopKey=(Keys)4;
					break;
				case 4:
					ProgramConfig.Behavior.PopKey=(Keys)5;
					break;
				case 5:
					ProgramConfig.Behavior.PopKey=(Keys)6;
					break;
			}
		}

		private void ManageExcludedTasksLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			using(ExcludedTasksSetup ets=new ExcludedTasksSetup())
			{
				ets.ShowDialog();
			}
		}

		#endregion

		#region About
		private void AuthorLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:lmmenge@ig.com.br");
		}

		private void HomepageLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.ecocardio.com.br/orbit/");
		}

		private void BugLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:lmmenge@ig.com.br?subject=Orbit Bug Report");
		}

		private void ImproveLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:lmmenge@ig.com.br?subject=Orbit Suggestion");
		}
		#endregion
		#endregion

		#region Auxiliary Functions
		private void ShowPanel(object PanelToShow)
		{
			int OldHeight=((System.Windows.Forms.Panel)PanelToShow).Height;

			this.LocationsPicture.Image=this.ItemPicture.Image;
			this.ImagesPicture.Image=this.ItemPicture.Image;
			this.FontsPicture.Image=this.ItemPicture.Image;
			this.AppearancePicture.Image=this.ItemPicture.Image;
			this.BehaviorPicture.Image=this.ItemPicture.Image;
			this.AboutPicture.Image=this.ItemPicture.Image;

			this.LocationsPanel.Height=24;
			this.ImagesPanel.Height=24;
			this.FontsPanel.Height=24;
			this.AppearancePanel.Height=24;
			this.BehaviorPanel.Height=24;
			this.AboutPanel.Height=24;

			this.ImagesPanel.Top=88;
			this.FontsPanel.Top=116;
			this.AppearancePanel.Top=144;
			this.BehaviorPanel.Top=172;
			this.AboutPanel.Top=200;

			if(OldHeight==24)
			{
				((System.Windows.Forms.Panel)PanelToShow).Height=Convert.ToInt32(((System.Windows.Forms.Panel)PanelToShow).Tag);

				switch(((System.Windows.Forms.Panel)PanelToShow).Name.ToLower())
				{
					case "locationspanel":
						this.FontsPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.ImagesPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.AppearancePanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.BehaviorPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.AboutPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						break;
					case "imagespanel":
						this.FontsPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.AppearancePanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.BehaviorPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.AboutPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						break;
					case "fontspanel":
						this.AppearancePanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.BehaviorPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.AboutPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						break;
					case "appearancepanel":
						this.BehaviorPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						this.AboutPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						break;
					case "behaviorpanel":
						this.AboutPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						break;
				}
			}
		}
		private void UpdateControlsFromConfig()
		{
			#region Locations
			this.ImagesDirDisplay.Text=ProgramConfig.Locations.ImagesPath;
			this.ItemsDirDisplay.Text=ProgramConfig.Locations.ItemsPath;
			#endregion

			#region Images
			this.BgImageDisable.Enabled=false;
			this.BgColorDisplay.BackColor=Color.FromArgb(0xFF, ProgramConfig.Images.BackgroundColor);
			this.BgImageUseWindowsLink.Enabled=!ProgramConfig.Images.UseWindowsWallpaper;
			if(ProgramConfig.Images.UseWindowsWallpaper)
			{
				this.BgColorDisplay.BackColor=Color.FromKnownColor(KnownColor.Desktop);
				try
				{
					Microsoft.Win32.RegistryKey WallpaperKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop");
					string UsingWallpaper=(string)WallpaperKey.GetValue("Wallpaper");
					if(UsingWallpaper!="" && UsingWallpaper!=null)
					{
						string WallpaperPath=(string)WallpaperKey.GetValue("ConvertedWallpaper");
						//string WallpaperStyle=(string)WallpaperKey.GetValue("WallpaperStyle");
						if(this.BgDisplay.Image!=null)
							this.BgDisplay.Image.Dispose();
						using(Image wallbig=Image.FromFile(UsingWallpaper))
						{
							this.BgDisplay.Image=Orbit.Utilities.ImageHelper.GetAspectThumbnail(wallbig, this.BgDisplay.Size);
						}
						ProgramConfig.Images.UseWindowsWallpaper=true;
						this.BgImageUseWindowsLink.Enabled=false;
						ProgramConfig.Images.BackgroundImagePath="";
						ProgramConfig.Images.BackgroundSize=new Size(0,0);
						this.BgImageDisable.Enabled=true;
					}
					else
					{
						this.BgImageDisable.Enabled|=false;
						this.BgImageUseWindowsLink.Enabled=true;
						this.ProgramConfig.Images.UseWindowsWallpaper=false;
						if(this.BgDisplay.Image!=null)
							this.BgDisplay.Image.Dispose();
					}
				}
				catch(Exception)
				{
					//MessageBox.Show(e.Message);
				}
			}
			if(System.IO.File.Exists(ProgramConfig.Images.BackgroundImagePath))
			{
				try
				{
					using(Image wallbig=Image.FromFile(ProgramConfig.Images.BackgroundImagePath))
					{
						this.BgDisplay.Image=Orbit.Utilities.ImageHelper.GetAspectThumbnail(wallbig, this.BgDisplay.Size);
					}
					this.BgImageDisable.Enabled=true;
				}
				catch(Exception)
				{
					ProgramConfig.Images.BackgroundImagePath="";
					this.BgImageDisable.Enabled|=false;
				}
			}
			if(System.IO.File.Exists(ProgramConfig.Images.IconBackgroundImagePath))
			{
				try
				{
					this.IconBgDisplay.Image=Image.FromFile(ProgramConfig.Images.IconBackgroundImagePath);
				}
				catch(Exception)
				{
					ProgramConfig.Images.IconBackgroundImagePath="";
				}
			}
			if(System.IO.File.Exists(ProgramConfig.Images.IconSelectedImagePath))
			{
				try
				{
					this.IconSelectedDisplay.Image=Image.FromFile(ProgramConfig.Images.IconSelectedImagePath);
				}
				catch(Exception)
				{
					ProgramConfig.Images.IconSelectedImagePath="";
				}
			}
			if(System.IO.File.Exists(ProgramConfig.Images.ScrollDownImagePath))
			{
				try
				{
					this.ScrollDownDisplay.Image=Image.FromFile(ProgramConfig.Images.ScrollDownImagePath);
				}
				catch(Exception)
				{
					ProgramConfig.Images.ScrollDownImagePath="";
				}
			}
			if(System.IO.File.Exists(ProgramConfig.Images.ScrollUpImagePath))
			{
				try
				{
					this.ScrollUpDisplay.Image=Image.FromFile(ProgramConfig.Images.ScrollUpImagePath);
				}
				catch(Exception)
				{
					ProgramConfig.Images.ScrollUpImagePath="";
				}
			}
			#endregion

			#region Fonts
			this.LabelFontFamilyDisplay.Text=ProgramConfig.Fonts.LabelFont+" - "+ProgramConfig.Fonts.LabelSize+" - "+ProgramConfig.Fonts.LabelStyle;
			this.LabelBorder.Checked=ProgramConfig.Fonts.ShowLabelBorder;
			this.LabelBorderColor.BackColor=ProgramConfig.Fonts.LabelBorderColor;
			this.LabelColor.BackColor=ProgramConfig.Fonts.LabelColor;

			this.DescriptionFontFamilyDisplay.Text=ProgramConfig.Fonts.DescriptionFont+" - "+ProgramConfig.Fonts.DescriptionSize+" - "+ProgramConfig.Fonts.DescriptionStyle;
			this.DescriptionBorder.Checked=ProgramConfig.Fonts.ShowDescriptionBorder;
			this.DescriptionBorderColor.BackColor=ProgramConfig.Fonts.DescriptionBorderColor;
			this.DescriptionColor.BackColor=ProgramConfig.Fonts.DescriptionColor;
			#endregion

			#region Appearance
			this.ShowItemLabels.Checked=ProgramConfig.Appearance.ShowLabels;
			this.UseTransparency.Checked=(ProgramConfig.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real);
			this.FakeTransparency.Checked=(ProgramConfig.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake);
			this.GroupIcons.Checked=ProgramConfig.Appearance.GroupIcons;
			this.ShowThumbnails.Checked=ProgramConfig.Appearance.ShowImageThumbnails;

			this.AppearanceValueToChange=0;
			//this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconSizeLink;
			this.AppearanceDisplay.Text=ProgramConfig.Appearance.IconMinifiedSize.ToString();
			this.AppearanceBar.Minimum=1;
			this.AppearanceBar.Maximum=256;
			this.AppearanceBar.TickFrequency=32;
			this.AppearanceBar.LargeChange=5;
			this.AppearanceBar.Value=ProgramConfig.Appearance.IconMinifiedSize;
			#endregion

			#region Behavior
			this.SpinOut.Checked=ProgramConfig.Behavior.TransitionSpin;
			this.SlideOut.Checked=ProgramConfig.Behavior.TransitionSlide;
			this.ZoomOut.Checked=ProgramConfig.Behavior.TransitionZoom;
			this.OpenInStart.Checked=ProgramConfig.Behavior.AlwaysOpenInStart;
			this.OpenOnMouseOver.Checked=ProgramConfig.Behavior.FolderPopOnHover;

			switch((int)ProgramConfig.Behavior.PopKey)
			{
				case 0:
					this.PopKey.SelectedIndex=0;
					break;
				case 1:
					this.PopKey.SelectedIndex=1;
					break;
				case 2:
					this.PopKey.SelectedIndex=2;
					break;
				case 4:
					this.PopKey.SelectedIndex=3;
					break;
				case 5:
					this.PopKey.SelectedIndex=4;
					break;
				case 6:
					this.PopKey.SelectedIndex=5;
					break;
			}
			#endregion
		}
		private void UpdateUILanguage()
		{
			this.OrbitConfigurationLabel.Text=Global.LanguageLoader.Language.DockSetup.OrbitConfigurationLabel;
			this.OrbitConfigurationDescriptionLabel.Text=Global.LanguageLoader.Language.DockSetup.OrbitConfigurationDescriptionLabel;

			#region About
			this.AboutLabel.Text=Global.LanguageLoader.Language.DockSetup.About.AboutLabel;
			this.AuthorLabel.Text=Global.LanguageLoader.Language.DockSetup.About.AuthorLabel;
			this.BugLink.Text=Global.LanguageLoader.Language.DockSetup.About.BugLink;
			this.HelpTheProjectLabel.Text=Global.LanguageLoader.Language.DockSetup.About.HelpTheProjectLabel;
			this.HomepageLink.Text=Global.LanguageLoader.Language.DockSetup.About.HomepageLink;
			this.ImproveLink.Text=Global.LanguageLoader.Language.DockSetup.About.ImproveLink;
			this.OrbitDescriptionLabel.Text=Global.LanguageLoader.Language.DockSetup.About.OrbitDescriptionLabel;
			this.VersionInformationLabel.Text=Global.LanguageLoader.Language.DockSetup.About.VersionInformationLabel;
			#endregion

			#region Appearance
			this.AnimSpeedLink.Text=Global.LanguageLoader.Language.DockSetup.Appearance.AnimSpeedLink;
			this.AppearanceLabel.Text=Global.LanguageLoader.Language.DockSetup.Appearance.AppearanceLabel;
			this.AppearanceDescription.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconSizeLink;
			this.BgOpacityLink.Text=Global.LanguageLoader.Language.DockSetup.Appearance.BgOpacityLink;
			this.FakeTransparency.Text=Global.LanguageLoader.Language.DockSetup.Appearance.FakeTransparencyLink;
			this.GroupIcons.Text=Global.LanguageLoader.Language.DockSetup.Appearance.GroupIconsLink;
			this.IconMagnifiedSize.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconMagnifiedLink;
			this.IconOpacityLink.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconOpacityLink;
			this.IconSizeLink.Text=Global.LanguageLoader.Language.DockSetup.Appearance.IconSizeLink;
			this.ItemsShownPerLineLink.Text=Global.LanguageLoader.Language.DockSetup.Appearance.ItemsShownPerLineLink;
			this.MouseWheelLink.Text=Global.LanguageLoader.Language.DockSetup.Appearance.MouseWheelLink;
			this.OtherSettingsLabel.Text=Global.LanguageLoader.Language.DockSetup.Appearance.OtherSettingsLabel;
			this.SelectASettingLabel.Text=Global.LanguageLoader.Language.DockSetup.Appearance.SelectASettingLabel;
			this.ShowItemLabels.Text=Global.LanguageLoader.Language.DockSetup.Appearance.ShowItemLabelsLink;
			this.ShowThumbnails.Text=Global.LanguageLoader.Language.DockSetup.Appearance.ShowThumbnailsLink;
			this.UseMultipleWindows.Text=Global.LanguageLoader.Language.DockSetup.Appearance.UseMultipleWindowsLink;
			this.UseSliderLabel.Text=Global.LanguageLoader.Language.DockSetup.Appearance.UseSliderLabel;
			this.UseTransparency.Text=Global.LanguageLoader.Language.DockSetup.Appearance.UseTransparencyLink;
			#endregion

			#region Behavior
			this.BehaviorLabel.Text=Global.LanguageLoader.Language.DockSetup.Behavior.BehaviorLabel;
			this.BehaviorOtherSettingsLabel.Text=Global.LanguageLoader.Language.DockSetup.Behavior.BehaviorOtherSettingsLabel;
			this.ClickAndMouseResponseLabel.Text=Global.LanguageLoader.Language.DockSetup.Behavior.ClickAndMouseResponseLabel;
			this.OpenInStart.Text=Global.LanguageLoader.Language.DockSetup.Behavior.OpenInStartLabel;
			this.OpenOnMouseOver.Text=Global.LanguageLoader.Language.DockSetup.Behavior.OpenOnMouseOverLabel;
			this.PopUpKeyLabel.Text=Global.LanguageLoader.Language.DockSetup.Behavior.PopUpKeyLabel;
			this.SlideOut.Text=Global.LanguageLoader.Language.DockSetup.Behavior.SlideOutLabel;
			this.SpinOut.Text=Global.LanguageLoader.Language.DockSetup.Behavior.SpinOutLabel;
			this.StartWithWindows.Text=Global.LanguageLoader.Language.DockSetup.Behavior.StartWithWindows;
			this.TransitionEffectsLabel.Text=Global.LanguageLoader.Language.DockSetup.Behavior.TransitionEffectsLabel;
			this.ZoomOut.Text=Global.LanguageLoader.Language.DockSetup.Behavior.ZoomOutLabel;
			this.ExcludedTasksLabel.Text=Global.LanguageLoader.Language.DockSetup.Behavior.ExcludedTasksLabel;
			this.ManageExcludedTasksLink.Text=Global.LanguageLoader.Language.DockSetup.Behavior.ManageExcludedTasksLink;
			#endregion

			#region Fonts
			this.DescriptionBorderColorChange.Text=Global.LanguageLoader.Language.DockSetup.Fonts.ChangeBorderColorLink;
			this.LabelBorderColorChange.Text=Global.LanguageLoader.Language.DockSetup.Fonts.ChangeBorderColorLink;
			this.DescriptionColorChange.Text=Global.LanguageLoader.Language.DockSetup.Fonts.ChangeFillColorLink;
			this.LabelColorChange.Text=Global.LanguageLoader.Language.DockSetup.Fonts.ChangeFillColorLink;
			this.LabelFontChangeLink.Text=Global.LanguageLoader.Language.DockSetup.Fonts.ChangeFontFamilyLink;
			this.LabelFontLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.LabelFontLabel;
			this.DescriptionFontChangeLink.Text=Global.LanguageLoader.Language.DockSetup.Fonts.ChangeFontFamilyLink;
			this.DescriptionFontLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.DescriptionFontLabel;
			this.DescriptionBorder.Text=Global.LanguageLoader.Language.DockSetup.Fonts.DisplayBorderLabel;
			this.LabelBorder.Text=Global.LanguageLoader.Language.DockSetup.Fonts.DisplayBorderLabel;
			this.FontsLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.FontsLabel;
			this.LabelColorsLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.LabelColorsLabel;
			this.DescriptionColorsLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.DescriptionColorsLabel;
			this.LabelFillLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.FillLabel;
			this.LabelBorderLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.BorderLabel;
			this.DescriptionFillLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.FillLabel;
			this.DescriptionBorderLabel.Text=Global.LanguageLoader.Language.DockSetup.Fonts.BorderLabel;
			#endregion

			#region Images
			this.BgImageBrowse.Text=Global.LanguageLoader.Language.DockSetup.Images.ChangeLabel;
			this.IconBgImageBrowse.Text=Global.LanguageLoader.Language.DockSetup.Images.ChangeLabel;
			this.IconSelectedImageBrowse.Text=Global.LanguageLoader.Language.DockSetup.Images.ChangeLabel;
			this.ScrollUpBrowse.Text=Global.LanguageLoader.Language.DockSetup.Images.ChangeLabel;
			this.ScrollDownBrowse.Text=Global.LanguageLoader.Language.DockSetup.Images.ChangeLabel;
			this.BgImageDisable.Text=Global.LanguageLoader.Language.DockSetup.Images.DisableLabel;
			this.IconBgImageDisable.Text=Global.LanguageLoader.Language.DockSetup.Images.DisableLabel;
			this.IconSelectedImageDisable.Text=Global.LanguageLoader.Language.DockSetup.Images.DisableLabel;
			this.IconAreaBackgroundLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.IconAreaBackgroundLabel;
			this.ImagesLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.ImagesLabel;
			this.NonTransparentBackgroundLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.NonTransparentBackgroundLabel;
			this.NonTransparentColorLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.NonTransparentColorLabel;
			this.ScrollDownLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.ScrollDownLabel;
			this.ScrollUpLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.ScrollUpLabel;
			this.SelectedItemLabel.Text=Global.LanguageLoader.Language.DockSetup.Images.SelectedItemLabel;
			this.PickColor.Text=Global.LanguageLoader.Language.DockSetup.Images.PickAnotherColorLabel;
			this.BgImageUseWindowsLink.Text=Global.LanguageLoader.Language.DockSetup.Images.BgImageUseWindowsLabel;
			#endregion

			#region Locations
			this.ItemsStructureLocationLabel.Text=Global.LanguageLoader.Language.DockSetup.Locations.ItemsStructureLocationLabel;
			this.LocationsLabel.Text=Global.LanguageLoader.Language.DockSetup.Locations.LocationsLabel;
			this.PreferredImagesLocationLabel.Text=Global.LanguageLoader.Language.DockSetup.Locations.PreferredImagesLocationLabel;
			#endregion
		}

		#endregion

		private void Language_LanguageLoaded(object sender, EventArgs e)
		{
			UpdateUILanguage();
		}
	}
}
