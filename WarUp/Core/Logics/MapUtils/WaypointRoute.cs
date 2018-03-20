using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;

namespace WarUp.Core.Logics.MapUtils
{
	public sealed class WaypointRoute : GameUtil
	{
		public override void Draw(CanvasDrawingSession session)
		{
			throw new NotImplementedException();
		}

		public override Polygon2D GetSelectPolygon()
		{
			throw new NotImplementedException();
		}

		public override bool IsAvailable()
		{
			throw new NotImplementedException();
		}

		public override bool IsSelected()
		{
			throw new NotImplementedException();
		}

		public override bool Select()
		{
			throw new NotImplementedException();
		}

		public override bool Unselect()
		{
			throw new NotImplementedException();
		}

		public override void Update()
		{
			throw new NotImplementedException();
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
		public HashSet<Waypoint> Neighbours { get; }

		public WaypointRouteNode(Waypoint waypoint)
		{
			this.Waypoint = waypoint;
			this.Neighbours = new HashSet<Waypoint>();
		}
	}

}
