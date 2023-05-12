using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrb : MovingAreas
{
	[SerializeField] private int _damage;
	[SerializeField] private float _damageDelay;

	[SerializeField] private AudioSource _attackedSound;

	private float _currentDamageDelay = 0;

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out HealthCompanent healthCompanent))
		{
			if (_currentDamageDelay <= 0)
			{
				_attackedSound.Play();
				healthCompanent.GetDamage(_damage);
				_currentDamageDelay = _damageDelay;
			}
		}
	}

	protected override void FixedUpdate()
	{
		_time += Time.fixedDeltaTime;

		if (_currentDamageDelay > 0)
		{
			_currentDamageDelay -= Time.fixedDeltaTime;
		}

		var velocity = (_targetPoint - transform.position);
		if (_time >= _changeTargetTimer)
		{
			_targetPoint = GetRandomMapPoint();
			_time = 0;
		}
		else if (velocity.magnitude < 0.5f)
		{
			_targetPoint = GetRandomMapPoint();
		}
		else
		{
			_rigidBody.velocity = velocity.normalized * _speed;
		}
	}
}
