using Mannequin.Inputs;
using System;
using UnityEngine;


namespace Mannequin
{
	[CreateAssetMenu(fileName = "PlayerIntents", menuName = "Daydream/Player/Inputs/Intents")]
	public class PlayerIntents : ScriptableObject, IObservable3dPlayerIntentsProvider
	{
		public IObservable3dPlayerIntentsProvider Source { get; set; } = null;

		public Vector2 MoveHorizontally => Source?.MoveHorizontally ?? default;
		public Vector2 MoveVertically => Source?.MoveVertically ?? default;
		public bool Jump => Source?.Jump ?? default;
		public bool Sprint => Source?.Sprint ?? default;
		public Vector2 Look => Source?.Look ?? default;


		public event Action<Vector2> OnMoveHorizontallyInput
		{
			add => Source.OnMoveHorizontallyInput += value;
			remove => Source.OnMoveHorizontallyInput -= value;
		}

		public event Action<Vector2> OnMoveVerticallyInput
		{
			add => Source.OnMoveVerticallyInput += value;
			remove => Source.OnMoveVerticallyInput -= value;
		}

		public event Action<bool> OnJumpInput
		{
			add => Source.OnJumpInput += value;
			remove => Source.OnJumpInput -= value;
		}

		public event Action<bool> OnSprintInput
		{
			add => Source.OnSprintInput += value;
			remove => Source.OnSprintInput -= value;
		}

		public event Action<Vector2> OnLookInput
		{
			add => Source.OnLookInput += value;
			remove => Source.OnLookInput -= value;
		}
	}
}
