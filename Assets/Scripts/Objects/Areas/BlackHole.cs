using System.Collections;
using UnityEngine;

public class BlackHole : MovingAreas
{
	[SerializeField] private int _damage;
	[SerializeField] private float _damageDelay;
	[SerializeField] private HealthCompanent _bossHealths;

	[SerializeField] private AudioSource _attackedSound;

	private float _currentDamageDelay = 0;

	protected override void OnEnable()
	{
		_speed = _startSpeed;
		transform.position = GetRandomMapPoint();
		StartCoroutine(PereodicHeal());
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FoxHealths healthCompanent))
		{
			if (_currentDamageDelay <= 0)
			{
				_attackedSound.Play();
				healthCompanent.GetDamage(_damage);
				_currentDamageDelay = _damageDelay;
				_bossHealths.GetHeal(4);
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

	protected override Vector3 GetRandomMapPoint()
	{
		return new Vector3(Random.Range(-8, 4), Random.Range(-4.5f, 4.5f), 0);
	}


	private IEnumerator PereodicHeal()
	{
		while (gameObject.activeSelf == true)
		{
			_bossHealths.GetHeal(1);
			yield return new WaitForSeconds(2);
		}
	}
}
