using Microsoft.Graphics.Canvas;
using WarUp.Core.Logics.Models;
using WarUp.Core.Storage;
using WarUp.GraphicEngine;
using WarUp.Logic;
using WarUp.Logic.Editor;
using WarUp.Utils.File;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarUp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class GamePage : Page
	{
		public bool WindowVisible { get; private set; }
		public bool WindowClosed { get; private set; }

		public SwapChainManager SwapChainManager { get; }

		private EditorCore EditorCore;
		private LogicCore LogicCore;

		public GamePage()
		{
			this.InitializeComponent();

			WindowVisible = false;
			WindowClosed = false;

			SwapChainManager = new SwapChainManager(new CanvasDevice());

			this.GameSwapChain.SwapChain = SwapChainManager.SwapChain;

			EditorCore = new EditorCore(EditorCanvas);

			LogicCore = new LogicCore(this);

		}
		
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var bound = Window.Current.CoreWindow.Bounds;
			WindowVisible = true;

		}

		private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
		{

		}

		private void PropertiesPanelSwitchButton_Click(object sender, RoutedEventArgs e)
		{
			SidePanel.IsPaneOpen = !SidePanel.IsPaneOpen;
		}

		private void GamePlayPauseButton_Click(object sender, RoutedEventArgs e)
		{
			if (!LogicCore.GameRunning)
			{
				GameResetButton.IsEnabled = true;
				EditorCanvas.IsEnabled = false;

				CanvasPanel.SelectedIndex = 1;
				EditorLoading.IsLoading = true;

				LogicCore.Start(EditorCore.Storage.Clone());
			}
			else
			{
				LogicCore.Pause_Resume();
			}

			GamePlayPauseButton.Icon = new SymbolIcon(!LogicCore.GamePaused ? Symbol.Pause : Symbol.Play);
			GamePlayPauseButton.Label = !LogicCore.GamePaused ? "Pause" : "Play";

		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			GameResetButton.IsEnabled = false;

			EditorCanvas.IsEnabled = true;
			EditorLoading.IsLoading = false;

			LogicCore.Reset();

			GamePlayPauseButton.Icon = new SymbolIcon(Symbol.Play);
			GamePlayPauseButton.Label = "Play";
		}

		private void FullScreenSwitchButton_Click(object sender, RoutedEventArgs e)
		{
			var view = ApplicationView.GetForCurrentView();
			if (!view.IsFullScreenMode)
			{
				view.TryEnterFullScreenMode();
				FullScreenSwitchButton.Icon = new SymbolIcon(Symbol.BackToWindow);
			}
			else
			{
				view.ExitFullScreenMode();
				FullScreenSwitchButton.Icon = new SymbolIcon(Symbol.FullScreen);
			}
		}

		private void WaypointToggleButton_Click(object sender, RoutedEventArgs e)
		{
			EditorCore.StateManager.SetState(Logic.Editor.States.EditorStateManager.State.Waypoint);
		}

		private void PointerToggleButton_Click(object sender, RoutedEventArgs e)
		{
			EditorCore.StateManager.SetState(Logic.Editor.States.EditorStateManager.State.Select);
		}


		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			SwapChainManager.RenderSize = new Size(GameSwapChain.ActualWidth, GameSwapChain.ActualHeight);

			EditorCanvas.ReDraw();
		}

		private void GameSwapChain_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			SwapChainManager.RenderSize = e.NewSize;
		}

		private void EditorCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{

		}

		private async void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			await SaveLoadGame.Save(EditorCore.Storage, typeof(StorageCore));
		}

		private async void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			GreenTile t = await SaveLoadGame.Load<GreenTile>();
		}
		
		private void Page_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			EditorCanvas.Focus(FocusState.Pointer);
			EditorCanvas.Focus();
		}
	}
}
