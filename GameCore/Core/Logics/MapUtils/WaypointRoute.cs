﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using Windows.UI;

namespace WarUp.Core.Logics.MapUtils
{
	[Serializable]
	public sealed class WaypointRoute : GameUtil
	{
		private HashSet<WaypointRouteNode> Nodes;

		public WaypointRoute(Waypoint start)
		{
			this.Nodes = new HashSet<WaypointRouteNode>();
			this.Nodes.Add(new WaypointRouteNode(start));
			start.ParentRoute = this;
		}

		public override void Draw(CanvasDrawingSession session)
		{
			Vector2 leftArrow = new Vector2(4, 8);
			Vector2 rightArrow = new Vector2(-4, 8);
			double rad;

			foreach (var node in Nodes)
			{
				foreach (var neighbour in node.Neighbours)
				{
					var color = (node.Waypoint.IsSelected() && neighbour.Waypoint.IsSelected()) ? Colors.Red : Colors.Yellow;
					session.DrawLine(node.Waypoint.Position, neighbour.Waypoint.Position, color);
					Vector2 delta = neighbour.Waypoint.Position - node.Waypoint.Position;

					rad = Math.Atan(delta.Y / delta.X);
					double sin = -(delta.Y * 1d) / delta.Length();
					double cos = -(delta.X * 1d) / delta.Length();

					Vector2 left = new Vector2((float)(-leftArrow.X * sin + leftArrow.Y * cos), (float)(leftArrow.X * cos + leftArrow.Y * sin));
					Vector2 right = new Vector2((float)(-rightArrow.X * sin + rightArrow.Y * cos), (float)(rightArrow.X * cos + rightArrow.Y * sin));

					session.DrawLine(neighbour.Waypoint.Position, neighbour.Waypoint.Position + left, color);
					session.DrawLine(neighbour.Waypoint.Position, neighbour.Waypoint.Position + right, color);
				}
				node.Waypoint.Draw(session);
			}
		}

		public override Polygon2D GetSelectPolygon()
		{
			var l = new List<Point2D>();
			l.Add(new Point2D());
			l.Add(new Point2D());

			return new Polygon2D(l);
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

		public void AddWaypoint(Waypoint parent, Waypoint newWaypoint)
		{
			var newNode = new WaypointRouteNode(newWaypoint);
			foreach (var item in Nodes)
			{
				if (item.Waypoint == parent)
				{
					item.Neighbours.Add(newNode);
					break;
				}
			}
			this.Nodes.Add(newNode);
			newWaypoint.ParentRoute = this;
		}
		
		public override Vector2 GetSize()
		{
			return Vector2.Zero;
		}

		public IEnumerable<Waypoint> GetWaypoints()
		{
			List<Waypoint> waypoints = new List<Waypoint>();
			foreach (var node in Nodes)
			{
				waypoints.Add(node.Waypoint);
			}

			return waypoints;
		}

		public IEnumerable<Waypoint> GetNeighboursOf(Waypoint waypoint)
		{
			WaypointRouteNode tarNode = null;
			foreach (var node in Nodes)
			{
				if (waypoint == node.Waypoint)
				{
					tarNode = node;
					break;
				}
			}

			if (tarNode == null)
			{
				throw new Exception("Waypoint is not in the route");
			}

			List<Waypoint> waypoints = new List<Waypoint>();

			foreach (var item in tarNode.Neighbours)
			{
				waypoints.Add(item.Waypoint);
			}

			return waypoints;
		}

		public override Rect GetBound()
		{
			return new Rect();
		}

		public bool RemoveWaypoint(Waypoint waypoint)
		{
			if (waypoint.ParentRoute != this)
			{
				return false;
			}

			WaypointRouteNode node = null;
			foreach (var item in Nodes)
			{
				if (item.Waypoint == waypoint)
				{
					node = item;
					break;
				}
			}

			foreach (var item in Nodes)
			{
				item.Neighbours.Remove(node);
			}

			Nodes.Remove(node);

			waypoint.ParentRoute = null;
			return true;
		}

		public override bool RequiresName()
		{
			return true;
		}

		public override string SuggestName(IEnumerable<string> existings)
		{
			string prefix = "Waypoint Route ";
			int postfix = 1;
			while(existings.Contains(prefix + postfix))
			{
				postfix++;
			}

			return prefix + postfix;
		}
	}

	/// <summary>
	/// Represents a waypoint node in waypoint route
	/// </summary>
	[Serializable]
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

		public override bool Equals(object obj)
		{
			if (!(obj is WaypointRouteNode))
				return false;
			if (((WaypointRouteNode)obj).Waypoint == this.Waypoint)
				return true;
			return false;
		}

		public override int GetHashCode()
		{
			return (int)(Waypoint.Position.X * 13 + Waypoint.Position.Y * 2);
		}
	}

}
