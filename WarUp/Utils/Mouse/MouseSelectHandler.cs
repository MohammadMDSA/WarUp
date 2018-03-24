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
		private bool Dragging;
		private Vector2 LastLeftPressPoint;
		private Vector2 LastRightPressPoint;

		public MouseSelectHandler(Mouse mouse) : base(mouse)
		{
			this.ActivePressedObject = null;
			Dragging = false;
			LastLeftPressPoint = Vector2.Zero;
			LastRightPressPoint = Vector2.Zero;
		}

		public override void Moved(UIElement sender, PointerRoutedEventArgs e)
		{
			var contactRect = e.GetCurrentPoint(sender).Properties.ContactRect;
			var currentPosition = new Vector2((float)contactRect.X, (float)contactRect.Y);

			var delta = currentPosition - Mouse.Position;

			if (Dragging)
			{
				var objects = Mouse.GetSelected();

				foreach (var item in objects)
				{
					item.Position += delta;
				}
			}

			if (Mouse.IsLeftPressed)
			{
				var selectionRect = Mouse.RenderManager.SelectionRect;

				Mouse.RenderManager.SelectionRect = new Rect(currentPosition.ToPoint(), LastLeftPressPoint.ToPoint());
			}
		}

		public override void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			var prop = e.GetCurrentPoint(sender).Properties;

			if (!Mouse.IsLeftPressed && prop.IsLeftButtonPressed)
				LastLeftPressPoint = Mouse.Position;
			if (!Mouse.IsRightPressed && prop.IsRightButtonPressed)
				LastRightPressPoint = Mouse.Position;


			Dragging = false;

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

			if (ActivePressedObject == null)
			{
				Mouse.ClearSelectedObjects();
				return;
			}

			if (!Mouse.GetSelected().Contains(ActivePressedObject))
			{
				Mouse.ClearSelectedObjects();
				Mouse.AddSelected(ActivePressedObject);

			}

			Dragging = true;
			Mouse.RenderManager.SelectionRect = new Rect(Mouse.Position.X, Mouse.Position.Y, 0, 0);
		}

		public override void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			ActivePressedObject = null;
			Dragging = false;

			Mouse.RenderManager.SelectionRect = new Rect();

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
