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
using WarUp.Logic;
using WarUp.Logic.Input;
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
		public bool WindowVisible { get; private set; }
		public bool WindowClosed { get; private set; }

		public SwapChainManager SwapChainManager { get; }
		private LogicCore LogicCore;

		public GamePage()
		{
			this.InitializeComponent();

			WindowVisible = false;
			WindowClosed = false;

			SwapChainManager = new SwapChainManager(new CanvasDevice());

			this.GameSwapChain.SwapChain = SwapChainManager.SwapChain;

			LogicCore = new LogicCore(this, EditorCanvas);
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

				LogicCore.Start();
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

			LogicCore.Reset();
			
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
			Input.Mouse.SetFunctionType(Mouse.FunctionType.Waypoint);
		}

		private void PointerToggleButton_Click(object sender, RoutedEventArgs e)
		{
			Input.Mouse.SetFunctionType(Mouse.FunctionType.Select);
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
			//await SaveLoadGame.Save(Storage, typeof(StorageCore));
		}

		private async void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			//GameLoadedStorage = await SaveLoadGame.Load<StorageCore>();
		}
	}
}
