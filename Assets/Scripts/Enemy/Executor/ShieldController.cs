using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	[SerializeField] private HealthCompanent _healthCompanent;
	[SerializeField] private BlackHoleHealths _blackHoleHealths;

	[SerializeField] private AudioSource _shieldCollided;
	[SerializeField] private AudioSource _shieldEnabled;
	[SerializeField] private AudioSource _shieldDisabled;

	private void OnEnable()
	{
		_blackHoleHealths.OnTheBlackHoleStart += EnableShield;
		_healthCompanent.OnShieldCollision += PlayShieldCollisionSound;
		_blackHoleHealths.OnTheEndEvent += DisableShield;
	}

	private void OnDisable()
	{
		_blackHoleHealths.OnTheBlackHoleStart -= EnableShield;
		_healthCompanent.OnShieldCollision -= PlayShieldCollisionSound;
		_blackHoleHealths.OnTheEndEvent -= DisableShield;
	}

	private void EnableShield()
	{
		_animator.Play("ProtectShieldEnabled");
		_healthCompanent.ChangeProtectionStatus(true);
		_shieldEnabled.Play();
	}

	private void DisableShield()
	{
		_animator.Play("DisableShield");
		_healthCompanent.ChangeProtectionStatus(false);
		_shieldDisabled.Play();
	}

	private void PlayShieldCollisionSound()
	{
		_shieldCollided.Play();
	}
}
