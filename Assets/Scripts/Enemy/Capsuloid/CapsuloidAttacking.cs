using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CapsuloidAttacking : BossesStandartAttackComponent
{
	private List<Vector3> _meteorPositions = new List<Vector3>();

	private int _saPostDelay = 3;
	private void Awake()
	{
		SetSAPostDelay(_saPostDelay);
		SetStandartAttackDelay(1);
	}

	protected override void MakeSPecialAttack(int step)
	{
		switch (step)
		{
			case 0:
				for (int i = -12; i <= 4; i += 2)
				{
					_meteorPositions.Add(new Vector3(i, -4, 0));
				}
				break;
			case 1:
				_saPostDelay += 1;
				SetSAPostDelay(_saPostDelay);
				for (int i = -12; i <= 4; i += 2)
				{
					_meteorPositions.Add(new Vector3(i, 4, 0));
				}
				break;
			case 2:
				_saPostDelay += 3;
				SetSAPostDelay(_saPostDelay);
				for (int i = -4; i <= 4; i += 2)
				{
					_meteorPositions.Add(new Vector3(4, i, 0));
				}
				break;
		}
		StartCoroutine(SpawnAttackWarnings());
		StartCoroutine(SpawnMeteorites());
	}

	private IEnumerator SpawnAttackWarnings()
	{
		foreach (var pos in _meteorPositions)
		{
			ActivateSpawnSign(pos);
			yield return new WaitForSeconds(0.15f);
		}
	}

	private IEnumerator SpawnMeteorites()
	{
		yield return new WaitForSeconds(0.9f);

		foreach (var pos in _meteorPositions)
		{
			Vector3 direction;

			if (pos.y == 4)
			{
				direction = new Vector3(pos.x, -pos.y, pos.z);
			}
			else if (pos.y == -4)
			{
				direction = new Vector3(pos.x, -pos.y, pos.z);
			}
			else
			{
				direction = new Vector3(-pos.x, pos.y, pos.z);
			}

			ActivateObject(pos, direction, StandartMeteorsPool);
			yield return new WaitForSeconds(0.15f);
		}
	}

	private void OnDestroy()
	{
		DataManager.GameDataInfo.AchievementsReceived["The beginning of a journey"] = true;
	}
}
