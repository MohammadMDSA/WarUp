using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
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
	public sealed partial class MainPage : Page
	{
		private CanvasSwapChain SwapChain;

		public MainPage()
		{
			this.InitializeComponent();

		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			CanvasSwapChainPanel p;
		}

		private void CreateSwapChain()
		{
			CanvasSwapChain ss = new CanvasSwapChain(Super, this.Frame.DesiredSize);
			
			SwapChainCanvas.SwapChain = ss;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (SwapChainCanvas.SwapChain == null)
				CreateSwapChain();
			SwapChain = SwapChainCanvas.SwapChain;
			using (var ds = this.SwapChain.CreateDrawingSession(Colors.Red))
			{
				ds.FillCircle(new Vector2(50, 50), 20, Colors.Black);
			}
			SwapChain.Present();
		}
	}
}
