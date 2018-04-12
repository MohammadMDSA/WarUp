using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WarUp.Core.Storage;
using WarUp.Logic.Editor.Input.Keyboard;
using WarUp.Logic.Editor.Input.Mouse;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
	public sealed partial class EditorCanvas : UserControl
	{
		public EditorMouse Mouse { get; set; }
		public EditorKeyboard Keyboard { get; set; }

		public StorageCore Storage { get; set; }
		public Rect SelectionRect { get; set; }

		public EditorCanvas()
		{
			this.InitializeComponent();

			SelectionRect = new Rect();
		}

		private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
		{
			var ds = args.DrawingSession;
			ds.Clear(Colors.Black);

			var drawables = Storage.GetDrawables();

			foreach (var item in drawables)
			{
				if (item.ShouldBeDrawn())
					item.Draw(ds);
			}

			ds.FillRectangle(SelectionRect, Color.FromArgb(127, 0, 127, 255));
			ds.DrawRectangle(SelectionRect, Colors.Blue);
		}

		private void SwapChainPanel_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			Mouse.PointerPressed(sender as UIElement, e);

			ReDraw();
		}

		private void SwapChainPanel_PointerReleased(object sender, PointerRoutedEventArgs e)
		{
			Mouse.PointerReleased(sender as UIElement, e);

			ReDraw();
		}

		private void SwapChainPanel_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
		{
			Mouse.WheelChanged(sender as UIElement, e);

			ReDraw();
		}

		private void SwapChainPanel_PointerMoved(object sender, PointerRoutedEventArgs e)
		{
			Mouse.Moved(sender as UIElement, e);

			ReDraw();
		}

		public void ReDraw()
		{
			ReDraw();
		}

		private void Canvas_KeyDown(object sender, KeyRoutedEventArgs e)
		{

			ReDraw();
		}

		private void Canvas_KeyUp(object sender, KeyRoutedEventArgs e)
		{

			ReDraw();
		}
	}
}
