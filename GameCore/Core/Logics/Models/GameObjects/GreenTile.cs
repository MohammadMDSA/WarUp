﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
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
	public class GreenTile : GameObject, IMovable
	{
		private bool Selected;
		private float Speed;
		private Vector2 UpperLeftEdge => Position - Size / 2;

		public GreenTile()
		{
			Position = new Vector2(0f);
			Selected = false;
			Speed = 5;
			Size = new Vector2(10f);
		}

		public override void Draw(CanvasDrawingSession session)
		{
			session.FillRectangle(GetBound(), Selected ? Colors.Red : Colors.Green);
			
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
			list.Add(new Point2D(Position.X - 5, Position.Y - 5));
			list.Add(new Point2D(Position.X - 5, Position.Y + 5));
			list.Add(new Point2D(Position.X + 5, Position.Y + 5));
			list.Add(new Point2D(Position.X + 5, Position.Y - 5));
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

		public override Rect GetBound()
		{
			return new Rect((Position - Size / 2).ToPoint(), Size.ToSize());
		}
	}
}
