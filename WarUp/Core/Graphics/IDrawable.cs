using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace WarUp.Core.Graphics
{
	interface IDrawable
	{
		/// <summary>
		/// Draws an object on canvas
		/// </summary>
		/// <param name="session">Canvas session of the drawing</param>
		void Draw(CanvasDrawingSession session);

		/// <summary>
		/// Determines if object should be drawn
		/// </summary>
		/// <returns>Returns true if it should be drawn</returns>
		bool ShouldBeDrawn();

		/// <summary>
		/// Size of object being drawn
		/// </summary>
		/// <returns>Object size</returns>
		Vector2 GetSize();
	}
}
