using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

namespace WarUp.Core.Logics.Models
{
	class GreenTile : IFrameworkObject
	{
		int x;
		int y;

		public void Draw(CanvasDrawingSession session)
		{
			throw new NotImplementedException();
		}

		public void Update()
		{
			throw new NotImplementedException();
		}

		enum Direction
		{
			Up = 0,
			Down,
			Left,
			Right
		}
	}
}
