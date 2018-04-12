using WarUp.Logic.Editor.Input.Keyboard;
using WarUp.Logic.Editor.Input.Mouse;

namespace WarUp.Logic.Editor.Input
{
	public static class EditorInput
	{
		public static EditorMouse Mouse { get; internal set; }
		public static EditorKeyboard Keyboard { get; internal set; }
	}
}
