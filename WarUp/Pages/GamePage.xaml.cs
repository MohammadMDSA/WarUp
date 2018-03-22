using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WarUp.Core;
using WarUp.GraphicEngine;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarUp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class GamePage : Page
	{
		private bool WindowVisible;
		private bool WindowClosed;

		private MainCore MainCore;
		SwapChainManager SwapChainManager;

		public GamePage()
		{
			this.InitializeComponent();

			WindowVisible = false;
			WindowClosed = false;

			SwapChainManager = new SwapChainManager(new CanvasDevice());

			this.SwapChainPanel.SwapChain = SwapChainManager.SwapChain; 

			MainCore = new MainCore(SwapChainManager);
			Task.Run(new Action(Run));
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var bound = Window.Current.CoreWindow.Bounds;
			SwapChainManager.RenderSize = new Size(bound.Width, bound.Height);
			WindowVisible = true;
		}

		public void Run()
		{
			while (!WindowClosed)
			{
				if (WindowVisible)
				{
					MainCore.Tick();
				}
				else
				{

				}
			}
		}

		private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			SwapChainManager.RenderSize = e.NewSize;
		}
	}
}
