using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMover : FoxMover
{
	private Joystick _joystick;

	public void SetReferences(Joystick joyStick)
	{
		_joystick = joyStick;
	}

	private void Start()
	{
		
		_speed = PlayerPrefs.GetFloat("FoxSpeed", 30);
	}

	private void OnEnable()
	{
		SpeedChanged += UpdateSpeed;
	}

	private void OnDisable()
	{
		SpeedChanged -= UpdateSpeed;
	}

	protected override void Update()
	{
		CheckPlayerPosition();
		if (_isControlled == true)
		{
			Vector2 moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
			_velocity = moveInput * _speed;
			_rigidBody.MovePosition(_rigidBody.position + _velocity * Time.deltaTime);
		}
	}

	private void UpdateSpeed()
	{
		_speed = PlayerPrefs.GetFloat("FoxSpeed");
	}
}
