using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WarUp.Core;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.UI.Core;

namespace WarUp
{
	class App : IFrameworkView
	{
		private bool WindowVisible;
		private bool WindowClosed;

		private MainCore MainCore;

		public App()
		{
			WindowVisible = true;
			WindowClosed = false;
		}

		public void Initialize(CoreApplicationView applicationView)
		{
			applicationView.Activated += ApplicationView_Activated;
			CoreApplication.Suspending += CoreApplication_Suspending;
			CoreApplication.Resuming += CoreApplication_Resuming;
			
		}

		public void SetWindow(CoreWindow window)
		{
			window.Closed += Window_Closed;
			window.SizeChanged += Window_SizeChanged;
			window.VisibilityChanged += Window_VisibilityChanged;

			var displayInfo = DisplayInformation.GetForCurrentView();
			displayInfo.OrientationChanged += DisplayInfo_OrientationChanged;
			displayInfo.DpiChanged += DisplayInfo_DpiChanged;
			DisplayInformation.DisplayContentsInvalidated += DisplayInformation_DisplayContentsInvalidated;

			MainCore = new MainCore(window);
		}

		public void Load(string entryPoint)
		{
		}

		public void Run()
		{
			while (!WindowClosed)
			{
				if (WindowVisible)
				{
					CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
					MainCore.Tick();
				}
				else
				{
					CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessOneAndAllPending);
				}
			}
		}

		public void Uninitialize()
		{
		}

		private void ApplicationView_Activated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args)
		{
			CoreWindow.GetForCurrentThread().Activate();
		}

		private void CoreApplication_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
		{
			SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

			Task.Run(new Action(() =>
			{
				MainCore.Suspend();
				deferral.Complete();
			}));
		}

		private void CoreApplication_Resuming(object sender, object e)
		{
			
		}

		private void Window_Closed(CoreWindow sender, CoreWindowEventArgs args)
		{
			this.WindowClosed = true;
		}

		private void Window_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
		{
			args.Handled = false;
		}

		private void Window_VisibilityChanged(CoreWindow sender, VisibilityChangedEventArgs args)
		{
			WindowVisible = args.Visible;
		}

		private void DisplayInfo_OrientationChanged(DisplayInformation sender, object args)
		{
			throw new NotImplementedException();
		}

		private void DisplayInfo_DpiChanged(DisplayInformation sender, object args)
		{
			throw new NotImplementedException();
		}

		private void DisplayInformation_DisplayContentsInvalidated(DisplayInformation sender, object args)
		{
			throw new NotImplementedException();
		}

	}

	sealed class AppSource : IFrameworkViewSource
	{
		public IFrameworkView CreateView()
		{
			return new App();
		}

		public static void Main(string[] args)
		{
			CoreApplication.Run(new AppSource());
		}
	}
}
