using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveForm : FlyingObject
{
	[SerializeField] private AudioSource _enableSound;
	[SerializeField] private int _damage;

	private int _angle = 0;
	private int _upDownSpeed = 15;
	private float _upDownRange = 0.5f;

	private void OnEnable()
	{
		_enableSound.Play();
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out HealthCompanent objHealths))
		{
			objHealths.GetDamage(_damage);
			gameObject.SetActive(false);
		}
	}

	protected override void FixedUpdate()
	{
		_angle += _upDownSpeed;
		_rigidBody2D.velocity = (_direction + new Vector2(0, Mathf.Sin(_angle * Mathf.Deg2Rad)) * _upDownRange) * _speed;
	}
}
