using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using WarUp.Core.Graphics;
using WarUp.Core.Logics.MapUtils;
using WarUp.Core.Logics.Models.Ability;
using WarUp.Core.Logics.Models.Instructions;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Shapes;

namespace WarUp.Core.Logics.Models
{
	class GreenTile : GameObject, IMovable
	{
		private bool Selected;
		private float Speed;

		public GreenTile()
		{
			Position = new Vector2(0f);
			Selected = false;
			Speed = 5;
			Size = new Vector2(10f);
		}

		public override void Draw(CanvasDrawingSession session)
		{
			session.FillRectangle(new Rect((Position - Size / 2).ToPoint(), Size.ToSize()), Colors.Green);
			session.DrawText(Position.X + " " + Position.Y, 500, 500, Colors.Azure);

		}

		public override void Update()
		{
			
			InstructionSet current;

			if (!GetFirstEnableInstruction(out current)) return;
			current.Tick();
		}

		public override bool IsSelected() => Selected;

		public override Polygon2D GetSelectPolygon()
		{
			var list = new List<Point2D>();
			list.Add(new Point2D(Position.X, Position.Y));
			list.Add(new Point2D(Position.X + 10, Position.Y));
			list.Add(new Point2D(Position.X + 10, Position.Y + 10));
			list.Add(new Point2D(Position.X, Position.Y + 10));
			return new Polygon2D(list);
		}

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

		public override bool IsAvailable()
		{
			return true;
		}
		
		public float GetSpeed() => this.Speed;

		public void Move(Vector2 destination)
		{
			throw new NotImplementedException();
		}

		public void Move(Waypoint destination)
		{
			throw new NotImplementedException();
		}

		public void Move(WaypointRoute path)
		{
			throw new NotImplementedException();
		}

		public override bool ShouldBeDrawn()
		{
			return true;
		}

		public override Vector2 GetSize()
		{
			return Size;
		}
	}
}
