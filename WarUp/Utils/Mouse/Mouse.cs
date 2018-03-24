using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.Models;
using WarUp.Core.Storage;
using WarUp.GraphicEngine;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Utils.Mouse
{
	public class Mouse : IMouseFunction
	{
		public Vector2 Position { get; private set; }
		public bool IsLeftPressed { get; private set; }
		public bool IsRightPressed { get; private set; }
		private List<FrameworkObject> SelectedObjects;
		public StorageCore Storage { get; }
		public FunctionType Type { get; private set; }
		public IMouseFunction ActiveFunction { get; private set; }
		public MouseSelectHandler SelectHandler { get; }
		public SwapChainManager RenderManager { get; }

		public Mouse(StorageCore storage, SwapChainManager manager)
		{
			this.RenderManager = manager;
			Position = Vector2.Zero;
			IsLeftPressed = IsRightPressed = false;
			SelectedObjects = new List<FrameworkObject>();
			this.Storage = storage;

			this.SelectHandler = new MouseSelectHandler(this);

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

		public bool SetFunctionType(FunctionType type)
		{
			this.Type = type;
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

		public enum FunctionType
		{
			Select = 0,
			Waypoint
		}
	}
}
