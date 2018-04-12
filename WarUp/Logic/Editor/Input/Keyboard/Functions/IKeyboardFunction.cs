using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Logic.Editor.Input.Keyboard.Functions
{
	interface IKeyboardFunction
	{
		void KeyDown(UIElement sender, KeyRoutedEventArgs e);
		void KeyUp(UIElement sender, KeyRoutedEventArgs e);
	}
}
