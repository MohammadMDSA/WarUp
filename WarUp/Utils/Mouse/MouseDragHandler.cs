using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace WarUp.Utils.Mouse
{
	public class MouseDragHandler : BaseMouseFunction
    {
		public MouseDragHandler(Mouse mouse) : base(mouse)
		{
			
		}

		public override void Moved(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public override void PointerPressed(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public override void PointerReleased(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public override void WheelChanged(UIElement sender, PointerRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
