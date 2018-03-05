using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using WarUp.Core;
using WarUp.Core.Graphics;
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
	public sealed partial class MainPage : Page, IRenderable
	{
		private CanvasSwapChain SwapChain;

		public MainPage()
		{
			this.InitializeComponent();

		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			MainCore.Core.RenderDevice = this;
		}

		private void CreateSwapChain()
		{
			CanvasSwapChain ss = new CanvasSwapChain(Super, this.Frame.DesiredSize);
			
			SwapChainCanvas.SwapChain = ss;
		}

		public void Render()
		{
			Ensurement();

			using (var ds = SwapChain.CreateDrawingSession(Colors.Black))
			{
				ds.DrawLine(new Vector2(0, 0), new Vector2(100, 100), Colors.Red);
			}

			SwapChain.Present();
		}
		
		public void Ensurement()
		{
			if (SwapChain == null)
			{
				CreateSwapChain();
			}
		}
	}
}
