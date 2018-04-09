using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Shapes;

namespace WarUp.Core.Logics.MapUtils
{
	/// <summary>
	/// Represents a point on map or a point of a route
	/// </summary>
	[Serializable]
	public sealed class Waypoint : GameUtil
	{
		/// <summary>
		/// Tells if waypoint is selected or not
		/// </summary>
		private bool Selected;

		public WaypointRoute ParentRoute { get; set; }

		public Waypoint(Vector2 position)
		{
			this.Position = position;
			this.ParentRoute = null;
			Size = new Vector2(10f);
		}

		public override void Draw(CanvasDrawingSession session)
		{
			session.DrawEllipse(Position, 5, 5, Selected ? Colors.Red : Colors.Yellow);

		}

		public override Polygon2D GetSelectPolygon()
		{
			var list = new List<Point2D>();
			for (int i = 0; i < 50; i++)
			{
				var sin = Math.Sin((Math.PI * i * 2) / 50);
				var cos = Math.Cos((Math.PI * i * 2) / 50);
				list.Add(new Point2D(Position.X + cos * 15, Position.Y + sin * 15));
			}
			return new Polygon2D(list);
		}

		public override bool IsAvailable()
		{
			return true;
		}

		public override bool IsSelected() => Selected;

		public override bool Select()
		{
			Selected = true;
			return true;
		}

		public override bool Unselect()
		{
			Selected = false;
			return true;
		}

		public override void Update()
		{
		}

		public override bool ShouldBeDrawn()
		{
			return ParentRoute == null ? true : false;
		}

		public override Vector2 GetSize()
		{
			return this.Size;
		}
		
		public override Rect GetBound()
		{
			return new Rect(Position.X, Position.Y, 5, 5);
		}
	}
}
