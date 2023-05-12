using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : FlyingObject
{
	[SerializeField] private int _score;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out ScoreCompanent scoreCompanent))
		{
			scoreCompanent.GetScore(_score);
			gameObject.SetActive(false);
		}
	}
}
