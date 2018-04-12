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
		public State CurrentState { get; }
		public EditorMouse Mouse { get; }
		public EditorKeyboard keyboard { get; }

		public EditorStateManager(EditorMouse mouse, EditorKeyboard keyboard)
		{
			this.Mouse = mouse;
			this.keyboard = keyboard;

		}

		public void SetState(State state)
		{
			switch (state)
			{
				case State.Select:
					Mouse.SetFunctionType(EditorMouse.FunctionType.Select);
					break;
				case State.Waypoint:
					Mouse.SetFunctionType(EditorMouse.FunctionType.Waypoint);
					break;
				default:
					break;
			}
		}

		public enum State
		{
			Select = 0,
			Waypoint
		}
	}
}
