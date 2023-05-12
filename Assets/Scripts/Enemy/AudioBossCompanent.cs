using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBossCompanent : MonoBehaviour
{
	// Enable
	[SerializeField] private AudioSource _enableSound;
	// Dead
	[SerializeField] private AudioSource _deadSound;
	[SerializeField] private BossHealthCompanent _healthCompanent;
	// KickOff
	[SerializeField] private AudioSource _kickOffSound;
	[SerializeField] private KickZone _kickZone;
	// Special attack
	[SerializeField] private AudioSource _specialAttackSound;
	[SerializeField] private BossesStandartAttackComponent _attackingController;
	// Get damage
	[SerializeField] private AudioSource _collisionSound;

	private void OnEnable()
	{
		_enableSound.Play();
		_healthCompanent.OnBossDead += PlayDeadSound;
		_kickZone.OnKickOff += PlayKickOffSound;
		_attackingController.OnSpecialAttack += PlaySpecialAttackSound;
		_healthCompanent.OnGetDamage += PlayGetDamageSound;
	}

	private void OnDisable()
	{
		_healthCompanent.OnBossDead -= PlayDeadSound;
		_kickZone.OnKickOff -= PlayKickOffSound;
		_attackingController.OnSpecialAttack -= PlaySpecialAttackSound;
		_healthCompanent.OnGetDamage -= PlayGetDamageSound;
	}

	private void PlayDeadSound()
	{
		_deadSound.Play();
	}

	private void PlayKickOffSound()
	{
		_kickOffSound.Play();
	}

	private void PlaySpecialAttackSound()
	{
		_specialAttackSound.Play();
	}

	private void PlayGetDamageSound()
	{
		_collisionSound.Play();
	}
}
