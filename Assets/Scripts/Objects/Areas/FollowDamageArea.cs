using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDamageArea : MovingAreas
{
	[SerializeField] private int _damage;
	[SerializeField] private float _speedUpDelay;

	[SerializeField] private AudioSource _attackedSound;
	[SerializeField] private AudioSource _enabledSound;

	private GameObject _fox;
	private bool _speedUpStageEnabled = false;

	private float _stageChangeTime;
	private Vector3 _velocity = new Vector3();

	protected override void OnEnable()
	{
		_enabledSound.Play();
		_speedUpStageEnabled = false;
		_speed = _startSpeed;
		Invoke("DisableObject", _disableDelay);
		_stageChangeTime = Time.time + _speedUpDelay;
		StartCoroutine(AppearAlpha(3));
	}

	public void SetTarget(GameObject fox)
	{
		_fox = fox;
	}

	protected override void FixedUpdate()
	{
		if (_speedUpStageEnabled == false && Time.time >= _stageChangeTime)
		{
			_speedUpStageEnabled = true;
		}
		if (_fox != null)
		{
			if (_speedUpStageEnabled == false)
			{
				_targetPoint = _fox.transform.position;
				_velocity = _targetPoint - transform.position;
			}
			else
			{
				_speed += 0.1f;
			}
			_rigidBody.velocity = _velocity.normalized * _speed;
		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FoxHealths foxHealths))
		{
			foxHealths.GetDamage(_damage);
			_attackedSound.Play();
			gameObject.SetActive(false);
		}
		else if (collision.TryGetComponent(out BossHealthCompanent bossHealths))
		{
			bossHealths.GetDamage(_damage * 3);
			_attackedSound.Play();
			gameObject.SetActive(false);
		}

	}
}
