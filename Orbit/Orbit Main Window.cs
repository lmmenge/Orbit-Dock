using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Orbit.Items;
using Orbit.Core;
using Orbit.Reference;
using Orbit.Utilities;
using Orbit.Configuration;
using Orbit.Language;
using Orbit.OrbitServices.OrbitServicesClient;


namespace Orbit
{
	/// <summary>
	/// Main Program
	/// </summary>
	public class OrbitWindow : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		#region Program Variables (big)
		#region DirectX Resources Variables
		// Direct3D manager
		private Direct3DManager RenderEngine;
		#endregion

		#region Runtime Control Variables
		// the program state
		private ProgramState ProgramDoing=ProgramState.Nothing;
		// index of the item to be popped after mouse-over popping timer has expired
		//private int FolderToPop=-1;
		private Orbit.Utilities.FolderPopper FolderPopper;
		/// <summary>
		/// Indicates that Orbit should have Always-On-Top windows
		/// </summary>
		public bool ShowOnTop;
		// flag that tracks the CTRL key for opening items in first loop when it's pressed
		private bool OpenInStartLoop=false;
		// indicates whether the hit code should tolerate a click that hits no items before hiding
		private bool TolerateNoHit=false;
		// control the hiding of the form for the first time that it's created
		private bool HideOnEmpty=true;
		#endregion

		#region Registry Variables
		// registry object
		private OrbitItemRegistry ItemRegistry;
		// last mouse-overed line
		private int SelectedLine=-1;
		// last item to be right clicked
		private int RightClickMenuOnItem=-1;
		#endregion

		#region Timers
		System.Threading.Timer tOutTimer;
		#endregion
		#endregion

		#region Form Items

		private System.Windows.Forms.ContextMenu ItemContextMenu;
		private System.Windows.Forms.MenuItem RemoveItemMenu;
		private System.Windows.Forms.MenuItem ItemPropertiesMenu;
		private System.Windows.Forms.MenuItem ConfigurationMenu;
		private System.Windows.Forms.MenuItem ExitMenu;
		private System.Windows.Forms.MenuItem MenuSeparator1;
		private System.Windows.Forms.MenuItem MenuSeparator2;
		private System.Windows.Forms.MenuItem AddItemToLevelMenu;
		private System.Windows.Forms.MenuItem AddItemToItemMenu;
		private System.Windows.Forms.MenuItem OpenInStart;
		private System.Windows.Forms.MenuItem OrbitSubMenu;
		private System.Windows.Forms.MenuItem OpenInExplorerMenu;
		private System.Windows.Forms.MenuItem ManualMenu;
		private System.Windows.Forms.NotifyIcon OrbitTray;
		private System.Windows.Forms.MenuItem CheckForUpdateMenu;
		private System.Windows.Forms.MenuItem LanguageMenu;
		private System.Windows.Forms.MenuItem EnglishLanguageMenu;
		private System.Windows.Forms.MenuItem MenuSeparator3;
		private System.Windows.Forms.MenuItem MenuSeparator4;
		//private System.Windows.Forms.Timer FolderPopTimer;
		private System.Windows.Forms.MenuItem IgnoreWindowMenu;
		#endregion

		#region Form Creation and Disposal functions
		/// <summary>
		/// Creates a new instance of the OrbitWindow class
		/// </summary>
		public OrbitWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//this.Load+=new EventHandler(OrbitWindow_Load);

			// check and/or create the dirs needed
			this.VerifyDirectories();

			InitializeOrbit();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			OrbitTray.Visible=false;
			DeInitializeD3D();

			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OrbitWindow));
			this.ItemContextMenu = new System.Windows.Forms.ContextMenu();
			this.OpenInStart = new System.Windows.Forms.MenuItem();
			this.OpenInExplorerMenu = new System.Windows.Forms.MenuItem();
			this.AddItemToLevelMenu = new System.Windows.Forms.MenuItem();
			this.AddItemToItemMenu = new System.Windows.Forms.MenuItem();
			this.RemoveItemMenu = new System.Windows.Forms.MenuItem();
			this.IgnoreWindowMenu = new System.Windows.Forms.MenuItem();
			this.MenuSeparator1 = new System.Windows.Forms.MenuItem();
			this.ItemPropertiesMenu = new System.Windows.Forms.MenuItem();
			this.MenuSeparator2 = new System.Windows.Forms.MenuItem();
			this.OrbitSubMenu = new System.Windows.Forms.MenuItem();
			this.ConfigurationMenu = new System.Windows.Forms.MenuItem();
			this.LanguageMenu = new System.Windows.Forms.MenuItem();
			this.EnglishLanguageMenu = new System.Windows.Forms.MenuItem();
			this.MenuSeparator3 = new System.Windows.Forms.MenuItem();
			this.ManualMenu = new System.Windows.Forms.MenuItem();
			this.CheckForUpdateMenu = new System.Windows.Forms.MenuItem();
			this.MenuSeparator4 = new System.Windows.Forms.MenuItem();
			this.ExitMenu = new System.Windows.Forms.MenuItem();
			this.OrbitTray = new System.Windows.Forms.NotifyIcon(this.components);
			// 
			// ItemContextMenu
			// 
			this.ItemContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.OpenInStart,
																							this.OpenInExplorerMenu,
																							this.AddItemToLevelMenu,
																							this.AddItemToItemMenu,
																							this.RemoveItemMenu,
																							this.IgnoreWindowMenu,
																							this.MenuSeparator1,
																							this.ItemPropertiesMenu,
																							this.MenuSeparator2,
																							this.OrbitSubMenu});
			// 
			// OpenInStart
			// 
			this.OpenInStart.Index = 0;
			this.OpenInStart.Text = "Start Loops here";
			this.OpenInStart.Click += new System.EventHandler(this.OpenInStart_Click);
			// 
			// OpenInExplorerMenu
			// 
			this.OpenInExplorerMenu.Index = 1;
			this.OpenInExplorerMenu.Text = "Open in Explorer";
			this.OpenInExplorerMenu.Click += new System.EventHandler(this.OpenInExplorerMenu_Click);
			// 
			// AddItemToLevelMenu
			// 
			this.AddItemToLevelMenu.Index = 2;
			this.AddItemToLevelMenu.Text = "Add item to this level...";
			this.AddItemToLevelMenu.Click += new System.EventHandler(this.AddItemToLevelMenu_Click);
			// 
			// AddItemToItemMenu
			// 
			this.AddItemToItemMenu.Index = 3;
			this.AddItemToItemMenu.Text = "Add item to\"%ItemName%\"...";
			this.AddItemToItemMenu.Click += new System.EventHandler(this.AddItemToItemMenu_Click);
			// 
			// RemoveItemMenu
			// 
			this.RemoveItemMenu.Index = 4;
			this.RemoveItemMenu.Text = "Remove this item";
			this.RemoveItemMenu.Click += new System.EventHandler(this.RemoveItemMenu_Click);
			// 
			// IgnoreWindowMenu
			// 
			this.IgnoreWindowMenu.Index = 5;
			this.IgnoreWindowMenu.Text = "Ignore this window";
			this.IgnoreWindowMenu.Click += new System.EventHandler(this.IgnoreWindowMenu_Click);
			// 
			// MenuSeparator1
			// 
			this.MenuSeparator1.Index = 6;
			this.MenuSeparator1.Text = "-";
			// 
			// ItemPropertiesMenu
			// 
			this.ItemPropertiesMenu.Index = 7;
			this.ItemPropertiesMenu.Text = "Properties...";
			this.ItemPropertiesMenu.Click += new System.EventHandler(this.ItemPropertiesMenu_Click);
			// 
			// MenuSeparator2
			// 
			this.MenuSeparator2.Index = 8;
			this.MenuSeparator2.Text = "-";
			this.MenuSeparator2.Visible = false;
			// 
			// OrbitSubMenu
			// 
			this.OrbitSubMenu.Index = 9;
			this.OrbitSubMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.ConfigurationMenu,
																						 this.LanguageMenu,
																						 this.MenuSeparator3,
																						 this.ManualMenu,
																						 this.CheckForUpdateMenu,
																						 this.MenuSeparator4,
																						 this.ExitMenu});
			this.OrbitSubMenu.Text = "Orbit";
			this.OrbitSubMenu.Visible = false;
			// 
			// ConfigurationMenu
			// 
			this.ConfigurationMenu.Index = 0;
			this.ConfigurationMenu.Text = "Configuration...";
			this.ConfigurationMenu.Click += new System.EventHandler(this.ConfigurationMenu_Click);
			// 
			// LanguageMenu
			// 
			this.LanguageMenu.Index = 1;
			this.LanguageMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.EnglishLanguageMenu});
			this.LanguageMenu.Text = "Language";
			// 
			// EnglishLanguageMenu
			// 
			this.EnglishLanguageMenu.Index = 0;
			this.EnglishLanguageMenu.Text = "English";
			// 
			// MenuSeparator3
			// 
			this.MenuSeparator3.Index = 2;
			this.MenuSeparator3.Text = "-";
			// 
			// ManualMenu
			// 
			this.ManualMenu.Index = 3;
			this.ManualMenu.Text = "Online Manual...";
			this.ManualMenu.Click += new System.EventHandler(this.ManualMenu_Click);
			// 
			// CheckForUpdateMenu
			// 
			this.CheckForUpdateMenu.Index = 4;
			this.CheckForUpdateMenu.Text = "Check for update...";
			this.CheckForUpdateMenu.Click += new System.EventHandler(this.CheckForUpdateMenu_Click);
			// 
			// MenuSeparator4
			// 
			this.MenuSeparator4.Index = 5;
			this.MenuSeparator4.Text = "-";
			// 
			// ExitMenu
			// 
			this.ExitMenu.Index = 6;
			this.ExitMenu.Text = "Exit";
			this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
			// 
			// OrbitTray
			// 
			this.OrbitTray.Icon = ((System.Drawing.Icon)(resources.GetObject("OrbitTray.Icon")));
			this.OrbitTray.Text = "Orbit";
			// 
			// OrbitWindow
			// 
			this.AllowDrop = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(416, 272);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "OrbitWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Orbit";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OrbitWindow_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OrbitWindow_Closing);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OrbitWindow_KeyPress);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OrbitWindow_MouseUp);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.OrbitWindow_DragOver);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OrbitWindow_KeyUp);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OrbitWindow_DragDrop);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OrbitWindow_MouseWheel);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OrbitWindow_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OrbitWindow_MouseMove);
			this.Deactivate += new System.EventHandler(this.OrbitWindow_Deactivate);

		}
		#endregion

		#region Core Program Code
		#region Transformation Methods
		private void RotateLoop(int Loop, int WheelDelta)
		{
			if(Math.Abs(WheelDelta)<120)
				return;

			// render multiple frames per wheel turn
			int i=0;
			while(i<Convert.ToInt32(Global.Configuration.Appearance.MouseWheelSensitivity/2))
			{
				// actually rotate
				ItemRegistry.Lines[Loop].RotatedDegrees+=-1*WheelDelta/30;
				// if goes over 360 or below -360, reset to zero (avoid big numbers)
				if(ItemRegistry.Lines[Loop].RotatedDegrees==360
					|| ItemRegistry.Lines[Loop].RotatedDegrees==-360) 
					ItemRegistry.Lines[Loop].RotatedDegrees=0;
				// force frame render
				/*RenderEngine.State=LoopState.Loop;
				RenderEngine.MainLoop();*/
				RenderEngine.ForceRenderNextFrameWait();
				//System.Threading.Thread.Sleep(10);
				i++;
			}
			RenderEngine.ForceRenderNextFrameWait();
			// rotate overlay window too
			if(Global.Configuration.Appearance.Transparency==TransparencyMode.Real)
				RenderEngine.UpdateTransparentOverlay();
		}

		#endregion

		#region Orbit Startup
		private void InitializeOrbit()
		{
			// run update checker if network is present
			this.CheckForOrbitUpdate(true);

			if(ShowOnTop)
				Win32.User32.User32API.SetWindowPos(this.Handle, -1, 0, 0, 0, 0, Win32.User32.WindowPositionSizeFlags.NoMove | Win32.User32.WindowPositionSizeFlags.NoSize);

			// refresh language list
			this.RefreshLanguageList();
			// load the language
			Global.LanguageLoader=new LanguageLoader();
			Global.LanguageLoader.LanguageLoaded+=new EventHandler(Language_LanguageLoaded);
			this.EnglishLanguageMenu.Click+=new EventHandler(SwitchLanguage);
			Global.LanguageLoader.LoadDefaultLanguage();

			bool isConfigOK=false;
			while(!isConfigOK)
			{
				// load the config
				LoadConfig();

				// check configuration
				if(CheckConfig())
				{
					// everything's ok: proceed
					isConfigOK=true;
				}
				else
				{
					// we need to reconfigure it:
					MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.InvalidINIFile, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
					using(DockSetup MySetup=new DockSetup())
					{
						if(MySetup.ShowDialog()!=DialogResult.OK)
						{
							// invalid configuration and user won't make it valid. Quitting.
							System.Diagnostics.Process.GetCurrentProcess().Kill();
						}
					}
				}					
			}

			// Item indexer
			ItemRegistry=new OrbitItemRegistry();
			ItemRegistry.LineQuantityChanged+=new EventHandler(ItemRegistry_LineQuantityChanged);
			ItemRegistry.Paint+=new EventHandler(TS_Paint);

			// set my position before initing DX
			// this has to be done after config has been init
			this.SizeWindow();

			// load our mouse-over pop timer
			FolderPopper = new FolderPopper();
			FolderPopper.Tick+=new EventHandler(FolderPopper_Tick);

			// init directx above all :P
			RenderEngine=new Direct3DManager(this);
			RenderEngine.DeviceLost+=new EventHandler(RenderEngine_DeviceLost);
			RenderEngine.ForcedModeSwitch+=new ForcedModeSwitchEventHandler(RenderEngine_ForcedModeSwitch);
			RenderEngine.FrameTick+=new EventHandler(RenderEngine_FrameTick);
			RenderEngine.Tick+=new EventHandler(RenderEngine_Tick);
			RenderEngine.RendererReset+=new EventHandler(RenderEngine_RendererReset);
			RenderEngine.WindowResize+=new EventHandler(RenderEngine_WindowResize);
			RenderEngine.Items=ItemRegistry;
			// init RenderEngine
			InitializationResult DxInitStats=InitializeD3D();

			if(DxInitStats==InitializationResult.Successful)
			{
				//this.FrameTimer.Enabled=true;
				RenderEngine.Resume();

				// registering HotKey
				Win32.User32.User32API.RegisterHotKey(this.Handle, 100, Win32.User32.KeyModifiers.Windows, Keys.W);
				Win32.User32.User32API.RegisterHotKey(this.Handle, 101, Win32.User32.KeyModifiers.Windows, Keys.Q);
				if(Global.Configuration.Behavior.PopKey!=Keys.None)
				{
					Orbit.Utilities.MouseHook.StartHook(this.Handle);
					Orbit.Utilities.MouseHook.SetShortcutKey(Global.Configuration.Behavior.PopKey);
				}

				// setup notify icon
				this.OrbitTray.Text="Click here to pop Orbit up";
				this.OrbitTray.MouseUp+=new MouseEventHandler(OrbitTray_MouseUp);
				this.OrbitTray.ContextMenu=new ContextMenu();
				this.OrbitTray.ContextMenu.MergeMenu(this.OrbitSubMenu);
				this.OrbitTray.Visible=true;

				// loop state - start it all
				RenderEngine.State=LoopState.Nothing;
				ProgramDoing=ProgramState.Hidden;
			}
			else
			{
				// tell the user why i failed to init
				switch(DxInitStats)
				{
					case InitializationResult.Failed:
						MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.CannotCreateD3DDevice, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						break;
					case InitializationResult.VideoCardNotSupported:
						MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.VideoCardNotSupported, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
				}
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}

		private InitializationResult InitializeD3D()
		{
			// start the magic
			InitializationResult DxInitStats=RenderEngine.Start();
			if(DxInitStats==InitializationResult.Successful
				&& Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				// hook 3d forms if real transparent mode
				RenderEngine.TransparentResourceManager.HookFormEvents(
					new EventHandler(Disp_Closed),
					new EventHandler(OrbitWindow_Deactivate),
					new MouseEventHandler(OrbitWindow_MouseMove),
					new MouseEventHandler(OrbitWindow_MouseUp),
					new MouseEventHandler(OrbitWindow_MouseWheel),
					new KeyEventHandler(OrbitWindow_KeyDown),
					new KeyEventHandler(OrbitWindow_KeyUp)
					);
			}
			return DxInitStats;
		}
		private void DeInitializeD3D()
		{
			if(RenderEngine!=null)
			{
				RenderEngine.Pause();
				RenderEngine.Stop();
				RenderEngine.Dispose();
			}
		}
		#endregion
		#endregion

		#region Event Handling
		#region Control Event Handling
		#region Timers
		private void FolderPopper_Tick(object sender, System.EventArgs e)
		{
			try
			{
				ExecuteFolderItem((OrbitItem)sender);
				/*// get the clicked item
				int SI=ItemRegistry.GetSelectedItem();
				if(SI==this.FolderToPop && SI!=-1)
				{
					// "click" on it if it actually exists
					if(!ItemRegistry[SI].IsToggled)
						ExecuteFolderItem(ItemRegistry[SI]);
				}
				// no folder to pop, so disable the timer
				this.FolderToPop=-1;
				this.FolderPopTimer.Enabled=false;*/
			}
			catch(Exception err)
			{
				//this.FolderPopTimer.Enabled=false;
				FolderPopper.Enabled=false;
				this.HandleErrorReport(err.GetType().ToString(), err.StackTrace);
			}
		}
		/*private void FolderPopTimer_Tick(object sender, System.EventArgs e)
		{
			try
			{
				// get the clicked item
				int SI=ItemRegistry.GetSelectedItem();
				if(SI==this.FolderToPop && SI!=-1)
				{
					// "click" on it if it actually exists
					if(!ItemRegistry[SI].IsToggled)
						ExecuteFolderItem(ItemRegistry[SI]);
				}
				// no folder to pop, so disable the timer
				this.FolderToPop=-1;
				this.FolderPopTimer.Enabled=false;
			}
			catch(Exception err)
			{
				this.FolderPopTimer.Enabled=false;
				this.HandleErrorReport(err.GetType().ToString(), err.StackTrace);
			}
		}*/
		#endregion

		#region OrbitWindow
		private void OrbitWindow_Closing(object sender, CancelEventArgs e)
		{
			OrbitTray.Visible=false;

			// unregistering the hook
			if(Global.Configuration.Behavior.PopKey!=Keys.None)
				Orbit.Utilities.MouseHook.StopHook(this.Handle);
			// unregistering the Hotkey
			Win32.User32.User32API.UnregisterHotKey(this.Handle, 100);
			Win32.User32.User32API.UnregisterHotKey(this.Handle, 101);
			// fade out and stop everything if running
			if(RenderEngine.Device!=null)
			{
				GoAway();
				// stop everything
				RenderEngine.State=LoopState.Nothing;
				ProgramDoing=ProgramState.Nothing;
				// free mem
				ItemRegistry.RemoveLine(0);
				DeInitializeD3D();
			}
		}
		private void OrbitWindow_Deactivate(object sender, EventArgs e)
		{
			System.GC.Collect();
		}
		private void OrbitWindow_Paint(object sender, PaintEventArgs e)
		{
			// Draw the picture again
			if(ProgramDoing!=ProgramState.Hidden)
			{
				RenderEngine.State=LoopState.Loop;
			}
		}


		#region Input
		private void OrbitWindow_MouseWheel(object sender, MouseEventArgs e)
		{
			if(RenderEngine.State==LoopState.NotInit)return;
			try
			{
				// consider the last mouse-overed line
				int LineToConsider=this.SelectedLine;
				// if CTRL key is pressed, rotate the last line
				if(this.OpenInStartLoop)
				{
					RotateLoop(ItemRegistry.Lines.Length-1, e.Delta);
					// notice a mousemove
					//MouseEventArgs mea=new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
					//this.OrbitWindow_MouseMove(this, mea);
					return;
				}
				// Get (based on the first radius), this line's quantity of icons allowed
				// to find out if we rotate or we scroll the items
				int ItemsAllowedInLine;
				if(Global.Configuration.Appearance.ItemsShownPerLine==0 || ItemRegistry.Lines.Length==1)
				{
					//ItemsAllowedInLine=Convert.ToInt32(((FirstRadius+ThisItemsRadius)*Math.Sqrt(4*Circle))/Global.Configuration.IconSize);
					/*ItemsAllowedInLine=ItemRegistry.Lines[LineToConsider].MaxItems;
					if(ItemRegistry.Count(LineToConsider)>ItemsAllowedInLine)
						ItemsAllowedInLine--;*/
					ItemsAllowedInLine=ItemRegistry.Lines[LineToConsider].VisibleItems;
				}
				else
					ItemsAllowedInLine=Global.Configuration.Appearance.ItemsShownPerLine;
				// rotate
				if(ItemsAllowedInLine<ItemRegistry.Count(LineToConsider))
				{
					// scroll the items
					if(e.Delta==120 || e.Delta==-120)
					{
						if(e.Delta<0)
						{
							if(!(ItemRegistry.Count(LineToConsider)-ItemRegistry.Lines[LineToConsider].StartIndex<=ItemsAllowedInLine))
							{
								ItemRegistry.Lines[LineToConsider].StartIndex++;
							}
						} 
						else 
						{
							if(ItemRegistry.Lines[LineToConsider].StartIndex>0)
							{
								ItemRegistry.Lines[LineToConsider].StartIndex--;
							}
						}
						//RenderEngine.State=LoopState.Loop;
						//RenderEngine.MainLoop();
						RenderEngine.ForceRenderNextFrameWait();
					}
				} 
				else
				{
					// rotate the loop
					RotateLoop(this.SelectedLine, e.Delta);
				}
				// send mousemove notice
				//MouseEventArgs meaA=new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
				//this.OrbitWindow_MouseMove(this, meaA);
				//RenderEngine.ForceRenderNextFrame();
			}
			catch(Exception){}
		}

		private void OrbitWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			// hide if esc is pressed
			if(e.KeyChar==(char)27)
			{
				this.GoAway();
			}
		}
		private void OrbitWindow_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				// hide and re-prepare the dock if the buttons were the left or right ones
				if(e.Button==MouseButtons.Left || e.Button==MouseButtons.Right)
				{
					bool Hide=true;
					bool HasToHide=false;
					int SI=ItemRegistry.GetSelectedItem();
					if(SI!=-1)
					{
						if(e.Button==MouseButtons.Left)
						{
							HasToHide=GetLeftButtonAction(ItemRegistry[SI]);
							Hide=HasToHide;
						}
						if(e.Button==MouseButtons.Right) 
						{
							TolerateNoHit=true;
							Hide=false;
							RenderEngine.State=LoopState.Configuring;
							GetRightButtonAction(SI);
							RenderEngine.State=LoopState.Nothing;
						}
					}
					else
					{
						if(TolerateNoHit)
						{
							Hide=false;
							TolerateNoHit=false;
						}
					}
					if(Hide)
					{
						// hide if the clicked item requested a hide
						GoAway();
					}
				}
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.GetType().ToString(), err.StackTrace);
			}
		}
		private void OrbitWindow_MouseMove(object sender, MouseEventArgs e)
		{
			if(ItemRegistry==null)
				return;

			try
			{
				// make sure the tansparent window is focused
				// might not be needed, i guess
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				{
					if(!RenderEngine.TransparentResourceManager.DisplayForm.Focused)
					{
						RenderEngine.TransparentResourceManager.DisplayForm.Focus();
						Win32.User32.User32API.SetActiveWindow(RenderEngine.TransparentResourceManager.DisplayForm.Handle);
						Win32.User32.User32API.SetForegroundWindow(RenderEngine.TransparentResourceManager.DisplayForm.Handle);
					}
				}
				int i=0;
				bool redraw=false;
				double closestDistance = 0;
				int closestIndex = -1;
				while (i<ItemRegistry.Length)
				{
					// skip item if not enabled or not shown
					if(ItemRegistry[i]==null || !ItemRegistry[i].Enabled || !ItemRegistry[i].IsShown)
					{
						i++;
						continue;
					}

					// by default no one's hit
					ItemRegistry[i].IsMouseOver=false;

					// find out the closest hit :P
					if(ItemRegistry[i].Enabled
						&& ItemRegistry[i].IsShown
						&& ItemRegistry[i].Rectangle.IntersectsWith(new Rectangle(new Point((Cursor.Position.X-this.Bounds.X-RenderEngine.ScreenToDraw.Bounds.Left), (Cursor.Position.Y-this.Bounds.Y-RenderEngine.ScreenToDraw.Bounds.Top)), new Size(1,1))))
					{
						double DistanceFromCursor;
						if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
							DistanceFromCursor=Math.Pow((Cursor.Position.X-this.RenderEngine.ScreenToDraw.Bounds.X)-(ItemRegistry[i].Rectangle.X+ItemRegistry[i].Rectangle.Width/2), 2)+Math.Pow((Cursor.Position.Y-this.RenderEngine.ScreenToDraw.Bounds.Y)-(ItemRegistry[i].Rectangle.Y+ItemRegistry[i].Rectangle.Height/2), 2);
						else
							DistanceFromCursor=Math.Pow((Cursor.Position.X-this.RenderEngine.ScreenToDraw.Bounds.X-this.RenderEngine.ScreenToDraw.WorkingArea.X)-(ItemRegistry[i].Rectangle.X+ItemRegistry[i].Rectangle.Width/2), 2)+Math.Pow((Cursor.Position.Y-this.RenderEngine.ScreenToDraw.Bounds.Y-this.RenderEngine.ScreenToDraw.WorkingArea.Y)-(ItemRegistry[i].Rectangle.Y+ItemRegistry[i].Rectangle.Height/2), 2);
						if(DistanceFromCursor<closestDistance || closestIndex==-1)
						{
							closestIndex=i;
							closestDistance=DistanceFromCursor;
						}
					}
						/*// this item is hit
						// tell line was hit
						this.SelectedLine=ItemRegistry[i].Line;
						// got hit
						ItemRegistry[i].IsMouseOver=true;
						//pop up if folder timer should be active
						if((ItemRegistry[i].GetType().ToString()=="Orbit.Items.FolderItem"
							|| ItemRegistry[i].GetType().ToString()=="Orbit.Items.FileSystemDirectoryItem"
							|| ItemRegistry[i].GetType().ToString()=="Orbit.Items.FileSystemFolderItem"
							|| ItemRegistry[i].GetType().ToString()=="Orbit.Items.TasksFolderItem")
							//&& redraw
							&& Global.Configuration.Behavior.FolderPopOnHover 
							&& !ItemRegistry[i].IsToggled)
						{
							FolderPopper.Run(ItemRegistry[i]);
						}
					} 
					else 
					{
						// not hit
						ItemRegistry[i].IsMouseOver=false;
					}*/
					i++;
				}
				// if we found a hit
				if(closestIndex!=-1)
				{
					// this item is hit
					// tell line was hit
					this.SelectedLine=ItemRegistry[closestIndex].Line;
					// got hit
					ItemRegistry[closestIndex].IsMouseOver=true;
					//pop up if folder timer should be active
					if((ItemRegistry[closestIndex].GetType()==typeof(Orbit.Items.FolderItem)
						|| ItemRegistry[closestIndex].GetType()==typeof(Orbit.Items.FileSystemDirectoryItem)
						|| ItemRegistry[closestIndex].GetType()==typeof(Orbit.Items.FileSystemFolderItem)
						|| ItemRegistry[closestIndex].GetType()==typeof(Orbit.Items.TasksFolderItem))
						//&& redraw
						&& Global.Configuration.Behavior.FolderPopOnHover 
						&& !ItemRegistry[closestIndex].IsToggled)
					{
						FolderPopper.Run(ItemRegistry[closestIndex]);
					}
				}
				if(redraw || true)
				{
					//RenderEngine.State=LoopState.Loop;
					RenderEngine.ForceRenderNextFrame();
				}
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}
		private void OrbitWindow_KeyDown(object sender, KeyEventArgs e)
		{
			// set the open in start loop status as the CTRL key status
			this.OpenInStartLoop=e.Control;
		}

		private void OrbitWindow_KeyUp(object sender, KeyEventArgs e)
		{
			// set the open in start loop status as the CTRL key status
			this.OpenInStartLoop=e.Control;
		}

		private void OrbitWindow_DragOver(object sender, DragEventArgs e)
		{
			// compute our internal coordinates
			Point point = PointToClient(new Point(e.X, e.Y));
			int offX = RenderEngine.Center.X-point.X;
			int offY = RenderEngine.Center.Y-point.Y;
			// distance from center so we know in which line we are dropping this thing
			double distanceFromCenter = Math.Sqrt(offX*offX + offY*offY);
			int targetLine = -1;
			// find out which line (cheaper than re-calculating that crap)
			for(int i=0; i<ItemRegistry.Lines.Length; i++)
			{
				if(distanceFromCenter < ItemRegistry.Lines[i].AbsoluteRadius+Global.Configuration.Runtime.IconSizeAverage/2
					&& distanceFromCenter > ItemRegistry.Lines[i].AbsoluteRadius-Global.Configuration.Runtime.IconSizeAverage/2)
				{
					targetLine = i;
				}
			}

			// if we have a line to drop, then we have something! otherwise, tough luck
			string dataTypes="";
			foreach(string dataType in e.Data.GetFormats())
			{
				dataTypes+=dataType;
			}
			System.Diagnostics.Debug.WriteLine("Drag Move at " + e.X + "x" + e.Y + "\nTypes:\n" + dataTypes);

			if(targetLine!=-1 && (e.Data.GetDataPresent(DataFormats.FileDrop, true)))
			{
				e.Effect=DragDropEffects.Link;
			}
			else
			{
				e.Effect=DragDropEffects.None;
			}
			// update our screen
			RenderEngine.ForceRenderNextFrame();
		}

		private void OrbitWindow_DragDrop(object sender, DragEventArgs e)
		{
			// compute our internal coordinates
			Point point = PointToClient(new Point(e.X, e.Y));
			int offX = RenderEngine.Center.X-point.X;
			int offY = RenderEngine.Center.Y-point.Y;
			// distance from center so we know in which line we are dropping this thing
			double distanceFromCenter = Math.Sqrt(offX*offX + offY*offY);
			int targetLine = -1;
			// find out which line (cheaper than re-calculating that crap)
			for(int i=0; i<ItemRegistry.Lines.Length; i++)
			{
				if(distanceFromCenter < ItemRegistry.Lines[i].AbsoluteRadius+Global.Configuration.Runtime.IconSizeAverage/2
					&& distanceFromCenter > ItemRegistry.Lines[i].AbsoluteRadius-Global.Configuration.Runtime.IconSizeAverage/2)
				{
					targetLine = i;
				}
			}

			// if we have a line to drop, then we have something! otherwise, tough luck
			if(targetLine!=-1 && (e.Data.GetDataPresent(DataFormats.FileDrop, true)))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				foreach(string file in files)
				{
					// find out where to save the new item in the hard disk
					string folderName = Orbit.Configuration.ItemSetup.GetTentativeFolderName(System.IO.Path.GetFileNameWithoutExtension(file));
					string folderPath = ItemRegistry.Lines[targetLine].LevelPath;
					Orbit.Configuration.Item newItem = new Item();
					newItem.Action=file;
					newItem.Arguments="";
					newItem.Description="";
					newItem.ImagePath="";
					newItem.Name=System.IO.Path.GetFileNameWithoutExtension(file);
					//Orbit.Configuration.ItemSetup.SaveItem(System.IO.Path.Combine(folderPath, folderName), Global.Configuration.Locations.ImagesPath, newItem);
					MessageBox.Show("Target Line: "+targetLine+"\nAt: "+System.IO.Path.Combine(folderPath, folderName));
				}
			}
		}

		/// <summary>
		/// Message processing method
		/// </summary>
		/// <param name="m">Window Message</param>
		protected override void WndProc(ref Message m)
		{	
			// catch our hotkey Win+W
			const int WM_HOTKEY = 0x0312; 
			const int WM_GLOBALMOUSECLICK = 0x0400 + 5000;
	
			bool showTasks = Global.Configuration.Behavior.ShowTasksOnly;
			switch(m.Msg)	
			{	
				case WM_HOTKEY:	
					if((int)m.WParam==100)
						PopKeyPressed(true);
					else if((int)m.WParam==101)
					{
						Global.Configuration.Behavior.ShowTasksOnly=true;
						PopKeyPressed(true);
						Global.Configuration.Behavior.ShowTasksOnly=showTasks;
					}
					break;	
				case WM_GLOBALMOUSECLICK:
					PopKeyPressed(true);
					break;
			} 	
			base.WndProc(ref m );
		}
		#endregion
		#endregion

		#region Menu Items
		private void OpenInStart_Click(object sender, System.EventArgs e)
		{
			// remember important item information since the items WILL BE UNLOADED
			int SI=RightClickMenuOnItem;
			if(SI<0)
				return;

			// pretend the CTRL key is pressed
			this.OpenInStartLoop=true;
			// execute the action
			ExecuteFolderItem(ItemRegistry[SI]);
			// pretend we released the CTRL key
			this.OpenInStartLoop=false;
		}
		private void OpenInExplorerMenu_Click(object sender, System.EventArgs e)
		{
			// find clicked item and bail out if it doesn't exist anymore
			int SI=RightClickMenuOnItem;
			if(SI<0)
				return;

			// get the path to open in explorer
			string Path=Global.Configuration.Locations.ItemsPath;
			// type dependant
			if(ItemRegistry[SI].GetType()==typeof(FileSystemDirectoryItem))
				Path=((FileSystemDirectoryItem)ItemRegistry[SI]).Path;
			if(ItemRegistry[SI].GetType()==typeof(FileSystemFolderItem))
				Path=((FileSystemFolderItem)ItemRegistry[SI]).Path;
			
			// open the path and hide
			System.Diagnostics.Process.Start("explorer.exe", "/e, "+Path);
			GoAway();
		}
		private void AddItemToItemMenu_Click(object sender, System.EventArgs e)
		{
			try
			{
				// get the item and bail out if doens't suit
				int SI=RightClickMenuOnItem;
				if(SI<0)
					return;

				// define paths to create in
				string CreateDirIn=((StoredOrbitItem)ItemRegistry[SI]).ItemPath;
				string NewDir=System.IO.Path.Combine(CreateDirIn,"$TempItem$");

				// create
				AddItemSomewhere(NewDir);
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}

		private void AddItemToLevelMenu_Click(object sender, System.EventArgs e)
		{
			try
			{
				// get right clicked item or bail out
				int SI=RightClickMenuOnItem;
				if(SI<0)
					return;
				// define paths
				try
				{
					string CreateDirIn=System.IO.Path.GetDirectoryName(((StoredOrbitItem)ItemRegistry[SI]).ItemPath);
					string NewDir=System.IO.Path.Combine(CreateDirIn,"$TempItem$");

					// create
					AddItemSomewhere(NewDir);
				}
				catch(Exception){}
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}

		/// <summary>
		/// Helper method for the adding item to "blah" event handlers
		/// </summary>
		/// <param name="path">Path to add item to</param>
		private void AddItemSomewhere(string path)
		{
			try
			{
				// hide
				GoAway();
				// stop render
				RenderEngine.Pause();
				// create the dir
				System.IO.Directory.CreateDirectory(path);
				// create config window
				using(ItemSetup ItemDialog=new ItemSetup(path, Global.Configuration.Locations.ImagesPath))
				{
					// show
					ItemDialog.ShowDialog();
					if(ItemDialog.DialogResult==DialogResult.OK)
					{
						// load root again
						ItemRegistry.RemoveLine(0);
						ItemRegistry.CreateRoot(Global.Configuration.Locations.ItemsPath, RenderEngine.Device);
					} 
					else 
					{
						// delete if bailed out on item creation
						if(System.IO.Directory.Exists(path))
							System.IO.Directory.Delete(path, true);
					}
				}
				// resume
				RenderEngine.Resume();
				RenderEngine.State=LoopState.Loop;
			}
			catch(Exception)
			{
				throw;
			}
		}
		private void RemoveItemMenu_Click(object sender, System.EventArgs e)
		{
			try
			{
				RenderEngine.State=LoopState.Configuring;
				int SI=RightClickMenuOnItem;
				if(SI<0)
					return;
				if(MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.ConfirmRemove.Replace("%ItemName%", ItemRegistry[SI].Name), ItemRegistry[SI].Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
				{
					// remove the item
					System.IO.Directory.Delete(((StoredOrbitItem)ItemRegistry[SI]).ItemPath, true);
				
					// re pop up
					ItemRegistry.RemoveLine(0);
					ItemRegistry.CreateRoot(Global.Configuration.Locations.ItemsPath, RenderEngine.Device);
				}
				RenderEngine.State=LoopState.Loop;
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}
		private void ItemPropertiesMenu_Click(object sender, System.EventArgs e)
		{
			Global.Configuration.Runtime.CanPop=false;
			try
			{
				RenderEngine.State=LoopState.Configuring;
				int SI=RightClickMenuOnItem;
				if(SI<0)
					return;
				// stop rendering
				RenderEngine.State=LoopState.Nothing;
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
					GoAway();
				else
					this.Hide();
				RenderEngine.Pause();
				// show dialog
				// create the dialog
				using(ItemSetup ItemDialog=new ItemSetup(((StoredOrbitItem)ItemRegistry[SI]).ItemPath, Global.Configuration.Locations.ImagesPath))
				{
					RenderEngine.State=LoopState.Configuring;
					ItemDialog.ShowDialog();
					ItemRegistry.RemoveLine(0);
				}
				GC.Collect();
				// resume render
				RenderEngine.State=LoopState.Nothing;
				RenderEngine.Resume();
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
			Global.Configuration.Runtime.CanPop=true;
		}

		private void IgnoreWindowMenu_Click(object sender, System.EventArgs e)
		{
			int SI=RightClickMenuOnItem;
			if(SI<0)
				return;

			// ignore window
			((TaskItem)ItemRegistry[SI]).IgnoreWindow();

			// remove ignored window
			ItemRegistry.Remove(SI);
		}

		private void ConfigurationMenu_Click(object sender, System.EventArgs e)
		{
			if(RenderEngine.State==LoopState.Configuring) return;

			// hide and stop rendering
			RenderEngine.State=LoopState.Nothing;
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				GoAway();
			else
				this.Hide();

			// run setup dll
			ShowConfigurationDialog();
		}
		private void ExitMenu_Click(object sender, System.EventArgs e)
		{
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				RenderEngine.TransparentResourceManager.DisplayForm.Close();
			} 
			else 
			{
				this.Close();
			}
		}
		private void ManualMenu_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.ecocardio.com.br/orbit/manual/");
			GoAway();
		}
		private void CheckForUpdateMenu_Click(object sender, System.EventArgs e)
		{
			this.GoAway();
			this.CheckForOrbitUpdate(false);
		}
		#endregion

		#region Other Items
		private void Disp_Closed(object sender, EventArgs e)
		{
			this.Close();
		}
		
		private void OrbitTray_MouseUp(object sender, MouseEventArgs e)
		{
			if(e.Button!=MouseButtons.Left || RenderEngine.State==LoopState.Loading || RenderEngine.State==LoopState.Checking)
				return;

			try
			{
				if(RenderEngine.IsDeviceLost
					//|| RenderEngine.IsPaused
					|| !Global.Configuration.Runtime.CanPop
					)
				{
					return;
				}
				/*if(RenderEngine.IsDeviceLost)
				{
					return;
				}*/
				if(this.Visible || (RenderEngine.TransparentResourceManager!=null && RenderEngine.TransparentResourceManager.DisplayForm.Visible))
				{
					return;
				}
				Global.Configuration.Runtime.CurrentLevel=Global.Configuration.Locations.ItemsPath;
				Global.Scale=1f;

				// position on the center of the screen
				RenderEngine.Center=new Point(Screen.PrimaryScreen.Bounds.Width/2, Screen.PrimaryScreen.Bounds.Height/2);
				RenderEngine.MultiMonitorCenter=RenderEngine.Center;
				RenderEngine.ScreenToDraw=Screen.PrimaryScreen;

				ItemRegistry.CreateRoot(Global.Configuration.Locations.ItemsPath, RenderEngine.Device);
				// capture background
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake && this.Visible==false)
				{
					RenderEngine.ScreenGrabber.Capture(CaptureMode.Clipboard);
				}
				// update transparent window stuff
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				{
					Global.Configuration.Runtime.CurrentIconBgAlpha=Global.Configuration.Appearance.IconBackgroundAlpha;
					RenderEngine.UpdateTransparentBackground();
					RenderEngine.TransparentResourceManager.DisplayForm.Clear();
				}
				// draw a frame (to update the layered window if needed)
				RenderEngine.State=LoopState.Loop;
				ProgramDoing=ProgramState.Nothing;
				PopUp(false);
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}
		
		#endregion
		#endregion

		#region Item Event Handling
		private bool GetLeftButtonAction(OrbitItem Item)
		{
			if(Item==null)
				return false;

			bool Hide=true;
			bool IsShortcut=true;
			/*switch(Item.GetType().ToString())
			{
				case "Orbit.Items.EmptyItem":
					// [empty]
					IsShortcut=false;
					Hide=false;
					break;
				case "Orbit.Items.FileSystemDirectoryItem":
					// [physicaldir]
					IsShortcut=false;
					Hide=false;
					ExecuteFolderItem(Item);
					break;
				case "Orbit.Items.FileSystemFolderItem":
					// [physicaldir]
					IsShortcut=false;
					Hide=false;
					ExecuteFolderItem(Item);
					break;
				case "Orbit.Items.FolderItem":
					// [dir]
					IsShortcut=false;
					Hide=false;
					ExecuteFolderItem(Item);
					break;
				case "Orbit.Items.TasksFolderItem":
					// [tasks]
					IsShortcut=false;
					Hide=false;
					ExecuteFolderItem(Item);
					break;
				case "Orbit.Items.ConfigurationItem":
					// [configuration]
					IsShortcut=false;
					Hide=false;
					// run setup dll
					if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
					{
						RenderEngine.TransparentResourceManager.DisplayForm.Hide();
						RenderEngine.TransparentResourceManager.BackForm.Hide();
						RenderEngine.TransparentResourceManager.LabelForm.Hide();
						RenderEngine.TransparentResourceManager.OverlayForm.Hide();
					} 
					else
					{
						this.Hide();
					}

					ShowConfigurationDialog();
					break;
				case "Orbit.Items.TaskItem":
					// [task]
					IsShortcut=false;
					Hide=true;
					((TaskItem)Item).SwitchTo();
					break;
			}*/
			Type type = Item.GetType();
			if(type==typeof(Orbit.Items.EmptyItem))
			{
				// [empty]
				IsShortcut=false;
				Hide=false;
			}
			else if(type==typeof(Orbit.Items.FileSystemDirectoryItem))
			{
				// [physicaldir]
				IsShortcut=false;
				Hide=false;
				ExecuteFolderItem(Item);
			}
			else if(type==typeof(Orbit.Items.FileSystemFolderItem))
			{
				// [physicaldir]
				IsShortcut=false;
				Hide=false;
				ExecuteFolderItem(Item);
			}
			else if(type==typeof(Orbit.Items.FolderItem))
			{
				// [dir]
				IsShortcut=false;
				Hide=false;
				ExecuteFolderItem(Item);
			}
			else if(type==typeof(Orbit.Items.TasksFolderItem))
			{
				// [tasks]
				IsShortcut=false;
				Hide=false;
				ExecuteFolderItem(Item);
			}
			else if(type==typeof(Orbit.Items.ConfigurationItem))
			{
				// [configuration]
				IsShortcut=false;
				Hide=false;
				// run setup dll
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				{
					RenderEngine.TransparentResourceManager.DisplayForm.Hide();
					RenderEngine.TransparentResourceManager.BackForm.Hide();
					RenderEngine.TransparentResourceManager.LabelForm.Hide();
					RenderEngine.TransparentResourceManager.OverlayForm.Hide();
				} 
				else
				{
					this.Hide();
				}

				ShowConfigurationDialog();
			}
			else if(type==typeof(Orbit.Items.TaskItem))
			{
				// [task]
				IsShortcut=false;
				Hide=true;
				((TaskItem)Item).SwitchTo();
			}

			if(IsShortcut)
			{
				// TODO: automate this check (IOpenItem)
				((IOpenItem)Item).Open();
				if(Item.RunAndLeave)this.Close();
				Hide=true;
			}
			return Hide;
		}

		private void GetRightButtonAction(int i)
		{
			// bail out if out of reach
			if(i<0 || i>=this.ItemRegistry.Length || ItemRegistry[i]==null)
				return;

			// setting menu text
			this.AddItemToItemMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.AddItemTo.Replace("%ItemName%", ItemRegistry[i].Name);
			this.AddItemToLevelMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.AddItemTo.Replace("%ItemName%", ItemRegistry[i].Parent);
			this.RemoveItemMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.RemoveItem.Replace("%ItemName%", ItemRegistry[i].Name);
			this.ItemPropertiesMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.ItemProperties.Replace("%ItemName%", ItemRegistry[i].Name);

			// getting the menu flags
			ItemMenuFlags MenuFlags=ItemRegistry[i].MenuFlags;

			// checking individual flags
			this.AddItemToItemMenu.Visible=(MenuFlags & ItemMenuFlags.AddItemToItem) == ItemMenuFlags.AddItemToItem;
			this.AddItemToLevelMenu.Visible=(MenuFlags & ItemMenuFlags.AddItemToLevel) == ItemMenuFlags.AddItemToLevel;
			this.RemoveItemMenu.Visible=(MenuFlags & ItemMenuFlags.RemoveItem) == ItemMenuFlags.RemoveItem;
			this.ItemPropertiesMenu.Visible=(MenuFlags & ItemMenuFlags.ItemProperties) == ItemMenuFlags.ItemProperties;
			this.OpenInStart.Visible=(MenuFlags & ItemMenuFlags.OpenInStart) == ItemMenuFlags.OpenInStart;
			this.OpenInExplorerMenu.Visible=(MenuFlags & ItemMenuFlags.OpenInExplorer) == ItemMenuFlags.OpenInExplorer;
			this.IgnoreWindowMenu.Visible=(MenuFlags & ItemMenuFlags.IgnoreWindow) == ItemMenuFlags.IgnoreWindow;
			this.MenuSeparator1.Visible=(MenuFlags & ItemMenuFlags.MenuSeparator) == ItemMenuFlags.MenuSeparator;

			// notify clicked item
			this.RightClickMenuOnItem=i;

			// pop up the menu
			RenderEngine.State=LoopState.Configuring;
			Point Pos;
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				int off=(Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Runtime.IconSizeAverage)/2;
				Pos=new Point(Cursor.Position.X-RenderEngine.MultiMonitorCenter.X+RenderEngine.AverageDockBounds.Width/2+off, Cursor.Position.Y-RenderEngine.MultiMonitorCenter.Y+RenderEngine.AverageDockBounds.Height/2+off);
			}
			else
			{
				Pos=new Point(Cursor.Position.X-this.Location.X, Cursor.Position.Y-this.Location.Y);
			}
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				this.ItemContextMenu.Show(this.RenderEngine.TransparentResourceManager.DisplayForm, Pos);
			}
			else
			{
				this.ItemContextMenu.Show(this, Pos);
			}
		}

		private void ExecuteFolderItem(OrbitItem Item)
		{
			try
			{
				if((this.OpenInStartLoop || Global.Configuration.Behavior.AlwaysOpenInStart)
					&& !Global.Configuration.Behavior.FolderPopOnHover)
				{
					// get the new items
					OrbitItem[] NewRoot=((IEnumeratorItem)Item).GetItems();

					// remember the path
					string NewRootPath=Global.Configuration.Locations.ItemsPath;
					if(Item.GetType()==typeof(FolderItem))
						NewRootPath=((FolderItem)Item).ItemPath;
					/*if(Item.GetType()==typeof(TasksFolderItem))
						NewRootPath=((TasksFolderItem)Item).ItemPath;*/

					ItemRegistry.CreateRoot(NewRoot, NewRootPath);

					// take screenshots if available and selected
					if(Global.Configuration.Appearance.ShowImageThumbnails)
					{
						//ThumbnailSync TS=new ThumbnailSync(NewRoot, 0);
						ItemRegistry.Lines[0].TS=new ThumbnailSync(NewRoot, 0);
					}
				} 
				else 
				{
					// remember clicked item name
					OrbitItem ClickedItem=Item;
					// set this item to toggled
					ItemRegistry.SetToggledItem(ItemRegistry.IndexOf(Item));
					// unload directories which are the same level or an above level
					if(Item.Line<ItemRegistry.Lines.Length-1)
					{
						ItemRegistry.RemoveLine(Item.Line+1);
					}
					// load this directory
					LoadDir(ItemRegistry[ItemRegistry.IndexOf(ClickedItem)]);
				}
				// draw
				RenderEngine.State=LoopState.Loop;
				this.PopUp(false);
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.GetType().ToString(), err.StackTrace);
			}
		}
		#endregion

		#region ThumbnailSync Event Handlers
		private void TS_Paint(object sender, EventArgs e)
		{
			if(RenderEngine.State==LoopState.Nothing)
				//RenderEngine.State=LoopState.Loop;
				RenderEngine.ForceRenderNextFrame();
		}
		#endregion

		#region ItemRegistry Event Handlers
		private void ItemRegistry_LineQuantityChanged(object sender, EventArgs e)
		{
			// new loop always requires a repainting of the background if in transparent mode
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real && ItemRegistry.Lines.Length!=1)
			{
				RenderEngine.UpdateTransparentBackground();
			}
		}
		#endregion

		#region RenderEngine Event Handlers
		private void RenderEngine_DeviceLost(object sender, EventArgs e)
		{
			try
			{
				// just unload all items
				ItemRegistry.RemoveLine(0);
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.GetType().ToString(), err.StackTrace);
			}
		}

		private void RenderEngine_ForcedModeSwitch(object sender, ForcedModeSwitchEventArgs e)
		{
			// notify the user when and why we switched mode.
			switch(e.ModeSwitchReason)
			{
				case ModeSwitchReason.TransparentModeNotSupported:
					MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.TransparentModeNotSupported, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					break;
				case ModeSwitchReason.UnexpectedError:
					MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.UnexpectedErrorOccurred, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					break;
			}
		}

		private void RenderEngine_FrameTick(object sender, EventArgs e)
		{
			try
			{
				if(RenderEngine.State==LoopState.NotInit)
					return;

				// Dissappear if mouse is clicked outside (and the form is not the active)
				// also de-select items if mouse is outside the dock area
				/*if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real && RenderEngine.TransparentResourceManager!=null)
				{
					if(RenderEngine.TransparentResourceManager.DisplayForm.Visible && RenderEngine.State!=LoopState.Configuring)
					{
						int i=0;
						while(i<ItemRegistry.Length)
						{
							if(!this.ItemRegistry[i].Rectangle.IntersectsWith(new Rectangle(new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top), new Size(1,1))))
							{
								if(this.ItemRegistry[i].IsMouseOver)
								{
									this.ItemRegistry[i].IsMouseOver=false;
									RenderEngine.State=LoopState.Loop;
									break;
								}
							}
							i++;
						}
						if(!RenderEngine.AverageDockBounds.IntersectsWith(new Rectangle(new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top), new Size(1,1))))
						{
							if(tOutTimer==null)
								tOutTimer=new System.Threading.Timer(new System.Threading.TimerCallback(OutTimer_Callback), null, 1000, System.Threading.Timeout.Infinite);

							//if(!this.OutTimer.Enabled)
							//{
							//	this.OutTimer.Interval=1000;
							//	this.OutTimer.Enabled=true;
							//	tOutTimer=new System.Threading.Timer(new System.Threading.TimerCallback(OutTimer_Callback), null, 1000, System.Threading.Timeout.Infinite);
							//}
						} 
						else 
						{
							if(tOutTimer!=null)
							{
								tOutTimer.Dispose();
								tOutTimer=null;
							}
							//if(this.OutTimer.Enabled)
							//{
							//	this.OutTimer.Enabled=false;
							//	this.OutTimer.Interval=1000;
							//}
						}
					}
				}*/
				// hide if there's nothing to do or is loading or configuring
				if(ItemRegistry.Lines.Length==0 && (RenderEngine.State!=LoopState.Loading && RenderEngine.State!=LoopState.Configuring))
				{
					// but only hide for the first time that this form is created :)
					if(HideOnEmpty)
					{
						HideOnEmpty=false;
						GoAway();
						this.Hide();
						if(RenderEngine.TransparentResourceManager!=null)
							RenderEngine.TransparentResourceManager.DisplayForm.Hide();
					}
				}
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}

		private void OutTimer_Callback(object state)
		{
			if(RenderEngine.AverageDockBounds.IntersectsWith(new Rectangle(new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top), new Size(1,1))))
				return;

			GoAway();
			// hid already... disable this
			if(tOutTimer!=null)
			{
				tOutTimer.Dispose();
				tOutTimer=null;
			}
			/*this.OutTimer.Enabled=false;
			this.OutTimer.Interval=1000;*/
		}

		private void RenderEngine_RendererReset(object sender, EventArgs e)
		{
			try
			{
				// load everything again
				ItemRegistry.CreateRoot(Global.Configuration.Locations.ItemsPath, RenderEngine.Device);
				// show again
				PopUp(false);
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.GetType().ToString(), err.StackTrace);
			}
		}

		private void RenderEngine_WindowResize(object sender, EventArgs e)
		{
			SizeWindow();
		}
		#endregion
		#endregion

		#region Pop Up Shortcut Methods
		// Pop key has been pressed
		private void PopKeyPressed(bool HotKey)
		{
			// if device is STILL lost, then don't do anything
			if(
				//this==null || 
				RenderEngine==null
				|| RenderEngine.IsDeviceLost
				//|| RenderEngine.IsPaused
				|| !Global.Configuration.Runtime.CanPop
				//|| RenderEngine.ScreenToDraw==null
				)
			{
				return;
			}
			// normal loop
			RenderEngine.State=LoopState.Checking;
			Rectangle OldRect=RenderEngine.AverageDockBounds;
			Global.Scale=1f;
			// show the layered window or the dx render window (depends on the "notransparency" command line)
			if(this.ItemRegistry.Length>0)
				ItemRegistry.RemoveLine(0);

			if(((RenderEngine.TransparentResourceManager!=null && RenderEngine.TransparentResourceManager.DisplayForm.Visible) || this.Visible) && OldRect.IntersectsWith(new Rectangle(new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top), new Size(1,1))) && Global.Configuration.Behavior.AlwaysOpenInStart)
			{
				string LevelUp;
				if(Global.Configuration.Runtime.CurrentLevel+"\\"==Global.Configuration.Locations.ItemsPath)
				{
					Global.Configuration.Runtime.CurrentLevel=Global.Configuration.Locations.ItemsPath;
					LevelUp=Global.Configuration.Locations.ItemsPath;
				} 
				else 
				{
					LevelUp=Global.Configuration.Runtime.CurrentLevel.Substring(0,Global.Configuration.Runtime.CurrentLevel.LastIndexOf("\\"));
				}

				ItemRegistry.CreateRoot(LevelUp, RenderEngine.Device);

				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				{
					Global.Configuration.Runtime.CurrentIconBgAlpha=Global.Configuration.Appearance.IconBackgroundAlpha;
					RenderEngine.UpdateTransparentBackground();
				}
				RenderEngine.State=LoopState.Loop;
				ProgramDoing=ProgramState.Nothing;
				PopUp(false);
			} 
			else 
			{
				Global.Configuration.Runtime.CurrentLevel=Global.Configuration.Locations.ItemsPath;
				
				ItemRegistry.CreateRoot(Global.Configuration.Locations.ItemsPath, RenderEngine.Device);
				// draw a frame (to update the layered window if needed)
				RenderEngine.State=LoopState.Loop;
				ProgramDoing=ProgramState.Nothing;
				PopUp(true);
			}
		}
		#endregion

		#region Orbit Web Services Integration Methods
		private void HandleErrorReport(string ErrorMessage, string StackTrace)
		{
			// hide all traces of Orbit's existence
			try
			{
				Win32.User32.User32API.UnregisterHotKey(this.Handle, 100);
				Win32.User32.User32API.UnregisterHotKey(this.Handle, 101);
				this.OrbitTray.Visible=false;
				this.Hide();
				if(RenderEngine.TransparentResourceManager!=null && RenderEngine.TransparentResourceManager.DisplayForm.Visible)
					this.RenderEngine.TransparentResourceManager.DisplayForm.Hide();

				// stop
				this.RenderEngine.Stop();
			}
			catch(Exception){}

			// error report
			string ErrorReportData=ErrorMessage+"\r\n"+StackTrace;
			if(!ErrorReport.WasReported(ErrorReportData))
			{
				ErrorReport ErrorForm=new ErrorReport(ErrorReportData);
				using(ErrorForm)
				{
					ErrorForm.ShowDialog();
				}
			}
			else
			{
				MessageBox.Show("Orbit has encountered an unhandled exception.\nThis error has already been reported by another user.\nOrbit will now exit.", "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}
		private void CheckForOrbitUpdate(bool RunSilent)
		{
			UpdateChecker UpdateClient=new UpdateChecker(Application.ProductVersion);
			using(UpdateClient)
			{
				UpdateClient.RunUpdateCheck(RunSilent);
			}
		}
		#endregion

		#region Window Methods
		// pops the dock
		private void PopUp(bool NewPosition)
		{
			if(Global.Configuration.Appearance.Transparency!=Orbit.Configuration.TransparencyMode.Real)
			{
				RenderEngine.ResourceManager.BackgroundProvider.Prepare();
				this.BackColor=RenderEngine.ResourceManager.BackgroundProvider.BackgroundColor;
			}
			if(NewPosition)
			{
				// the non transparent mode has trouble with sidebars and stuff
				if(Global.Configuration.Behavior.AlwaysPopInCenter)
				{
					RenderEngine.Center=new Point(Screen.PrimaryScreen.Bounds.Width/2, Screen.PrimaryScreen.Bounds.Height/2);
					RenderEngine.MultiMonitorCenter=RenderEngine.Center;
					RenderEngine.ScreenToDraw=Screen.PrimaryScreen;
				}
				else
				{
					RenderEngine.ScreenToDraw=Screen.FromPoint(Cursor.Position);
					if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
						RenderEngine.Center=new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top);
					else
						RenderEngine.Center=new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left-RenderEngine.ScreenToDraw.WorkingArea.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top-RenderEngine.ScreenToDraw.WorkingArea.Top);
					RenderEngine.MultiMonitorCenter=Cursor.Position;
				}
				
				ProgramDoing=ProgramState.Nothing;

				Global.Configuration.Runtime.CurrentIconBgAlpha=Global.Configuration.Appearance.IconBackgroundAlpha;
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				{
					RenderEngine.UpdateTransparentBackground();
					RenderEngine.TransparentResourceManager.DisplayForm.Clear();
				}
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake && !this.Visible && Global.Configuration.Appearance.Transparency!=Orbit.Configuration.TransparencyMode.Real)
				{
					RenderEngine.ScreenGrabber.Capture(CaptureMode.Clipboard);
				}

				/*RenderEngine.State=LoopState.Loop;
				RenderEngine.MainLoop();*/
				RenderEngine.ForceRenderNextFrame();
			}
			// start any loop
			if(ProgramDoing!=ProgramState.ToHide && ProgramDoing!=ProgramState.FadeOut)ProgramDoing=ProgramState.Nothing;
			// show
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				RenderEngine.UpdateTransparentBackground();
				RenderEngine.TransparentResourceManager.BackForm.Show();
				RenderEngine.TransparentResourceManager.DisplayForm.Show();
				RenderEngine.TransparentResourceManager.OverlayForm.Show();
				if(NewPosition)
				{
					Win32.User32.User32API.SetForegroundWindow(RenderEngine.TransparentResourceManager.DisplayForm.Handle);
					Win32.User32.User32API.SetActiveWindow(RenderEngine.TransparentResourceManager.DisplayForm.Handle);
					RenderEngine.TransparentResourceManager.DisplayForm.Activate();
				}
			}
			else 
			{
				this.Show();
				this.Activate();
			}

			//this.OutTimer.Enabled=false;
			//this.OutTimer.Interval=1000;

			TolerateNoHit=false;
			
			//if(!(Global.Configuration.Behavior.PopKey!=Keys.None))
				RenderEngine.Resume();
		}
		// fades out and hides the dock
		private void GoAway()
		{
			//return; // never hide
			if(RenderEngine!=null && RenderEngine.State!=LoopState.Configuring)
			{
				// if not in a fade out already
				if(ProgramDoing!=ProgramState.FadeOut && Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				{
					// set fade out
					ProgramDoing=ProgramState.FadeOut;
					// start drawing
					RenderEngine.State=LoopState.Loop;
					//Application.DoEvents();
				}
				// set me to hidden
				ProgramDoing=ProgramState.Hidden;
				// hide windows
				this.Hide();
				// hiding background, label and overlay windows
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real
					&& RenderEngine.TransparentResourceManager.DisplayForm!=null
					&& RenderEngine.TransparentResourceManager.BackForm!=null
					&& RenderEngine.TransparentResourceManager.LabelForm!=null
					&& RenderEngine.TransparentResourceManager.OverlayForm!=null)
				{
					RenderEngine.TransparentResourceManager.DisplayForm.Hide();
					RenderEngine.TransparentResourceManager.BackForm.Hide();
					RenderEngine.TransparentResourceManager.LabelForm.Hide();
					RenderEngine.TransparentResourceManager.OverlayForm.Hide();
				}
				// re-setting global states
				this.OpenInStartLoop=false;
				if(ItemRegistry!=null)
					ItemRegistry.RemoveLine(0);

				//if(!(Global.Configuration.Behavior.PopKey!=Keys.None))
					//this.FrameTimer.Enabled=false;
					RenderEngine.Pause();

				//this.FpsTimer.Enabled=false;
				if(tOutTimer!=null)
				{
					tOutTimer.Dispose();
					tOutTimer=null;
				}
				/*this.OutTimer.Enabled=false;*/
				//this.FolderPopTimer.Enabled=false;
				FolderPopper.Enabled=false;
				//this.WindowState=FormWindowState.Minimized;

				RenderEngine.State=LoopState.Nothing;
			}
			GC.Collect();
		}
		// sizes the window according to the mode being used
		private void SizeWindow()
		{
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				try
				{
					this.Bounds=new Rectangle(new Point(0,0), new Size(1024,768));
				}
				catch(Exception){}
				try
				{
					this.Location=new Point(0,0);
				}
				catch(Exception){}
			} 
			else 
			{
				try
				{
					this.Bounds=Screen.PrimaryScreen.WorkingArea;
				}
				catch(Exception){}
			}
		}
		
		#endregion

		#region My Registry Methods
		// Loads a dir (a line) resizing the registry only once and filling the items
		private void LoadDir(OrbitItem ParentItem)
		{
			// bail out if not the right type of item
			// TODO: block non IEnumeratorItem objects
			//if(!ParentItem.GetType().i)
				//throw new InvalidCastException("Not IEnumeratorItem");

			// states and cursors
			//RenderEngine.Pause();
			try
			{
				//RenderEngine.State=LoopState.Loading;
				Cursor=Cursors.AppStarting;

				// load
				ItemRegistry.ExpandFrom(ParentItem);

				// states and cursors
				Cursor=Cursors.Default;
				//RenderEngine.State=LoopState.Checking;
			}
			catch(Exception)
			{
				throw;
			}
			//RenderEngine.Resume();
		}
		#endregion

		#region Configuration Loading/Verifying/Creating Methods
		private void ShowConfigurationDialog()
		{
			Global.Configuration.Runtime.CanPop=false;
			RenderEngine.Pause();
			ItemRegistry.RemoveLine(0);

			RenderEngine.State=LoopState.Configuring;
			using(DockSetup DSetup=new DockSetup())
			{
				DSetup.ShowDialog();
				if(DSetup.DialogResult==DialogResult.OK)
				{
					// deinit d3d
					DeInitializeD3D();
					// update hotkey
					MouseHook.StopHook(this.Handle);
					if(Global.Configuration.Behavior.PopKey!=Keys.None)
					{
						MouseHook.StartHook(this.Handle);
						MouseHook.SetShortcutKey(Global.Configuration.Behavior.PopKey);
					}
					SizeWindow();
					// re init directx
					InitializeD3D();
				}
			}
			GC.Collect();

			RenderEngine.State=LoopState.Nothing;
			RenderEngine.Resume();
			Global.Configuration.Runtime.CanPop=true;
		}
		// TODO: Think what methods can be moved to other classes
		// Load the configuration file
		private void LoadConfig()
		{
			// load from INI
			// i'm not too confident about using the XML settings because
			// of localization shit (bad commas instead of periods in decimal numbers caused problems already)
			Global.Configuration=Orbit.Configuration.ConfigurationInfo.FromINI();
			
			// if profiles path doesn't exist, create it
			if(!System.IO.Directory.Exists(Global.Configuration.Locations.ItemsPath))
				CreateUserConfig();

			// load
			Global.Configuration=Orbit.Configuration.ConfigurationInfo.FromINI();
			Global.Configuration.UpdateRuntimeValues();
			Global.Configuration.StripInvalidValues();
		}
		// Check the configuration paths on startup
		private bool CheckConfig()
		{
			bool isOk=true;
			// we need an images path
			if(System.IO.Directory.Exists(Global.Configuration.Locations.ImagesPath)==false)
			{
				MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.ImageDirectoryNotFound, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
				isOk=false;
			}
			// we need an items path
			if(System.IO.Directory.Exists(Global.Configuration.Locations.ItemsPath)==false)
			{
				MessageBox.Show(Global.LanguageLoader.Language.Orbit.Messages.ItemsDirectoryNotFound, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
				isOk=false;
			}
			/*// re-config if config is broken
			if(isOk==false)
			{
				
			}*/

			return isOk;
		}

		// verification and creation of directories that the app needs
		private void VerifyDirectories()
		{
			// defining paths
			string ExePath=System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			string BackgroundPath=System.IO.Path.Combine(ExePath, "background");
			string ImagesPath=System.IO.Path.Combine(ExePath, "images");
			string LanguagePath=System.IO.Path.Combine(ExePath, "language");
			string ItemsPath=System.IO.Path.Combine(ExePath, "myitems");
			string ExtensionsPath=System.IO.Path.Combine(ExePath, "extensions");
			string ProfilesPath=System.IO.Path.Combine(ExePath, "profiles");
			string ThisProfilePath=System.IO.Path.Combine(ProfilesPath, System.Environment.UserName);

			// creating if theydon't exist
			if(!System.IO.Directory.Exists(BackgroundPath))
				System.IO.Directory.CreateDirectory(BackgroundPath);
			if(!System.IO.Directory.Exists(ImagesPath))
				System.IO.Directory.CreateDirectory(ImagesPath);
			if(!System.IO.Directory.Exists(LanguagePath))
				System.IO.Directory.CreateDirectory(LanguagePath);
			if(!System.IO.Directory.Exists(ItemsPath))
				System.IO.Directory.CreateDirectory(ItemsPath);
			if(!System.IO.Directory.Exists(ExtensionsPath))
				System.IO.Directory.CreateDirectory(ExtensionsPath);
			if(!System.IO.Directory.Exists(ProfilesPath))
				System.IO.Directory.CreateDirectory(ProfilesPath);
			if(!System.IO.Directory.Exists(ThisProfilePath))
				System.IO.Directory.CreateDirectory(ThisProfilePath);
		}
		// create a configuration for this user
		private void CreateUserConfig()
		{
			// create config directory
			if(!System.IO.Directory.Exists(Global.Configuration.Locations.OrbitDataPath))
				System.IO.Directory.CreateDirectory(Global.Configuration.Locations.OrbitDataPath);
			if(!System.IO.Directory.Exists(Global.Configuration.Locations.ItemsPath))
				System.IO.Directory.CreateDirectory(Global.Configuration.Locations.ItemsPath);

			string ApplicationPath=ConfigurationInfo.LocationsConfig.GetApplicationPath();

			// copy the default items to it
			CopyDirectory(ApplicationPath + "myitems\\", Global.Configuration.Locations.ItemsPath);

			// if a config.ini exists, copy it too
			if(System.IO.File.Exists(ApplicationPath + "config.ini"))
				System.IO.File.Copy(ApplicationPath + "config.ini", System.IO.Path.Combine(Global.Configuration.Locations.OrbitDataPath, "config.ini"));
		}

		// copy a directory (used to copy the default items to the user folder)
		private void CopyDirectory(string Src, string Dst)
		{
			String[] Files;

			if(Dst[Dst.Length-1]!=System.IO.Path.DirectorySeparatorChar) 
				Dst+=System.IO.Path.DirectorySeparatorChar;
			if(!System.IO.Directory.Exists(Dst)) System.IO.Directory.CreateDirectory(Dst);
			Files=System.IO.Directory.GetFileSystemEntries(Src);
			foreach(string Element in Files)
			{
				// Sub directories
				if(System.IO.Directory.Exists(Element)) 
					CopyDirectory(Element,Dst+System.IO.Path.GetFileName(Element));
					// Files in directory
				else 
					System.IO.File.Copy(Element,Dst+System.IO.Path.GetFileName(Element),true);
			}
		}
		#endregion

		#region Language Methods
		private void RefreshLanguageList()
		{
			string ApplicationPath=ConfigurationInfo.LocationsConfig.GetApplicationPath();

			if(!System.IO.Directory.Exists(System.IO.Path.Combine(ApplicationPath, "language")))
				System.IO.Directory.CreateDirectory(System.IO.Path.Combine(ApplicationPath, "language"));

			string[] l=System.IO.Directory.GetFiles(System.IO.Path.Combine(ApplicationPath, "language"), "*.xml");
			int i=0;
			while(i<l.Length)
			{
				// skip if it's this one or the english one
				if(l[i].ToLower()==System.IO.Path.Combine(ApplicationPath, @"language\default.xml").ToLower()
					|| l[i].ToLower()==System.IO.Path.Combine(ApplicationPath, @"language\english.xml").ToLower())
				{
					i++;
					continue;
				}

				// or add entry to the menu
				System.Windows.Forms.MenuItem LangItem=new MenuItem(System.IO.Path.GetFileNameWithoutExtension(l[i]), new EventHandler(SwitchLanguage));
				this.LanguageMenu.MenuItems.Add(LangItem);

				i++;
			}
		}

		private void SwitchLanguage(object sender, EventArgs e)
		{
			Global.LanguageLoader.LoadLanguage(System.IO.Path.Combine(ConfigurationInfo.LocationsConfig.GetApplicationPath(), @"language\"+((MenuItem)sender).Text+".xml"));
		}

		private void Language_LanguageLoaded(object sender, EventArgs e)
		{
			this.OpenInStart.Text=Global.LanguageLoader.Language.Orbit.Menu.StartLoopsHere;
			this.OpenInExplorerMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.OpenInExplorer;
			this.IgnoreWindowMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.IgnoreThisWindow;

			this.ConfigurationMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.Configuration;
			this.LanguageMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.Language;
			this.ManualMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.OnlineManual;
			this.CheckForUpdateMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.CheckForUpdate;
			this.ExitMenu.Text=Global.LanguageLoader.Language.Orbit.Menu.Exit;

			this.OrbitTray.Text=Global.LanguageLoader.Language.Orbit.Menu.ClickHereToPop;

			string CurrentLanguage=Global.LanguageLoader.GetDefaultLanguageName();
			int i=0;
			while(i<this.LanguageMenu.MenuItems.Count)
			{
				if(this.LanguageMenu.MenuItems[i].Text==CurrentLanguage)
					this.LanguageMenu.MenuItems[i].Checked=true;
				else
					this.LanguageMenu.MenuItems[i].Checked=false;
				i++;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets whether the renderer should show the FPS meter onscreen
		/// </summary>
		public bool ShowFPS
		{
			get
			{
				return RenderEngine.ShowFPS;
			}
			set
			{
				RenderEngine.ShowFPS=value;
			}
		}
		/// <summary>
		/// Gets/Sets whether the tray icon is visible or not
		/// </summary>
		public bool ShowTrayIcon
		{
			get
			{
				return OrbitTray.Visible;
			}
			set
			{
				OrbitTray.Visible=value;
			}
		}
		public Direct3DManager Renderer
		{
			get
			{
				return RenderEngine;
			}
		}
		#endregion

		private void RenderEngine_Tick(object sender, EventArgs e)
		{
			try
			{
				if(RenderEngine.State==LoopState.NotInit)
					return;

				// Dissappear if mouse is clicked outside (and the form is not the active)
				// also de-select items if mouse is outside the dock area
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real && RenderEngine.TransparentResourceManager!=null)
				{
					if(RenderEngine.TransparentResourceManager.DisplayForm.Visible && RenderEngine.State!=LoopState.Configuring)
					{
						int i=0;
						while(i<ItemRegistry.Length)
						{
							if(!this.ItemRegistry[i].Rectangle.IntersectsWith(new Rectangle(new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top), new Size(1,1))))
							{
								if(this.ItemRegistry[i].IsMouseOver)
								{
									this.ItemRegistry[i].IsMouseOver=false;
									RenderEngine.State=LoopState.Loop;
									break;
								}
							}
							i++;
						}
						if(!RenderEngine.AverageDockBounds.IntersectsWith(new Rectangle(new Point(Cursor.Position.X-RenderEngine.ScreenToDraw.Bounds.Left, Cursor.Position.Y-RenderEngine.ScreenToDraw.Bounds.Top), new Size(1,1))))
						{
							if(tOutTimer==null)
								tOutTimer=new System.Threading.Timer(new System.Threading.TimerCallback(OutTimer_Callback), null, 1000, System.Threading.Timeout.Infinite);
						} 
						else 
						{
							if(tOutTimer!=null)
							{
								tOutTimer.Dispose();
								tOutTimer=null;
							}
						}
						RenderEngine.ForceRenderNextFrame();
					}
				}
			}
			catch(Exception err)
			{
				this.HandleErrorReport(err.Message, err.StackTrace);
			}
		}
	}
}