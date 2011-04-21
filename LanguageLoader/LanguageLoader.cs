using System;
using System.Windows.Forms;
using System.IO;

namespace Orbit.Language
{
	/// <summary>
	/// Summary description for LanguageLoader.
	/// </summary>
	public class LanguageLoader
	{
		private LanguageFile Lang;

		private string UserPath;

		#region Creator
		public LanguageLoader()
		{
			this.UserPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), @"profiles\\"+System.Environment.UserName);;
			Lang=CreateEnglish();
		}

		#endregion

		#region Functions
		private LanguageFile CreateEnglish()
		{
			LanguageFile e=new LanguageFile();

			#region Orbit
			#region Context Menu
			e.Orbit.Menu.StartLoopsHere="Start loops here";
			e.Orbit.Menu.OpenInExplorer="Open in Explorer";
			e.Orbit.Menu.AddItemTo="Add item to \"%ItemName%\"...";
			e.Orbit.Menu.RemoveItem="Remove \"%ItemName%\"";
			e.Orbit.Menu.ItemProperties="\"%ItemName%\" Properties...";
			e.Orbit.Menu.ClickHereToPop="Click here to pop Orbit up";
			e.Orbit.Menu.IgnoreThisWindow="Ignore this window";
			#endregion

			#region Orbit Menu
			e.Orbit.Menu.Configuration="Configuration...";
			e.Orbit.Menu.Language="Language";
			e.Orbit.Menu.OnlineManual="Online Manual...";
			e.Orbit.Menu.CheckForUpdate="Check for update...";
			e.Orbit.Menu.Exit="Exit";
			#endregion

			#region Message Boxes
			e.Orbit.Messages.CannotCreateD3DDevice="Cannot create Direct3D device. Please e-mail the author with your system specs for more information.";
			e.Orbit.Messages.ConfirmRemove="Do you wish to remove \"%ItemName%\"?";
			e.Orbit.Messages.ImageDirectoryNotFound="Images Directory not found!";
			e.Orbit.Messages.InvalidINIFile="Invalid config.ini File. Please configure Orbit again.";
			e.Orbit.Messages.ItemsDirectoryNotFound="Items Directory not found!";
			e.Orbit.Messages.TransparentModeNotSupported="Transparent mode is not supported by your Video Card. Cannot create offscreen render target. Using Non-Transparent mode.";
			e.Orbit.Messages.UnexpectedErrorOccurred="An Unexpected error has occurred. Cannot create offscreen render target. Using Non-Transparent mode.";
			e.Orbit.Messages.VideoCardNotSupported="You do not have a video card which can run Orbit. Orbit needs a 3D Hardware accelerated card.";
			e.Orbit.Messages.UnableToConnect="Unable to connect from the Orbit Services server.";
			e.Orbit.Messages.YouAreUpToDate="You have the latest version of Orbit.";
			e.Orbit.Messages.UpdateAvailable="An update to Orbit is available. Do you want to visit the Orbit Homepage?";
			#endregion
			#endregion

			#region DockSetup
			e.DockSetup.OrbitConfigurationDescriptionLabel="Customize the way that Orbit shows up on your screen";
			e.DockSetup.OrbitConfigurationLabel="Orbit Configuration";

			#region About
			e.DockSetup.About.AboutLabel="About";
			e.DockSetup.About.AuthorLabel="Author";
			e.DockSetup.About.BugLink="Report a Bug";
			e.DockSetup.About.HelpTheProjectLabel="Help the Project";
			e.DockSetup.About.HomepageLink="Official Homepage";
			e.DockSetup.About.ImproveLink="Suggest an Improvement";
			e.DockSetup.About.OrbitDescriptionLabel="The foating round dock for Windows";
			e.DockSetup.About.VersionInformationLabel="Version Information";
			#endregion

			#region Appearance
			e.DockSetup.Appearance.AnimSpeedLink="Animation Speed...";
			e.DockSetup.Appearance.AppearanceLabel="Appearance";
			e.DockSetup.Appearance.BgOpacityLink="Icon Area Background Opacity...";
			e.DockSetup.Appearance.FakeTransparencyLink="Use fake transparency";
			e.DockSetup.Appearance.GroupIconsLink="Group icons together";
			e.DockSetup.Appearance.IconMagnifiedLink="Magnified Icon Size...";
			e.DockSetup.Appearance.IconOpacityLink="Icon Opacity...";
			e.DockSetup.Appearance.IconSizeLink="Normal Icon Size...";
			e.DockSetup.Appearance.ItemsShownPerLineLink="Items Shown Per Line...";
			e.DockSetup.Appearance.MouseWheelLink="Mouse Wheel Sensitivity...";
			e.DockSetup.Appearance.OtherSettingsLabel="Other Settings";
			e.DockSetup.Appearance.SelectASettingLabel="Select an Appearance Setting";
			e.DockSetup.Appearance.ShowItemLabelsLink="Show Item Labels";
			e.DockSetup.Appearance.ShowThumbnailsLink="Show image file's thumbnails";
			e.DockSetup.Appearance.UseMultipleWindowsLink="Make each item a separate window";
			e.DockSetup.Appearance.UseSliderLabel="And use the slider to adjust its value";
			e.DockSetup.Appearance.UseTransparencyLink="Use Transparent Mode";
			e.DockSetup.Appearance.NoLimitLabel="No limit";
			#endregion

			#region Behavior
			e.DockSetup.Behavior.BehaviorLabel="Behavior";
			e.DockSetup.Behavior.BehaviorOtherSettingsLabel="Other Settings";
			e.DockSetup.Behavior.ClickAndMouseResponseLabel="Click and Mouse Response";
			e.DockSetup.Behavior.OpenInStartLabel="Always open folders in the same loop";
			e.DockSetup.Behavior.OpenOnMouseOverLabel="Open folders on Mouse-Over";
			e.DockSetup.Behavior.PopUpKeyLabel="Pop up key:";
			e.DockSetup.Behavior.SlideOutLabel="Slide";
			e.DockSetup.Behavior.SpinOutLabel="Spin";
			e.DockSetup.Behavior.StartWithWindows="Start with Windows";
			e.DockSetup.Behavior.TransitionEffectsLabel="Transition Effects";
			e.DockSetup.Behavior.ZoomOutLabel="Zoom";
			e.DockSetup.Behavior.ExcludedTasksLabel="Excluded Tasks";
			e.DockSetup.Behavior.ManageExcludedTasksLink="Manage Excluded Tasks...";
			#endregion

			#region Fonts
			e.DockSetup.Fonts.ChangeBorderColorLink="Change border color...";
			e.DockSetup.Fonts.ChangeFillColorLink="Change fill color...";
			e.DockSetup.Fonts.ChangeFontFamilyLink="Change this font family...";
			e.DockSetup.Fonts.DescriptionFontLabel="Description Font";
			e.DockSetup.Fonts.FontsLabel="Fonts";
			e.DockSetup.Fonts.LabelFontLabel="Label Font";
			e.DockSetup.Fonts.DescriptionColorsLabel="Description Colors";
			e.DockSetup.Fonts.LabelColorsLabel="Label Colors";
			e.DockSetup.Fonts.FillLabel="Fill";
			e.DockSetup.Fonts.BorderLabel="Border";
			e.DockSetup.Fonts.DisplayBorderLabel="Display border";
			#endregion

			#region Images
			e.DockSetup.Images.ChangeLabel="Change...";
			e.DockSetup.Images.DisableLabel="Disable";
			e.DockSetup.Images.IconAreaBackgroundLabel="Icon Area Background";
			e.DockSetup.Images.ImagesLabel="Images";
			e.DockSetup.Images.NonTransparentBackgroundLabel="Non-Transparent Mode Background";
			e.DockSetup.Images.NonTransparentColorLabel="Non-Transparent Mode Background Color";
			e.DockSetup.Images.PickAnotherColorLabel="Pick another color...";
			e.DockSetup.Images.ScrollDownLabel="Scroll Down Indicator";
			e.DockSetup.Images.ScrollUpLabel="Scroll Up Indicator";
			e.DockSetup.Images.SelectedItemLabel="Selected Item Indicator";
			e.DockSetup.Images.BgImageUseWindowsLabel="Use Windows wallpaper";
			#endregion

			#region Locations
			e.DockSetup.Locations.ItemsStructureLocationLabel="Items Structure Location";
			e.DockSetup.Locations.LocationsLabel="Locations";
			e.DockSetup.Locations.PreferredImagesLocationLabel="Preferred Images Location";
			#endregion

			#region Messages
			e.DockSetup.Messages.NeedsImageDirectory="You must specify an image directory.";
			#endregion

			#endregion

			#region ItemSetup
			e.ItemSetup.DialogDescriptionLabel="Customize this item's properties";
			e.ItemSetup.DialogTitleLabel="%ItemName% Properties";

			#region Actions
			e.ItemSetup.Actions.ActionsLabel="Actions";
			e.ItemSetup.Actions.ChangeItemLink="Change this item's classification...";
			e.ItemSetup.Actions.ConfigurationLink="This item brings up the Orbit Configuration Dialog...";
			e.ItemSetup.Actions.TasksLink="This item shows the tasks that are running...";
			e.ItemSetup.Actions.FolderLink="This item is a folder in Orbit...";
			e.ItemSetup.Actions.PhysicalFolderLink="This item shows the contents of a folder on a disk...";
			e.ItemSetup.Actions.ShortcutArgumentsLabel="Shortcut arguments";
			e.ItemSetup.Actions.ShortcutLocationLabel="Shortcut location";
			e.ItemSetup.Actions.ShortcutToFileLink="This item is a shortcut to a file...";
			e.ItemSetup.Actions.ShortcutToFolderLink="This item is a shortcut to a folder...";
			e.ItemSetup.Actions.ShortcutToWebLink="This item is a shortcut to a World Wide Web address...";
			e.ItemSetup.Actions.ActionsDescriptionLabel="You can either select an option from above or drag a file, folder or URL into this dialog to set this item to it.";
			e.ItemSetup.Actions.WhatIsThisItemLabel="What is this item?";
			#endregion

			#region Display
			e.ItemSetup.Display.BrowsePictureLink="Browse for another image or drag an image file to the image box to change this item's display image";
			e.ItemSetup.Display.BrowsePictureLinkLength=24;
			e.ItemSetup.Display.BrowsePictureLinkStart=0;
			e.ItemSetup.Display.DisplayLabel="Display";
			e.ItemSetup.Display.ItemDescriptionLabel="Description";
			e.ItemSetup.Display.ItemImageLabel="Image";
			e.ItemSetup.Display.ItemNameLabel="Name";
			#endregion

			#region Messages
			e.ItemSetup.Messages.InvalidConfiguration="Invalid item configuration.";
			e.ItemSetup.Messages.ItemAlreadyExists="An item with this name already exists in this folder.";
			e.ItemSetup.Messages.NeedsAction="The item must have an action.";
			e.ItemSetup.Messages.NeedsImage="No image was selected for this item.";
			e.ItemSetup.Messages.NeedsName="You must specify a name for the item.";
			#endregion
			#endregion

			#region ExcludedTasks
			e.ExcludedTasks.ExcludedTasksLabel="Excluded Tasks";
			e.ExcludedTasks.ExcludedTasksDescriptionLabel="Manage the task lister";
			e.ExcludedTasks.InstructionsLabel="Below is a list of the tasks that are excluded from Orbit's task folder. Use the link on each of them to manage them.";
			e.ExcludedTasks.StopIgnoringThisWindowLink="Stop ignoring this window";
			#endregion

			// Update the default file
			if(!System.IO.Directory.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"language\")))
			{
				MessageBox.Show("dir doesn't exist");
				System.IO.Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"language\"));
			}

			System.IO.StreamWriter f=new System.IO.StreamWriter(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"language\English.xml"), false, System.Text.UnicodeEncoding.Unicode);
			System.Xml.Serialization.XmlSerializer x=new System.Xml.Serialization.XmlSerializer(typeof(LanguageFile));
			x.Serialize(f, e);
			f.Close();

			return e;
		}

		public string GetDefaultLanguageName()
		{
			// get user path
			if(System.IO.File.Exists(Path.Combine(UserPath, "Language.xml")))
			{
				// open file
				System.IO.StreamReader f=new StreamReader(Path.Combine(UserPath, "Language.xml"));
				// serializer
				System.Xml.Serialization.XmlSerializer x=new System.Xml.Serialization.XmlSerializer(typeof(LanguageConfig));
				// read
				LanguageConfig l=(LanguageConfig)x.Deserialize(f);
				// close
				f.Close();
				return l.LanguageName;
			}
			else
				return "English";
		}
		public void LoadLanguage(string LanguageFileName)
		{
			// create serializer
			System.Xml.Serialization.XmlSerializer x=new System.Xml.Serialization.XmlSerializer(typeof(LanguageFile));

			if(System.IO.File.Exists(LanguageFileName))
			{
				// open file
				System.IO.StreamReader f=new System.IO.StreamReader(LanguageFileName);

				try
				{
					// load
					this.Lang=(LanguageFile)x.Deserialize(f);
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
					this.Lang=this.CreateEnglish();
					MessageBox.Show("Could not load language file", "Language error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				// close file
				f.Close();
			}

			// save as default
			// create language information file
			LanguageConfig LC=new LanguageConfig();
			LC.LanguageName=Path.GetFileNameWithoutExtension(LanguageFileName);
			// serializer
			System.Xml.Serialization.XmlSerializer x1=new System.Xml.Serialization.XmlSerializer(typeof(LanguageConfig));
			// open file
			if(System.IO.Directory.Exists(UserPath))
			{
				System.IO.StreamWriter fs=new System.IO.StreamWriter(Path.Combine(UserPath, "Language.xml"), false, System.Text.UnicodeEncoding.Unicode);
				// save
				x1.Serialize(fs, LC);
				fs.Close();
			}

			if(this.LanguageLoaded!=null)this.LanguageLoaded(this, new EventArgs());
		}

		public void LoadDefaultLanguage()
		{
			// get the default language name
			string LanguageName=this.GetDefaultLanguageName();
			// load the language
			this.LoadLanguage(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"language\"+LanguageName+".xml"));
		}
		#endregion

		#region Properties
		public LanguageFile Language
		{
			get
			{
				return Lang;
			}
		}

		#endregion

		#region Events
		public event System.EventHandler LanguageLoaded;
		#endregion
	}
}
