using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using WarUp.Core.Graphics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Shapes;

namespace WarUp.Core.Logics.Models
{
	class GreenTile : FrameworkObject, IDrawable
	{
		private Direction Direction;
        private Vector2 Position;

		public GreenTile()
		{
			this.Direction = Direction.Right;
            Position = new Vector2(0f);
        }

		public override void Draw(CanvasDrawingSession session)
		{
			session.FillRectangle(Position.X, Position.Y, 10, 10, Colors.Green);
            session.DrawText(Position.X + " " + Position.Y, 500, 500, Colors.Azure);
		}

		public override void Update()
		{
            switch (this.Direction)
            {
                case Direction.Up:
                    if (Position.Y < 11)
                        Direction = Direction.Right;
                    Position.Y -= 10;
                    break;
                case Direction.Down:
                    if (Position.Y > 89)
                        Direction = Direction.Left;
                    Position.Y += 10;
                    break;
                case Direction.Left:
                    if (Position.X < 11)
                        Direction = Direction.Up;
                    Position.X -= 10;
                    break;
                case Direction.Right:
                    if (Position.X > 89)
                        Direction = Direction.Down;
                    Position.X += 10;
                    break;
            }
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
