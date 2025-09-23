using UnityEngine;

namespace Mannequin.Inputs
{
	public interface IPlayer3dIntentsProvider
	{
		bool Jump { get; }
		bool Sprint { get; }
		Vector2 Look { get; }
		Vector2 MoveHorizontally { get; }
		Vector2 MoveVertically { get; }
	}
}