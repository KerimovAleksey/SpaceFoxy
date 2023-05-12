using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSounds : MonoBehaviour
{
    [SerializeField] private BlackHoleHealths _healthComponent;
	[SerializeField] private AudioSource _onGetDamageSound;
	[SerializeField] private Animator _animator;

	private void OnEnable()
	{
		_healthComponent.OnGetDamage += PlayGetDamageSound;
	}

	private void OnDisable()
	{
		_healthComponent.OnGetDamage -= PlayGetDamageSound;
	}

	private void PlayGetDamageSound()
	{
		_animator.speed += 0.5f;
		_onGetDamageSound.Play();
	}

}
