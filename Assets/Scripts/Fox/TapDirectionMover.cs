using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDirectionMover : FoxMover
{
	private Vector3 click_pos;
	private void Start()
	{
		_speed = 7;
	}

	protected override void Update()
	{
		CheckPlayerPosition();
		if (_isControlled == true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				click_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			Vector2 direction = click_pos - transform.position;

			if (direction.magnitude > 0.11f)
			{
				_rigidBody.velocity = direction.normalized * _speed;
			}
			else
			{
				_rigidBody.velocity = Vector2.zero;
			}
		}
	}
}
