using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Canvases;
using WarUp.Core;
using WarUp.Core.Storage;
using WarUp.GraphicEngine;
using WarUp.Utils.Mouse;

namespace WarUp.Logic
{
	public class LogicCore
	{
		private Task GameThread;
		public bool GameRunning { get; private set; }
		public bool GamePaused { get; private set; }

		private Mouse Mouse;

		public StorageCore Storage { get; }
		private MainCore MainCore;

		private GamePage GamePage;

		public LogicCore(GamePage gamePage, EditorCanvas editor)
		{
			this.GamePage = gamePage;

			Storage = new StorageCore();

			Mouse = new Mouse(Storage, editor);
			Input.Input.Mouse = Mouse;

			editor.Mouse = Mouse;
			editor.Storage = Storage;

			GamePaused = false;
			GameRunning = false;
		}
		public async void Run()
		{
			MainCore = new MainCore(GamePage.SwapChainManager, Storage);

			while (!GamePage.WindowClosed && GameRunning)
			{
				if (GamePage.WindowVisible && !GamePaused)
				{
					MainCore.Tick();
				}
				else
				{
					await Task.Delay(1000);
				}
			}
		}

		public void Start()
		{
			GamePaused = false;
			GameRunning = true;
			GameThread = Task.Run(new Action(Run));
		}

		public void Pause_Resume()
		{
			GamePaused = !GamePaused;
		}

		public void Reset()
		{
			GamePaused = false;
			GameRunning = false;

			Task.WaitAll(GameThread);
			GameThread.Dispose();

			MainCore.Restart();
			MainCore.Tick();
		}
	}
}
