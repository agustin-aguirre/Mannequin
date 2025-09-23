using UnityEngine;


namespace Mannequin.Vision
{
	public interface ISightSystem
	{
		Transform Transform { get; }
		Vector3 LookDirection { get; }
		bool IsLocked { get; set; }
		void LookAt(Transform target);
	}
}
