using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Canvases;
using WarUp.Core.Storage;
using WarUp.Logic.Editor.Input.Keyboard.Functions;
using WarUp.Logic.Editor.States;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using static WarUp.Logic.Editor.States.EditorStateManager;

namespace WarUp.Logic.Editor.Input.Keyboard
{
	public class EditorKeyboard : IKeyboardFunction
	{
		public EditorCanvas Editor { get; }
		public Dictionary<VirtualKey, KeyStatus> KeysStatus { get; }
		public StorageCore Storage { get; }
		public State State { get => EditorStateManager.StateManager.CurrentState; }

		private KeyboardSelectHandler SelectHandler;
		public BaseKeyboardFunction ActiveKeyboardFundtion { get; private set; }

		public EditorKeyboard(StorageCore storage, EditorCanvas editor)
		{
			this.Editor = editor;
			this.Storage = storage;

			this.KeysStatus = new Dictionary<VirtualKey, KeyStatus>();

			var keys = Enum.GetValues(typeof(VirtualKey)).Cast<VirtualKey>();
			foreach (var item in keys)
			{
				KeysStatus.TryAdd(item, KeyStatus.Released);
			}

			this.SelectHandler = new KeyboardSelectHandler(this);

			this.ActiveKeyboardFundtion = this.SelectHandler;
		}

		public void KeyDown(UIElement sender, KeyRoutedEventArgs e)
		{
			ActiveKeyboardFundtion.KeyDown(sender, e);
			KeysStatus[e.Key] = KeyStatus.Pressed;
		}

		public void KeyUp(UIElement sender, KeyRoutedEventArgs e)
		{
			ActiveKeyboardFundtion.KeyUp(sender, e);
			KeysStatus[e.Key] = KeyStatus.Released;
		}

		public void SetState(State state)
		{
			ActiveKeyboardFundtion.Reset();
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


		public enum KeyStatus
		{
			Pressed = 0,
			Released
		}
	}
}
