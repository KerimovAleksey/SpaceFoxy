using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : FlyingObject
{
	[SerializeField] private int _damage;
	[SerializeField] private AudioSource _enableSound;
	private void OnEnable()
	{
		_enableSound.Play();
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out HealthCompanent healthCompanent))
		{
			healthCompanent.GetDamage(_damage);
			gameObject.SetActive(false);
		}
	}
}
