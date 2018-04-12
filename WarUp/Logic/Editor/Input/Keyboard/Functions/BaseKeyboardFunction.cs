using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Logic.Editor.Input.Keyboard.Functions
{
	public abstract class BaseKeyboardFunction : IKeyboardFunction
	{
		public readonly EditorKeyboard Keyboard;

		public BaseKeyboardFunction(EditorKeyboard keyboard)
		{
			this.Keyboard = keyboard;
		}

		public abstract void KeyDown(UIElement sender, KeyRoutedEventArgs e);
		public abstract void KeyUp(UIElement sender, KeyRoutedEventArgs e);
		public abstract void Reset();
	}
}
