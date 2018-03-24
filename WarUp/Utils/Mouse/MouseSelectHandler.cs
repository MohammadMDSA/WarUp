using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.Models;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Utils.Mouse
{
	public class MouseSelectHandler : BaseMouseFunction
    {
		private FrameworkObject ActivePressedObject;

		public MouseSelectHandler(Mouse mouse) : base(mouse)
		{
			this.ActivePressedObject = null;
		}

		public override void Moved(UIElement sender, PointerRoutedEventArgs e)
		{
		}

		public override void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			var objects = Mouse.Storage.GetFrameworkObjects();
			Point2D point = new Point2D(Mouse.Position.X, Mouse.Position.Y);

			foreach (var item in objects)
			{
				var p = item.GetSelectPolygon();
				if (Polygon2D.IsPointInPolygon(point, p))
				{
					ActivePressedObject = item;
					break;
				}
			}
		}

		public override void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			var prop = e.GetCurrentPoint(sender).Properties;
			if (Mouse.IsLeftPressed && !prop.IsLeftButtonPressed)
				LeftClick(sender, e);
			if (Mouse.IsRightPressed && !prop.IsRightButtonPressed)
				RightClick(sender, e);
		}

		public override void WheelChanged(UIElement sender, PointerRoutedEventArgs e)
		{
		}

		private void LeftClick(UIElement sender, PointerRoutedEventArgs e)
		{
			//var tile = new GreenTile();
			//var rect = e.GetCurrentPoint(sender).Properties.ContactRect;
			//tile.Position = new Vector2((float)rect.X, (float)rect.Y);

			//Mouse.Storage.AddObject(tile);
		}

		private void RightClick(UIElement sender, PointerRoutedEventArgs e)
		{

		}
	}
}
