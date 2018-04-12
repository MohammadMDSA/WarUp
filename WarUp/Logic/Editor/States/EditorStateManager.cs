using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Logic.Editor.Input.Keyboard;
using WarUp.Logic.Editor.Input.Mouse;

namespace WarUp.Logic.Editor.States
{
	public class EditorStateManager
	{
		public static EditorStateManager StateManager { get; private set; }

		public State CurrentState { get; private set; }
		public EditorMouse Mouse { get; }
		public EditorKeyboard Keyboard { get; }

		public EditorStateManager(EditorMouse mouse, EditorKeyboard keyboard)
		{
			this.Mouse = mouse;
			this.Keyboard = keyboard;
			StateManager = this;
		}

		public void SetState(State state)
		{
			this.CurrentState = state;
			Mouse.SetState(state);
			Keyboard.SetState(state);
		}

		public enum State
		{
			Select = 0,
			Waypoint
		}
	}
}
