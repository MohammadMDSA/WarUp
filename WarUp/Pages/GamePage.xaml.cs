﻿using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using WarUp.Core;
using WarUp.GraphicEngine;
using WarUp.Utils;
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

		private MainCore MainCore;
		SwapChainManager SwapChainManager;

		public GamePage()
		{
			this.InitializeComponent();

			WindowVisible = false;
			WindowClosed = false;

			SwapChainManager = new SwapChainManager(new CanvasDevice());

			this.GameSwapChain.SwapChain = SwapChainManager.SwapChain;

			MainCore = new MainCore(SwapChainManager);
			Mouse = new Mouse(MainCore.Storage, SwapChainManager);

			GameSwapChain.Mouse = Mouse;

			GamePaused = false;
			GameRunning = false;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var bound = Window.Current.CoreWindow.Bounds;
			WindowVisible = true;

		}

		public void Run()
		{
			while (!WindowClosed && GameRunning)
			{
				if (WindowVisible && !GamePaused)
				{
					MainCore.Tick();
				}
				else
				{
					Task.Delay(1000);
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

			GamePaused = false;
			GameRunning = false;

			Task.WaitAll(GameThread);
			GameThread.Dispose();

			MainCore.Restart();

			GamePlayPauseButton.Icon = new SymbolIcon(Symbol.Play);
			GamePlayPauseButton.Label = "Play";

			MainCore.Tick();
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
			MainCore.Tick();
		}

		private void GameSwapChain_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			SwapChainManager.RenderSize = e.NewSize;
		}
	}
}
