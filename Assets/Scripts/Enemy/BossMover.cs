using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMover : MonoBehaviour
{
	[SerializeField] private Transform _targetToMove;
	[SerializeField] private float _speed;
	[SerializeField] private CapsuloidAttacking _nextStage;

	private Rigidbody2D _rigidBody2D;

	private void Awake()
	{
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		if (transform.position.x - _targetToMove.position.x > 1)
		{
			_rigidBody2D.velocity = GetDirection().normalized * _speed;
		}
		else
		{
			_rigidBody2D.velocity = Vector2.zero;
			_nextStage.enabled = true;
			enabled = false;
		}
	}

	private Vector3 GetDirection()
	{
		return _targetToMove.position - transform.position;
	}
}
