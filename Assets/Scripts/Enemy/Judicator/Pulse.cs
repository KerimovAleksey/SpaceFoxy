using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : FlyingObject
{
	[SerializeField] private int _damage;
	[SerializeField] private int _heal;
	[SerializeField] private AudioSource _enableSound;

	private void OnEnable()
	{
		_enableSound.Play();
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FoxHealths foxHealths))
		{
			foxHealths.GetDamage(_damage);
			gameObject.SetActive(false);
		}
		else if (collision.TryGetComponent(out BlackHoleHealths blackHoleHealths))
		{
			blackHoleHealths.GetDamage(_damage);
			gameObject.SetActive(false);
		}
		else if (collision.TryGetComponent(out BossHealthCompanent bossHealths))
		{
			bossHealths.GetHeal(_heal);
			gameObject.SetActive(false);
		}

	}
}
