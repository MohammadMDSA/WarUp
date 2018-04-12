using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Canvases;
using WarUp.Logic.Editor.Input.Mouse;
using WarUp.Logic.Editor.Input;
using WarUp.Core.Storage;

namespace WarUp.Logic.Editor
{
	class EditorCore
	{
		public EditorCanvas EditorCanvas { get; }
		public EditorMouse EditorMouse { get; }
		public StorageCore Storage { get; }

		public EditorCore(EditorCanvas editorCanvas)
		{
			this.EditorCanvas = editorCanvas;

			this.Storage = new StorageCore();
			EditorInput.Mouse = this.EditorMouse = new EditorMouse(Storage, EditorCanvas);

			EditorCanvas.Mouse = EditorMouse;
			EditorCanvas.Storage = Storage;
		}

	}
}
