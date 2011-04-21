using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Orbit.Configuration;
using Orbit.Items;
using Orbit.Reference;
using Orbit.Utilities;

namespace Orbit.Core
{
	/// <summary>
	/// This class encapsulates the Direct3D Device and its resources
	/// </summary>
	public class Direct3DManager:IDisposable
	{
		#region Variables
		#region Managed Resources
		private Device _Device;
		private OrbitResourceManager _ResourceManager;
		private ScreenGrabber _ScreenGrabber;
		//private Timer FrameTimer;
		private System.Threading.Thread FrameThread;
		private Timer FpsTimer;
		#endregion

		#region Flags
		private bool _IsDeviceLost;
		private bool _ShowFPS;
		private bool _ForceRenderNextFrame;
		private LoopState _State;
		private bool FrameThreadEnabled;
		private bool _IsPaused;
		#endregion

		#region Counters
		private int FramesDrawn;
		private int MaxFPS;

		private int LastFrameTick;
		private int CurrentFrameTick;
		#endregion

		#region Outside Resources
		private Control _Parent;
		private TransparentResourceManager _TransparentResourceManager;
		private OrbitItemRegistry _Items;
		#endregion

		#region Rendering Guides
		private Rectangle _AverageDockBounds;
		private Point _ScreenToDrawCenter;
		private Point _MultiMonitorCenter;
		private Screen _ScreenToDraw;
		#endregion
		#endregion

		#region Creator and destructor
		/// <summary>
		/// Creates a new instance of the Direct3DManager class
		/// </summary>
		public Direct3DManager(Control parent)
		{
			// bail out if our needed arguments are null
			if(parent==null)
				throw new ArgumentNullException();

			// store some important stuff
			_Parent=parent;
			// store some stuff that was causing trouble :P
			_ScreenToDraw=Screen.PrimaryScreen;

			// prepare timers
			/*FrameTimer=new Timer();
			FrameTimer.Interval=10;
			FrameTimer.Enabled=false;
			FrameTimer.Tick+=new EventHandler(FrameTimer_Tick);*/
			FrameThread=new System.Threading.Thread(new System.Threading.ThreadStart(FrameThread_Proc));
			FrameThreadEnabled=false;
			FrameThread.Start();

			FpsTimer=new Timer();
			FpsTimer.Interval=1000;
			FpsTimer.Enabled=false;
			FpsTimer.Tick+=new EventHandler(FpsTimer_Tick);
		}

		/// <summary>
		/// Disposes the Direct3DManager object
		/// </summary>
		public void Dispose()
		{
			// the device lost exception does all the disposing we might want to do for the device
			HandleDeviceLostException();
		}
		#endregion

		#region Public Methods
		#region Starting and Stopping
		/// <summary>
		/// Initializes all Direct3D resources and enables the program
		/// </summary>
		/// <returns>The result of the initialization attempt</returns>
		public InitializationResult Start()
		{
			Stop();
			_State=LoopState.NotInit;

			SetCompatibilitySettings();

			// create the device _Device configuration
			Microsoft.DirectX.Direct3D.PresentParameters description=new PresentParameters();
			try
			{
				description.Windowed=true;
				description.BackBufferCount=1;
				description.EnableAutoDepthStencil=true;
				description.SwapEffect=SwapEffect.Discard;
				description.BackBufferFormat=Manager.Adapters[0].CurrentDisplayMode.Format;
				description.BackBufferWidth=_Parent.ClientSize.Width;
				description.BackBufferHeight=_Parent.ClientSize.Height;
				description.AutoDepthStencilFormat=DepthFormat.D16;
				description.PresentationInterval=PresentInterval.Immediate;
				description.MultiSample=MultiSampleType.None;
				description.MultiSampleQuality=0;
			}
			catch(Exception)
			{
				//MessageBox.Show(this.Language.Language.Orbit.Messages.VideoCardNotSupported, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return InitializationResult.VideoCardNotSupported;
			}

			// check for hardware or software vertex processing
			CreateFlags CreF;
			try
			{
				Caps DevCaps=Manager.GetDeviceCaps(0, DeviceType.Hardware);
				if(DevCaps.DeviceCaps.SupportsHardwareTransformAndLight)
				{
					CreF=CreateFlags.HardwareVertexProcessing | CreateFlags.MultiThreaded;
				}
				else
				{
					CreF=CreateFlags.SoftwareVertexProcessing | CreateFlags.MultiThreaded;
				}
			}
			catch(Exception)
			{
				//MessageBox.Show(this.Language.Language.Orbit.Messages.VideoCardNotSupported, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return InitializationResult.VideoCardNotSupported;
			}

			// create the device
			try
			{
				_Device=new Device(0, DeviceType.Hardware, _Parent.Handle, CreF, description);
			}
			catch (Exception)
			{
				//MessageBox.Show(this.Language.Language.Orbit.Messages.CannotCreateD3DDevice, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				_State=LoopState.NotInit;
				return InitializationResult.Failed;
			}

			// set some options
			_Device.RenderState.ZBufferEnable=false;
			_Device.RenderState.Lighting=false;
			_Device.RenderState.SeparateAlphaBlendEnabled=false;
			_Device.RenderState.CullMode=Cull.None;

			// initiate transparent or non transparent stuff
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
			{
				if(Manager.CheckDepthStencilMatch(0, DeviceType.Hardware, _Device.PresentationParameters.BackBufferFormat, Format.A8R8G8B8, _Device.PresentationParameters.AutoDepthStencilFormat)
					&& _Device.DeviceCaps.NumberSimultaneousRts>=1
					&& !(System.Environment.OSVersion.Platform != System.PlatformID.Win32NT))
				{
					try
					{
						_TransparentResourceManager=new TransparentResourceManager(_Device);
						_TransparentResourceManager.SetUpFrontBuffer(_Parent.ClientSize);
						_TransparentResourceManager.SetUpBlitBuffer(_Parent.ClientSize);
						_TransparentResourceManager.ProjectionSize=_Parent.ClientSize;
						_Device.RenderState.AlphaBlendEnable=true;
					}
					catch (Exception)
					{
						//MessageBox.Show(this.Language.Language.Orbit.Messages.UnexpectedErrorOccurred, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						if(ForcedModeSwitch!=null)ForcedModeSwitch(this, new ForcedModeSwitchEventArgs(ModeSwitchReason.UnexpectedError));
						Global.Configuration.Appearance.Transparency=Orbit.Configuration.TransparencyMode.None;
						Stop();
						if(WindowResize!=null)WindowResize(this, new EventArgs());
						return Start();
					}
				} 
				else 
				{
					//MessageBox.Show(this.Language.Language.Orbit.Messages.TransparentModeNotSupported, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if(ForcedModeSwitch!=null)ForcedModeSwitch(this, new ForcedModeSwitchEventArgs(ModeSwitchReason.TransparentModeNotSupported));
					Global.Configuration.Appearance.Transparency=Orbit.Configuration.TransparencyMode.None;
					Stop();
					if(WindowResize!=null)WindowResize(this, new EventArgs());
					return Start();
				}
			}

			// handle a device loss
			_IsDeviceLost=false;
			_Device.DeviceLost+=new EventHandler(Device_DeviceLost);
			_Device.DeviceResizing+=new CancelEventHandler(Device_DeviceResizing);
			_Device.DeviceReset+=new EventHandler(Device_DeviceLost);

			FramesDrawn=0;
			FpsTimer.Enabled=true;
			_State=LoopState.Nothing;
			_IsDeviceLost=false;

			// intialize memory
			InitDeviceMem();
			return 0;
		}

		/// <summary>
		/// Deinitializes all the Direct3D resources and stops the program
		/// </summary>
		public void Stop()
		{
			Pause();

			// unload memory resources
			DeInitDeviceMem();

			_State=LoopState.Loading;
			
			// transparent resource manager
			if(_TransparentResourceManager!=null)
				_TransparentResourceManager.Dispose();

			// device
			if(_Device!=null)
				_Device.Dispose();

			_State=LoopState.NotInit;

		}
		#endregion

		#region Pausing and Resuming
		/// <summary>
		/// Pauses all the rendering timers
		/// </summary>
		public void Pause()
		{
			
			//this.FrameTimer.Enabled=false;
			FrameThreadEnabled=false;
			this.FpsTimer.Enabled=false;
			_IsPaused=true;

			// collect garbage. this is where the shit happens :P
			//wGC.Collect();
			//GC.WaitForPendingFinalizers();
		}
		/// <summary>
		/// Resumes all the rendering timers
		/// </summary>
		public void Resume()
		{
			//this.FrameTimer.Enabled=true;
			LastFrameTick = Environment.TickCount;
			FrameThreadEnabled=true;
			this.FpsTimer.Enabled=true;
			_IsPaused=false;
		}
		#endregion

		#region Rendering
		#region Main Loop
		/// <summary>
		/// Executes the main drawing methods for both Transparent and Non-Transparent modes
		/// </summary>
		private void MainLoop()
		{
			if(_ForceRenderNextFrame)
			{
				_State=LoopState.Loop;
				_ForceRenderNextFrame=false;
			}

			switch(_State)
			{
				case LoopState.Loop:
					// take separate paths for transparent and non transparent modes
					if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
					{
						DrawTransparent();
					}
					else
					{
						Draw();
					}
					break;
			}
		}

		#endregion

		#region Transparent Mode Specific
		/// <summary>
		/// Forces an update on the background window for the Transparent mode
		/// </summary>
		public void UpdateTransparentBackground()
		{
			/*
			 * This is the function which updates the background
			 * image. It is called whenever the background is resized
			 * or drawn for the first time or simply needs an update
			 * THIS FUNCTION IS NOT CALLED IN THE MAIN LOOP.
			 * IT IS CALLED ON CERTAINS OCCASIONS WHITHIN THE CODE.
			*/

			// bail out if our needed stuff is not valid
			if(_Items.Length==0 || _Items.Lines.Length==0)
				return;

			// make sure the dock is in proper shape
			this.MakeRound();
			try
			{
				// start background painting
				_Device.BeginScene();
				_ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);

				// draw background or clear if no background image
				_Device.Clear(ClearFlags.Target, Color.FromArgb(0x00, Color.Black), 0, 0);
				
				// draw icon backgrounds
				_Device.RenderState.BlendFactor=Color.FromArgb(Global.Configuration.Runtime.CurrentIconBgAlpha, Color.White);
				_Device.RenderState.SourceBlend=Blend.BlendFactor;

				if(Global.Configuration.Images.IconBackgroundImagePath!="")
					// Non pre multiplied alpha
					_ResourceManager.Sprite.Draw2D(this._ResourceManager.IconBackground.Texture,
						Rectangle.Empty,
						new SizeF((float)this.AverageDockBounds.Width, (float)this.AverageDockBounds.Height),
						new Point(0, 0),
						Color.White);

				_ResourceManager.Sprite.End();
				_Device.EndScene();
				_Device.Present();

				try
				{
					int off=Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Runtime.IconSizeAverage;
					Rectangle SurfaceCropRectangle=new Rectangle(new Point(0,0), new Size(AverageDockBounds.Width+(off), AverageDockBounds.Height+(off)));
					
					// update window
					using(Bitmap bBg=_TransparentResourceManager.GetCropFrontBuffer(ref SurfaceCropRectangle))
					{
						_TransparentResourceManager.BackForm.Update(bBg, new Rectangle(new Point(_MultiMonitorCenter.X-this.AverageDockBounds.Width/2, _MultiMonitorCenter.Y-this.AverageDockBounds.Width/2), new Size(SurfaceCropRectangle.Width, SurfaceCropRectangle.Height)));
					}
				}
				catch(Exception)
				{
					_State=LoopState.Loop;
					this.UpdateTransparentBackground();
				}
			}
			catch (DeviceLostException)
			{
				this.HandleDeviceLostException();
			}
			catch (Exception)
			{
				this.HandleGeneralRenderException();
			}

			// end background painting
			// update overlay
			this.UpdateTransparentOverlay();
		}

		/// <summary>
		/// Forces an update on the overlay window for the Transparent mode
		/// </summary>
		public void UpdateTransparentOverlay()
		{
			/*
			 * This function is responsible for updating the
			 * images and symbols that go on top of everything.
			 * This includes the more item markers and the open
			 * folder markers
			 * THIS FUNCTION IS NOT CALLED IN THE MAIN LOOP.
			 * IT IS CALLED ON CERTAINS OCCASIONS WHITHIN THE CODE.
			*/
			this.MakeRound();
			try
			{
				// start background painting
				_Device.BeginScene();
				_ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);

				// draw background or clear if no background image
				_Device.Clear(ClearFlags.Target, Color.FromArgb(0x00, Color.Black), 0, 0);
				
				// draw icon backgrounds
				_Device.RenderState.BlendFactor=Color.FromArgb(Global.Configuration.Runtime.CurrentIconBgAlpha, Color.White);
				_Device.RenderState.SourceBlend=Blend.BlendFactor;
				
				// draw selected icon markers
				int i=0;
				while(i<_Items.Lines.Length)
				{
					int Toggle=_Items.GetToggledItem(i);
					if(Global.Configuration.Images.IconSelectedImagePath!="" && Toggle>=0
						&& _Items[Toggle].IsShown)
						_ResourceManager.Sprite.Draw2D(this._ResourceManager.IconSelectedIndicator.Texture,
													Rectangle.Empty,
													new SizeF((float)_Items[Toggle].Rectangle.Width, (float)_Items[Toggle].Rectangle.Height),
													new Point((int)(_Items[Toggle].Rectangle.Left-_ScreenToDrawCenter.X+this.AverageDockBounds.Width/2), (int)(_Items[Toggle].Rectangle.Top-_ScreenToDrawCenter.Y+this.AverageDockBounds.Height/2)),
													_Items[Toggle].ColorKey);

					if(_Items.Lines[i].ShowsMoreIndicator)
					{
						int ItemsAllowedInLine;
						if(Global.Configuration.Appearance.ItemsShownPerLine==0 || _Items.Lines.Length==1)
							ItemsAllowedInLine=_Items.Lines[i].MaxVisibleItems;
						else
							ItemsAllowedInLine=Global.Configuration.Appearance.ItemsShownPerLine+1;

						// there are more _Items below
						if(!(_Items.Count(i)-_Items.Lines[i].StartIndex<ItemsAllowedInLine))
						{
							RectangleF MorePosition=this.GetMoreDownPosition(i);
							_ResourceManager.Sprite.Draw2D(this._ResourceManager.ScrollDownIndicator.Texture,
														Rectangle.Empty,
														new SizeF((float)Global.Configuration.Runtime.IconSizeAverage/2.0f, (float)Global.Configuration.Runtime.IconSizeAverage/2.0f),
														new Point((int)(MorePosition.Left-_ScreenToDrawCenter.X+this.AverageDockBounds.Width/2), (int)(MorePosition.Top-_ScreenToDrawCenter.Y+this.AverageDockBounds.Height/2)),
														Color.White);
						}
						// there are more _Items above
						if(_Items.Lines[i].StartIndex>0)
						{
							RectangleF MorePosition=this.GetMoreUpPosition(i);
							_ResourceManager.Sprite.Draw2D(this._ResourceManager.ScrollUpIndicator.Texture,
								Rectangle.Empty,
								new SizeF((float)Global.Configuration.Runtime.IconSizeAverage/2.0f, (float)Global.Configuration.Runtime.IconSizeAverage/2.0f),
								new Point((int)(MorePosition.Left-_ScreenToDrawCenter.X+this.AverageDockBounds.Width/2), (int)(MorePosition.Top-_ScreenToDrawCenter.Y+this.AverageDockBounds.Height/2)),
								Color.White);
						}
					}
					i++;
				}

				_ResourceManager.Sprite.End();
				_Device.EndScene();
				_Device.Present();

				try
				{
					int off=Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Runtime.IconSizeAverage;
					Rectangle SurfaceCropRectangle=new Rectangle(new Point(0,0), new Size(AverageDockBounds.Width+(off), AverageDockBounds.Height+(off)));

					// update window
					using(Bitmap bBg=_TransparentResourceManager.GetCropFrontBuffer(ref SurfaceCropRectangle))
					{
						_TransparentResourceManager.OverlayForm.Update(bBg, new Rectangle(new Point(_MultiMonitorCenter.X-this.AverageDockBounds.Width/2, _MultiMonitorCenter.Y-this.AverageDockBounds.Width/2), new Size(SurfaceCropRectangle.Width, SurfaceCropRectangle.Height)));
					}
				}
				catch(Exception)
				{
					_State=LoopState.Loop;
					this.UpdateTransparentBackground();
				}
			}
			catch (DeviceLostException)
			{
				this.HandleDeviceLostException();
			}
			catch (Exception)
			{
				this.HandleGeneralRenderException();
			}

			// end background painting
		}

		#endregion
		#endregion

		#region Frame Request Enqueuing
		/// <summary>
		/// Forces the renderer to render a frame on the upcoming MainLoop call
		/// </summary>
		public void ForceRenderNextFrame()
		{
			_ForceRenderNextFrame=true;
			//LastFrameTick = Environment.TickCount;
			//System.Threading.Thread.Sleep(10);
			//_ForceRenderNextFrame=true;
		}
		/// <summary>
		/// Forces the renderer to render a frame on the upcoming MainLoop call and waits for it to render to force again
		/// </summary>
		/// <remarks>This ensures that the next frame is rendered because it renders two frames. But this causes a 10ms wait on the thread from which this was called</remarks>
		public void ForceRenderNextFrameWait()
		{
			ForceRenderNextFrame();
			//LastFrameTick = Environment.TickCount;
			System.Threading.Thread.Sleep(5);
			ForceRenderNextFrame();
			//LastFrameTick = Environment.TickCount;
		}
		#endregion
		#endregion

		#region Private Methods
		#region Initialization Helpers
		private void InitDeviceMem()
		{
			_State=LoopState.Loading;

			// resource manager
			if(_ResourceManager!=null)
				_ResourceManager.Dispose();
			_ResourceManager=new OrbitResourceManager(_Device);

			// screen grabber
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake)
			{
				_ScreenGrabber=new ScreenGrabber(_Device, this._ResourceManager.Sprite, new Rectangle(_Parent.Location, _Parent.Size));
				_ScreenGrabber.Paint+=new EventHandler(TS_Paint);
			}

			_State=LoopState.Nothing;
		}

		private void DeInitDeviceMem()
		{
			_State=LoopState.Loading;

			// resource manager
			if(_ResourceManager!=null)
				_ResourceManager.Dispose();

			// fake transparency module
			if(_ScreenGrabber!=null)
				this._ScreenGrabber.Dispose();

			_State=LoopState.Nothing;
		}

		private void SetCompatibilitySettings()
		{
			// gather some compatibility info on the device
			AdapterDetails ad=Manager.Adapters[0].Information;

			// set the compatibility settings for this vendor
			if(ad.VendorId==(int)KnownVendors.ATI
				|| ad.VendorId==(int)KnownVendors.Matrox
				|| ad.VendorId==(int)KnownVendors.Trident
				|| Global.Configuration.Behavior.ForceLowQualityLabels)
				// disable HQ labels for ATI, Matrox and Trident
				FeatureSets.UseHQLabels=false;
			else
				FeatureSets.UseHQLabels=true;
			//FeatureSets.UseHQLabels=false;

			// disable HQ task switching if using transparent mode
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real
				|| Global.Configuration.Behavior.ForceNoPreviewAnimation)
				FeatureSets.UseHQPreviews=false;
			else
				FeatureSets.UseHQPreviews=true;
			//FeatureSets.UseHQPreviews=false;
		}

		#endregion

		#region Animation Controller
		private LoopState Animate()
		{
			/*
			 * This function sets up the next frame to be drawn
			 * and tells the drawing funcion if there are more frames
			 * in the animation of this is the last frame
			*/
			_State=LoopState.AnimatingFrame;
			LoopState ToDoNext=LoopState.Nothing;

			// calculate the animation increment per frame
			// read our timings
			CurrentFrameTick=Environment.TickCount;
			int FrameCounter=CurrentFrameTick-LastFrameTick;
			// estimated frame time for 50fps (our reference fps in the animation speed settings)
			int DefaultFrameTime=1000/50;
			// calculate the increment in bytes (cause all anim counters are in bytes)
			byte FrameIncrement=0;
			if((float)FrameCounter/DefaultFrameTime * Global.Configuration.Appearance.AnimationSpeed>255)
				FrameIncrement = Global.Configuration.Runtime.LastFrameSpeed = 255;
			else
				// direct usage
				//FrameIncrement = Global.Configuration.Runtime.LastFrameSpeed = Convert.ToByte((float)FrameCounter/DefaultFrameTime * Global.Configuration.Appearance.AnimationSpeed);
				// average with last frame
				FrameIncrement = Global.Configuration.Runtime.LastFrameSpeed = Convert.ToByte((Global.Configuration.Runtime.LastFrameSpeed+(float)FrameCounter/DefaultFrameTime * Global.Configuration.Appearance.AnimationSpeed)/2);
			// update our time counter
			if(FrameCounter>DefaultFrameTime)
			{
				FrameCounter-=DefaultFrameTime;
			}
			// set our last frame to be the current one
			LastFrameTick=CurrentFrameTick;

			// now animate
			// animate screen capture if it exists
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake)
			{
				ToDoNext=_ScreenGrabber.Animate();
			}
			try
			{
				// animate in items
				int i=0;
				while(i<_Items.Length)
				{
					try
					{
						// set color to totally transparent if item doesn't exist, is disabled or not shown
						if(_Items[i]==null || !_Items[i].Enabled || !_Items[i].IsShown)
						{
							if(_Items[i]!=null)
								//_Items[i].ColorKey=Color.FromArgb(0x00, _Items[i].ColorKey);
								_Items[i].AnimationState=0.0f;
							i++;
							continue;
						}
						// animate to full opacity if mouse is over
						if(_Items[i].IsMouseOver)
						{
							// Fade to 255 if selected
							if(_Items[i].AnimationState<255)
							{
								if(_Items[i].AnimationState+FrameIncrement>=255)
								{
									_Items[i].AnimationState=255;
								}
								else 
								{
									_Items[i].AnimationState=_Items[i].AnimationState+FrameIncrement;
									ToDoNext=LoopState.Loop;
								}
							}
						} 
						else 
						{
							// fade in to default transparency, if no mouse over
							if(_Items[i].AnimationState<Global.Configuration.Appearance.IconAlpha)
							{
								// Fade in to Alpha color if icon just popped up
								if(_Items[i].AnimationState+FrameIncrement>=Global.Configuration.Appearance.IconAlpha)
								{
									_Items[i].AnimationState=Global.Configuration.Appearance.IconAlpha;
								}
								else
								{
									_Items[i].AnimationState=_Items[i].AnimationState+FrameIncrement;
									ToDoNext=LoopState.Loop;
								}
							} 
							else 
							{
								// Fade out to Alpha color if the icon isn't mouseovered anymore
								if(_Items[i].AnimationState>Global.Configuration.Appearance.IconAlpha)
								{
									if(_Items[i].AnimationState-FrameIncrement<=Global.Configuration.Appearance.IconAlpha)
									{
										_Items[i].AnimationState=Global.Configuration.Appearance.IconAlpha;
									}
									else
									{
										_Items[i].AnimationState=_Items[i].AnimationState-FrameIncrement;
										ToDoNext=LoopState.Loop;
									}
								}
							}
						}
					}
					catch(Exception){}
					i++;
				}
			}
			catch(Exception exc)
			{
#if DEBUG
				MessageBox.Show(exc.Message+"\n"+exc.StackTrace);
#endif
			}

			// make sure we're all in right positions
			if(_Parent.Visible
				|| (_TransparentResourceManager!=null && _TransparentResourceManager.DisplayForm.Visible))
				this.MakeRound();
			// set loop state
			_State=LoopState.Nothing;
			//if(ToDoNext==LoopState.Loop)ForceRenderNextFrame();
			return ToDoNext;
		}
		
		#endregion

		#region Transformation Methods
		private void MakeRound()
		{
			// bail out if nothing to manipulate
			if(_Items==null || _Items.Lines==null)
				return;

			try
			{
				// vars for our rectangle and regularly used stuff (like circle length hehe)
				Rectangle MyRect=new Rectangle(new Point(int.MaxValue,int.MaxValue), new Size(0,0));
				Rectangle OldBounds=_AverageDockBounds;
				this._AverageDockBounds=MyRect;
				double Circle=2.0*Math.PI;

				// root radius
				double FirstRadius=_Items.Lines[0].AbsoluteRadius;

				int[] ItemsPerLine=new int[_Items.Lines.Length];
				int[] ItemsPassed=new int[_Items.Lines.Length];
				int i=0;
				int StoredLength=_Items.Length;

				// cycle through the registry and apply transforms
				while(i<_Items.Length)
				{
					// bail out if our registry was changed while we were doing this :P
					if(StoredLength!=_Items.Length)
						return;

					OrbitItem item=_Items[i];
					// skip this item if it's not visible or not enabled
					if(item==null || !item.Enabled)
					{
						i++;
						continue;
					}
					// old item position
					RectangleF OldRect=item.Rectangle;

					// determine if this is visible or not
					if(ItemsPassed[item.Line]<_Items.Lines[item.Line].StartIndex
						|| ItemsPassed[item.Line]-_Items.Lines[item.Line].StartIndex>=_Items.Lines[item.Line].VisibleItems)
					{
						item.IsShown=false;
						_Items.Lines[item.Line].ShowsMoreIndicator=true;					
						ItemsPassed[item.Line]++;
						i++;
						continue;
					}

					/*// hit testing
					if(item.IsShown
						&& item.Rectangle.IntersectsWith(new Rectangle(new Point((Cursor.Position.X-_Parent.Bounds.X-_ScreenToDraw.Bounds.Left), (Cursor.Position.Y-_Parent.Bounds.Y-_ScreenToDraw.Bounds.Top)), new Size(1,1))))
						item.IsMouseOver=true;
					else
						item.IsMouseOver=false;*/

					// Apply Transition Effects
					double ItemRadius;
					int ItemRotation;
					float ItemSize;
					if(item.AnimationState<Global.Configuration.Appearance.IconAlpha)
					{
						this.GetTransitionEffectsOnItem(item, out ItemRotation, out ItemRadius, out ItemSize);
					} 
					else
					{
						ItemRotation=_Items.Lines[item.Line].RotatedDegrees;
						ItemSize=Global.Configuration.Runtime.IconSizeAverage;
						ItemRadius=FirstRadius;
					}

					// calculate X and Y Positions
					float ItemPositionX = (float)((ItemRadius+_Items.Lines[item.Line].RelativeRadius)*Global.Scale*Math.Cos(Circle+item.RotationOffset))+_ScreenToDrawCenter.X-ItemSize/2*Global.Scale;
					float ItemPositionY = (float)((ItemRadius+_Items.Lines[item.Line].RelativeRadius)*Global.Scale*Math.Sin(Circle+item.RotationOffset))+_ScreenToDrawCenter.Y-ItemSize/2*Global.Scale;

					// get our Proposed position if no magnification is involved
					RectangleF ProposedPosition = new RectangleF(new PointF(ItemPositionX, ItemPositionY), new SizeF(ItemSize, ItemSize));

					// set magnification
					if(Global.Configuration.Appearance.IconMagnifiedSize!=Global.Configuration.Appearance.IconMinifiedSize)
					{
						// blend with transition effects
						float blend=(float)item.AnimationState/(float)Global.Configuration.Appearance.IconAlpha;
						if(blend>1)blend=1;
						float MagnifiedItemSize=GetMagnificationEffectOnItem(item, ProposedPosition)*blend+ItemSize*(1-blend);

						// set our values
						float IconPositionDelta=ItemSize - MagnifiedItemSize;
						ItemPositionX=ItemPositionX+IconPositionDelta/2*Global.Scale;
						ItemPositionY=ItemPositionY+IconPositionDelta/2*Global.Scale;
						ItemSize=MagnifiedItemSize;

						// update the proposed rectangle
						ProposedPosition = new RectangleF(new PointF(ItemPositionX, ItemPositionY), new SizeF(ItemSize, ItemSize));
					}

					// setting stuff
					item.RotationOffset=((float)ItemRotation/360.0*Circle)-(Circle/_Items.Lines[item.Line].MaxVisibleItems*ItemsPerLine[item.Line]);
					item.Rectangle=new RectangleF(new PointF(ItemPositionX, ItemPositionY), new SizeF(ProposedPosition.Size.Width*Global.Scale, ProposedPosition.Size.Height*Global.Scale));				
					item.IsShown=true;

					// alter our dock rectangle
					if(item.Rectangle.X<=MyRect.X)MyRect.X=Convert.ToInt32(item.Rectangle.X);
					if(item.Rectangle.Y<=MyRect.Y)MyRect.Y=Convert.ToInt32(item.Rectangle.Y);

					// hit testing
					/*if(item.IsShown
						&& item.Rectangle.IntersectsWith(new Rectangle(new Point((Cursor.Position.X-_Parent.Bounds.X-_ScreenToDraw.Bounds.Left), (Cursor.Position.Y-_Parent.Bounds.Y-_ScreenToDraw.Bounds.Top)), new Size(1,1))))
					{
						double DistanceFromCursor;
						if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
							DistanceFromCursor=Math.Pow((Cursor.Position.X-_ScreenToDraw.Bounds.X)-(item.Rectangle.X+item.Rectangle.Width/2), 2)+Math.Pow((Cursor.Position.Y-_ScreenToDraw.Bounds.Y)-(item.Rectangle.Y+item.Rectangle.Height/2), 2);
						else
							DistanceFromCursor=Math.Pow((Cursor.Position.X-_ScreenToDraw.Bounds.X-_ScreenToDraw.WorkingArea.X)-(item.Rectangle.X+item.Rectangle.Width/2), 2)+Math.Pow((Cursor.Position.Y-_ScreenToDraw.Bounds.Y-_ScreenToDraw.WorkingArea.Y)-(item.Rectangle.Y+item.Rectangle.Height/2), 2);
						if(DistanceFromCursor<closestDistance || closestIndex==-1)
						{
							closestIndex=i;
							closestDistance=DistanceFromCursor;
							//item.IsMouseOver=true;
						}
						else
							item.IsMouseOver=false;
					}
					else
						item.IsMouseOver=false;

					item.IsMouseOver=false;*/

					// continuing
					ItemsPerLine[item.Line]++;
					ItemsPassed[item.Line]++;
					i++;
				}
				/*if(closestIndex!=-1)
					_Items[closestIndex].IsMouseOver=true;*/
				//float MyRectUnscaled=(float)FirstRadius*2+Global.Configuration.Runtime.IconSizeAverage+Global.Configuration.Runtime.LoopDistance*(_Items.Lines.Length-2)*2+Global.Configuration.Runtime.IconSizeAverage*(_Items.Lines.Length-1)*2;
				float MyRectUnscaled=Convert.ToInt32(_Items.Lines[_Items.Lines.Length-1].AbsoluteRadius)*2+Global.Configuration.Runtime.IconSizeAverage;
				//MyRect.Width=Convert.ToInt32((FirstRadius*2+Global.Configuration.Runtime.IconSizeAverage+Global.Configuration.Runtime.LoopDistance*(_Items.Lines.Length-1)*2+Global.Configuration.Runtime.IconSizeAverage*(_Items.Lines.Length-1)*2)*Global.Scale);
				MyRect.Width=Convert.ToInt32(MyRectUnscaled*Global.Scale);
				MyRect.Height=MyRect.Width;

				if(Global.Configuration.Appearance.Transparency!=TransparencyMode.Real)
				{
                    ScaleOrbit(MyRectUnscaled, MyRect.Width);
					PlaceOrbit(MyRect.Width);
				}

				this._AverageDockBounds=MyRect;
				// resize our buffers if we need to :)
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real
					&& (OldBounds.Width!=MyRect.Width || OldBounds.Height!=MyRect.Height))
				{
					OldBounds=MyRect;
					ResizeBuffers(OldBounds.Size);
				}
			}
			catch(Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				//MessageBox.Show(exc.Message+"\n"+exc.StackTrace);
#endif
			}
		}

		private void GetTransitionEffectsOnItem(OrbitItem item, out int itemRotation, out double itemRadius, out float itemSize)
		{
			// smoothen out the regular transition curve with a Sin interpolator :P
			double Percentage=Math.Sin(Math.PI/2*((double)item.AnimationState/(double)Global.Configuration.Appearance.IconAlpha));

			// Spin Transition Effect
			if(Global.Configuration.Behavior.TransitionSpin)
			{
				itemRotation=180/(item.Line+1)-Convert.ToInt32((float)180/(item.Line+1)*Percentage)+_Items.Lines[item.Line].RotatedDegrees;
			}
			else
			{
				itemRotation=_Items.Lines[item.Line].RotatedDegrees;
			}

			// Slide Transition Effect
			if(Global.Configuration.Behavior.TransitionSlide)
			{
				itemRadius=_Items.Lines[0].AbsoluteRadius*Percentage;
			} 
			else 
			{
				itemRadius=_Items.Lines[0].AbsoluteRadius;
			}

			// hide labels if the items are not on their final destination
			//if(itemRadius<_Items.Lines[0].AbsoluteRadius*((double)(Global.Configuration.Appearance.IconAlpha-Global.Configuration.Appearance.AnimationSpeed)/(double)Global.Configuration.Appearance.IconAlpha))
			//if(itemRadius<_Items.Lines[0].AbsoluteRadius*((double)(Global.Configuration.Appearance.IconAlpha-Global.Configuration.Runtime.LastFrameSpeed)/(double)Global.Configuration.Appearance.IconAlpha))
			/*if(item.AnimationState<Global.Configuration.Appearance.IconAlpha)
				Global.Configuration.Runtime.AllowLabels=false;
			else
				Global.Configuration.Runtime.AllowLabels=true;*/

			// Zoom Transition Effect
			if(Global.Configuration.Behavior.TransitionZoom)
			{
				int NewSize=Convert.ToInt32((double)Global.Configuration.Appearance.IconMinifiedSize*Percentage);
				//item.Rectangle=new RectangleF(item.Rectangle.Location, new SizeF(NewSize, NewSize));
				itemSize=NewSize;
			} 
			else 
			{
				//item.Rectangle=new RectangleF(item.Rectangle.Location, new Size(Global.Configuration.Runtime.IconSizeAverage, Global.Configuration.Runtime.IconSizeAverage));
				itemSize=Global.Configuration.Runtime.IconSizeAverage;
			}
		}

		private float GetMagnificationEffectOnItem(OrbitItem item, RectangleF previousBounds)
		{
			// general control variables
			int MagnificationDelta=Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Appearance.IconMinifiedSize;

			// the distance from the cursor varies depending on the position of the form
			// this is due to sidebars that might move the form down or right
			// so, non transparent mode takes into consideration those.
			// transparent mode is NOT affected by this
			float DistanceFromCursor;
			if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
				DistanceFromCursor=(float)Math.Sqrt(Math.Pow((Cursor.Position.X-_ScreenToDraw.Bounds.X)-(previousBounds.X+previousBounds.Width/2), 2)+Math.Pow((Cursor.Position.Y-_ScreenToDraw.Bounds.Y)-(previousBounds.Y+previousBounds.Height/2), 2));
			else
				DistanceFromCursor=(float)Math.Sqrt(Math.Pow((Cursor.Position.X-_ScreenToDraw.Bounds.X-_ScreenToDraw.WorkingArea.X)-(previousBounds.X+previousBounds.Width/2), 2)+Math.Pow((Cursor.Position.Y-_ScreenToDraw.Bounds.Y-_ScreenToDraw.WorkingArea.Y)-(previousBounds.Y+previousBounds.Height/2), 2));

			//DistanceFromCursor*=Global.Scale;
					
			// arbitrary settings for the magnification
			float SteepRate=Global.Configuration.Runtime.IconSizeAverage*2/3*Global.Scale;
			float MagnificationBalance=0.6f;

			// calculating the percentage of magnification
			float distPercent=(SteepRate/DistanceFromCursor)*MagnificationBalance;
			if(distPercent>MagnificationBalance)
				distPercent=1f*(SteepRate-DistanceFromCursor)/(SteepRate)*(1-MagnificationBalance)+MagnificationBalance;

			// calculating new icon size based on the delta and percentage
			float NewIconSize=(float)Global.Configuration.Appearance.IconMinifiedSize*Global.Scale + ((float)MagnificationDelta*(float)distPercent);
					
			return NewIconSize;
		}

		
		private void ScaleOrbit(float SizeUnscaled, float SizeScaled)
		{
			// scale the entire thing to always fit onscreen
			//Global.Scale=1f;
			if(ScreenToDraw.WorkingArea.Width<ScreenToDraw.WorkingArea.Height)
			{
				// using width as limiter
				if(SizeUnscaled>ScreenToDraw.WorkingArea.Width)
				{
					// downscaling
					//Global.Scale=(float)ScreenToDraw.WorkingArea.Width/MyRectUnscaled;
					if(SizeScaled>ScreenToDraw.WorkingArea.Width)
					{
						float NewScale=(float)ScreenToDraw.WorkingArea.Width/SizeUnscaled;
						Global.Scale-=(Global.Scale-NewScale)*0.2f;
						ForceRenderNextFrame();
					}
				}
				else
				{
					// upscaling
					if(SizeScaled<SizeUnscaled)
					{
						float NewScale=(float)SizeUnscaled/SizeScaled;
						Global.Scale+=(NewScale-Global.Scale)*0.1f;
						ForceRenderNextFrame();
					}
				}
			}
			else
			{
				// using height as limiter
				if(SizeUnscaled>ScreenToDraw.WorkingArea.Height)
				{
					// downscaling
					//Global.Scale=(float)ScreenToDraw.WorkingArea.Height/MyRectUnscaled;
					if(SizeScaled>ScreenToDraw.WorkingArea.Height)
					{
						float NewScale=(float)ScreenToDraw.WorkingArea.Height/SizeUnscaled;
						Global.Scale-=(Global.Scale-NewScale)*0.2f;
						ForceRenderNextFrame();
					}
				}
				else
				{
					// upscaling
					if(SizeScaled<SizeUnscaled)
					{
						float NewScale=(float)SizeUnscaled/SizeScaled;
						Global.Scale+=(NewScale-Global.Scale)*0.1f;
						ForceRenderNextFrame();
					}
				}
			}
		}
		private void PlaceOrbit(float SizeScaled)
		{
			SizeScaled=SizeScaled+(Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Runtime.IconSizeAverage)*Global.Scale;

			//Point ScreenToDrawCenterOffset=new Point(_ScreenToDrawCenter.X-ScreenToDraw.WorkingArea.X, _ScreenToDrawCenter.Y-ScreenToDraw.WorkingArea.Y);
			Point ScreenToDrawCenterOffset=new Point(_ScreenToDrawCenter.X, _ScreenToDrawCenter.Y);
			// change our center so Orbit's always entirely onscreen
			// right border
			//if(SizeScaled/2+ScreenToDrawCenterOffset.X+ScreenToDraw.WorkingArea.X>ScreenToDraw.WorkingArea.Right-ScreenToDraw.WorkingArea.X)
			if(SizeScaled/2+ScreenToDrawCenterOffset.X>ScreenToDraw.WorkingArea.Right-ScreenToDraw.WorkingArea.X)
			{
				//_ScreenToDrawCenter.X=Convert.ToInt32(ScreenToDraw.WorkingArea.Right-SizeScaled/2);
				//_ScreenToDrawCenter.X-=Convert.ToInt32((ScreenToDrawCenterOffset.X+SizeScaled/2-ScreenToDraw.WorkingArea.Right)*0.2f);
				_ScreenToDrawCenter.X-=Convert.ToInt32((ScreenToDrawCenterOffset.X+SizeScaled/2-(ScreenToDraw.WorkingArea.Right-ScreenToDraw.WorkingArea.X))*0.2f);
				ForceRenderNextFrame();
			}
			// bottom border
			//if(SizeScaled/2+ScreenToDrawCenterOffset.Y+ScreenToDraw.WorkingArea.Y>ScreenToDraw.WorkingArea.Bottom-ScreenToDraw.WorkingArea.Y)
			if(SizeScaled/2+ScreenToDrawCenterOffset.Y>ScreenToDraw.WorkingArea.Bottom-ScreenToDraw.WorkingArea.Y)
			{
				//_ScreenToDrawCenter.Y=Convert.ToInt32(ScreenToDraw.WorkingArea.Bottom-SizeScaled/2);
				_ScreenToDrawCenter.Y-=Convert.ToInt32((ScreenToDrawCenterOffset.Y+SizeScaled/2-(ScreenToDraw.WorkingArea.Bottom-ScreenToDraw.WorkingArea.Y))*0.2f);
				ForceRenderNextFrame();
			}
			// top border
			if(ScreenToDrawCenterOffset.Y-SizeScaled/2<0)
			{
				//_ScreenToDrawCenter.Y=Convert.ToInt32(SizeScaled/2);
				_ScreenToDrawCenter.Y+=Convert.ToInt32((0-(ScreenToDrawCenterOffset.Y-SizeScaled/2))*0.2f);
				ForceRenderNextFrame();
			}
			// left border
			if(ScreenToDrawCenterOffset.X-SizeScaled/2<0)
			{
				//_ScreenToDrawCenter.X=Convert.ToInt32(SizeScaled/2);
				_ScreenToDrawCenter.X+=Convert.ToInt32((0-(ScreenToDrawCenterOffset.X-SizeScaled/2))*0.2f);
				ForceRenderNextFrame();
			}
		}
		#endregion

		#region Helper Methods
		private RectangleF GetMoreUpPosition(int line)
		{
			// Get the first radius
			double Circle=2.0*Math.PI;
			double FirstRadius=_Items.Lines[0].AbsoluteRadius;
			// Get this _Items radius and other information
			double ItemRotation=-_Items.Lines[line].RotatedDegrees;
			double ItemRadius=_Items.Lines[line].RelativeRadius;
			if(ItemRadius==FirstRadius)
				ItemRadius=0;
			// Get (based on the first radius), this line's quantity of icons allowed
			int MaxItemsInThisLine=_Items.Lines[line].MaxVisibleItems;
			int ItemsAllowedInLine;
			if(Global.Configuration.Appearance.ItemsShownPerLine==0 || line==0)
				ItemsAllowedInLine=MaxItemsInThisLine-1;
			else
				ItemsAllowedInLine=-1;

			/*RectangleF MyPosition=new RectangleF(new PointF(0,0), new SizeF(Global.Configuration.Runtime.IconSizeAverage, Global.Configuration.Runtime.IconSizeAverage));
			MyPosition.X=(float)((FirstRadius+ItemRadius)*Math.Cos(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.X-Global.Configuration.Runtime.IconSizeAverage/2;
			MyPosition.X+=Global.Configuration.Runtime.IconSizeAverage/4;
			MyPosition.Y=(float)((FirstRadius+ItemRadius)*Math.Sin(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.Y-Global.Configuration.Runtime.IconSizeAverage/2;*/
			RectangleF MyPosition=new RectangleF(new PointF(0,0), new SizeF(Global.Configuration.Runtime.IconSizeAverage*Global.Scale, Global.Configuration.Runtime.IconSizeAverage*Global.Scale));
			MyPosition.X=(float)((FirstRadius+ItemRadius)*Global.Scale*Math.Cos(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.X-Global.Configuration.Runtime.IconSizeAverage/2;
			MyPosition.X+=Global.Configuration.Runtime.IconSizeAverage*Global.Scale/4;
			MyPosition.Y=(float)((FirstRadius+ItemRadius)*Global.Scale*Math.Sin(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.Y-Global.Configuration.Runtime.IconSizeAverage/2;

			return MyPosition;
		}
		private RectangleF GetMoreDownPosition(int line)
		{
			// Get the first radius
			double Circle=2.0*Math.PI;
			double FirstRadius=_Items.Lines[0].AbsoluteRadius;
			// Get this _Items radius and other information
			double ItemRotation=-_Items.Lines[line].RotatedDegrees;
			double ItemRadius=_Items.Lines[line].RelativeRadius;
			// Get (based on the first radius), this line's quantity of icons allowed
			int MaxItemsInThisLine=_Items.Lines[line].MaxVisibleItems;
			int ItemsAllowedInLine;
			if(Global.Configuration.Appearance.ItemsShownPerLine==0 || line==0)
				ItemsAllowedInLine=MaxItemsInThisLine-1;
			else
				ItemsAllowedInLine=Global.Configuration.Appearance.ItemsShownPerLine;

			/*RectangleF MyPosition=new RectangleF(new PointF(0,0), new SizeF(Global.Configuration.Runtime.IconSizeAverage, Global.Configuration.Runtime.IconSizeAverage));
			MyPosition.X=(float)((FirstRadius+ItemRadius)*Math.Cos(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.X-Global.Configuration.Runtime.IconSizeAverage/2;
			MyPosition.X+=Global.Configuration.Runtime.IconSizeAverage/4;
			MyPosition.Y=(float)((FirstRadius+ItemRadius)*Math.Sin(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.Y-Global.Configuration.Runtime.IconSizeAverage/2;
			MyPosition.Y+=Global.Configuration.Runtime.IconSizeAverage/2;*/
			RectangleF MyPosition=new RectangleF(new PointF(0,0), new SizeF(Global.Configuration.Runtime.IconSizeAverage*Global.Scale, Global.Configuration.Runtime.IconSizeAverage*Global.Scale));
			MyPosition.X=(float)((FirstRadius+ItemRadius)*Global.Scale*Math.Cos(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.X-Global.Configuration.Runtime.IconSizeAverage/2;
			MyPosition.X+=Global.Configuration.Runtime.IconSizeAverage*Global.Scale/4;
			MyPosition.Y=(float)((FirstRadius+ItemRadius)*Global.Scale*Math.Sin(Circle-((float)ItemRotation/360.0*Circle)-(Circle/MaxItemsInThisLine*(ItemsAllowedInLine))))+_ScreenToDrawCenter.Y-Global.Configuration.Runtime.IconSizeAverage/2;
			MyPosition.Y+=Global.Configuration.Runtime.IconSizeAverage*Global.Scale/2;

			return MyPosition;
		}

		private void DrawLabelText(OrbitItem item, float positionX, float positionY)
		{
			// cropping details
			// no transparency, fake transparency and real transparency use this
			//DrawTextFormat DTF=DrawTextFormat.NoClip | DrawTextFormat.WordEllipsis;
			DrawTextFormat DTF=DrawTextFormat.NoClip;
			// this is only used when in non-real transparency and using low quality labels
			if(!FeatureSets.UseHQLabels && Global.Configuration.Appearance.Transparency!=TransparencyMode.Real)
				DTF=DrawTextFormat.NoClip;

			int CropSize=(int)((Global.Configuration.Fonts.DescriptionSize+Global.Configuration.Fonts.LabelSize+5)*3f/2f);

			#region Drawing the Name
			if(Global.Configuration.Fonts.ShowLabelBorder)
			{
				Global.Configuration.Fonts.LabelBorderColor=Color.FromArgb(0xFF, Global.Configuration.Fonts.LabelBorderColor);
				_ResourceManager.LabelFont.DrawText(null, item.Name, new Rectangle(new Point(Convert.ToInt32(positionX+4), Convert.ToInt32(positionY+1)), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.LabelBorderColor);
				_ResourceManager.LabelFont.DrawText(null, item.Name, new Rectangle(new Point(Convert.ToInt32(positionX+4), Convert.ToInt32(positionY-1)), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.LabelBorderColor);
				_ResourceManager.LabelFont.DrawText(null, item.Name, new Rectangle(new Point(Convert.ToInt32(positionX+6), Convert.ToInt32(positionY+1)), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.LabelBorderColor);
				_ResourceManager.LabelFont.DrawText(null, item.Name, new Rectangle(new Point(Convert.ToInt32(positionX+6), Convert.ToInt32(positionY-1)), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.LabelBorderColor);
			}
			Global.Configuration.Fonts.LabelColor=Color.FromArgb(0xFF, Global.Configuration.Fonts.LabelColor);
			_ResourceManager.LabelFont.DrawText(null, item.Name, new Rectangle(new Point(Convert.ToInt32(positionX+5), Convert.ToInt32(positionY)), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.LabelColor);
			#endregion

			#region Drawing the Description
			if(item.Description!="" && item.Description!=null)
			{
				if(Global.Configuration.Fonts.ShowDescriptionBorder)
				{
					Global.Configuration.Fonts.DescriptionBorderColor=Color.FromArgb(0xFF, Global.Configuration.Fonts.DescriptionBorderColor);
					_ResourceManager.DescriptionFont.DrawText(null, item.Description, new Rectangle(new Point(Convert.ToInt32(positionX+4), Convert.ToInt32(positionY+1+Global.Configuration.Fonts.LabelSize*(3f/2f))), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.DescriptionBorderColor);
					_ResourceManager.DescriptionFont.DrawText(null, item.Description, new Rectangle(new Point(Convert.ToInt32(positionX+4), Convert.ToInt32(positionY-1+Global.Configuration.Fonts.LabelSize*(3f/2f))), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.DescriptionBorderColor);
					_ResourceManager.DescriptionFont.DrawText(null, item.Description, new Rectangle(new Point(Convert.ToInt32(positionX+6), Convert.ToInt32(positionY+1+Global.Configuration.Fonts.LabelSize*(3f/2f))), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.DescriptionBorderColor);
					_ResourceManager.DescriptionFont.DrawText(null, item.Description, new Rectangle(new Point(Convert.ToInt32(positionX+6), Convert.ToInt32(positionY-1+Global.Configuration.Fonts.LabelSize*(3f/2f))), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.DescriptionBorderColor);
				}
				Global.Configuration.Fonts.DescriptionColor=Color.FromArgb(0xFF, Global.Configuration.Fonts.DescriptionColor);
				_ResourceManager.DescriptionFont.DrawText(null, item.Description, new Rectangle(new Point(Convert.ToInt32(positionX+5), Convert.ToInt32(positionY+Global.Configuration.Fonts.LabelSize*(3f/2f))), new Size(512, CropSize)), DTF, Global.Configuration.Fonts.DescriptionColor);
			}
			#endregion
		}
		#endregion

		#region Non Transparent Mode Specific Rendering
		private void Draw()
		{
			/*
			 * This is the one and only drawing function for the
			 * non-transparent mode. it draws EVERYTHING.
			 * By understanding this function, you must be able to understand
			 * all the others below
			*/
			try
			{
				// update state
				_State=LoopState.DrawingFrame;
				// Prepare scene and know what loopstate to enter next
				// based on the need of another animation frame or not
				LoopState DoNext=Animate();

				// bake the labels if using HQ labels
				if(FeatureSets.UseHQLabels)
					this.BakeLabel();

				// start scene
				_Device.BeginScene();
			
				// draw background or clear if no background image
				_Device.Clear(ClearFlags.Target, _ResourceManager.BackgroundProvider.BackgroundColor, 0, 0);

				this._ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);

				// keep the selected item
				int SI=-1;
			
				#region Background
				// don't draw bg if we have a screenshot in front of us
				if((Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake && _ScreenGrabber.AlphaStage!=255)
					|| Global.Configuration.Appearance.Transparency!=Orbit.Configuration.TransparencyMode.Fake)
				{
					Size ReferenceSize;
					if(Global.Configuration.Images.UseWindowsWallpaper)
						ReferenceSize=Screen.PrimaryScreen.Bounds.Size;
					else
						ReferenceSize=_Parent.Size;

					_ResourceManager.BackgroundProvider.Draw(false, ReferenceSize, _Parent.Location);
				}
				#endregion

				#region Screen Capturew
				try
				{
					// draw screencapture when available
					if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Fake)
					{
						_ScreenGrabber.Render(false);
					}
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				}
				#endregion
			
				#region Icon Background
				_ResourceManager.DrawIconBg(false, this.AverageDockBounds.Size, _ScreenToDrawCenter);

				// i have absolutely no idea why the Begin() and End() below are needed
				// but if commented, they cause black blocks while drawing BG+No IconBg+No Fake Trans
				_ResourceManager.Sprite.End();
				_ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);
				#endregion

				// alter the matrix to that which we want :)
				_Device.Transform.World=Matrix.Identity;
				_Device.Transform.Projection=Matrix.OrthoOffCenterLH(0f, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, 0f, 2f, -1f);
				_Device.Transform.View=Matrix.Identity;

				#region Icons
				try
				{
					// draw icons and selected markers
					int i=0;
					int x=1;
					int y=1;
					while (i<_Items.Length)
					{
						// icon drawing
						try
						{
							if(_Items[i]!=null && _Items[i].Enabled && _Items[i].IsShown && _Items[i].AnimationState>0)
							{
								_Items[i].Draw(0,0);
							}
						}
						catch (Exception)
						{
							_State=LoopState.Loop;
						}

						// book if item is selected (to avoid going through it all again)
						if(_Items[i].IsMouseOver)
							SI=i;
						// next
						x++;
						y++;
						i++;
					}
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				}
				#endregion

				#region Markers and overlays
				try
				{
					// draw selected icon markers and more item markers
					int i=0;
					while(i<_Items.Lines.Length)
					{
						// Toggled item in line
						try
						{
							// get toggled item in line (if any)
							int Toggle=_Items.GetToggledItem(i);
							// if there is a image for the selected item and there is a selected item,
							// then draw it
							if(Toggle>=0 && Global.Configuration.Images.IconSelectedImagePath!=""
								&& _Items[Toggle].IsShown)
							{
								_Items[Toggle].Draw(_ResourceManager.IconSelectedIndicator);
							}
						}
						catch(Exception ex)
						{
							System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
						}
						// if the line needs markers that highlight the fact that there are more _Items,
						// then draw them
						if(_Items.Lines[i].ShowsMoreIndicator)
						{
							// reset the spritepainter matrixes...
							//this._ResourceManager.Sprite.End();
							//this._ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);

							int ItemsAllowedInLine;
							if(Global.Configuration.Appearance.ItemsShownPerLine==0 || _Items.Lines.Length==1)
								ItemsAllowedInLine=_Items.Lines[i].MaxVisibleItems;
							else
								ItemsAllowedInLine=Global.Configuration.Appearance.ItemsShownPerLine+1;

							// there are more _Items below
							if(!(_Items.Count(i)-_Items.Lines[i].StartIndex<ItemsAllowedInLine))
							{
								RectangleF MorePosition=this.GetMoreDownPosition(i);
								_ResourceManager.DrawScrollDownIndicator(false, MorePosition);
							}
							// there are more _Items above
							if(_Items.Lines[i].StartIndex>0)
							{
								RectangleF MorePosition=this.GetMoreUpPosition(i);
								_ResourceManager.DrawScrollUpIndicator(false, MorePosition);
							}

							_Device.Transform.World=Matrix.Identity;
							_Device.Transform.Projection=Matrix.OrthoOffCenterLH(0f, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, 0f, 2f, -1f);
							_Device.Transform.View=Matrix.Identity;
						}
						i++;
					}
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				}
				#endregion

				// reset the spritepainter matrixes...
				this._ResourceManager.Sprite.End();
				this._ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);

				#region Labels
				// draw labels
				if(Global.Configuration.Appearance.ShowLabels && Global.Configuration.Runtime.AllowLabels)
				{
					// draw if there is one selected
					if(SI>-1)
					{
						// confirm it's selected
						if(_Items[SI].IsMouseOver)
						{
							// draw HQ label if using HQ
							if(FeatureSets.UseHQLabels)
								this.DrawBakedLabel(_Items[SI]);
							else
								this.DrawLabel(_Items[SI]);
						}
					}
				}
				#endregion

				// Finalize sprite painting process
				this._ResourceManager.Sprite.End();

				#region Additional Text
				// Draw fps if this is transparent
				if(_ShowFPS)this._ResourceManager.LabelFont.DrawText(null, MaxFPS.ToString(), new Rectangle(new Point(0,0), new Size(50,20)), DrawTextFormat.NoClip, Color.Black);
				// Show debug build if debug
#if DEBUG
				int mColor;
				if((Global.Configuration.Images.BackgroundColor.R+Global.Configuration.Images.BackgroundColor.G+Global.Configuration.Images.BackgroundColor.B)/3>128)
					mColor=0;
				else
					mColor=255;
				this._ResourceManager.DebugFont.DrawText(null, "Orbit Nightly (Debug)\nBuild "+BuildInformation.BuildNumber, new Rectangle(new Point(10, _Parent.Height-40), new Size(_Parent.Width-10,40)), DrawTextFormat.Right|DrawTextFormat.NoClip, Color.FromArgb(mColor, mColor, mColor));
#endif
				#endregion

				// Finalize and present scene
				_Device.EndScene();
				_Device.Present(_Parent);

				// Update fps counter and set loop state
				FramesDrawn++;
				_State=DoNext;
			}
			catch (NullReferenceException e)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine("NULL reference caught");
				System.Diagnostics.Debug.WriteLine(e.Message);
				System.Diagnostics.Debug.WriteLine(e.Source);
				System.Diagnostics.Debug.WriteLine(e.StackTrace);
#endif
			}
			catch (DeviceLostException)
			{
				this.HandleDeviceLostExceptionWhileRendering();
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
#endif
				this.HandleGeneralRenderException();
			}
		}

		private void DrawLabel(OrbitItem item)
		{
			if(item==null)
				return;

			try
			{
				// drawing the label text
				DrawLabelText(item, item.Rectangle.X+item.Rectangle.Width, item.Rectangle.Location.Y+(item.Rectangle.Size.Height/2)-Global.Configuration.Runtime.IconSizeAverage/2+Global.Configuration.Runtime.IconSizeAverage/6);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				_Device.SetRenderTarget(0, this._ResourceManager.BackBuffer);
			}
		}
		private void BakeLabel()
		{
			// if there's no surface to bake to, then don't
			if(this._ResourceManager.Composite==null)
				return;

			// find the selected item
			int RegistryIndex=_Items.GetSelectedItem();
			// if no selected item, then no label either
			if(RegistryIndex==-1)
				return;

			// draw label
			try
			{
				// start rendering
				_Device.SetRenderTarget(0, this._ResourceManager.CompositeBuffer);
				_Device.BeginScene();
				_Device.Clear(ClearFlags.Target, Color.FromArgb(0x00, Color.Black), 0, 0);

				// drawing he label text at position 1x1
				DrawLabelText(_Items[RegistryIndex], 1, 1);

				// end of rendering
				_Device.EndScene();
				_Device.Present();
				_Device.SetRenderTarget(0, this._ResourceManager.BackBuffer);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				try
				{
					_Device.EndScene();
				}
				catch(Exception){}
				_Device.SetRenderTarget(0, this._ResourceManager.BackBuffer);
			}
		}

		private void DrawBakedLabel(OrbitItem item)
		{
			// if there's no surface to draw from, then don't
			if(this._ResourceManager.Composite==null || item==null)
				return;

			try
			{
				int CropSize=Convert.ToInt32((Global.Configuration.Fonts.DescriptionSize+Global.Configuration.Fonts.LabelSize+5)*3f/2f);
				int LabelAlpha;
				if(Global.Configuration.Appearance.IconAlpha==255)
					LabelAlpha=255;
				else
					LabelAlpha=Convert.ToInt32((float)255*((float)(item.AnimationState-Global.Configuration.Appearance.IconAlpha)/(float)(255-Global.Configuration.Appearance.IconAlpha)));
				if(LabelAlpha<0)LabelAlpha=0;
				// new draw and crop somewhere else
				// IMPORTANT: the LabelAlpha value, right now, is ignored. Make it so that this changes :)
				_ResourceManager.DrawTextureOnGenericQuad(this._ResourceManager.Composite.Texture,
															new RectangleF(new PointF((item.Rectangle.X+item.Rectangle.Width), (item.Rectangle.Y+(item.Rectangle.Size.Height/2)-Global.Configuration.Runtime.IconSizeAverage/2+Global.Configuration.Runtime.IconSizeAverage/6)), new SizeF(500, CropSize)),
															Color.FromArgb(LabelAlpha, Color.White));
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}

		#endregion

		#region Transparent Mode Specific
		private void DrawTransparent()
		{
			int off=Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Runtime.IconSizeAverage;
			/*
			 * This is the Icons drawing funcion for the
			 * Transparent mode which uses one window for all icons.
			 * It later on calls the UpdateTransparentLabels() funcion
			 * as to update labels.
			*/
			try
			{
				_State=LoopState.DrawingFrame;
				// Prepare scene and know what loopstate to enter next
				// based on the need of another animation frame or not
				LoopState DoNext=Animate();

				// start to _Device icons
				_Device.BeginScene();
				_ResourceManager.Sprite.Begin(SpriteFlags.AlphaBlend);

				// clear the background that was drawn
				_Device.Clear(ClearFlags.Target, Color.FromArgb(0x00, Color.Black), 0, 0);

				// alter the matrix to that which we want :)
				_Device.Transform.World=Matrix.Identity;
				//_Device.Transform.Projection=Matrix.OrthoOffCenterLH(0f, 1024, 768, 0f, 2f, -1f);
				_Device.Transform.Projection=Matrix.OrthoOffCenterLH(0f, this.TransparentResourceManager.ProjectionSize.Width, this.TransparentResourceManager.ProjectionSize.Height, 0f, 2f, -1f);
				_Device.Transform.View=Matrix.Identity;

				// draw icons
				int i=0;
				while(i!=_Items.Length)
				{
					try
					{
						/*_Device.RenderState.BlendFactor=_Items[i].ColorKey;
						_Device.RenderState.SourceBlend=Blend.BlendFactor;*/
						_Device.RenderState.BlendOperation=BlendOperation.Max;
						//_Device.RenderState.AlphaBlendOperation=BlendOperation.
						// draw icons on top left edge of screen for later blitting if we're going transparent
						// or draw them in place if no blitting is going to be done
						
						if(_Items[i].AnimationState>0 && _Items[i].IsShown && _Items[i].Enabled)
						{
							_Items[i].Draw(-_ScreenToDrawCenter.X+(this.AverageDockBounds.Width+off)/2, -_ScreenToDrawCenter.Y+(this.AverageDockBounds.Width+off)/2);
						}
						i++;

					}
					catch (Exception)
					{
						i++;
						_State=LoopState.Loop;
					}

				}

				// Finalize sprite painting process
				_ResourceManager.Sprite.End();
				// Draw fps if this is transparent
				if(_ShowFPS)_ResourceManager.LabelFont.DrawText(null, MaxFPS.ToString(), new Rectangle(new Point(0,0), new Size(50,20)), DrawTextFormat.NoClip, Color.Black);
				// Finalize and present scene
				_Device.EndScene();
				_Device.Present();

				// this is the REAL dock size. only moved to point(0,0).
				// Just do a _ScreenToDrawCenter.X-SurfaceCropRectangle.Width/2 to get the real dock's X position.
				Rectangle SurfaceCropRectangle=new Rectangle(new Point(0,0), new Size(AverageDockBounds.Width+(off), AverageDockBounds.Height+(off)));

				// update window
				using(Bitmap b=_TransparentResourceManager.GetCropFrontBuffer(ref SurfaceCropRectangle))
				{
					_TransparentResourceManager.DisplayForm.Update(b, new Rectangle(new Point(_MultiMonitorCenter.X-SurfaceCropRectangle.Width/2, _MultiMonitorCenter.Y-SurfaceCropRectangle.Height/2), new Size(SurfaceCropRectangle.Width, SurfaceCropRectangle.Height)));
					// TODO: use this when Center becomes multi-monitor compliant (right now it crops to the main monior) and thus abolishing the _MultiMonitorCenter var (which is same as center, but multi-monitor)
					//_TransparentResourceManager.DisplayForm.Update(b, new Rectangle(new Point(_ScreenToDrawCenter.X-SurfaceCropRectangle.Width/2, _ScreenToDrawCenter.Y-SurfaceCropRectangle.Width/2), new Size(SurfaceCropRectangle.Width, SurfaceCropRectangle.Height)));
				}

				/*
				// Diagnosing all the rectangles we had
				MessageBox.Show(
					"Center: "+Center.ToString()+
					"\nDockRect: "+AverageDockBounds.ToString()+
					"\nSurfaceCropRectangle: "+SurfaceCropRectangle.ToString()+
					"\nNewUpdateRect: "+new Rectangle(new Point(Center.X-this.AverageDockBounds.Width/2-off/2, Center.Y-this.AverageDockBounds.Height/2-off/2), new Size(SurfaceCropRectangle.Width, SurfaceCropRectangle.Height))
					);
				*/

				// Update fps counter and set loop state
				FramesDrawn++;
				_State=DoNext;
			}
			catch (DeviceLostException)
			{
				this.HandleDeviceLostExceptionWhileRendering();
			}
			catch (Exception)
			{
				this.HandleGeneralRenderException();
			}
			this.UpdateTransparentLabel();
		}

		private void UpdateTransparentLabel()
		{
			/*
			 * This function is the one which updates the labels
			 * when using transparent mode. That's all it does.
			*/
			int SI=_Items.GetSelectedItem();
			if(SI!=-1 && (Global.Configuration.Appearance.ShowLabels && Global.Configuration.Runtime.AllowLabels))
			{
				try
				{
					_State=LoopState.DrawingFrame;
					// Prepare scene and know what loopstate to enter next
					// based on the need of another animation frame or not
					LoopState DoNext=Animate();

					// start to _Device icons
					_Device.BeginScene();
					_Device.Clear(ClearFlags.Target, Color.FromArgb(0x00, Color.Black), 0, 0);
					//_ResourceManager.Sprite.Begin();

					// drawing the labels text
					DrawLabelText(_Items[SI], 1, 1);

					// Finalize sprite painting process
					//_ResourceManager.Sprite.End();
					// Finalize and present scene
					_Device.EndScene();
					_Device.Present();

					Rectangle dock=new Rectangle(new Point(0,0), new Size(512, Convert.ToInt32((Global.Configuration.Fonts.LabelSize+Global.Configuration.Fonts.DescriptionSize)*(4f/2))));
					
					// update window
					using(Bitmap b=_TransparentResourceManager.GetCropFrontBuffer(ref dock))
					{
						_TransparentResourceManager.LabelForm.Update(b, new Rectangle(new Point(Convert.ToInt32(_Items[SI].Rectangle.X+_Items[SI].Rectangle.Width+5+_ScreenToDraw.Bounds.Left), Convert.ToInt32(_Items[SI].Rectangle.Y+(_Items[SI].Rectangle.Size.Height/2)-Global.Configuration.Runtime.IconSizeAverage/3+_ScreenToDraw.Bounds.Top)), new Size(dock.Width, dock.Height)));
					}

					// Update fps counter and set loop state
					FramesDrawn++;
					_State=DoNext;
				}
				catch (DeviceLostException)
				{
					this.HandleDeviceLostException();
				}
				catch (Exception)
				{
					this.HandleGeneralRenderException();
				}
				_TransparentResourceManager.LabelForm.Show();
			} 
			else 
			{
				_TransparentResourceManager.LabelForm.Hide();
			}
		}


		private void ResizeBuffers(Size newSize)
		{
			int Offset=Global.Configuration.Appearance.IconMagnifiedSize-Global.Configuration.Runtime.IconSizeAverage;
			newSize=new Size(newSize.Width+Offset,newSize.Height+Offset);
			
			_IsPaused=true;
			FrameThreadEnabled=false;
			this._Device.SetRenderTarget(0, this.ResourceManager.BackBuffer);

			this.TransparentResourceManager.ProjectionSize=newSize;
			this.TransparentResourceManager.SetUpBlitBuffer(newSize);
			this.TransparentResourceManager.SetUpFrontBuffer(newSize);

			this._Device.SetRenderTarget(0, this.TransparentResourceManager.FrontBuffer);
			_IsPaused=false;
			FrameThreadEnabled=true;
		}
		#endregion
		#endregion

		#region FrameThread Process
		private void FrameThread_Proc()
		{
			while(true)
			{
				if(Tick!=null)Tick(this, new EventArgs());
				if(FrameThreadEnabled)
					FrameTimer_Tick();
				
				System.Threading.Thread.Sleep(5); // could be sleep 0 for ultra super duper high framerate... or not...
			}
		}
		#endregion

		#region Exception Handling
		private void HandleDeviceLostException()
		{		
			// stop everything
			Pause();
			// tell the program to re-init the device next time it's called
			_IsDeviceLost=true;
			// set the loop to do nothing
			_State=LoopState.Nothing;
			// throw event requesting item unloading
			if(DeviceLost!=null) DeviceLost(this, new EventArgs());
			// free the _Device device
			Stop();
			_State=LoopState.NotInit;
		}
		private bool HandleDeviceLostExceptionWhileRendering()
		{
			// treat Device Lost
			// TODO: find out if it's needed to throw this DeviceLost exception. It might be getting thrown twice
			HandleDeviceLostException();

			// load a new device
			if(Start()==InitializationResult.Failed)
			{
				_State=LoopState.NotInit;
				_IsDeviceLost=true;
				//this.FrameTimer.Enabled=true;
				FrameThreadEnabled=true;
				return false;
			}
			// loading...
			_State=LoopState.Loading;
			// throw event to request a registry reloading
			if(RendererReset!=null)RendererReset(this, new EventArgs());
			// start again
			Resume();
			_State=LoopState.Loop;
			_IsDeviceLost=false;
			return true;
		}
		private void HandleGeneralRenderException()
		{
			//MessageBox.Show(e.Message+"\n"+e.Source+"\n"+e.StackTrace);
			// general exception... try again.
			try{_TransparentResourceManager.FrontBuffer.UnlockRectangle();}
			catch(Exception){}
			try{_ResourceManager.Sprite.End();}
			catch(Exception){}
			try{_Device.EndScene();}
			catch(Exception){}
			try{_Device.Present();}
			catch(Exception){}
			_State=LoopState.Loop;
		}
		#endregion

		#region Event Handling
		#region Device Event Handling
		private void Device_DeviceLost(object sender, EventArgs e)
		{
			/*int coop=0;
			if(_Device.CheckCooperativeLevel(out coop))
			{
				if(((ResultCode)coop)!=ResultCode.DeviceNotReset)
					return;
			}*/
			// treat as DeviceLost
			// TODO: possibly handle device losses
			//HandleDeviceLostException();
		}
		private void Device_DeviceResizing(object sender, CancelEventArgs e)
		{
			e.Cancel=true;
			this.HandleDeviceLostExceptionWhileRendering();
			//HandleDeviceLostException();
		}
		#endregion

		#region ScreenGrabber Event Handling
		private void TS_Paint(object sender, EventArgs e)
		{
			/*if(_State==LoopState.Nothing)
				_State=LoopState.Loop;*/
			ForceRenderNextFrame();
		}
		#endregion

		#region Timers Event Handling
		private void FpsTimer_Tick(object sender, System.EventArgs e)
		{
			// draw fps
			MaxFPS=FramesDrawn;
			FramesDrawn=0;
		}

		//private void FrameTimer_Tick(object sender, System.EventArgs e)
		private void FrameTimer_Tick()
		{	
			// send Frame Tick message
			if(FrameTick!=null)FrameTick(this, new EventArgs());
			// call main loop
			MainLoop();
		}
		#endregion
		#endregion

		#region Events
		/// <summary>
		/// Occurs when the frame timer ticks
		/// </summary>
		public event EventHandler FrameTick;
		/// <summary>
		/// Occurs when the frame timer ticks (independently of being enabled or not)
		/// </summary>
		public event EventHandler Tick;
		/// <summary>
		/// Occurs when the Direct3D Device was lost while rendering. 
		/// When this happens, all _Items must be unloaded 
		/// and the window re-popped to continue rendering.
		/// Whenever this happens, a DeviceLost event has happened before, 
		/// so all you need to implement is registry re-loading and window popping
		/// </summary>
		public event EventHandler RendererReset;
		/// <summary>
		/// Occurs when the Direct3D Device is lost.
		/// When this happens, all _Items must be unloaded
		/// </summary>
		public event EventHandler DeviceLost;
		/// <summary>
		/// Occurs when the render mode changes and the window has to be resized.
		/// </summary>
		/// <remarks>When in transparent mode, it's recommended that the window be sized to 1024x768</remarks>
		public event EventHandler WindowResize;
		/// <summary>
		/// Occurs when the manager deliberately switches from 
		/// Transparent mode to Non-Transparent mode because it 
		/// failed to initialize or your video card didn't support it
		/// </summary>
		public event ForcedModeSwitchEventHandler ForcedModeSwitch;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets the centerpoint of all the loops
		/// </summary>
		public Point Center
		{
			get
			{
				return _ScreenToDrawCenter;
			}
			set
			{
				/*
				_MultiMonitorCenter=value;
				_ScreenToDraw=Screen.FromPoint(value);
				if(Global.Configuration.Appearance.Transparency==Orbit.Configuration.TransparencyMode.Real)
					_ScreenToDrawCenter=new Point(value.X-_ScreenToDraw.Bounds.Left, value.Y-_ScreenToDraw.Bounds.Top);
				else
					_ScreenToDrawCenter=new Point(value.X-_ScreenToDraw.Bounds.Left-_ScreenToDraw.WorkingArea.Left, value.Y-_ScreenToDraw.Bounds.Top-_ScreenToDraw.WorkingArea.Top);
				*/
				
				if(_ScreenToDrawCenter!=value)
				{
					//_ScreenToDraw=Screen.FromPoint(value);
					_ScreenToDrawCenter=value;
				}
				
			}
		}
		/// <summary>
		/// Gets the Multi-monitor compliant center
		/// </summary>
		public Point MultiMonitorCenter
		{
			get
			{
				return _MultiMonitorCenter;
			}
			set
			{
				if(_MultiMonitorCenter!=value)
					_MultiMonitorCenter=value;
			}
		}
		/// <summary>
		/// Gets the bounds of the dock calculated using the average icon size
		/// </summary>
		public Rectangle AverageDockBounds
		{
			get
			{
				return _AverageDockBounds;
			}
		}
		/// <summary>
		/// Gets the screen to which orbit's drawing to
		/// </summary>
		public Screen ScreenToDraw
		{
			get
			{
				return _ScreenToDraw;
			}
			set
			{
				if(_ScreenToDraw!=value)
					_ScreenToDraw=value;
			}
		}
		/// <summary>
		/// Gets the Direct3D Device associated with this renderer
		/// </summary>
		public Device Device
		{
			get
			{
				return _Device;
			}
		}
		/// <summary>
		/// Gets the OrbitResourceManager associated with this renderer
		/// </summary>
		public OrbitResourceManager ResourceManager
		{
			get
			{
				return _ResourceManager;
			}
		}
		/// <summary>
		/// Gets the TransparentResourceManager associated with this renderer
		/// </summary>
		public TransparentResourceManager TransparentResourceManager
		{
			get
			{
				return _TransparentResourceManager;
			}
		}
		/// <summary>
		/// Gets the ScreenGrabber used by this renderer
		/// </summary>
		public ScreenGrabber ScreenGrabber
		{
			get
			{
				return _ScreenGrabber;
			}
		}
		/// <summary>
		/// Gets/Sets the item registry used by the renderer
		/// </summary>
		public OrbitItemRegistry Items
		{
			get
			{
				return _Items;
			}
			set
			{
				if(_Items!=value)
					_Items=value;
			}
		}
		/// <summary>
		/// Gets/Sets the renderer state
		/// </summary>
		public LoopState State
		{
			get
			{
				return _State;
			}
			set
			{
				if(_State!=value)
					_State=value;
			}
		}
		/// <summary>
		/// Gets the state of the device
		/// </summary>
		public bool IsDeviceLost
		{
			get
			{
				return _IsDeviceLost;
			}
		}
		/// <summary>
		/// Gets/Sets if the program should display FPS onscreen
		/// </summary>
		public bool ShowFPS
		{
			get
			{
				return _ShowFPS;
			}
			set
			{
				if(_ShowFPS!=value)
					_ShowFPS=value;
			}
		}
		/// <summary>
		/// Gets the state of the engine
		/// </summary>
		public bool IsPaused
		{
			get
			{
				return _IsPaused;
			}
		}
		#endregion
	}
}
