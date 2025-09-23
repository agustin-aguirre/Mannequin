using UnityEngine;


namespace Mannequin.Configurators
{
	[CreateAssetMenu(fileName = "New Player Inputs Config", menuName = "Mannequin/Inputs Config")]
	public class PlayerInputsConfig : ScriptableObject, IPlayerInputConfig
	{
		[field: SerializeField] public bool InvertHorizontalLookAxis { get; set; } = false;
		[field: SerializeField] public bool InvertVerticalLookAxis { get; set; } = false;
		[field: SerializeField, Min(.01f)] public float HorizontalLookSensitivityMultiplier { get; set; } = 1f;
		[field: SerializeField, Min(.01f)] public float VerticalLookSensitivityMultiplier { get; set; } = 1f;
	}
}
