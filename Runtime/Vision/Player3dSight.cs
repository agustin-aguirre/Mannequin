using Mannequin.Configurators;
using Mannequin.Inputs.Readers;
using Matemagicas;
using UnityEngine;


namespace Mannequin.Vision
{
	public class Player3dSight : MonoBehaviour, ISightSystem
	{
		[Header("Global References:")]
		[SerializeField] Player3dParameters playerAttributes;

		[Header("Scene References:")]
		[SerializeField] Player3dInputsReader playerInputs;
		[SerializeField] CharacterController characterController;
		[SerializeField] GameObject cinemachineCameraTarget;

		[Header("Debug:")]
		[field: SerializeField] public bool IsLocked { get; set; } = false;

		public Transform Transform => transform;
		public Vector3 LookDirection => transform.forward;

		float cinemachineTargetPitch;
		float rotationSpeed;

		const float THRESHOLD = 0.01f;
		const float TOP_CLAMP = 90.0f;
		const float BOTTOM_CLAMP = -90.0f;


		public void LookAt(Transform target)
		{
			throw new System.NotImplementedException("FPSPlayerSight # ForceLookAt (Transform) is not yet implemented.");
		}

		public void ForceLookAt(Vector3 target)
		{
			throw new System.NotImplementedException("FPSPlayerSight # ForceLookAt (Vector3) is not yet implemented.");
		}

		public void FollowTarget(Transform target)
		{
			throw new System.NotImplementedException("FPSPlayerSight # FollowTarget (Transform) is not yet implemented.");
		}


		private void LateUpdate()
		{
			if (IsLocked)
				return;
			// if there is an input
			if (playerInputs.LookDirection.sqrMagnitude >= THRESHOLD)
			{
				cinemachineTargetPitch += playerInputs.LookDirection.y * playerAttributes.Sensitivity;
				rotationSpeed = playerInputs.LookDirection.x * playerAttributes.Sensitivity;

				// clamp our pitch rotation
				cinemachineTargetPitch = AngleTricks.ClampAngle(cinemachineTargetPitch, BOTTOM_CLAMP, TOP_CLAMP);

				// Update Cinemachine camera target pitch
				cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				characterController.transform.Rotate(Vector3.up * rotationSpeed);
			}
		}
	}
}