using UnityEngine;
using System;


namespace Mannequin.Inputs
{
	public interface IObservable3dPlayerIntentsProvider : IPlayer3dIntentsProvider
	{
		event Action<Vector2> OnMoveHorizontallyInput;
		event Action<Vector2> OnMoveVerticallyInput;
		event Action<bool> OnJumpInput;
		event Action<bool> OnSprintInput;
		event Action<Vector2> OnLookInput;
	}
}