using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : FlyingObject
{
	[SerializeField] private int _damage;
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out HealthCompanent healthCompanent))
		{
			healthCompanent.GetDamage(_damage);
			gameObject.SetActive(false);
		}
	}
}
