using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using Windows.UI;

namespace WarUp.Core.Logics.Models
{
	class GreenTile : IFrameworkObject
	{
		private int x;
		private int y;
		private Direction Direction;

		public GreenTile()
		{
			x = 0;
			y = 0;
			this.Direction = Direction.Right;
		}

		public void Draw(CanvasDrawingSession session)
		{
			session.FillRectangle(new Rect(x, y, 10, 10), Colors.Green);
		}

		public void Update()
		{
			switch (this.Direction)
			{
				case Direction.Up:
					if (x < 11)
						Direction = Direction.Right;
					x -= 10;
					break;
				case Direction.Down:
					if (x > 89)
						Direction = Direction.Left;
					x += 10;
					break;
				case Direction.Left:
					if (y < 11)
						Direction = Direction.Up;
					y -= 10;
					break;
				case Direction.Right:
					if (y > 89)
						Direction = Direction.Down;
					y += 10;
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
