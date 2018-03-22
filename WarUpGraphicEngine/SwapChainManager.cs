using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace WarUp.GraphicEngine
{
	public class SwapChainManager
	{
		public CanvasSwapChain SwapChain { get; private set; }
		public Size SwapChainSize => SwapChain.SourceSize;

		public SwapChainManager(CanvasSwapChain swapChain)
		{
			float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
			//SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, currentDpi);
			SwapChain = swapChain;
		}

		//public void EnsureMatchesWindow(Page window)
		//{
		//	var bounds = window.Bounds;
		//	Size windowSize = new Size(bounds.Width, bounds.Height);
		//	float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

		//	if (!SizeEqualsWithTolerance(windowSize, SwapChain.Size) || dpi != SwapChain.Dpi)
		//	{
		//		SwapChain.ResizeBuffers((float)windowSize.Width, (float)windowSize.Height, dpi);
		//	}
		//}

		static public bool SizeEqualsWithTolerance(Size sizeA, Size sizeB)
		{
			const float tolerance = 0.1f;

			if (Math.Abs(sizeA.Width - sizeB.Width) > tolerance)
			{
				return false;
			}

			if (Math.Abs(sizeA.Height - sizeB.Height) > tolerance)
			{
				return false;
			}

			return true;
		}

	}
}
