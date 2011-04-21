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
	public class ItemSetup : System.Windows.Forms.Form
	{
		private Item EditItem;
		private string OldName;
		private string ImgPath;
		private bool GetAsImage=false;

		#region Windows Forms Objects
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox SavePictureBox;
		private System.Windows.Forms.PictureBox ClosePictureBox;
		private System.Windows.Forms.Panel DisplayPanel;
		private System.Windows.Forms.Label DisplayLabel;
		private System.Windows.Forms.PictureBox DisplayPicture;
		private System.Windows.Forms.PictureBox ItemSelectedPicture;
		private System.Windows.Forms.PictureBox ItemPicture;
		private System.Windows.Forms.Panel ActionsPanel;
		private System.Windows.Forms.Label ActionsLabel;
		private System.Windows.Forms.PictureBox ActionsPicture;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox ItemDisplayPicture;
		private System.Windows.Forms.TextBox ItemDisplayName;
		private System.Windows.Forms.LinkLabel BrowsePictureLink;
		private System.Windows.Forms.LinkLabel ShortcutToFileLink;
		private System.Windows.Forms.LinkLabel FolderLink;
		private System.Windows.Forms.LinkLabel ConfigurationLink;
		private System.Windows.Forms.LinkLabel ShortcutToFolderLink;
		private System.Windows.Forms.LinkLabel ShortcutToWebLink;
		private System.Windows.Forms.Label DialogTitle;
		private System.Windows.Forms.LinkLabel ChangeItemLink;
		private System.Windows.Forms.Panel ClassificationPanel;
		private System.Windows.Forms.Label ItemClassification;
		#endregion
		private System.Windows.Forms.PictureBox DialogPicture;
		private System.Windows.Forms.LinkLabel PhysicalFolderLink;
		private System.Windows.Forms.TextBox ItemDescription;
		private System.Windows.Forms.Label ItemDescriptionLabel;
		private System.Windows.Forms.Label ItemNameLabel;
		private System.Windows.Forms.Label ItemImageLabel;
		private System.Windows.Forms.Label DialogDescriptionLabel;
		private System.Windows.Forms.Label ActionsDescriptionLabel;
		private System.Windows.Forms.Panel ShortcutPanel;
		private System.Windows.Forms.Button BrowseLocation;
		private System.Windows.Forms.TextBox ItemAction;
		private System.Windows.Forms.Label ShortcutLocationLabel;
		private System.Windows.Forms.Panel ToFilePanel;
		private System.Windows.Forms.TextBox ItemArguments;
		private System.Windows.Forms.Label ShortcutArgumentsLabel;
		private System.Windows.Forms.LinkLabel TasksLink;
		private System.Windows.Forms.Label WhatIsThisItemLabel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ItemSetup(string ItemPath, string ImagesPath)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			EditItem=LoadItem(ItemPath, ImagesPath);

			ImgPath=ImagesPath;

			UpdateUILanguage();
			Orbit.Configuration.Global.LanguageLoader.LanguageLoaded += new EventHandler(Language_LanguageLoaded);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(DisplayPicture.Image!=null)
				DisplayPicture.Image.Dispose();

			foreach(Control control in this.Controls)
			{
				if(control!=null)
				{
					if(control.Controls!=null)
					{
						foreach(Control child in control.Controls)
						{
							if(child!=null)
								child.Dispose();
						}
					}
					control.Dispose();
				}
			}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ItemSetup));
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.SavePictureBox = new System.Windows.Forms.PictureBox();
			this.ClosePictureBox = new System.Windows.Forms.PictureBox();
			this.DialogTitle = new System.Windows.Forms.Label();
			this.DialogPicture = new System.Windows.Forms.PictureBox();
			this.DialogDescriptionLabel = new System.Windows.Forms.Label();
			this.DisplayPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ItemDescription = new System.Windows.Forms.TextBox();
			this.ItemDescriptionLabel = new System.Windows.Forms.Label();
			this.BrowsePictureLink = new System.Windows.Forms.LinkLabel();
			this.ItemImageLabel = new System.Windows.Forms.Label();
			this.ItemDisplayName = new System.Windows.Forms.TextBox();
			this.ItemNameLabel = new System.Windows.Forms.Label();
			this.ItemDisplayPicture = new System.Windows.Forms.PictureBox();
			this.DisplayLabel = new System.Windows.Forms.Label();
			this.DisplayPicture = new System.Windows.Forms.PictureBox();
			this.ItemSelectedPicture = new System.Windows.Forms.PictureBox();
			this.ItemPicture = new System.Windows.Forms.PictureBox();
			this.ActionsPanel = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ClassificationPanel = new System.Windows.Forms.Panel();
			this.ToFilePanel = new System.Windows.Forms.Panel();
			this.ItemArguments = new System.Windows.Forms.TextBox();
			this.ShortcutArgumentsLabel = new System.Windows.Forms.Label();
			this.ShortcutPanel = new System.Windows.Forms.Panel();
			this.BrowseLocation = new System.Windows.Forms.Button();
			this.ItemAction = new System.Windows.Forms.TextBox();
			this.ShortcutLocationLabel = new System.Windows.Forms.Label();
			this.ChangeItemLink = new System.Windows.Forms.LinkLabel();
			this.ItemClassification = new System.Windows.Forms.Label();
			this.PhysicalFolderLink = new System.Windows.Forms.LinkLabel();
			this.ActionsDescriptionLabel = new System.Windows.Forms.Label();
			this.ShortcutToWebLink = new System.Windows.Forms.LinkLabel();
			this.ShortcutToFolderLink = new System.Windows.Forms.LinkLabel();
			this.ConfigurationLink = new System.Windows.Forms.LinkLabel();
			this.FolderLink = new System.Windows.Forms.LinkLabel();
			this.ShortcutToFileLink = new System.Windows.Forms.LinkLabel();
			this.WhatIsThisItemLabel = new System.Windows.Forms.Label();
			this.TasksLink = new System.Windows.Forms.LinkLabel();
			this.ActionsLabel = new System.Windows.Forms.Label();
			this.ActionsPicture = new System.Windows.Forms.PictureBox();
			this.DisplayPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.ActionsPanel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.ClassificationPanel.SuspendLayout();
			this.ToFilePanel.SuspendLayout();
			this.ShortcutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.White;
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(472, 56);
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			// 
			// SavePictureBox
			// 
			this.SavePictureBox.BackColor = System.Drawing.Color.White;
			this.SavePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SavePictureBox.Image")));
			this.SavePictureBox.Location = new System.Drawing.Point(444, 4);
			this.SavePictureBox.Name = "SavePictureBox";
			this.SavePictureBox.Size = new System.Drawing.Size(24, 24);
			this.SavePictureBox.TabIndex = 13;
			this.SavePictureBox.TabStop = false;
			this.SavePictureBox.Click += new System.EventHandler(this.SavePictureBox_Click);
			// 
			// ClosePictureBox
			// 
			this.ClosePictureBox.BackColor = System.Drawing.Color.White;
			this.ClosePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ClosePictureBox.Image")));
			this.ClosePictureBox.Location = new System.Drawing.Point(444, 28);
			this.ClosePictureBox.Name = "ClosePictureBox";
			this.ClosePictureBox.Size = new System.Drawing.Size(24, 24);
			this.ClosePictureBox.TabIndex = 12;
			this.ClosePictureBox.TabStop = false;
			this.ClosePictureBox.Click += new System.EventHandler(this.ClosePictureBox_Click);
			// 
			// DialogTitle
			// 
			this.DialogTitle.BackColor = System.Drawing.Color.White;
			this.DialogTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DialogTitle.Location = new System.Drawing.Point(52, 4);
			this.DialogTitle.Name = "DialogTitle";
			this.DialogTitle.Size = new System.Drawing.Size(392, 32);
			this.DialogTitle.TabIndex = 14;
			this.DialogTitle.Text = "New Item Properties";
			// 
			// DialogPicture
			// 
			this.DialogPicture.BackColor = System.Drawing.Color.White;
			this.DialogPicture.Image = ((System.Drawing.Image)(resources.GetObject("DialogPicture.Image")));
			this.DialogPicture.Location = new System.Drawing.Point(4, 4);
			this.DialogPicture.Name = "DialogPicture";
			this.DialogPicture.Size = new System.Drawing.Size(48, 48);
			this.DialogPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.DialogPicture.TabIndex = 15;
			this.DialogPicture.TabStop = false;
			// 
			// DialogDescriptionLabel
			// 
			this.DialogDescriptionLabel.BackColor = System.Drawing.Color.White;
			this.DialogDescriptionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DialogDescriptionLabel.Location = new System.Drawing.Point(56, 36);
			this.DialogDescriptionLabel.Name = "DialogDescriptionLabel";
			this.DialogDescriptionLabel.Size = new System.Drawing.Size(372, 16);
			this.DialogDescriptionLabel.TabIndex = 16;
			this.DialogDescriptionLabel.Text = "Customize this item\'s properties";
			// 
			// DisplayPanel
			// 
			this.DisplayPanel.BackColor = System.Drawing.SystemColors.Control;
			this.DisplayPanel.Controls.Add(this.panel1);
			this.DisplayPanel.Controls.Add(this.DisplayLabel);
			this.DisplayPanel.Controls.Add(this.DisplayPicture);
			this.DisplayPanel.ForeColor = System.Drawing.Color.Black;
			this.DisplayPanel.Location = new System.Drawing.Point(4, 60);
			this.DisplayPanel.Name = "DisplayPanel";
			this.DisplayPanel.Size = new System.Drawing.Size(460, 24);
			this.DisplayPanel.TabIndex = 17;
			this.DisplayPanel.Tag = "268";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.ItemDescription);
			this.panel1.Controls.Add(this.ItemDescriptionLabel);
			this.panel1.Controls.Add(this.BrowsePictureLink);
			this.panel1.Controls.Add(this.ItemImageLabel);
			this.panel1.Controls.Add(this.ItemDisplayName);
			this.panel1.Controls.Add(this.ItemNameLabel);
			this.panel1.Controls.Add(this.ItemDisplayPicture);
			this.panel1.Location = new System.Drawing.Point(4, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(452, 240);
			this.panel1.TabIndex = 1;
			// 
			// ItemDescription
			// 
			this.ItemDescription.Location = new System.Drawing.Point(10, 64);
			this.ItemDescription.Name = "ItemDescription";
			this.ItemDescription.Size = new System.Drawing.Size(436, 21);
			this.ItemDescription.TabIndex = 1;
			this.ItemDescription.Text = "";
			this.ItemDescription.TextChanged += new System.EventHandler(this.ItemDescription_TextChanged);
			// 
			// ItemDescriptionLabel
			// 
			this.ItemDescriptionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ItemDescriptionLabel.Location = new System.Drawing.Point(6, 48);
			this.ItemDescriptionLabel.Name = "ItemDescriptionLabel";
			this.ItemDescriptionLabel.Size = new System.Drawing.Size(262, 16);
			this.ItemDescriptionLabel.TabIndex = 0;
			this.ItemDescriptionLabel.Text = "Description";
			// 
			// BrowsePictureLink
			// 
			this.BrowsePictureLink.LinkArea = new System.Windows.Forms.LinkArea(0, 24);
			this.BrowsePictureLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BrowsePictureLink.Location = new System.Drawing.Point(140, 156);
			this.BrowsePictureLink.Name = "BrowsePictureLink";
			this.BrowsePictureLink.Size = new System.Drawing.Size(304, 48);
			this.BrowsePictureLink.TabIndex = 2;
			this.BrowsePictureLink.TabStop = true;
			this.BrowsePictureLink.Text = "Browse for another image or drag an image file to the image box to change this it" +
				"em\'s display image";
			this.BrowsePictureLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.BrowsePictureLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BrowsePictureLink_LinkClicked);
			// 
			// ItemImageLabel
			// 
			this.ItemImageLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ItemImageLabel.Location = new System.Drawing.Point(4, 92);
			this.ItemImageLabel.Name = "ItemImageLabel";
			this.ItemImageLabel.Size = new System.Drawing.Size(264, 16);
			this.ItemImageLabel.TabIndex = 2;
			this.ItemImageLabel.Text = "Image";
			// 
			// ItemDisplayName
			// 
			this.ItemDisplayName.Location = new System.Drawing.Point(8, 20);
			this.ItemDisplayName.Name = "ItemDisplayName";
			this.ItemDisplayName.Size = new System.Drawing.Size(436, 21);
			this.ItemDisplayName.TabIndex = 0;
			this.ItemDisplayName.Text = "";
			this.ItemDisplayName.TextChanged += new System.EventHandler(this.ItemDisplayName_TextChanged);
			// 
			// ItemNameLabel
			// 
			this.ItemNameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ItemNameLabel.Location = new System.Drawing.Point(4, 4);
			this.ItemNameLabel.Name = "ItemNameLabel";
			this.ItemNameLabel.Size = new System.Drawing.Size(264, 16);
			this.ItemNameLabel.TabIndex = 0;
			this.ItemNameLabel.Text = "Name";
			// 
			// ItemDisplayPicture
			// 
			this.ItemDisplayPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ItemDisplayPicture.Location = new System.Drawing.Point(8, 108);
			this.ItemDisplayPicture.Name = "ItemDisplayPicture";
			this.ItemDisplayPicture.Size = new System.Drawing.Size(128, 128);
			this.ItemDisplayPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ItemDisplayPicture.TabIndex = 0;
			this.ItemDisplayPicture.TabStop = false;
			this.ItemDisplayPicture.MouseEnter += new System.EventHandler(this.ItemDisplayPicture_MouseEnter);
			this.ItemDisplayPicture.MouseLeave += new System.EventHandler(this.ItemDisplayPicture_MouseLeave);
			// 
			// DisplayLabel
			// 
			this.DisplayLabel.BackColor = System.Drawing.Color.Transparent;
			this.DisplayLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DisplayLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.DisplayLabel.Location = new System.Drawing.Point(28, 4);
			this.DisplayLabel.Name = "DisplayLabel";
			this.DisplayLabel.Size = new System.Drawing.Size(420, 16);
			this.DisplayLabel.TabIndex = 1;
			this.DisplayLabel.Text = "Display";
			// 
			// DisplayPicture
			// 
			this.DisplayPicture.BackColor = System.Drawing.SystemColors.Control;
			this.DisplayPicture.Location = new System.Drawing.Point(4, 0);
			this.DisplayPicture.Name = "DisplayPicture";
			this.DisplayPicture.Size = new System.Drawing.Size(452, 24);
			this.DisplayPicture.TabIndex = 0;
			this.DisplayPicture.TabStop = false;
			this.DisplayPicture.Click += new System.EventHandler(this.DisplayPicture_Click);
			// 
			// ItemSelectedPicture
			// 
			this.ItemSelectedPicture.Image = ((System.Drawing.Image)(resources.GetObject("ItemSelectedPicture.Image")));
			this.ItemSelectedPicture.Location = new System.Drawing.Point(420, 4);
			this.ItemSelectedPicture.Name = "ItemSelectedPicture";
			this.ItemSelectedPicture.Size = new System.Drawing.Size(24, 24);
			this.ItemSelectedPicture.TabIndex = 19;
			this.ItemSelectedPicture.TabStop = false;
			this.ItemSelectedPicture.Visible = false;
			// 
			// ItemPicture
			// 
			this.ItemPicture.Image = ((System.Drawing.Image)(resources.GetObject("ItemPicture.Image")));
			this.ItemPicture.Location = new System.Drawing.Point(396, 4);
			this.ItemPicture.Name = "ItemPicture";
			this.ItemPicture.Size = new System.Drawing.Size(24, 24);
			this.ItemPicture.TabIndex = 18;
			this.ItemPicture.TabStop = false;
			this.ItemPicture.Visible = false;
			// 
			// ActionsPanel
			// 
			this.ActionsPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ActionsPanel.Controls.Add(this.panel2);
			this.ActionsPanel.Controls.Add(this.ActionsLabel);
			this.ActionsPanel.Controls.Add(this.ActionsPicture);
			this.ActionsPanel.ForeColor = System.Drawing.Color.Black;
			this.ActionsPanel.Location = new System.Drawing.Point(5, 88);
			this.ActionsPanel.Name = "ActionsPanel";
			this.ActionsPanel.Size = new System.Drawing.Size(460, 176);
			this.ActionsPanel.TabIndex = 20;
			this.ActionsPanel.Tag = "192";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.ClassificationPanel);
			this.panel2.Controls.Add(this.PhysicalFolderLink);
			this.panel2.Controls.Add(this.ActionsDescriptionLabel);
			this.panel2.Controls.Add(this.ShortcutToWebLink);
			this.panel2.Controls.Add(this.ShortcutToFolderLink);
			this.panel2.Controls.Add(this.ConfigurationLink);
			this.panel2.Controls.Add(this.FolderLink);
			this.panel2.Controls.Add(this.ShortcutToFileLink);
			this.panel2.Controls.Add(this.TasksLink);
			this.panel2.Controls.Add(this.WhatIsThisItemLabel);
			this.panel2.Location = new System.Drawing.Point(4, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(452, 164);
			this.panel2.TabIndex = 2;
			// 
			// ClassificationPanel
			// 
			this.ClassificationPanel.Controls.Add(this.ToFilePanel);
			this.ClassificationPanel.Controls.Add(this.ShortcutPanel);
			this.ClassificationPanel.Controls.Add(this.ChangeItemLink);
			this.ClassificationPanel.Controls.Add(this.ItemClassification);
			this.ClassificationPanel.Location = new System.Drawing.Point(0, 4);
			this.ClassificationPanel.Name = "ClassificationPanel";
			this.ClassificationPanel.Size = new System.Drawing.Size(452, 156);
			this.ClassificationPanel.TabIndex = 11;
			// 
			// ToFilePanel
			// 
			this.ToFilePanel.Controls.Add(this.ItemArguments);
			this.ToFilePanel.Controls.Add(this.ShortcutArgumentsLabel);
			this.ToFilePanel.Location = new System.Drawing.Point(0, 84);
			this.ToFilePanel.Name = "ToFilePanel";
			this.ToFilePanel.Size = new System.Drawing.Size(452, 40);
			this.ToFilePanel.TabIndex = 12;
			// 
			// ItemArguments
			// 
			this.ItemArguments.Location = new System.Drawing.Point(8, 16);
			this.ItemArguments.Name = "ItemArguments";
			this.ItemArguments.Size = new System.Drawing.Size(436, 21);
			this.ItemArguments.TabIndex = 4;
			this.ItemArguments.Text = "";
			this.ItemArguments.TextChanged += new System.EventHandler(this.ItemArguments_TextChanged);
			// 
			// ShortcutArgumentsLabel
			// 
			this.ShortcutArgumentsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ShortcutArgumentsLabel.Location = new System.Drawing.Point(4, 0);
			this.ShortcutArgumentsLabel.Name = "ShortcutArgumentsLabel";
			this.ShortcutArgumentsLabel.Size = new System.Drawing.Size(252, 16);
			this.ShortcutArgumentsLabel.TabIndex = 0;
			this.ShortcutArgumentsLabel.Text = "Shortcut arguments";
			// 
			// ShortcutPanel
			// 
			this.ShortcutPanel.Controls.Add(this.BrowseLocation);
			this.ShortcutPanel.Controls.Add(this.ItemAction);
			this.ShortcutPanel.Controls.Add(this.ShortcutLocationLabel);
			this.ShortcutPanel.Location = new System.Drawing.Point(0, 36);
			this.ShortcutPanel.Name = "ShortcutPanel";
			this.ShortcutPanel.Size = new System.Drawing.Size(452, 40);
			this.ShortcutPanel.TabIndex = 11;
			// 
			// BrowseLocation
			// 
			this.BrowseLocation.BackColor = System.Drawing.SystemColors.Control;
			this.BrowseLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BrowseLocation.Location = new System.Drawing.Point(420, 16);
			this.BrowseLocation.Name = "BrowseLocation";
			this.BrowseLocation.Size = new System.Drawing.Size(24, 20);
			this.BrowseLocation.TabIndex = 3;
			this.BrowseLocation.Text = "...";
			this.BrowseLocation.Click += new System.EventHandler(this.BrowseLocation_Click);
			// 
			// ItemAction
			// 
			this.ItemAction.Location = new System.Drawing.Point(8, 16);
			this.ItemAction.Name = "ItemAction";
			this.ItemAction.Size = new System.Drawing.Size(408, 21);
			this.ItemAction.TabIndex = 2;
			this.ItemAction.Text = "";
			this.ItemAction.TextChanged += new System.EventHandler(this.ItemAction_TextChanged);
			// 
			// ShortcutLocationLabel
			// 
			this.ShortcutLocationLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ShortcutLocationLabel.Location = new System.Drawing.Point(4, 0);
			this.ShortcutLocationLabel.Name = "ShortcutLocationLabel";
			this.ShortcutLocationLabel.Size = new System.Drawing.Size(252, 16);
			this.ShortcutLocationLabel.TabIndex = 0;
			this.ShortcutLocationLabel.Text = "Shortcut location";
			// 
			// ChangeItemLink
			// 
			this.ChangeItemLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ChangeItemLink.Location = new System.Drawing.Point(8, 16);
			this.ChangeItemLink.Name = "ChangeItemLink";
			this.ChangeItemLink.Size = new System.Drawing.Size(440, 16);
			this.ChangeItemLink.TabIndex = 1;
			this.ChangeItemLink.TabStop = true;
			this.ChangeItemLink.Text = "Change this item\'s classification...";
			this.ChangeItemLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ChangeItemLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChangeItemLink_LinkClicked);
			// 
			// ItemClassification
			// 
			this.ItemClassification.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ItemClassification.Location = new System.Drawing.Point(4, 0);
			this.ItemClassification.Name = "ItemClassification";
			this.ItemClassification.Size = new System.Drawing.Size(444, 16);
			this.ItemClassification.TabIndex = 0;
			this.ItemClassification.Text = "This item is a";
			// 
			// PhysicalFolderLink
			// 
			this.PhysicalFolderLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.PhysicalFolderLink.Location = new System.Drawing.Point(8, 68);
			this.PhysicalFolderLink.Name = "PhysicalFolderLink";
			this.PhysicalFolderLink.Size = new System.Drawing.Size(428, 16);
			this.PhysicalFolderLink.TabIndex = 4;
			this.PhysicalFolderLink.TabStop = true;
			this.PhysicalFolderLink.Text = "This item shows the contents of a folder on a disk...";
			this.PhysicalFolderLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.PhysicalFolderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PhysicalFolderLink_LinkClicked);
			// 
			// ActionsDescriptionLabel
			// 
			this.ActionsDescriptionLabel.Location = new System.Drawing.Point(8, 132);
			this.ActionsDescriptionLabel.Name = "ActionsDescriptionLabel";
			this.ActionsDescriptionLabel.Size = new System.Drawing.Size(440, 28);
			this.ActionsDescriptionLabel.TabIndex = 12;
			this.ActionsDescriptionLabel.Text = "You can either select an option from above or drag a file, folder or URL into thi" +
				"s dialog to set this item to it.";
			// 
			// ShortcutToWebLink
			// 
			this.ShortcutToWebLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShortcutToWebLink.Location = new System.Drawing.Point(8, 52);
			this.ShortcutToWebLink.Name = "ShortcutToWebLink";
			this.ShortcutToWebLink.Size = new System.Drawing.Size(428, 16);
			this.ShortcutToWebLink.TabIndex = 3;
			this.ShortcutToWebLink.TabStop = true;
			this.ShortcutToWebLink.Text = "This item is a shortcut to a World Wide Web address...";
			this.ShortcutToWebLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShortcutToWebLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShortcutToWebLink_LinkClicked);
			// 
			// ShortcutToFolderLink
			// 
			this.ShortcutToFolderLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShortcutToFolderLink.Location = new System.Drawing.Point(8, 36);
			this.ShortcutToFolderLink.Name = "ShortcutToFolderLink";
			this.ShortcutToFolderLink.Size = new System.Drawing.Size(428, 16);
			this.ShortcutToFolderLink.TabIndex = 2;
			this.ShortcutToFolderLink.TabStop = true;
			this.ShortcutToFolderLink.Text = "This item is a shortcut to a folder...";
			this.ShortcutToFolderLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShortcutToFolderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShortcutToFolderLink_LinkClicked);
			// 
			// ConfigurationLink
			// 
			this.ConfigurationLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ConfigurationLink.Location = new System.Drawing.Point(8, 116);
			this.ConfigurationLink.Name = "ConfigurationLink";
			this.ConfigurationLink.Size = new System.Drawing.Size(428, 16);
			this.ConfigurationLink.TabIndex = 6;
			this.ConfigurationLink.TabStop = true;
			this.ConfigurationLink.Text = "This item brings up the Orbit Configuration Dialog...";
			this.ConfigurationLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ConfigurationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigurationLink_LinkClicked);
			// 
			// FolderLink
			// 
			this.FolderLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.FolderLink.Location = new System.Drawing.Point(8, 84);
			this.FolderLink.Name = "FolderLink";
			this.FolderLink.Size = new System.Drawing.Size(428, 16);
			this.FolderLink.TabIndex = 5;
			this.FolderLink.TabStop = true;
			this.FolderLink.Text = "This item is a folder in Orbit...";
			this.FolderLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.FolderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FolderLink_LinkClicked);
			// 
			// ShortcutToFileLink
			// 
			this.ShortcutToFileLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShortcutToFileLink.Location = new System.Drawing.Point(8, 20);
			this.ShortcutToFileLink.Name = "ShortcutToFileLink";
			this.ShortcutToFileLink.Size = new System.Drawing.Size(428, 16);
			this.ShortcutToFileLink.TabIndex = 1;
			this.ShortcutToFileLink.TabStop = true;
			this.ShortcutToFileLink.Text = "This item is a shortcut to a file...";
			this.ShortcutToFileLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShortcutToFileLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShortcutToFileLink_LinkClicked);
			// 
			// WhatIsThisItemLabel
			// 
			this.WhatIsThisItemLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.WhatIsThisItemLabel.Location = new System.Drawing.Point(4, 4);
			this.WhatIsThisItemLabel.Name = "WhatIsThisItemLabel";
			this.WhatIsThisItemLabel.Size = new System.Drawing.Size(444, 16);
			this.WhatIsThisItemLabel.TabIndex = 0;
			// 
			// TasksLink
			// 
			this.TasksLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.TasksLink.Location = new System.Drawing.Point(8, 100);
			this.TasksLink.Name = "TasksLink";
			this.TasksLink.Size = new System.Drawing.Size(428, 16);
			this.TasksLink.TabIndex = 21;
			this.TasksLink.TabStop = true;
			this.TasksLink.Text = "This item shows the tasks that are running...";
			this.TasksLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.TasksLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TasksLink_LinkClicked);
			// 
			// ActionsLabel
			// 
			this.ActionsLabel.BackColor = System.Drawing.Color.Transparent;
			this.ActionsLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ActionsLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.ActionsLabel.Location = new System.Drawing.Point(28, 4);
			this.ActionsLabel.Name = "ActionsLabel";
			this.ActionsLabel.Size = new System.Drawing.Size(420, 16);
			this.ActionsLabel.TabIndex = 1;
			this.ActionsLabel.Text = "Actions";
			// 
			// ActionsPicture
			// 
			this.ActionsPicture.BackColor = System.Drawing.SystemColors.Control;
			this.ActionsPicture.Location = new System.Drawing.Point(4, 0);
			this.ActionsPicture.Name = "ActionsPicture";
			this.ActionsPicture.Size = new System.Drawing.Size(452, 24);
			this.ActionsPicture.TabIndex = 0;
			this.ActionsPicture.TabStop = false;
			this.ActionsPicture.Click += new System.EventHandler(this.ActionsPicture_Click);
			// 
			// ItemSetup
			// 
			this.AllowDrop = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(470, 360);
			this.ControlBox = false;
			this.Controls.Add(this.DisplayPanel);
			this.Controls.Add(this.ActionsPanel);
			this.Controls.Add(this.ItemSelectedPicture);
			this.Controls.Add(this.ItemPicture);
			this.Controls.Add(this.DialogDescriptionLabel);
			this.Controls.Add(this.DialogPicture);
			this.Controls.Add(this.DialogTitle);
			this.Controls.Add(this.SavePictureBox);
			this.Controls.Add(this.ClosePictureBox);
			this.Controls.Add(this.pictureBox2);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ItemSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Item Properties";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form2_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form2_DragEnter);
			this.DisplayPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ActionsPanel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ClassificationPanel.ResumeLayout(false);
			this.ToFilePanel.ResumeLayout(false);
			this.ShortcutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Windows Forms Event Handlers
		private void Form2_Load(object sender, System.EventArgs e)
		{
			this.DisplayPicture.Image=this.ItemPicture.Image;
			this.ActionsPicture.Image=this.ItemPicture.Image;

			this.DisplayLabel.Click+=new EventHandler(DisplayPicture_Click);
			this.ActionsLabel.Click+=new EventHandler(ActionsPicture_Click);

			ShowPanel(this.DisplayPanel);
			this.DisplayPicture.Image=this.ItemSelectedPicture.Image;

			this.GetTypeFromAction();
		}

		private void Form2_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect=DragDropEffects.All;
		}

		private void Form2_DragDrop(object sender, DragEventArgs e)
		{
			/*if(1==1)
			{
				string hrm="";
				string[] Types=e.Data.GetFormats();
				foreach (string Ty in Types)
				{
					hrm+=Ty+"\n";
				}
				MessageBox.Show(hrm);
			}*/
			if(e.Data.GetDataPresent("FileNameW"))
			{
				string[] FileNames=(string[])e.Data.GetData("FileNameW");
				foreach (string File in FileNames)
				{
					if(this.GetAsImage)
					{
						try
						{
							if(System.IO.Path.GetExtension(File).ToLower()==".xml")
							{
								System.IO.StreamReader stream=new System.IO.StreamReader(File);
								System.Xml.Serialization.XmlSerializer serializer=new System.Xml.Serialization.XmlSerializer(typeof(MeshInfo));
								MeshInfo info=(MeshInfo)serializer.Deserialize(stream);
								stream.Close();
								string PreviewPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(File),info.PreviewPictureFileName);
								if(System.IO.File.Exists(PreviewPath))
								{
									this.ItemDisplayPicture.Image=Image.FromFile(PreviewPath);
								}
							}
							else
							{
								this.ItemDisplayPicture.Image=Image.FromFile(File);
							}
							EditItem.ImagePath=File;
						}
						catch(Exception)
						{
							this.ItemDisplayPicture.Image=Image.FromFile(EditItem.ImagePath);
						}
						this.DialogPicture.Image=this.ItemDisplayPicture.Image;
					} 
					else 
					{
						try
						{
							string ItemName="";
							if(System.IO.File.Exists(File))
								ItemName=File.Substring(File.LastIndexOf("\\")+1, File.LastIndexOf(".")-File.LastIndexOf("\\")-1);
							if(System.IO.Directory.Exists(File))
								ItemName=File.Substring(File.LastIndexOf("\\")+1, File.Length-File.LastIndexOf("\\")-1);
							this.ItemDisplayName.Text=ItemName;
							EditItem.Name=ItemName;
							this.ItemAction.Text=File;
							EditItem.Action=File;
							this.GetTypeFromAction();
						}
						catch(Exception){}
					}
				}
			} 
			if(e.Data.GetDataPresent(typeof(System.String)))
			{
				try
				{
					System.String File=(System.String)e.Data.GetData(typeof(System.String));
					this.ItemDisplayName.Text=File;
					EditItem.Name=File;
					this.ItemAction.Text=File;
					EditItem.Action=File;
					this.GetTypeFromAction();
				}
				catch(Exception){}
			}
			/*
			else
			{
				string hrm="";
				string[] Types=e.Data.GetFormats();
				foreach (string Ty in Types)
				{
					hrm+=Ty+"\n";
				}
				MessageBox.Show(hrm);
			}
			*/
		}

		private void ClosePictureBox_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
		}

		private void SavePictureBox_Click(object sender, System.EventArgs e)
		{
			bool CanSave=true;
			string ExtraMessage="";
			if(EditItem.Name==null || EditItem.Name=="")
			{
				CanSave=false;
				ExtraMessage+="\n"+Global.LanguageLoader.Language.ItemSetup.Messages.NeedsName;
			}
			if(EditItem.Action==null || EditItem.Action=="")
			{
				CanSave=false;
				ExtraMessage+="\n"+Global.LanguageLoader.Language.ItemSetup.Messages.NeedsAction;
			}
			if(EditItem.ImagePath==null || EditItem.ImagePath=="")
			{
				CanSave=false;
				ExtraMessage+="\n"+Global.LanguageLoader.Language.ItemSetup.Messages.NeedsImage;
			}
			string FolderPath=EditItem.Path.Substring(0, EditItem.Path.LastIndexOf("\\"));
			string[] Dirs=System.IO.Directory.GetDirectories(FolderPath.Substring(0, FolderPath.LastIndexOf("\\")));
			int i=0;
			while(i<Dirs.Length)
			{
				if(Dirs[i].Substring(Dirs[i].LastIndexOf("\\")+1, Dirs[i].Length-Dirs[i].LastIndexOf("\\")-1)==EditItem.Name && OldName!=EditItem.Name)
				{
					CanSave=false;
					ExtraMessage+="\n"+Global.LanguageLoader.Language.ItemSetup.Messages.ItemAlreadyExists;
					break;
				}
				i++;
			}
			if(CanSave)
			{
				SaveItem(EditItem.Path);
				this.DialogResult=DialogResult.OK;
			}
			else 
			{
				MessageBox.Show(Global.LanguageLoader.Language.ItemSetup.Messages.InvalidConfiguration+ExtraMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Panels
		private void DisplayPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.DisplayPanel);
			if(this.DisplayPanel.Height!=24)
			{
				this.DisplayPicture.Image=this.ItemSelectedPicture.Image;
			}
		}

		private void ActionsPicture_Click(object sender, System.EventArgs e)
		{
			ShowPanel(this.ActionsPanel);
			if(this.ActionsPanel.Height!=24)
			{
				this.ActionsPicture.Image=this.ItemSelectedPicture.Image;
			}
			this.GetTypeFromAction();
		}

		// Display Panel
		private void ItemDisplayName_TextChanged(object sender, System.EventArgs e)
		{
			if(Global.LanguageLoader!=null)
				this.Text=Global.LanguageLoader.Language.ItemSetup.DialogTitleLabel.Replace("%ItemName%", this.ItemDisplayName.Text);
			this.DialogTitle.Text=this.Text;
			EditItem.Name=this.ItemDisplayName.Text;
		}

		private void ItemDescription_TextChanged(object sender, System.EventArgs e)
		{
			EditItem.Description=this.ItemDescription.Text;
		}
		private void BrowsePictureLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog FileBrowse=new OpenFileDialog();
			//FileBrowse.Title="Browse for "+this.ItemDisplayName.Text+"'s Image";
			FileBrowse.Title=Global.LanguageLoader.Language.ItemSetup.Display.BrowsePictureLink.Substring(Global.LanguageLoader.Language.ItemSetup.Display.BrowsePictureLinkStart, Global.LanguageLoader.Language.ItemSetup.Display.BrowsePictureLinkLength);
			FileBrowse.Multiselect=false;
			FileBrowse.ValidateNames=true;
			FileBrowse.CheckFileExists=true;
			FileBrowse.Filter="Supported Image Files|*.jpg;*.gif;*.png;*.tga;*.bmp;*.ico;*.omf.xml";
			if(EditItem.ImagePath!="" && EditItem.ImagePath!=null)
				if(EditItem.ImagePath.IndexOf(":\\")>-1)
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(EditItem.ImagePath);
				else
					FileBrowse.InitialDirectory=System.IO.Path.GetDirectoryName(System.IO.Path.Combine(this.ImgPath, EditItem.ImagePath));
			else
				FileBrowse.InitialDirectory=this.ImgPath;
			FileBrowse.ShowDialog();
			if(FileBrowse.FileName!=null && System.IO.File.Exists(FileBrowse.FileName))
			{
				try
				{
					if(System.IO.Path.GetExtension(FileBrowse.FileName).ToLower()==".xml")
					{
						System.IO.StreamReader stream=new System.IO.StreamReader(FileBrowse.FileName);
						System.Xml.Serialization.XmlSerializer serializer=new System.Xml.Serialization.XmlSerializer(typeof(MeshInfo));
						MeshInfo info=(MeshInfo)serializer.Deserialize(stream);
						stream.Close();
						string PreviewPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FileBrowse.FileName),info.PreviewPictureFileName);
						if(System.IO.File.Exists(PreviewPath))
						{
							this.ItemDisplayPicture.Image=Image.FromFile(PreviewPath);
						}
					}
					else
					{
						if(System.IO.Path.GetExtension(FileBrowse.FileName).ToLower()==".ico")
						{
							using(Icon ico=new Icon(FileBrowse.FileName))
							{
								using(Icon icoLarge=new Icon(ico, 128, 128))
								{
									this.ItemDisplayPicture.Image=Orbit.Utilities.ImageHelper.GetBitmapFromIcon(icoLarge);
								}
							}
						}
						else
						{
							this.ItemDisplayPicture.Image=Image.FromFile(FileBrowse.FileName);
						}
					}
					EditItem.ImagePath=FileBrowse.FileName;
				}
				catch(Exception)
				{
					this.ItemDisplayPicture.Image=Image.FromFile(EditItem.ImagePath);
				}
				this.DialogPicture.Image=this.ItemDisplayPicture.Image;
			}
		}

		private void ItemDisplayPicture_MouseEnter(object sender, EventArgs e)
		{
			this.GetAsImage=true;
			//this.Text="in";
		}

		private void ItemDisplayPicture_MouseLeave(object sender, EventArgs e)
		{
			this.GetAsImage=false;
			//this.Text="out";
		}
		// Actions Panel
		private void ShortcutToFileLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=true;
			this.ToFilePanel.Visible=true;
			this.BrowseLocation.Enabled=true;
			this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFileLink;
			this.ItemAction.ReadOnly=false;
			this.ClassificationPanel.Visible=true;
			EditItem.InternalType=0;
		}

		private void ShortcutToFolderLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=true;
			this.ToFilePanel.Visible=false;
			this.BrowseLocation.Enabled=true;
			this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFolderLink;
			this.ClassificationPanel.Visible=true;
			this.ItemAction.ReadOnly=true;
			this.ItemAction.BackColor=Color.FromKnownColor(KnownColor.Window);
			EditItem.InternalType=1;
		}

		private void ShortcutToWebLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=true;
			this.ToFilePanel.Visible=false;
			this.BrowseLocation.Enabled=false;
			this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToWebLink;
			this.ItemAction.ReadOnly=false;
			this.ClassificationPanel.Visible=true;
			EditItem.InternalType=2;
		}

		private void PhysicalFolderLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=true;
			this.ToFilePanel.Visible=false;
			this.BrowseLocation.Enabled=true;
			this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.PhysicalFolderLink;
			this.ItemAction.ReadOnly=true;
			this.ItemAction.BackColor=Color.FromKnownColor(KnownColor.Window);
			this.ClassificationPanel.Visible=true;
			EditItem.InternalType=5;
		}
		private void FolderLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=false;
			this.ToFilePanel.Visible=false;
			this.ItemAction.ReadOnly=false;

			EditItem.Action="[dir]";
			this.GetTypeFromAction();
			EditItem.InternalType=3;
		}

		private void TasksLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=false;
			this.ToFilePanel.Visible=false;
			this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.TasksLink;
			this.ItemAction.ReadOnly=false;
			EditItem.Action="[tasks]";
			this.GetTypeFromAction();
			EditItem.InternalType=6;
		}
		private void ConfigurationLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShortcutPanel.Visible=false;
			this.ToFilePanel.Visible=false;
			this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ConfigurationLink;
			this.ItemAction.ReadOnly=false;
			EditItem.Action="[configuration]";
			this.GetTypeFromAction();
			EditItem.InternalType=4;
		}

		private void ItemAction_TextChanged(object sender, System.EventArgs e)
		{
			EditItem.Action=this.ItemAction.Text;
		}

		private void ItemArguments_TextChanged(object sender, System.EventArgs e)
		{
			EditItem.Arguments=this.ItemArguments.Text;
		}

		private void BrowseLocation_Click(object sender, System.EventArgs e)
		{
			switch(EditItem.InternalType)
			{
				case 0:
					System.Windows.Forms.OpenFileDialog BrowseFile=new OpenFileDialog();
					//BrowseFile.Title="Browse for file location";
					BrowseFile.CheckFileExists=true;
					BrowseFile.CheckPathExists=true;
					BrowseFile.Multiselect=false;
					BrowseFile.InitialDirectory=ImgPath;
					BrowseFile.Filter="Any File|*.*";
					BrowseFile.ShowDialog();
					if(System.IO.File.Exists(BrowseFile.FileName))
					{
						EditItem.Action=BrowseFile.FileName;
						this.ItemAction.Text=EditItem.Action;
					}
					break;
				case 1:
					System.Windows.Forms.FolderBrowserDialog BrowseFolder=new FolderBrowserDialog();
					//BrowseFolder.Description="Browse for folder location";
					BrowseFolder.ShowNewFolderButton=true;
					BrowseFolder.ShowDialog();
					if(System.IO.Directory.Exists(BrowseFolder.SelectedPath))
					{
						EditItem.Action=(BrowseFolder.SelectedPath+"\\").Replace("\\\\", "\\");
						this.ItemAction.Text=EditItem.Action;
					}
					break;
				case 5:
					System.Windows.Forms.FolderBrowserDialog BrowsePhysicalFolder=new FolderBrowserDialog();
					//BrowsePhysicalFolder.Description="Browse for folder location";
					BrowsePhysicalFolder.ShowNewFolderButton=true;
					BrowsePhysicalFolder.ShowDialog();
					if(System.IO.Directory.Exists(BrowsePhysicalFolder.SelectedPath))
					{
						EditItem.Arguments=(BrowsePhysicalFolder.SelectedPath+"\\").Replace("\\\\", "\\");
						this.ItemAction.Text=EditItem.Arguments;
						EditItem.Action="[physicaldir]";
					}
					break;
			}
		}

		private void ChangeItemLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ClassificationPanel.Visible=false;
			this.ShortcutPanel.Visible=false;
			this.ToFilePanel.Visible=false;
			this.ItemAction.Text="";
			this.ItemArguments.Text="";
		}
		#endregion

		#region Auxiliary Functions
		private void ShowPanel(object PanelToShow)
		{
			int OldHeight=((System.Windows.Forms.Panel)PanelToShow).Height;

			this.DisplayPicture.Image=this.ItemPicture.Image;
			this.ActionsPicture.Image=this.ItemPicture.Image;

			this.DisplayPanel.Height=24;
			this.ActionsPanel.Height=24;

			this.ActionsPanel.Top=88;

			if(OldHeight==24)
			{
				((System.Windows.Forms.Panel)PanelToShow).Height=Convert.ToInt32(((System.Windows.Forms.Panel)PanelToShow).Tag);

				switch(((System.Windows.Forms.Panel)PanelToShow).Name.ToLower())
				{
					case "displaypanel":
						this.ActionsPanel.Top+=((System.Windows.Forms.Panel)PanelToShow).Height-24;
						break;
				}
			}
		}
		private void GetTypeFromAction()
		{
			this.ClassificationPanel.Visible=false;
			this.ShortcutPanel.Visible=false;
			this.ToFilePanel.Visible=false;
			this.BrowseLocation.Enabled=true;
			this.ItemAction.ReadOnly=false;

			EditItem.InternalType=-1;
			if(System.IO.File.Exists(EditItem.Action) || (EditItem.Action.LastIndexOf("\\")!=EditItem.Action.Length && EditItem.Action!=""))
			{
				EditItem.InternalType=0;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFileLink;
				this.ShortcutPanel.Visible=true;
				this.ToFilePanel.Visible=true;
			}
			if(System.IO.Directory.Exists(EditItem.Action))
			{
				EditItem.InternalType=1;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFolderLink;
				this.ShortcutPanel.Visible=true;
				this.ToFilePanel.Visible=false;
				this.ItemAction.ReadOnly=true;
				this.ItemAction.BackColor=Color.FromKnownColor(KnownColor.Window);
			}
			if(EditItem.Action.ToLower().IndexOf("://")>=0)
			{
				EditItem.InternalType=2;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToWebLink;
				this.ShortcutPanel.Visible=true;
				this.ToFilePanel.Visible=false;
				this.BrowseLocation.Enabled=false;
			}
			if(EditItem.Action.ToLower()=="[dir]")
			{
				EditItem.InternalType=3;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.FolderLink;
				this.ShortcutPanel.Visible=false;
				this.ToFilePanel.Visible=false;
			}
			if(EditItem.Action.ToLower()=="[configuration]")
			{
				EditItem.InternalType=4;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ConfigurationLink;
				this.ShortcutPanel.Visible=false;
				this.ToFilePanel.Visible=false;
			}
			if(EditItem.Action.ToLower()=="[physicaldir]")
			{
				EditItem.InternalType=5;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.PhysicalFolderLink;
				this.ShortcutPanel.Visible=true;
				this.ToFilePanel.Visible=false;
				this.ItemAction.ReadOnly=true;
				this.ItemAction.BackColor=Color.FromKnownColor(KnownColor.Window);
			}
			if(EditItem.Action.ToLower()=="[tasks]")
			{
				EditItem.InternalType=6;
				this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.TasksLink;
				this.ShortcutPanel.Visible=false;
				this.ToFilePanel.Visible=false;
			}
			if(EditItem.InternalType==-1)
			{
				if(EditItem.Action!="")
				{
					this.ClassificationPanel.Visible=true;
					EditItem.InternalType=0;
					this.ItemClassification.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFileLink;
					this.ShortcutPanel.Visible=true;
					this.ToFilePanel.Visible=true;
				}
			} 
			else 
			{
				this.ClassificationPanel.Visible=true;
			}
		}
		private void UpdateUILanguage()
		{
			this.Text=Global.LanguageLoader.Language.ItemSetup.DialogTitleLabel.Replace("%ItemName%", this.EditItem.Name);
			this.DialogTitle.Text=this.Text;
			this.DialogDescriptionLabel.Text=Global.LanguageLoader.Language.ItemSetup.DialogDescriptionLabel;

			#region Actions
			this.ActionsLabel.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ActionsLabel;
			this.ChangeItemLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ChangeItemLink;
			this.ConfigurationLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ConfigurationLink;
			this.FolderLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.FolderLink;
			this.PhysicalFolderLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.PhysicalFolderLink;
			this.ShortcutArgumentsLabel.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutArgumentsLabel;
			this.ShortcutLocationLabel.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutLocationLabel;
			this.ShortcutToFileLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFileLink;
			this.ShortcutToFolderLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToFolderLink;
			this.ShortcutToWebLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ShortcutToWebLink;
			this.TasksLink.Text=Global.LanguageLoader.Language.ItemSetup.Actions.TasksLink;
			this.ActionsDescriptionLabel.Text=Global.LanguageLoader.Language.ItemSetup.Actions.ActionsDescriptionLabel;
			this.WhatIsThisItemLabel.Text=Global.LanguageLoader.Language.ItemSetup.Actions.WhatIsThisItemLabel;
			#endregion

			#region Display
			this.BrowsePictureLink.Text=Global.LanguageLoader.Language.ItemSetup.Display.BrowsePictureLink;
			this.BrowsePictureLink.LinkArea=new LinkArea(Global.LanguageLoader.Language.ItemSetup.Display.BrowsePictureLinkStart, Global.LanguageLoader.Language.ItemSetup.Display.BrowsePictureLinkLength);
			this.DisplayLabel.Text=Global.LanguageLoader.Language.ItemSetup.Display.DisplayLabel;
			this.ItemDescriptionLabel.Text=Global.LanguageLoader.Language.ItemSetup.Display.ItemDescriptionLabel;
			this.ItemNameLabel.Text=Global.LanguageLoader.Language.ItemSetup.Display.ItemNameLabel;
			this.ItemImageLabel.Text=Global.LanguageLoader.Language.ItemSetup.Display.ItemImageLabel;
			#endregion
		}
		#endregion

		#region My Item API
		private Item LoadItem(string ItemPath, string ImagesPath)
		{
			Item MyItem=new Item();
			MyItem.Action="";
			MyItem.Arguments="";
			MyItem.ImagePath="";
			MyItem.Name="New Item";
			MyItem.Path=ItemPath;
			if(System.IO.File.Exists(System.IO.Path.Combine(ItemPath,"item.ini")))
			{
				System.IO.StreamReader iFile=new System.IO.StreamReader(System.IO.Path.Combine(ItemPath,"item.ini"));
				while(iFile.Peek()>=0)
				{
					string[] Param=iFile.ReadLine().Split(new char[]{char.Parse("=")}, 2);
					switch(Param[0].ToLower())
					{
						case "name":
							MyItem.Name=Param[1];
							this.ItemDisplayName.Text=MyItem.Name;
							this.DialogTitle.Text=MyItem.Name+" Properties";
							this.Text=MyItem.Name+" Properties";
							OldName=MyItem.Name;
							break;
						case "description":
							MyItem.Description=Param[1];
							this.ItemDescription.Text=MyItem.Description;
							break;
						case "action":
							MyItem.Action=Param[1];
							this.ShortcutPanel.Visible=true;
							this.ToFilePanel.Visible=true;
							this.ItemAction.Text=MyItem.Action;
							switch(MyItem.Action.ToLower())
							{
								case "[dir]":
									this.ShortcutPanel.Visible=false;
									this.ToFilePanel.Visible=false;
									break;
								case "[configuration]":
									this.ShortcutPanel.Visible=false;
									this.ToFilePanel.Visible=false;
									break;
								case "[tasks]":
									this.ShortcutPanel.Visible=false;
									this.ToFilePanel.Visible=false;
									break;
								case "[physicaldir]":
									this.ToFilePanel.Visible=false;
									break;
							}
							break;
						case "args":
							MyItem.Arguments=Param[1];
							this.ItemArguments.Text=MyItem.Arguments;
							if(MyItem.Action.ToLower()=="[physicaldir]")
							{
								this.ItemAction.Text=Param[1];
							}
							break;
						case "image":
							if(Param[1].IndexOf(":\\")>0)
							{
								if(System.IO.File.Exists(Param[1]))
								{
									MyItem.ImagePath=Param[1];
									if(System.IO.Path.GetExtension(MyItem.ImagePath).ToLower()==".xml")
									{
										System.IO.StreamReader stream=new System.IO.StreamReader(MyItem.ImagePath);
										System.Xml.Serialization.XmlSerializer serializer=new System.Xml.Serialization.XmlSerializer(typeof(MeshInfo));
										MeshInfo info=(MeshInfo)serializer.Deserialize(stream);
										stream.Close();
										string PreviewPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(MyItem.ImagePath),info.PreviewPictureFileName);
										if(System.IO.File.Exists(PreviewPath))
										{
											this.ItemDisplayPicture.Image=Image.FromFile(PreviewPath);
										}
									}
									else
									{
										if(System.IO.Path.GetExtension(MyItem.ImagePath).ToLower()==".ico")
										{
											using(Icon ico=new Icon(MyItem.ImagePath))
											{
												using(Icon icoLarge=new Icon(ico, 128, 128))
												{
													this.ItemDisplayPicture.Image=Orbit.Utilities.ImageHelper.GetBitmapFromIcon(icoLarge);
												}
											}
										}
										else
										{
											this.ItemDisplayPicture.Image=Image.FromFile(MyItem.ImagePath);
										}
									}
									this.DialogPicture.Image=this.ItemDisplayPicture.Image;
								} 
								else
								{
									MyItem.ImagePath="";
								}
							} 
							else 
							{
								if(System.IO.File.Exists(ImagesPath+"\\"+Param[1]))
								{
									MyItem.ImagePath=Param[1];
									this.ItemDisplayPicture.Image=Image.FromFile(ImagesPath+"\\"+MyItem.ImagePath);
									this.DialogPicture.Image=this.ItemDisplayPicture.Image;
								} 
								else
								{
									MyItem.ImagePath="";
								}
							}
							break;
						case "mesh":
							MyItem.Description=Param[1];
							this.ItemDescription.Text=MyItem.Description;
							break;
					}
				}
				iFile.Close();
			} 
			else 
			{
				MyItem.Name="";
				MyItem.Action="";
				MyItem.Arguments="";
				MyItem.ImagePath="";
			}
			return MyItem;
		}
		private void SaveItem(string ItemPath)
		{
			System.IO.File.Delete(System.IO.Path.Combine(ItemPath,"item.ini"));
			System.IO.StreamWriter iFile=new System.IO.StreamWriter(System.IO.Path.Combine(ItemPath,"item.ini"), false);
			iFile.WriteLine("Name="+EditItem.Name);
			iFile.WriteLine("Description="+EditItem.Description);
			iFile.WriteLine("Action="+EditItem.Action);
			iFile.WriteLine("Args="+EditItem.Arguments);
			if(EditItem.ImagePath!="")
			{
				if(System.IO.Path.GetDirectoryName(EditItem.ImagePath)==this.ImgPath)
					iFile.WriteLine("Image="+System.IO.Path.GetFileName(EditItem.ImagePath));
				else
					iFile.WriteLine("Image="+EditItem.ImagePath);
			}
			iFile.Close();
			try
			{
				string FolderName=ItemSetup.GetTentativeFolderName(EditItem.Name);
				string FolderPath=ItemPath.Substring(0, ItemPath.LastIndexOf("\\"));
				FolderPath=ItemPath;
				//System.IO.Directory.Move(FolderPath, FolderPath.Substring(0, FolderPath.LastIndexOf("\\"))+"\\"+FolderName);
				System.IO.Directory.Move(FolderPath, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FolderPath),FolderName));
			}
			catch(Exception){}
		}
		static public bool SaveItem(string itemPath, string imagesPath, Item item)
		{
			try
			{
				if(!System.IO.Directory.Exists(itemPath))
					System.IO.Directory.CreateDirectory(itemPath);

				System.IO.StreamWriter iFile=new System.IO.StreamWriter(System.IO.Path.Combine(itemPath,"item.ini"), false);
				iFile.WriteLine("Name="+item.Name);
				iFile.WriteLine("Description="+item.Description);
				iFile.WriteLine("Action="+item.Action);
				iFile.WriteLine("Args="+item.Arguments);
				if(item.ImagePath!="")
				{
					if(System.IO.Path.GetDirectoryName(item.ImagePath)==imagesPath)
						iFile.WriteLine("Image="+System.IO.Path.GetFileName(item.ImagePath));
					else
						iFile.WriteLine("Image="+item.ImagePath);
				}
				iFile.Close();
			}
			catch(Exception)
			{
				return false;
			}
			return true;
		}
		static public string GetTentativeFolderName(string itemName)
		{
			string folderName=itemName.Replace("/", "");
			folderName=folderName.Replace("\\", "");
			folderName=folderName.Replace(":", "");
			folderName=folderName.Replace("*","");
			folderName=folderName.Replace("?","");
			folderName=folderName.Replace("\"","");
			folderName=folderName.Replace("<","");
			folderName=folderName.Replace(">","");
			folderName=folderName.Replace("|","");
			return folderName;
		}
		#endregion

		private void Language_LanguageLoaded(object sender, EventArgs e)
		{
			UpdateUILanguage();
		}
	}
}
