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
		private WaypointRoute ActiveWaypointRoute;
		private Waypoint LastOnTarget;
		private bool IsNewAdded;

		public MouseWaypointHandler(Mouse mouse) : base(mouse)
		{
			Reset();
		}

		public override void Reset()
		{
			ActivePressedWaypoint = null;
			Expanding = false;
			NewWaypoint = null;
			ActiveWaypointRoute = null;
			LastOnTarget = null;
			IsNewAdded = false;
		}

		public override void Moved(UIElement sender, PointerRoutedEventArgs e)
		{
			if (!Mouse.IsLeftPressed)
				return;
			
			// Find out mouse is on a waypoint
			var objects = Mouse.Storage.GetWaypoints();
			Point2D point = new Point2D(Mouse.Position.X, Mouse.Position.Y);
			Waypoint pointerOnWaypoint = null;
			foreach (var item in objects)
			{
				if (Polygon2D.IsPointInPolygon(point, item.GetSelectPolygon()))
				{
					pointerOnWaypoint = item;
					break;
				}
			}




			if (pointerOnWaypoint != null)
			{
				// Remove new waypoint
				RemoveFromActiveRoute(NewWaypoint, true);
				IsNewAdded = false;

				// Finish job if active waypoint is the first waypoint to put on click operation
				if (pointerOnWaypoint == ActivePressedWaypoint)
				{
					RemoveFromActiveRoute(LastOnTarget, false);
					return;
				}

				// Destination alreadywaypoint exist in game

				// Check if new target waypoint is the same with last registered
				if (pointerOnWaypoint == LastOnTarget)
				{
					// Add to route if it doesn't already exist in route
					if (ActiveWaypointRoute != LastOnTarget.ParentRoute)
						ActiveWaypointRoute.AddWaypoint(ActivePressedWaypoint, LastOnTarget);
					return;
				}
				else
				{
					RemoveFromActiveRoute(LastOnTarget, false);
					LastOnTarget = pointerOnWaypoint;
					if (ActiveWaypointRoute != LastOnTarget.ParentRoute)
						ActiveWaypointRoute.AddWaypoint(ActivePressedWaypoint, LastOnTarget);
					return;
				}

			}

			// Remove existing ontarget
			RemoveFromActiveRoute(LastOnTarget, false);

			// Add a new waypoint
			if (!IsNewAdded)
			{
				IsNewAdded = true;
				Mouse.Storage.AddObject(NewWaypoint);
				ActiveWaypointRoute.AddWaypoint(ActivePressedWaypoint, NewWaypoint);
			}



			NewWaypoint.Position = Mouse.Position;
		}

		public override void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			IsNewAdded = false;
			LastOnTarget = null;
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
				ActiveWaypointRoute = ActivePressedWaypoint.ParentRoute;
				return;
			}

			// Not expandig
			ActivePressedWaypoint = new Waypoint(Mouse.Position);
			Mouse.Storage.AddObject(ActivePressedWaypoint);
			ActiveWaypointRoute = new WaypointRoute(ActivePressedWaypoint);
			Mouse.Storage.AddObject(ActiveWaypointRoute);
		}

		public override void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			ActivePressedWaypoint = null;
			Expanding = false;
			NewWaypoint = null;
			LastOnTarget = null;
			ActiveWaypointRoute = null;
			IsNewAdded = false;
		}

		public override void WheelChanged(UIElement sender, PointerRoutedEventArgs e)
		{

		}

		private void RemoveFromActiveRoute(Waypoint waypoint, bool removeFromGame)
		{
			if (waypoint == null)
				return;

			if (waypoint.ParentRoute == ActiveWaypointRoute)
			{
				ActiveWaypointRoute.RemoveWaypoint(waypoint);
			}

			if (removeFromGame)
				Mouse.Storage.RemoveObject(waypoint);
		}
	}
}
