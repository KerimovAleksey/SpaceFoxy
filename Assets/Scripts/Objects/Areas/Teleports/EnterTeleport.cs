using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTeleport : MovingAreas
{
	private GameObject _exitTeleport;

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FlyingObject obj))
		{
			obj.transform.position = _exitTeleport.transform.position;
		}
		else if (collision.TryGetComponent(out FoxMover fox))
		{
			fox.transform.position = _exitTeleport.transform.position;
		}
	}

	public void SetExitTeleport(GameObject exitTeleport)
	{
		_exitTeleport = exitTeleport;
	}
}
