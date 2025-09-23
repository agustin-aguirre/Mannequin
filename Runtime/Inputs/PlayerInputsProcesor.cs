using Mannequin.Configurators;
using Matemagicas.Extensions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Mannequin.Inputs
{
	public class PlayerInputsProcesor : MonoBehaviour, IObservable3dPlayerIntentsProvider
	{
		[SerializeField] PlayerInputsConfig config;
		[SerializeField] PlayerIntents output;

		[Header("Readings:")]
		[field: SerializeField, Range(-1, 1)] public float ChangeValue { get; set; } = default;
		[field: SerializeField] public bool Jump { get; set; } = default;
		[field: SerializeField] public Vector2 Look { get; set; } = default;
		[field: SerializeField] public Vector2 MoveHorizontally { get; set; } = default;
		[field: SerializeField] public Vector2 MoveVertically { get; set; } = default;
		[field: SerializeField] public bool Pause { get; set; } = default;
		[field: SerializeField] public bool Primary { get; set; } = default;
		[field: SerializeField] public bool Secondary { get; set; } = default;
		[field: SerializeField] public bool Sprint { get; set; } = default;
		[field: SerializeField] public bool TabsMenu { get; set; } = default;
		[field: SerializeField] public bool ToolWheel { get; set; } = default;

		public event Action<Vector2> OnMoveHorizontallyInput;
		public event Action<Vector2> OnMoveVerticallyInput;
		public event Action<bool> OnJumpInput;
		public event Action<bool> OnSprintInput;
		public event Action<Vector2> OnLookInput;
		public event Action<bool> OnPrimaryInput;
		public event Action<bool> OnSecondaryInput;
		public event Action<float> OnChangeValueInput;
		public event Action<bool> OnTabsMenuInput;
		public event Action<bool> OnToolWheelInput;
		public event Action<bool> OnPauseInput;


		private void Awake()
		{
			output.Source = this;
		}


		// PlayerInput events
		public void OnMove(InputValue value)
		{
			MoveHorizontally = value.Get<Vector2>();
			OnMoveHorizontallyInput?.Invoke(MoveHorizontally);
		}

		public void OnLook(InputValue value)
		{
			Look = value.Get<Vector2>()
				.MultX(config.HorizontalLookSensitivityMultiplier * (config.InvertHorizontalLookAxis ? -1 : 0))
				.MultY(config.VerticalLookSensitivityMultiplier * (config.InvertVerticalLookAxis ? -1 : 0));
			OnLookInput?.Invoke(Look);
		}

		public void OnJump(InputValue value)
		{
			Jump = value.isPressed;
			OnJumpInput?.Invoke(Jump);
		}

		public void OnSprint(InputValue value)
		{
			Sprint = value.isPressed;
			OnSprintInput?.Invoke(Sprint);
		}

		public void OnPrimary(InputValue value)
		{
			Primary = value.isPressed;
			OnPrimaryInput?.Invoke(Primary);
		}

		public void OnSecondary(InputValue value)
		{
			Secondary = value.isPressed;
			OnSecondaryInput?.Invoke(Secondary);
		}

		public void OnChangeToolModeValue(InputValue value)
		{
			float readValue = value.Get<float>();
			ChangeValue = readValue != 0f ? (int)Mathf.Sign(readValue) : 0;
			OnChangeValueInput?.Invoke(ChangeValue);
		}

		public void OnTabsMenu(InputValue value)
		{
			TabsMenu = value.isPressed;
			OnTabsMenuInput?.Invoke(TabsMenu);
		}

		public void OnToolWheel(InputValue value)
		{
			ToolWheel = value.isPressed;
			OnToolWheelInput?.Invoke(ToolWheel);
		}

		public void OnPausita(InputValue value)
		{
			Pause = value.isPressed;
			OnPauseInput?.Invoke(Pause);
		}
	}
}
