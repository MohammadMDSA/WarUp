using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using WarUp.Core;
using WarUp.Core.Storage;
using WarUp.GraphicEngine;
using WarUp.Utils;
using WarUp.Utils.File;
using WarUp.Utils.Mouse;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarUp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class GamePage : Page
	{
		private bool WindowVisible;
		private bool WindowClosed;
		private Mouse Mouse;
		private Task GameThread;
		private bool GameRunning;
		private bool GamePaused;

		private StorageCore GameLoadedStorage;
		private StorageCore Storage;
		private MainCore MainCore;
		private SwapChainManager SwapChainManager;

		public GamePage()
		{
			this.InitializeComponent();

			WindowVisible = false;
			WindowClosed = false;

			SwapChainManager = new SwapChainManager(new CanvasDevice());

			this.GameSwapChain.SwapChain = SwapChainManager.SwapChain;

			Storage = new StorageCore();

			Mouse = new Mouse(Storage, EditorCanvas);

			EditorCanvas.Mouse = Mouse;
			EditorCanvas.Storage = Storage;

			GamePaused = false;
			GameRunning = false;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var bound = Window.Current.CoreWindow.Bounds;
			WindowVisible = true;

		}

		public async void Run()
		{
			MainCore = new MainCore(SwapChainManager, Storage);

			while (!WindowClosed && GameRunning)
			{
				if (WindowVisible && !GamePaused)
				{
					MainCore.Tick();
				}
				else
				{
					await Task.Delay(1000);
				}
			}
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
			if (!GameRunning)
			{
				GameResetButton.IsEnabled = true;
				GamePaused = false;
				GameRunning = true;
				EditorCanvas.IsEnabled = false;
				GameThread = Task.Run(new Action(Run));
			}
			else
			{
				GamePaused = !GamePaused;
			}

			GamePlayPauseButton.Icon = new SymbolIcon(!GamePaused ? Symbol.Pause : Symbol.Play);
			GamePlayPauseButton.Label = !GamePaused ? "Pause" : "Play";
			
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			GameResetButton.IsEnabled = false;

			EditorCanvas.IsEnabled = true;

			GamePaused = false;
			GameRunning = false;

			Task.WaitAll(GameThread);
			GameThread.Dispose();

			MainCore.Restart();
			MainCore.Tick();

			GamePlayPauseButton.Icon = new SymbolIcon(Symbol.Play);
			GamePlayPauseButton.Label = "Play";
		}

		private void FullScreenSwitchButton_Click(object sender, RoutedEventArgs e)
		{
			var view = ApplicationView.GetForCurrentView();
			if(!view.IsFullScreenMode)
			{
				view.TryEnterFullScreenMode();
			}
			else
			{
				view.ExitFullScreenMode();
			}
		}

		private void WaypointToggleButton_Click(object sender, RoutedEventArgs e)
		{
			Mouse.SetFunctionType(Mouse.FunctionType.Waypoint);
		}

		private void PointerToggleButton_Click(object sender, RoutedEventArgs e)
		{
			Mouse.SetFunctionType(Mouse.FunctionType.Select);
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
			await SaveLoadGame.Save(Storage, typeof(StorageCore));
		}

		private async void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			GameLoadedStorage = await SaveLoadGame.Load<StorageCore>();
		}
	}
}
