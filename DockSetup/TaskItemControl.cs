using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Orbit.Configuration
{
	/// <summary>
	/// Summary description for TaskItem.
	/// </summary>
	public class TaskItemControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox WindowScreenshotPictureBox;
		private System.Windows.Forms.Label WindowTitleLabel;
		private System.Windows.Forms.Label ProcessNameLabel;
		private System.Windows.Forms.LinkLabel StopIgnoringLink;
		private System.Windows.Forms.Label ClassNameLabel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TaskItemControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(this.WindowScreenshotPictureBox.Image!=null)
				this.WindowScreenshotPictureBox.Image.Dispose();

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.WindowScreenshotPictureBox = new System.Windows.Forms.PictureBox();
			this.WindowTitleLabel = new System.Windows.Forms.Label();
			this.ProcessNameLabel = new System.Windows.Forms.Label();
			this.StopIgnoringLink = new System.Windows.Forms.LinkLabel();
			this.ClassNameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// WindowScreenshotPictureBox
			// 
			this.WindowScreenshotPictureBox.Location = new System.Drawing.Point(4, 4);
			this.WindowScreenshotPictureBox.Name = "WindowScreenshotPictureBox";
			this.WindowScreenshotPictureBox.Size = new System.Drawing.Size(64, 64);
			this.WindowScreenshotPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.WindowScreenshotPictureBox.TabIndex = 0;
			this.WindowScreenshotPictureBox.TabStop = false;
			// 
			// WindowTitleLabel
			// 
			this.WindowTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.WindowTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.WindowTitleLabel.Location = new System.Drawing.Point(72, 4);
			this.WindowTitleLabel.Name = "WindowTitleLabel";
			this.WindowTitleLabel.Size = new System.Drawing.Size(252, 16);
			this.WindowTitleLabel.TabIndex = 1;
			this.WindowTitleLabel.Text = "WindowTitle";
			// 
			// ProcessNameLabel
			// 
			this.ProcessNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ProcessNameLabel.Location = new System.Drawing.Point(72, 20);
			this.ProcessNameLabel.Name = "ProcessNameLabel";
			this.ProcessNameLabel.Size = new System.Drawing.Size(252, 16);
			this.ProcessNameLabel.TabIndex = 2;
			this.ProcessNameLabel.Text = "ProcessName";
			// 
			// StopIgnoringLink
			// 
			this.StopIgnoringLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.StopIgnoringLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.StopIgnoringLink.Location = new System.Drawing.Point(72, 52);
			this.StopIgnoringLink.Name = "StopIgnoringLink";
			this.StopIgnoringLink.Size = new System.Drawing.Size(252, 16);
			this.StopIgnoringLink.TabIndex = 3;
			this.StopIgnoringLink.TabStop = true;
			this.StopIgnoringLink.Text = "Stop ignoring this window";
			this.StopIgnoringLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.StopIgnoringLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StopIgnoringLink_LinkClicked);
			// 
			// ClassNameLabel
			// 
			this.ClassNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ClassNameLabel.Location = new System.Drawing.Point(72, 36);
			this.ClassNameLabel.Name = "ClassNameLabel";
			this.ClassNameLabel.Size = new System.Drawing.Size(252, 16);
			this.ClassNameLabel.TabIndex = 4;
			this.ClassNameLabel.Text = "ClassName";
			// 
			// TaskItemControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.ClassNameLabel);
			this.Controls.Add(this.StopIgnoringLink);
			this.Controls.Add(this.ProcessNameLabel);
			this.Controls.Add(this.WindowTitleLabel);
			this.Controls.Add(this.WindowScreenshotPictureBox);
			this.Name = "TaskItemControl";
			this.Size = new System.Drawing.Size(328, 72);
			this.ResumeLayout(false);

		}
		#endregion

		#region Form Event Handling
		private void StopIgnoringLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(RemoveLinkClicked!=null)RemoveLinkClicked(this, new EventArgs());
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets the WindowTitle message shown
		/// </summary>
		public string WindowTitle
		{
			get
			{
				return WindowTitleLabel.Text;
			}
			set
			{
				if(WindowTitleLabel.Text!=value)
				{
					int MaxLen=40;
					if(value.Length<MaxLen+4)
						WindowTitleLabel.Text=value;
					else
						WindowTitleLabel.Text=value.Substring(0,MaxLen)+"...";
				}
			}
		}
		/// <summary>
		/// Gets/Sets the ProcessName message shown
		/// </summary>
		public string ProcessName
		{
			get
			{
				return ProcessNameLabel.Text;
			}
			set
			{
				if(ProcessNameLabel.Text!=value)
					ProcessNameLabel.Text=value;
			}
		}
		/// <summary>
		/// Gets/Sets the ClassName message shown
		/// </summary>
		public string ClassName
		{
			get
			{
				return ClassNameLabel.Text;
			}
			set
			{
				if(ClassNameLabel.Text!=value)
					ClassNameLabel.Text=value;
			}
		}
		/// <summary>
		/// Gets/Sets the screenshot shown
		/// </summary>
		public Image WindowScreenshot
		{
			get
			{
				return WindowScreenshotPictureBox.Image;
			}
			set
			{
				if(WindowScreenshotPictureBox.Image!=value)
					WindowScreenshotPictureBox.Image=value;
			}
		}
		/// <summary>
		/// Gets/Sets the text for the "Stop Ignoring this window..." link
		/// </summary>
		public string StopIgnoringThisWindowLink
		{
			get
			{
				return StopIgnoringLink.Text;
			}
			set
			{
				if(StopIgnoringLink.Text!=value)
					StopIgnoringLink.Text=value;
			}
		}
		#endregion

		#region Events
		public event EventHandler RemoveLinkClicked;
		#endregion
	}
}
