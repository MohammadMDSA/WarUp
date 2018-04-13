using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
		[NonSerialized]
		private Vector2 _Position;
		public Vector2 Position { get => _Position; set => _Position = value; }
		private float _PositionX, _PositionY;

		/// <summary>
		/// Name of object to access
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Size of the object
		/// </summary>
		[NonSerialized]
		private Vector2 _Size;
		public Vector2 Size { get => _Size; protected set => _Size = value; }
		private float _SizeX, _SizeY;

		[NonSerialized] protected bool Selected;

		public bool SetName(string name)
		{
			var existings = MainCore.Storage.GetNamesList();
			if (existings.Contains(name))
				return false;
			this.Name = name;
			return true;
		}

		public virtual bool IsSelected() => Selected;

		public abstract void Draw(CanvasDrawingSession session);
		public abstract Rect GetBound();
		public abstract Polygon2D GetSelectPolygon();
		public abstract Vector2 GetSize();
		public abstract bool IsAvailable();
		public abstract bool RequiresName();
		public abstract bool Select();
		public abstract bool ShouldBeDrawn();
		public abstract string SuggestName(IEnumerable<string> existings);
		public abstract bool Unselect();
		public abstract void Update();
		
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			_PositionX = _Position.X;
			_PositionY = _Position.Y;

			_SizeX = _Size.X;
			_SizeY = _Size.Y;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			_Position = new Vector2(_PositionX, _PositionY);

			_Size = new Vector2(_SizeX, _SizeY);

			Selected = false;
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
