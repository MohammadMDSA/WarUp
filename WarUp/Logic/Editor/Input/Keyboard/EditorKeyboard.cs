using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Canvases;
using WarUp.Logic.Editor.Input.Keyboard.Functions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using static WarUp.Logic.Editor.States.EditorStateManager;

namespace WarUp.Logic.Editor.Input.Keyboard
{
	public class EditorKeyboard : IKeyboardFunction
	{
		public EditorCanvas Editor { get; }
		public State State { get; private set; }
		
		public BaseKeyboardFunction ActiveKeyboardFundtion { get; private set; }

		public EditorKeyboard(EditorCanvas editor)
		{
			this.Editor = editor;
		}

		public void KeyDown(UIElement sender, KeyRoutedEventArgs e)
		{
			ActiveKeyboardFundtion.KeyDown(sender, e);
		}

		public void KeyUp(UIElement sender, KeyRoutedEventArgs e)
		{
			ActiveKeyboardFundtion.KeyUp(sender, e);
		}

		public void SetState(State state)
		{
			this.State = State;
			switch (state)
			{
				case State.Select:
					break;
				case State.Waypoint:
					break;
				default:
					break;
			}
		}
	}
}
