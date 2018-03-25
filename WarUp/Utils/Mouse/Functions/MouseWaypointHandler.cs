using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.MapUtils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Utils.Mouse.Functions
{
	public class MouseWaypointHandler : BaseMouseFunction
	{
		private Waypoint ActivePressedWaypoint;
		private bool Expanding;
		private Waypoint NewWaypoint;

		public MouseWaypointHandler(Mouse mouse) : base(mouse)
		{
			Reset();
		}

		public override void Reset()
		{
			ActivePressedWaypoint = null;
			Expanding = false;
			NewWaypoint = null;
		}

		public override void Moved(UIElement sender, PointerRoutedEventArgs e)
		{
			if (!Mouse.IsLeftPressed)
				return;

			var contactRect = e.GetCurrentPoint(sender).Properties.ContactRect;
			var currentPosition = new Vector2((float)contactRect.X, (float)contactRect.Y);

			var delta = currentPosition - Mouse.Position;

			NewWaypoint.Position += delta;
		}

		public override void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			Expanding = false;

			var prop = e.GetCurrentPoint(sender).Properties;

			if (!Mouse.IsRightPressed && prop.IsRightButtonPressed)
			{
				return;
			}

			var objects = Mouse.Storage.GetWaypoints();
			Point2D point = new Point2D(Mouse.Position.X, Mouse.Position.Y);

			// Find out if mouse is on a waypoint
			foreach (var item in objects)
			{
				if (Polygon2D.IsPointInPolygon(point, item.GetSelectPolygon()))
				{
					ActivePressedWaypoint = item;
					Expanding = true;
					break;
				}
			}

			NewWaypoint = new Waypoint(Mouse.Position);


			if (Expanding)
			{
				var route = ActivePressedWaypoint.ParentRoute;
				Mouse.Storage.AddObject(NewWaypoint);
				route.AddWaypoint(ActivePressedWaypoint, NewWaypoint);

				return;
			}

			// Not expandig
			var newWaypoint = new Waypoint(Mouse.Position);
			Mouse.Storage.AddObject(newWaypoint);
			var newRoute = new WaypointRoute(newWaypoint);
			Mouse.Storage.AddObject(newRoute);
			Mouse.Storage.AddObject(NewWaypoint);

			newRoute.AddWaypoint(newWaypoint, NewWaypoint);


		}

		public override void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			ActivePressedWaypoint = null;
			Expanding = false;
			NewWaypoint = null;
		}

		public override void WheelChanged(UIElement sender, PointerRoutedEventArgs e)
		{

		}
	}
}
