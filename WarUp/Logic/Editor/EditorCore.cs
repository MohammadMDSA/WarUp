using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Canvases;
using WarUp.Logic.Editor.Input.Mouse;
using WarUp.Logic.Editor.Input;
using WarUp.Core.Storage;
using WarUp.Logic.Editor.Input.Keyboard;
using WarUp.Logic.Editor.States;

namespace WarUp.Logic.Editor
{
	class EditorCore
	{
		public EditorCanvas EditorCanvas { get; }
		public EditorMouse Mouse { get; }
		public EditorKeyboard Keyboard { get; }
		public StorageCore Storage { get; }
		public EditorStateManager StateManager { get; }

		public EditorCore(EditorCanvas editorCanvas)
		{
			this.EditorCanvas = editorCanvas;

			this.Storage = new StorageCore();
			EditorInput.Mouse = this.Mouse = new EditorMouse(Storage, EditorCanvas);
			EditorInput.Keyboard = this.Keyboard = new EditorKeyboard(Storage, EditorCanvas);

			EditorCanvas.Mouse = Mouse;
			EditorCanvas.Storage = Storage;

			this.StateManager = new EditorStateManager(Mouse, Keyboard);
		}

	}
}
