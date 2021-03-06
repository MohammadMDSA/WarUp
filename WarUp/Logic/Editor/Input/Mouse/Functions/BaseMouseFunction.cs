﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Logic.Editor.Input.Mouse.Functions
{
	public abstract class BaseMouseFunction : IMouseFunction
	{
		public readonly EditorMouse Mouse;

		public BaseMouseFunction(EditorMouse mouse)
		{
			this.Mouse = mouse;
		}

		public abstract void Moved(UIElement sender, PointerRoutedEventArgs e);
		public abstract void PointerPressed(UIElement sender, PointerRoutedEventArgs e);
		public abstract void PointerReleased(UIElement sender, PointerRoutedEventArgs e);
		public abstract void WheelChanged(UIElement sender, PointerRoutedEventArgs e);
		public abstract void Reset();
	}
}
