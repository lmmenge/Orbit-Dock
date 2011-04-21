using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Orbit.Language;
using Orbit.Utilities;

namespace Orbit.Configuration
{
	/// <summary>
	/// Form that allows you to manage the excluded tasks from Orbit
	/// </summary>
	public class ExcludedTasksSetup : System.Windows.Forms.Form
	{
		#region Form Elements
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Label ExcludedTasksLabel;
		private System.Windows.Forms.Label ExcludedTasksDescriptionLabel;
		private System.Windows.Forms.PictureBox SavePictureBox;
		private System.Windows.Forms.PictureBox ClosePictureBox;
		private System.Windows.Forms.Label InstructionsLabel;
		private System.Windows.Forms.Panel TasksListPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructor
		public ExcludedTasksSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			try
			{
				LoadExcludedTasks();
			}
			catch(Exception){}

			UpdateUILanguage();
			Global.LanguageLoader.LanguageLoaded+=new EventHandler(Language_LanguageLoaded);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			foreach(TaskItemControl tic in this.TasksListPanel.Controls)
			{
				if(tic!=null)
				{
					if(tic.WindowScreenshot!=null)
						tic.WindowScreenshot.Dispose();
					tic.Dispose();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExcludedTasksSetup));
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.ExcludedTasksLabel = new System.Windows.Forms.Label();
			this.ExcludedTasksDescriptionLabel = new System.Windows.Forms.Label();
			this.SavePictureBox = new System.Windows.Forms.PictureBox();
			this.ClosePictureBox = new System.Windows.Forms.PictureBox();
			this.InstructionsLabel = new System.Windows.Forms.Label();
			this.TasksListPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.White;
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(424, 56);
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackColor = System.Drawing.Color.White;
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(4, 4);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(48, 48);
			this.pictureBox3.TabIndex = 5;
			this.pictureBox3.TabStop = false;
			// 
			// ExcludedTasksLabel
			// 
			this.ExcludedTasksLabel.BackColor = System.Drawing.Color.White;
			this.ExcludedTasksLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ExcludedTasksLabel.Location = new System.Drawing.Point(52, 4);
			this.ExcludedTasksLabel.Name = "ExcludedTasksLabel";
			this.ExcludedTasksLabel.Size = new System.Drawing.Size(292, 32);
			this.ExcludedTasksLabel.TabIndex = 6;
			this.ExcludedTasksLabel.Text = "Excluded Tasks";
			// 
			// ExcludedTasksDescriptionLabel
			// 
			this.ExcludedTasksDescriptionLabel.BackColor = System.Drawing.Color.White;
			this.ExcludedTasksDescriptionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ExcludedTasksDescriptionLabel.Location = new System.Drawing.Point(56, 36);
			this.ExcludedTasksDescriptionLabel.Name = "ExcludedTasksDescriptionLabel";
			this.ExcludedTasksDescriptionLabel.Size = new System.Drawing.Size(288, 16);
			this.ExcludedTasksDescriptionLabel.TabIndex = 9;
			this.ExcludedTasksDescriptionLabel.Text = "Manage the task lister";
			// 
			// SavePictureBox
			// 
			this.SavePictureBox.BackColor = System.Drawing.Color.White;
			this.SavePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SavePictureBox.Image")));
			this.SavePictureBox.Location = new System.Drawing.Point(396, 4);
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
			this.ClosePictureBox.Location = new System.Drawing.Point(396, 28);
			this.ClosePictureBox.Name = "ClosePictureBox";
			this.ClosePictureBox.Size = new System.Drawing.Size(24, 24);
			this.ClosePictureBox.TabIndex = 12;
			this.ClosePictureBox.TabStop = false;
			this.ClosePictureBox.Click += new System.EventHandler(this.ClosePictureBox_Click);
			// 
			// InstructionsLabel
			// 
			this.InstructionsLabel.Location = new System.Drawing.Point(8, 60);
			this.InstructionsLabel.Name = "InstructionsLabel";
			this.InstructionsLabel.Size = new System.Drawing.Size(408, 28);
			this.InstructionsLabel.TabIndex = 15;
			this.InstructionsLabel.Text = "Below is a list of the tasks that are excluded from Orbit\'s task folder. Use the " +
				"link on each of them to manage them.";
			// 
			// TasksListPanel
			// 
			this.TasksListPanel.AutoScroll = true;
			this.TasksListPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.TasksListPanel.Location = new System.Drawing.Point(8, 92);
			this.TasksListPanel.Name = "TasksListPanel";
			this.TasksListPanel.Size = new System.Drawing.Size(408, 220);
			this.TasksListPanel.TabIndex = 16;
			// 
			// ExcludedTasksSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(422, 319);
			this.ControlBox = false;
			this.Controls.Add(this.TasksListPanel);
			this.Controls.Add(this.InstructionsLabel);
			this.Controls.Add(this.SavePictureBox);
			this.Controls.Add(this.ClosePictureBox);
			this.Controls.Add(this.ExcludedTasksDescriptionLabel);
			this.Controls.Add(this.ExcludedTasksLabel);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExcludedTasksSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Excluded Tasks";
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region Titlebar Event Handling
		private void ClosePictureBox_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SavePictureBox_Click(object sender, System.EventArgs e)
		{
			SaveExcludedTasks();
			this.Close();
		}
		#endregion

		#region Internal Helper Methods
		private void LoadExcludedTasks()
		{
			// load the exclusion list
			ExcludedWindow[] ExclusionList=WindowsTaskManager.LoadExcludedClassWindows(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), @"profiles\"+System.Environment.UserName+@"\ExcludedTasks.xml"));

			if(ExclusionList==null)
				return;

			// actually catalog them
			int i=0;
			foreach(ExcludedWindow window in ExclusionList)
			{
				TaskItemControl tic=new TaskItemControl();

				tic.ProcessName=window.ProcessName;
				tic.ClassName=window.ClassName;

				// try to find a currently running window that matches this spec
				IntPtr FoundWindow=FindFirstWindowLike(window);
				if(FoundWindow!=IntPtr.Zero)
				{
					tic.WindowTitle=WindowsTaskManager.GetWindowText(FoundWindow);
					using(Bitmap shot=WindowsTaskManager.GetWindowBitmap(FoundWindow))
					{
						tic.WindowScreenshot=ImageHelper.GetAspectThumbnail(shot, new Size(64,64));
					}
				}
				else
				{
					tic.WindowTitle=System.IO.Path.GetFileNameWithoutExtension(window.ProcessName);
				}

				// set this properties
				tic.Width=this.TasksListPanel.Width;
				tic.Anchor|=AnchorStyles.Right;
				tic.Top=tic.Height*i;

				// hook up events
				tic.RemoveLinkClicked+=new EventHandler(tic_RemoveLinkClicked);

				this.TasksListPanel.Controls.Add(tic);
				i++;
			}
		}
		private void SaveExcludedTasks()
		{
			// copy to array
			ExcludedWindow[] ExcludedTasks=new ExcludedWindow[this.TasksListPanel.Controls.Count];
			int i=0;
			while(i<ExcludedTasks.Length)
			{
				TaskItemControl tic=(TaskItemControl)this.TasksListPanel.Controls[i];
				ExcludedTasks[i]=new ExcludedWindow(tic.ClassName, tic.ProcessName);
				i++;
			}

			// save
			WindowsTaskManager.SaveExcludedClassWindows(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), @"profiles\"+System.Environment.UserName+@"\ExcludedTasks.xml"), ExcludedTasks);
		}
		#endregion

		#region Private Helper Methods
		private IntPtr FindFirstWindowLike(ExcludedWindow Window)
		{
			IntPtr[] Windows=WindowsTaskManager.GetWindowHandles();
			foreach(IntPtr Handle in Windows)
			{
				if(WindowsTaskManager.GetWindowClass(Handle)==Window.ClassName
					&& System.IO.Path.GetFileName(WindowsTaskManager.GetExecutableName(Handle)).ToLower()==Window.ProcessName.ToLower())
					return Handle;
			}
			return IntPtr.Zero;
		}
		private void UpdateUILanguage()
		{
			this.Text=Global.LanguageLoader.Language.ExcludedTasks.ExcludedTasksLabel;
			this.ExcludedTasksLabel.Text=this.Text;
			this.ExcludedTasksDescriptionLabel.Text=Global.LanguageLoader.Language.ExcludedTasks.ExcludedTasksDescriptionLabel;

			this.InstructionsLabel.Text=Global.LanguageLoader.Language.ExcludedTasks.InstructionsLabel;

			foreach(TaskItemControl tic in TasksListPanel.Controls)
			{
				// set language
				tic.StopIgnoringThisWindowLink=Global.LanguageLoader.Language.ExcludedTasks.StopIgnoringThisWindowLink;
			}
		}
		#endregion

		#region Excluded Tasks Event Handling
		private void tic_RemoveLinkClicked(object sender, EventArgs e)
		{
			TasksListPanel.Controls.Remove((TaskItemControl)sender);

			int i=0;
			foreach(TaskItemControl tic in TasksListPanel.Controls)
			{
				tic.Top=tic.Height*i;
				i++;
			}
		}
		#endregion

		private void Language_LanguageLoaded(object sender, EventArgs e)
		{
			UpdateUILanguage();
		}
	}
}
