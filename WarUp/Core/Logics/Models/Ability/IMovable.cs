using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WarUp.Core.Logics.MapUtils;
using Windows.UI.Composition;

namespace WarUp.Core.Logics.Models.Ability
{
	/// <summary>
	/// Represents ability of moving
	/// </summary>
	interface IMovable : IAbility
	{
		/// <summary>
		/// Move to a specific location
		/// </summary>
		/// <param name="destination">Coodinance of destination</param>
        void Move(Vector2 destination);

		/// <summary>
		/// Move to a specific waypoint
		/// </summary>
		/// <param name="destination">Waypoint of destination</param>
        void Move(Waypoint destination);

		/// <summary>
		/// Move along a waypoint route
		/// </summary>
		/// <param name="path">Route of movement</param>
        void Move(WaypointRoute path);

		float GetSpeed();
	}
}
