using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Mannequin.Inputs.Readers
{
	[RequireComponent(typeof(PlayerInput))]
	public class Player3dInputsReader : MonoBehaviour
	{
		[Header("Config:")]
		[field: SerializeField] public bool InvertLateralMoveDirection { get; set; } = true;
		[field: SerializeField] public bool InvertForwardMoveDirection { get; set; } = true;
		[field: SerializeField] public bool InvertVerticalLookDirection { get; set; } = false;
		
		public Vector3 MoveDirection { get; private set; }
		public Vector2 LookDirection { get; private set; }


		[Header("Debug:")]
		[SerializeField, ReadOnly] bool isJumping = false;
		[field: SerializeField] public bool Sprint { get; private set; }


		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnJump(InputValue value)
		{
			isJumping = value.isPressed;
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnLook(InputValue value)
		{
			LookInput(value.Get<Vector2>());
		}


		public void JumpInput(bool newJumpState)
		{
			MoveDirection = new Vector3(
				x: MoveDirection.x,
				y: newJumpState ? 1 : 0,
				z: MoveDirection.z
			);
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			MoveDirection = new Vector3(
				x: newMoveDirection.x * (InvertLateralMoveDirection ? -1 : 1),
				y: MoveDirection.y,
				z: newMoveDirection.y * (InvertForwardMoveDirection ? -1 : 1)
			);
		}

		public void SprintInput(bool newSprintState)
		{
			Sprint = newSprintState;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			LookDirection = new Vector2(newLookDirection.x, newLookDirection.y * (InvertVerticalLookDirection ? -1 : 1));
		}
	}
}