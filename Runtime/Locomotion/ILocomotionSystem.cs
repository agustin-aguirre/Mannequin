using UnityEngine;


namespace Mannequin.Locomotion
{
	// WIP => ADD MORE METHODS AS NEEDED IN THE FUTURE
	public interface ILocomotionSystem
	{
		Transform Transform { get; }
		bool IsLocked { get; set; }
	}
}
