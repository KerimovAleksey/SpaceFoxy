using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerHoldMover : FoxMover
{
	private float _tapRange = 1;

	private bool _isPressed = false;

	private void Start()
	{
		_speed = 0;
	}

	protected override void Update()
	{
		CheckPlayerPosition();
		if (_isControlled == true)
		{
			if (Input.GetMouseButton(0))
			{
				if (ClickInFoxRange(GetClickPosition()) == true)
				{
					_isPressed = true;
				}
			}
			else
			{
				_isPressed = false;
			}
			if (_isPressed == true)
			{
				transform.position = GetClickPosition();
			}
		}
		else
		{
			_isPressed = false;
		}
	}

	private Vector2 GetClickPosition()
	{
		var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector2(clickPos.x, clickPos.y);
	}

	private bool ClickInFoxRange(Vector2 clickPos)
	{
		var distance = (clickPos - new Vector2(transform.position.x, transform.position.y)).magnitude;
		return distance < _tapRange;
	}
}
