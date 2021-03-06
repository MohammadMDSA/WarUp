﻿using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Graphics;
using WarUp.Core.Logics;
using WarUp.Core.Storage;
using WarUp.Core.Utils;
using WarUp.GraphicEngine;
using Windows.UI.Core;

namespace WarUp.Core
{
	public class MainCore : ITickable
	{
		private Renderer Renderer;
		public static StorageCore Storage { get; private set; }
		private LogicCore Logic;

		public MainCore(SwapChainManager swapChainManager) : this(swapChainManager, new StorageCore())
		{
		}

		public MainCore(SwapChainManager swapChainManager, StorageCore storage)
		{
			Storage = storage;
			Renderer = new Renderer(swapChainManager);
			Logic = new LogicCore(Storage);
		}

		public void Tick()
		{
			Logic.Tick();
			Renderer.Render(Storage.GetDrawables());
		}

		public void Suspend()
		{
			Renderer.Trim();
		}

		public void Restart()
		{
			Storage.Reset();
		}
		
	}
}
