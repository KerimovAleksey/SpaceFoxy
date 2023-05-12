using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoxHealths : HealthCompanent
{
	[SerializeField] private MenuController _menuController;
	[SerializeField] private GameObject _gameOverMenuPanel;

	[SerializeField] private AudioSource _getDamageSound;

	protected override void OnEnable()
	{
		OnGetDamage += PlayGetDamageSound;
		_health = _maxHealth;
	}

	private void OnDisable()
	{
		OnGetDamage -= PlayGetDamageSound;
	}

	private void PlayGetDamageSound()
	{
		_getDamageSound.Play();
	}

	protected override void OnTheEnd()
	{
		_menuController.ChangePanelEnabled(_gameOverMenuPanel);
	}
}
