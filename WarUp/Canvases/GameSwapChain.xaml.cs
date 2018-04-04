using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WarUp.Utils.Mouse;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace WarUp.Canvases
{
	public sealed partial class GameSwapChain : UserControl
	{
		public Mouse Mouse { get; set; }
		public CanvasSwapChain SwapChain { get => SwapChainPanel.SwapChain; set => SwapChainPanel.SwapChain = value; }

		public GameSwapChain()
		{
			this.InitializeComponent();
		}

		private void SwapChainPanel_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			Mouse.PointerPressed(sender as UIElement, e);
		}

		private void SwapChainPanel_PointerReleased(object sender, PointerRoutedEventArgs e)
		{
			Mouse.PointerReleased(sender as UIElement, e);
		}

		private void SwapChainPanel_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
		{
			Mouse.WheelChanged(sender as UIElement, e);
		}

		private void SwapChainPanel_PointerMoved(object sender, PointerRoutedEventArgs e)
		{
			Mouse.Moved(sender as UIElement, e);
		}

	}
}
