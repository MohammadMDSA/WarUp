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
		private Direction Direction;
		private bool Selected;
		private float Speed;

		public GreenTile()
		{
			this.Direction = Direction.Right;
			Position = new Vector2(0f);
			Selected = false;
			Speed = 5;
		}

		public override void Draw(CanvasDrawingSession session)
		{
			session.FillRectangle(Position.X, Position.Y, 10, 10, Colors.Green);
			session.DrawText(Position.X + " " + Position.Y, 500, 500, Colors.Azure);

		}

		public override void Update()
		{
			//switch (this.Direction)
			//{
			//	case Direction.Up:
			//		if (Position.Y < 11)
			//			Direction = Direction.Right;
			//		Position.Y -= 10f;
			//		break;
			//	case Direction.Down:
			//		if (Position.Y > 89)
			//			Direction = Direction.Left;
			//		Position.Y += 10;
			//		break;
			//	case Direction.Left:
			//		if (Position.X < 11)
			//			Direction = Direction.Up;
			//		Position.X -= 10;
			//		break;
			//	case Direction.Right:
			//		if (Position.X > 89)
			//			Direction = Direction.Down;
			//		Position.X += 10;
			//		break;
			//}
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
	}

	enum Direction
	{
		Up = 0,
		Down,
		Left,
		Right
	}
}
