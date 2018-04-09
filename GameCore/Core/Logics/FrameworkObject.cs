using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using WarUp.Core.Graphics;
using Windows.Foundation;

namespace WarUp.Core.Logics.Models
{

	/// <summary>
	/// Represents a framework object
	/// </summary>
	[Serializable]
	public abstract class FrameworkObject : IUpdatable, IDrawable, ISelectable
    {

		/// <summary>
		/// Position of Waypoint
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Name of object to access
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Size of the object
		/// </summary>
		public Vector2 Size { get; protected set; }

		public abstract void Draw(CanvasDrawingSession session);
		public abstract Rect GetBound();
		public abstract Polygon2D GetSelectPolygon();
		public abstract Vector2 GetSize();
		public abstract bool IsAvailable();
		public abstract bool IsSelected();
		public abstract bool Select();
		public abstract bool ShouldBeDrawn();
		public abstract bool Unselect();
		public abstract void Update();
    }
}
