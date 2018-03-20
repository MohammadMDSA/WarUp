using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace WarUp.Core.Logics.MapUtils
{
	public sealed class WaypointRoute : GameUtil
	{
		private HashSet<WaypointRouteNode> Nodes;

		public override void Draw(CanvasDrawingSession session)
		{
			foreach (var node in Nodes)
			{
				foreach (var neighbour in node.Neighbours)
				{
					session.DrawLine(node.Waypoint.Position, neighbour.Waypoint.Position, (node.Waypoint.IsSelected() && neighbour.Waypoint.IsSelected()) ? Colors.Red : Colors.Yellow);
				}
				node.Waypoint.Draw(session);
			}
		}

		public override Polygon2D GetSelectPolygon()
		{
			return new Polygon2D(new List<Point2D>());
		}

		public override bool IsAvailable()
		{
			return false;
		}

		public override bool IsSelected()
		{
			if (Nodes.Count < 1) return false;
			foreach (var item in Nodes)
			{
				if (!item.Waypoint.IsSelected()) return false;
			}
			return true;
		}

		public override bool Select()
		{
			var res = true;
			foreach (var item in Nodes)
			{
				res &= item.Waypoint.Select();
			}
			return res;
		}

		public override bool ShouldBeDrawn()
		{
			return true;
		}

		public override bool Unselect()
		{
			var res = true;
			foreach (var item in Nodes)
			{
				res &= item.Waypoint.Unselect();
			}
			return res;
		}

		public override void Update()
		{
		}
	}

	/// <summary>
	/// Represents a waypoint node in waypoint route
	/// </summary>
	class WaypointRouteNode
	{
		/// <summary>
		/// Waypoint being represented by node
		/// </summary>
		public Waypoint Waypoint { get; }

		/// <summary>
		/// Set of this node's neighbours
		/// </summary>
		public HashSet<WaypointRouteNode> Neighbours { get; }

		public WaypointRouteNode(Waypoint waypoint)
		{
			this.Waypoint = waypoint;
			this.Neighbours = new HashSet<WaypointRouteNode>();
		}
	}

}
