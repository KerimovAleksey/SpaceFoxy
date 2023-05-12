using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedSounds : MonoBehaviour
{
	[SerializeField] private AudioSource _destructedSoundSource;
	[SerializeField] private AudioSource _blackHoleDestructed;

	[SerializeField] private BossHealthCompanent _capsuleHealthCompanent;
	[SerializeField] private BossHealthCompanent _judicatorHealthCompanent;
	[SerializeField] private BossHealthCompanent _executorHealthCompanent;
	[SerializeField] private BlackHoleHealths _blackHoleHealths;

	[SerializeField] private AudioClip _capsuloidDestructedSoundClip;
	[SerializeField] private AudioClip _judicatorDestructedSoundClip;
	[SerializeField] private AudioClip _executorDestructedSoundClip;

	private Dictionary<string, AudioClip> _bossDestructedSounds;

	private void Start()
	{
		_bossDestructedSounds= new Dictionary<string, AudioClip>()
		{
			{ "Capsuloid", _capsuloidDestructedSoundClip },
			{ "Judicator", _judicatorDestructedSoundClip},
			{ "High Executor", _executorDestructedSoundClip }
		};
	}

	private void OnEnable()
	{
		_capsuleHealthCompanent.OnDesctructed += PlayDestructedSound;
		_judicatorHealthCompanent.OnDesctructed += PlayDestructedSound;
		_executorHealthCompanent.OnDesctructed += PlayDestructedSound;
		_blackHoleHealths.OnTheEndEvent += PlayBlackHoleDestructedSound;
	}

	private void OnDisable()
	{
		_capsuleHealthCompanent.OnDesctructed -= PlayDestructedSound;
		_judicatorHealthCompanent.OnDesctructed -= PlayDestructedSound;
		_executorHealthCompanent.OnDesctructed -= PlayDestructedSound;
		_blackHoleHealths.OnTheEndEvent -= PlayBlackHoleDestructedSound;
	}

	private void PlayDestructedSound(string bossName)
	{
		_destructedSoundSource.clip = _bossDestructedSounds[bossName];
		_destructedSoundSource.Play();
	}

	private void PlayBlackHoleDestructedSound()
	{
		_blackHoleDestructed.Play();
	}
}