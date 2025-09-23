using Mannequin.Configurators;
using Mannequin.Inputs.Readers;
using Matemagicas.Extensions;
using UnityEngine;


namespace Mannequin.Locomotion
{
	public class Player3dLocomotion : MonoBehaviour, ILocomotionSystem
	{
		public Transform Transform => transform;

		[Header("Global References:")]
		[SerializeField] Player3dParameters playerAttributes;

		[Header("Scene References:")]
		[SerializeField] CharacterController characterController;
		[SerializeField] Player3dInputsReader playerInputs;

		[Header("Config:")]
		[SerializeField] float groundedOffset;
		[SerializeField] float groundedRadius;
		[SerializeField] LayerMask groundLayers = 1;

		[Header("Debug:")]
		[field: SerializeField] public bool IsLocked { get; set; } = false;
		[field: SerializeField] public bool IsGrounded { get; private set; } = false;

		[field: SerializeField] bool applyGravity = true;
		[field: SerializeField] bool jumpAvailable = false;
		[field: SerializeField] bool processingJump = false;
		[field: SerializeField] bool processingAcceleration = false;
		[field: SerializeField] bool processingDeceleration = false;

		public const float SPEED_CONST_MULT = 10F;

		float ascentTimeDelta = 0f;
		float descentTimeDelta = 0f;
		float accelerationTimeDelta = 0f;

		Vector3 currentVelocity = Vector3.zero;


		private void Update()
		{
			if (jumpAvailable && readPlayerIntentToJump() && !IsLocked)
			{
				jumpAvailable = false;
				processingJump = true;
				ascentTimeDelta = 0f;
				descentTimeDelta = 0f;
			}

			if (processingJump)
			{
				// are we going up?
				if (!IsLocked && ascentTimeDelta < playerAttributes.AscentTime)
				{
					ascentTimeDelta = Mathf.Min(ascentTimeDelta + Time.deltaTime, playerAttributes.AscentTime);
					float ascentTimeProportion = ascentTimeDelta / playerAttributes.AscentTime;
					currentVelocity.y = (1 - playerAttributes.AscentCurve.Evaluate(ascentTimeProportion)) * playerAttributes.MaxJumpForce;
				}
				else
				{
					descentTimeDelta = Mathf.Min(descentTimeDelta + Time.deltaTime, playerAttributes.DescentTime);
					float descentTimeProportion = descentTimeDelta / playerAttributes.DescentTime;
					currentVelocity.y = playerAttributes.DescentCurve.Evaluate(descentTimeProportion) * playerAttributes.VerticalTerminalVelocity;
				}
			}

			// if we are moving
			processingAcceleration = Mathf.Abs(playerInputs.MoveDirection.x) + Mathf.Abs(playerInputs.MoveDirection.z) != 0f;
			processingDeceleration = !processingAcceleration;

			if (!IsLocked && processingAcceleration)
			{
				accelerationTimeDelta = Mathf.Min(accelerationTimeDelta + Time.deltaTime, playerAttributes.AccelerationTime);
				float accelerationTimeProportion = accelerationTimeDelta / playerAttributes.AccelerationTime;
				float targetSpeed = (playerInputs.Sprint ? playerAttributes.SprintSpeed : playerAttributes.WalkSpeed) / SPEED_CONST_MULT;    // la magnitud va a estar entre 1 (sticks a tope) y 0 (0.9, 0.4) en caso de no mandarle a tope
				Vector2 horzInput = playerInputs.MoveDirection.CutY().normalized;
				float velocity = -playerAttributes.AccelerationCurve.Evaluate(accelerationTimeProportion) * targetSpeed;


				float vertVelocity = currentVelocity.y;


				currentVelocity = characterController.transform.right * horzInput.x * velocity +
					characterController.transform.forward * horzInput.y * velocity +
					Vector3.up * vertVelocity;
			}
			else
			{
				currentVelocity.x = 0f;
				currentVelocity.z = 0f;
				accelerationTimeDelta = 0f;
			}

			characterController.Move(currentVelocity);
		}


		private bool readPlayerIntentToJump()
			=> playerInputs.MoveDirection.y > 0f;


		private void FixedUpdate()
		{
			IsGrounded = checkIsGrounded();
			jumpAvailable = IsGrounded;
			processingJump &= !IsGrounded;
			if (IsGrounded)
				currentVelocity.y = 0f;
			else
			{
				if (applyGravity && !processingJump)
					characterController.Move(Vector3.up * playerAttributes.VerticalTerminalVelocity);
			}
		}

		private bool checkIsGrounded()
		{
			// set sphere position, with offset
			Vector3 spherePosition = characterController.transform.position.SumToY(-1 * groundedOffset);
			return Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
			// try with Collision.Contacts[0].point
		}
	}
}