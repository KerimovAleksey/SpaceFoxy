using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackHoleHealths : HealthCompanent
{
	[SerializeField] private AudioSource _getDamageSound;
	[SerializeField] private ShieldBarController _shieldBarController;

	public event UnityAction OnTheEndEvent;
	public event UnityAction OnTheBlackHoleStart;

	private int _try = 1;
	private float _currentMaxHP;

	protected override void OnEnable()
	{
		_currentMaxHP = _maxHealth * _try;
		_health = (int)_currentMaxHP;
		OnTheBlackHoleStart?.Invoke();
		HealthChanged += OnGetDamageAction;

		_shieldBarController.Enable();
	}

	protected override void OnTheEnd()
	{
		OnTheEndEvent?.Invoke();
		_try++;
		gameObject.SetActive(false);
		HealthChanged -= OnGetDamageAction;

		_shieldBarController.Disable();
	}

	private void OnGetDamageAction(float damage)
	{
		_getDamageSound.Play();

		_shieldBarController.ChangeValue(damage);
	}

	public float GetCurrentMaxHealth()
	{
		return _currentMaxHP;
	}
}
