using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarUp.Core.Logics
{
	/// <summary>
	/// Makes a framwork object selectable
	/// </summary>
    interface ISelectable
    {
		/// <summary>
		/// Gets object bound to select if mouse click is inside it
		/// </summary>
		/// <returns>Bound polygon of visual object</returns>
        Polygon2D GetSelectPolygon();

		/// <summary>
		/// Tells if the object is selected
		/// </summary>
		/// <returns>Returns true if object is selected and returns false otherwise</returns>
        bool IsSelected();
		
		/// <summary>
		/// Make object selected
		/// </summary>
		/// <returns>If objects state successfully changed to select returns true, other wise returns false</returns>
		bool Select();

		/// <summary>
		/// Make object unselected
		/// </summary>
		/// <returns>If objects state successfully changed to unselect returns true, other wise returns false</returns>
		bool Unselect();

		/// <summary>
		/// Tells if the object is availabe to select or not
		/// </summary>
		/// <returns>Returns true if object is available to select</returns>
		bool IsAvailable();
    }
}
