using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Canvases;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Logic.Editor.Input.Keyboard
{
	public class EditorKeyboard
	{
		public EditorCanvas Editor { get; }

		public EditorKeyboard(EditorCanvas editor)
		{
			this.Editor = editor;
		}

		public void KeyDown(UIElement sender, KeyRoutedEventArgs e)
		{

		}

		public void KeyUp(UIElement sender, KeyRoutedEventArgs e)
		{

		}
	}
}
