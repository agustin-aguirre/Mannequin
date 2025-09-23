namespace Mannequin.Configurators
{
	public interface IPlayerInputConfig
	{
		float HorizontalLookSensitivityMultiplier { get; }
		bool InvertHorizontalLookAxis { get; }
		bool InvertVerticalLookAxis { get; }
		float VerticalLookSensitivityMultiplier { get; }
	}
}