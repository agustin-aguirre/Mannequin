using UnityEngine;


namespace Mannequin.Configurators
{
	[CreateAssetMenu(fileName = "New Player 3d Parameters", menuName = "Mannequin/Player 3d Parameters")]
	public class Player3dParameters : ScriptableObject
	{
		[Header("Jump Ascent:")]
		[field: SerializeField] public AnimationCurve AscentCurve { get; private set; }
		[field: SerializeField] public float AscentTime { get; private set; } = .2f;
		[field: SerializeField] public float MaxJumpForce { get; private set; } = 1.2f;

		[Header("Decent:")]
		[field: SerializeField] public AnimationCurve DescentCurve { get; private set; }
		[field: SerializeField] public float DescentTime { get; private set; } = .3f;
		[field: SerializeField] public float VerticalTerminalVelocity { get; private set; } = 2f;

		[Header("Horizontal Moving:")]
		[field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; }
		[field: SerializeField] public float AccelerationTime { get; private set; } = 2f;
		[field: SerializeField] public float WalkSpeed { get; private set; } = 5f;
		[field: SerializeField] public float SprintSpeed { get; private set; } = 8f;

		[Header("Camera:")]
		[field: SerializeField] public float Sensitivity { get; private set; } = 1.0f;
	}
}
