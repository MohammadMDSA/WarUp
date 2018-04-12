using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Logic.Editor.Input.Keyboard.Functions
{
	public class KeyboardSelectHandler : BaseKeyboardFunction
	{
		public KeyboardSelectHandler(EditorKeyboard keyboard) : base(keyboard)
		{
			Reset();
		}

		public override void KeyDown(UIElement sender, KeyRoutedEventArgs e)
		{
			switch (e.Key)
			{
				case VirtualKey.Delete:
					if (Keyboard.KeysStatus[VirtualKey.Delete] == EditorKeyboard.KeyStatus.Released)
						Delete();
					break;
				default:
					break;
			}
		}

		public override void KeyUp(UIElement sender, KeyRoutedEventArgs e)
		{
			
		}

		public override void Reset()
		{
			
		}

		private void Delete()
		{
			var selected = EditorInput.Mouse.GetSelected();
			Keyboard.Storage.RemoveObject(selected);
		}
	}
}
