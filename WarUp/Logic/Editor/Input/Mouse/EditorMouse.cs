using System.Collections.Generic;
using System.Numerics;
using WarUp.Canvases;
using WarUp.Core.Logics.Models;
using WarUp.Core.Storage;
using WarUp.Logic.Editor.Input.Mouse.Functions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using static WarUp.Logic.Editor.States.EditorStateManager;

namespace WarUp.Logic.Editor.Input.Mouse
{
	public class EditorMouse : IMouseFunction
	{
		public Vector2 Position { get; private set; }
		public bool IsLeftPressed { get; private set; }
		public bool IsRightPressed { get; private set; }
		private List<FrameworkObject> SelectedObjects;
		public StorageCore Storage { get; }
		public EditorCanvas Editor { get; }

		private MouseWaypointHandler WaypointHandler;
		private MouseSelectHandler SelectHandler;
		public State State { get; private set; }
		public BaseMouseFunction ActiveFunction { get; private set; }

		public EditorMouse(StorageCore storage, EditorCanvas editor)
		{
			this.Editor = editor;
			Position = Vector2.Zero;
			IsLeftPressed = IsRightPressed = false;
			SelectedObjects = new List<FrameworkObject>();
			this.Storage = storage;

			this.SelectHandler = new MouseSelectHandler(this);
			this.WaypointHandler = new MouseWaypointHandler(this);

			this.ActiveFunction = SelectHandler;
		}
		
		public void AddSelected(FrameworkObject fObject)
		{
			fObject.Select();
			this.SelectedObjects.Add(fObject);
		}

		public bool RemoveFromSelected(FrameworkObject fObject)
		{
			var res = SelectedObjects.Remove(fObject);
			if (res)
				fObject.Unselect();
			return res;
		}

		public void ClearSelectedObjects()
		{
			foreach (var item in SelectedObjects)
			{
				item.Unselect();
			}
			SelectedObjects.Clear();
		}

		public IEnumerable<FrameworkObject> GetSelected()
		{
			return SelectedObjects.AsReadOnly();
		}

		public bool SetState(State state)
		{
			ActiveFunction.Reset();
			this.State = State;
			switch (state)
			{
				case State.Select:
					ActiveFunction = SelectHandler;
					break;
				case State.Waypoint:
					ActiveFunction = WaypointHandler;
					break;
				default:
					break;
			}

			return true;
		}

		public void Moved(UIElement sender, PointerRoutedEventArgs e)
		{
			ActiveFunction.Moved(sender, e);
		
			var rect = e.GetCurrentPoint(sender).Properties.ContactRect;
			Position = new Vector2((float)rect.X, (float)rect.Y);
		}

		public void WheelChanged(UIElement sender, PointerRoutedEventArgs e)
		{
			ActiveFunction.WheelChanged(sender, e);
		}

		public void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			ActiveFunction.PointerPressed(sender, e);

			var properties = e.GetCurrentPoint(sender).Properties;
			IsRightPressed = properties.IsRightButtonPressed;
			IsLeftPressed = properties.IsLeftButtonPressed;
		}

		public void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			ActiveFunction.PointerReleased(sender, e);

			var properties = e.GetCurrentPoint(sender).Properties;
			IsRightPressed = properties.IsRightButtonPressed;
			IsLeftPressed = properties.IsLeftButtonPressed;
		}
		
	}
}
