using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameCore.Core.Logics.Utils;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using WarUp.Core.Graphics;
using WarUp.Core.Logics.Utils;
using Windows.Foundation;

namespace WarUp.Core.Logics.Models
{

	/// <summary>
	/// Represents a framework object
	/// </summary>
	[Serializable]
	public abstract class FrameworkObject : IUpdatable, IDrawable, ISelectable, INameSuggester
    {

		/// <summary>
		/// Position of Waypoint
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Name of object to access
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Size of the object
		/// </summary>
		public Vector2 Size { get; protected set; }

		public bool SetName(string name)
		{
			var existings = MainCore.Storage.GetNamesList();
			if (existings.Contains(name))
				return false;
			this.Name = name;
			return true;
		}

		public abstract void Draw(CanvasDrawingSession session);
		public abstract Rect GetBound();
		public abstract Polygon2D GetSelectPolygon();
		public abstract Vector2 GetSize();
		public abstract bool IsAvailable();
		public abstract bool IsSelected();
		public abstract bool RequiresName();
		public abstract bool Select();
		public abstract bool ShouldBeDrawn();
		public abstract string SuggestName(IEnumerable<string> existings);
		public abstract bool Unselect();
		public abstract void Update();
    }
}
