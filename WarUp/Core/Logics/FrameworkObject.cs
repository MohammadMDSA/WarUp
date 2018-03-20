using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using WarUp.Core.Graphics;

namespace WarUp.Core.Logics.Models
{

	/// <summary>
	/// Represents a framework object
	/// </summary>
    public abstract class FrameworkObject : IUpdatable, IDrawable, ISelectable
    {
		/// <summary>
		/// Name of object to access
		/// </summary>
        public string Name { get; set; }

        public abstract void Draw(CanvasDrawingSession session);
        public abstract Polygon2D GetSelectPolygon();
		public abstract bool IsAvailable();
		public abstract bool IsSelected();
		public abstract bool Select();
		public abstract bool ShouldBeDrawn();
		public abstract bool Unselect();
		public abstract void Update();
    }
}
