using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KickZone : MonoBehaviour
{
	[SerializeField] private HealthCompanent _foxHealths;
	[SerializeField] private int _collisionDamage;
	[SerializeField] private Animator _shield;

	public event UnityAction OnKickOff;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FoxMover foxMover))
		{
			OnKickOff?.Invoke();
			_shield.Play("KickOffIdle");
			foxMover.KickOut();
			_foxHealths.GetDamage(_collisionDamage);
		}
	}
}
