using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WarUp.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

		public GamePage()
		{
			this.InitializeComponent();

			WindowVisible = true;
			WindowClosed = false;

			
			this.SwapChainPanel.SwapChain = new CanvasSwapChain(new CanvasDevice(), 500, 500, CanvasControlPanel.Dpi);

			MainCore = new MainCore(SwapChainPanel.SwapChain);

			Run();
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
		}
	}
}
