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

namespace WarUp.Logic.Editor.Input.Mouse.Functions
{
	public class MouseSelectHandler : BaseMouseFunction
    {
		private FrameworkObject ActivePressedObject;
		private bool Dragging;
		private Vector2 LastLeftPressPoint;
		private Vector2 LastRightPressPoint;

		public MouseSelectHandler(EditorMouse mouse) : base(mouse)
		{
			Reset();
		}

		public override void Reset()
		{
			Mouse.Editor.SelectionRect = new Rect();
			this.ActivePressedObject = null;
			Dragging = false;
			LastLeftPressPoint = Vector2.Zero;
			LastRightPressPoint = Vector2.Zero;
			Mouse.ClearSelectedObjects();
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
				return;
			}


			// Drag select
			if (Mouse.IsLeftPressed && !Dragging)
			{
				Mouse.ClearSelectedObjects();

				var newRect = new Rect(currentPosition.ToPoint(), LastLeftPressPoint.ToPoint());
				Mouse.Editor.SelectionRect = newRect;

				var list = new List<Point2D>();
				list.Add(new Point2D(newRect.Left, newRect.Bottom));
				list.Add(new Point2D(newRect.Right, newRect.Bottom));
				list.Add(new Point2D(newRect.Right, newRect.Top));
				list.Add(new Point2D(newRect.Left, newRect.Top));
				var RectPolygon = new Polygon2D(list);

				foreach (var item in Mouse.Storage.GetFrameworkObjects())
				{
					if (Polygon2D.ArePolygonVerticesColliding(RectPolygon, item.GetSelectPolygon()))
					{
						Mouse.AddSelected(item);
					}
				}
			}
		}

		public override void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			var prop = e.GetCurrentPoint(sender).Properties;

			if (!Mouse.IsLeftPressed && prop.IsLeftButtonPressed)
				LastLeftPressPoint = Mouse.Position;
			if (!Mouse.IsRightPressed && prop.IsRightButtonPressed)
			{
				LastRightPressPoint = Mouse.Position;
				return;
			}


			Dragging = false;

			var objects = Mouse.Storage.GetFrameworkObjects();
			Point2D point = new Point2D(Mouse.Position.X, Mouse.Position.Y);

			// Find the object under pointer
			foreach (var item in objects)
			{
				var p = item.GetSelectPolygon();
				if (Polygon2D.IsPointInPolygon(point, p))
				{
					ActivePressedObject = item;
					break;
				}
			}

			// Mouse in on no object
			if (ActivePressedObject == null)
			{
				Mouse.ClearSelectedObjects();
				return;
			}

			// Mouse is on an unselected object
			if (!Mouse.GetSelected().Contains(ActivePressedObject))
			{
				Mouse.ClearSelectedObjects();
				Mouse.AddSelected(ActivePressedObject);

			}

			Dragging = true;
			Mouse.Editor.SelectionRect = new Rect(Mouse.Position.X, Mouse.Position.Y, 0, 0);
		}

		public override void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			ActivePressedObject = null;
			Dragging = false;

			Mouse.Editor.SelectionRect = new Rect();

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
			
		}

		private void RightClick(UIElement sender, PointerRoutedEventArgs e)
		{

		}
	}
}
