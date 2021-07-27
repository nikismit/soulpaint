using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

using Input = OVRInput;
using Button = OVRInput.Button;
using Axis2D = OVRInput.Axis2D;


public class OculusRotator : WaitForPlayAreaAwake
{
	[SerializeField]
	private Mode _mode;

    [SerializeField]
    private float _rotationPerStep = 1;

	[SerializeField]
	private float _thumbstickThreshold = 0.75f;

	[SerializeField]
	private float _thumbstickCooldown = 0.5f;

	[SerializeField]
	private HandSide _handSide;

	[SerializeField]
	private Transform _playArea;

	[SerializeField]
	private Transform _head;

	private float _nextUse = 0;

	#region Initialization

	private bool _enabled = false;

	private void Start ()
	{
		if (PlayArea.wasInitialized)
			OnPlayAreaAwake (PlayArea.instance);
	}

	public override void OnPlayAreaAwake (PlayArea playArea)
	{
		//if (playArea.type != PlayAreaType.OCULUS) {
		//	Destroy (this);
		//	return;
		//}

		_enabled = true;
	}

	#endregion

	private void Update ()
	{
		if (!_enabled)
			return;

		bool both = _mode == Mode.BOTH;

		if (both || _mode == Mode.BUTTONS) {
			_updateButtonsInput ();
		}

		if (both || _mode == Mode.THUMBSTICKS) {
			_updateThumbstickInput ();
		}
	}

	// Buttons input

	private void _updateButtonsInput ()
	{
		bool pressed = false;

		if (_handSide == HandSide.LEFT) {
			pressed = Input.GetDown (Button.Three) || Input.GetDown (Button.Four);
		} else {
			pressed = Input.GetDown (Button.One) || Input.GetDown (Button.Two);
		}

		if (pressed) {
			_applyRotation ();
		}
	}

	// Thumbstick input

	private void _updateThumbstickInput ()
	{
		if (Time.time <= _nextUse)
			return;

		Vector2 axis = Vector2.zero;

		if (_handSide == HandSide.LEFT)
			axis = Input.Get (Axis2D.PrimaryThumbstick);
		else
			axis = Input.Get (Axis2D.SecondaryThumbstick);

		if (axis.x > _thumbstickThreshold) {
			_applyThumbstickRotation (false);
		} else if (axis.x < -_thumbstickThreshold) {
			_applyThumbstickRotation (true);
		}
	}

	private void _applyThumbstickRotation (bool left)
	{
		_applyRotation (left);
		_nextUse = Time.time + _thumbstickCooldown;
	}

	// Rotation

	/// <summary>
	/// Apply a rotation step.
	/// </summary>
	private void _applyRotation ()
	{
		_applyRotation (_handSide == HandSide.LEFT);
	}

	/// <summary>
	/// Apply a rotation step
	/// </summary>
	/// <param name="left">If true we rotate to the left, if false to the right</param>
	private void _applyRotation (bool left)
	{
		float rotation = left ? -_rotationPerStep : _rotationPerStep;

		_playArea.transform.RotateAround (_head.transform.position, new Vector3 (0, 2, 0), rotation);
	}

	public enum Mode
	{
		THUMBSTICKS,
		BUTTONS,
		BOTH
	}

	public enum HandSide
	{
		LEFT,
		RIGHT
	}
}
