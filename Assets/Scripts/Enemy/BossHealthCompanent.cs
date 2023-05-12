using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealthCompanent : HealthCompanent
{
	[SerializeField] private Animator _animator;
	[SerializeField] private SpawnersController _spawnersController;
	[SerializeField] private GameSpeedController _gameSpeedController;

	[SerializeField] private GameObject _bar;
	[SerializeField] private Image _healthBar;
	[SerializeField] private BossesNameLabelCompanent _bossLabel;

	private bool _isDead = false;
	public event UnityAction OnBossDead;
	public event UnityAction<string> OnDesctructed;

	private void Start()
	{
		_bar.SetActive(true);
		_healthBar.fillAmount = 1;
	}

	protected override void OnTheEnd()
	{
		if (_isDead == false)
		{
			OnBossDead?.Invoke();
			_isDead = true;
			_gameSpeedController.OnBossDead();
			StartCoroutine(SpeedUpAnimation());
		}
	}

	private IEnumerator SpeedUpAnimation()
	{
		while (_animator.speed < 20)
		{
			_animator.speed += 0.1f;
			yield return new WaitForEndOfFrame();
		}

		_spawnersController.EnableAllSpawners();
		OnDesctructed?.Invoke(_bossLabel.GetBossName());
		_bar.SetActive(false);

		Destroy(gameObject);
	}

	protected override void OnEnable()
	{
		_health = _maxHealth;
		HealthChanged += SetBossHealthValue;
	}

	private void OnDisable()
	{
		HealthChanged -= SetBossHealthValue;
	}

	private void SetBossHealthValue(float damageValue)
	{
		var delta = -damageValue / _maxHealth;
		_healthBar.fillAmount -= delta;
	}
}
