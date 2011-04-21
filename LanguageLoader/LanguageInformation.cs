namespace Orbit.Language
{
	public struct LanguageFile
	{
		public OrbitUIText Orbit;
		public DockSetupUIText DockSetup;
		public ItemSetupUIText ItemSetup;
		public ExcludedTasksSetupUIText ExcludedTasks;
	}

	public struct LanguageConfig
	{
		public string LanguageName;
	}


	#region Orbit
	public struct OrbitMenuText
	{
		public string ClickHereToPop;
		public string StartLoopsHere;
		public string OpenInExplorer;
		public string AddItemTo;
		public string RemoveItem;
		public string IgnoreThisWindow;
		public string ItemProperties;
		public string Configuration;
		public string Language;
		public string OnlineManual;
		public string CheckForUpdate;
		public string Exit;
	}

	public struct OrbitMessageBoxText
	{
		public string ConfirmRemove;
		public string TransparentModeNotSupported;
		public string UnexpectedErrorOccurred;
		public string CannotCreateD3DDevice;
		public string VideoCardNotSupported;
		public string ImageDirectoryNotFound;
		public string ItemsDirectoryNotFound;
		public string InvalidINIFile;
		public string UnableToConnect;
		public string UpdateAvailable;
		public string YouAreUpToDate;
	}

	public struct OrbitUIText
	{
		public OrbitMenuText Menu;
		public OrbitMessageBoxText Messages;
	}

	#endregion

	#region ItemSetup
	public struct ItemSetupMessageBoxText
	{
		public string InvalidConfiguration;
		public string NeedsName;
		public string NeedsAction;
		public string NeedsImage;
		public string ItemAlreadyExists;
	}

	public struct ItemSetupDisplayText
	{
		public string DisplayLabel;
		public string ItemNameLabel;
		public string ItemDescriptionLabel;
		public string ItemImageLabel;
		public string BrowsePictureLink;
		public int BrowsePictureLinkStart;
		public int BrowsePictureLinkLength;
	}

	public struct ItemSetupActionsText
	{
        public string ActionsLabel;
		public string ShortcutLocationLabel;
		public string ShortcutArgumentsLabel;
		public string ChangeItemLink;
		public string ShortcutToFileLink;
		public string ShortcutToFolderLink;
		public string ShortcutToWebLink;
		public string PhysicalFolderLink;
		public string FolderLink;
		public string ConfigurationLink;
		public string TasksLink;
		public string ActionsDescriptionLabel;
		public string WhatIsThisItemLabel;
	}

	public struct ItemSetupUIText
	{
		public ItemSetupActionsText Actions;
		public ItemSetupDisplayText Display;
		public ItemSetupMessageBoxText Messages;
		public string DialogTitleLabel;
		public string DialogDescriptionLabel;
	}

	#endregion

	#region DockSetup
	public struct DockSetupMessageBoxText
	{
		public string NeedsImageDirectory;
	}

	public struct DockSetupLocationsText
	{
		public string LocationsLabel;
		public string PreferredImagesLocationLabel;
		public string ItemsStructureLocationLabel;
	}

	public struct DockSetupImagesText
	{
		public string ImagesLabel;
		public string ChangeLabel;
		public string DisableLabel;
		public string BgImageUseWindowsLabel;
		public string PickAnotherColorLabel;
		public string NonTransparentBackgroundLabel;
		public string NonTransparentColorLabel;
		public string IconAreaBackgroundLabel;
		public string SelectedItemLabel;
		public string ScrollUpLabel;
		public string ScrollDownLabel;
	}

	public struct DockSetupFontsText
	{
		public string FontsLabel;
		public string LabelFontLabel;
		public string DescriptionFontLabel;
		public string ChangeFontFamilyLink;
		public string ChangeBorderColorLink;
		public string ChangeFillColorLink;
		public string DisplayBorderLabel;
		public string LabelColorsLabel;
		public string DescriptionColorsLabel;
		public string BorderLabel;
		public string FillLabel;
	}
	public struct DockSetupAppearanceText
	{
		public string AppearanceLabel;
		public string SelectASettingLabel;
		public string UseSliderLabel;
		public string OtherSettingsLabel;
		public string NoLimitLabel;

		public string IconSizeLink;
		public string IconMagnifiedLink;
		public string IconOpacityLink;
		public string BgOpacityLink;
		public string AnimSpeedLink;
		public string MouseWheelLink;
		public string ItemsShownPerLineLink;

		public string ShowItemLabelsLink;
		public string UseTransparencyLink;
		public string UseMultipleWindowsLink;
		public string ShowThumbnailsLink;
		public string GroupIconsLink;
		public string FakeTransparencyLink;
	}
	public struct DockSetupBehaviorText
	{
		public string BehaviorLabel;
		public string TransitionEffectsLabel;
		public string ClickAndMouseResponseLabel;
		public string BehaviorOtherSettingsLabel;
		public string PopUpKeyLabel;
		public string SpinOutLabel;
		public string SlideOutLabel;
		public string ZoomOutLabel;
		public string OpenInStartLabel;
		public string OpenOnMouseOverLabel;
		public string StartWithWindows;
		public string ExcludedTasksLabel;
		public string ManageExcludedTasksLink;
	}
	public struct DockSetupAboutText
	{
		public string AboutLabel;
		public string OrbitDescriptionLabel;
		public string AuthorLabel;
		public string HelpTheProjectLabel;
		public string VersionInformationLabel;
		public string HomepageLink;
		public string BugLink;
		public string ImproveLink;
	}

	public struct DockSetupUIText
	{
		public string OrbitConfigurationLabel;
		public string OrbitConfigurationDescriptionLabel;
		public DockSetupMessageBoxText Messages;
		public DockSetupLocationsText Locations;
		public DockSetupImagesText Images;
		public DockSetupFontsText Fonts;
		public DockSetupAppearanceText Appearance;
		public DockSetupBehaviorText Behavior;
		public DockSetupAboutText About;
	}

	#endregion

	#region ExcludedTasksSetup
	public struct ExcludedTasksSetupUIText
	{
		public string ExcludedTasksLabel;
		public string ExcludedTasksDescriptionLabel;
		public string InstructionsLabel;
		public string StopIgnoringThisWindowLink;
	}
	#endregion
}