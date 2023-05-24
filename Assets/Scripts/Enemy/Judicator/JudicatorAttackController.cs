using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class JudicatorAttackController : BossesStandartAttackComponent
{
	[SerializeField] private ObjectPool _pulsePool;
	[SerializeField] private GameObject _emitterMissile;
	[SerializeField] private GameObject _damageOrb;
	[SerializeField] private GameObject _orbsSpawnSigns;

	private void Awake()
	{
		SetSAPostDelay(3);
		SetStandartAttackDelay(1);
		SetStandartMeteorsCount(2);
		SetSATimes(new int[] { 10, 20, 30, 40, 50, 60, 70});
	}

	protected override void MakeSPecialAttack(int step)
	{
		switch (step)
		{
			case 0:
				StartCoroutine(RapidFire(10, 1.5f));
				break;
			case 1:
				SetSAPostDelay(6);
				int[] positionsY = new int[] {-4, -3, -2, -1, 0, 1, 2, 3, 4};
				int[] reversedPositionsY = new int[] {4 ,3 ,2 ,1 ,0 ,-1 ,-2 ,-3 ,-4};
				StartCoroutine(CascadeAttackSigns(positionsY, reversedPositionsY));
				StartCoroutine(CascadeAttackMeteors(positionsY, reversedPositionsY));
				break;
			case 2:
				SetSAPostDelay(4);
				StartCoroutine(SpawnEmitters(new int[] {-3, 0, 3}));
				break;
			case 3:
				SetSAPostDelay(14);
				StartCoroutine(SpawnWaveLineAttack(5));
				break;
			case 4:
				SetSAPostDelay(5);
				StartCoroutine(RapidFire(20, 1.5f));
				break;
			case 5:
				SetSAPostDelay(3);
				SpawnOrbsSigns(3);
				break;
			case 6:
				SetSAPostDelay(1);
				SetStandartMeteorsCount(5);
				Invoke("ChangeMeteorsCount", 9);
				break;
		}
	}

	private IEnumerator RapidFire(int count, float delay)
	{
		Vector3[] positions = new Vector3[count];
		for (int i = 0; i < count; i++)
		{
			var pos = GetRandomMapPosition();
			positions[i] = pos;
			ActivateSpawnSign(pos);
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(delay);

		foreach (var pos in positions)
		{
			ActivateObject(pos, Fox.transform.position, _pulsePool);
		}
	}

	private IEnumerator CascadeAttackSigns(int[] positions, int[] reversed)
	{
		for (int i = 0; i < positions.Length; i++)
		{
			var position = new Vector3(4, positions[i], 0);
			ActivateSpawnSign(position);
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(1);
		
		for (int i = 0; i < reversed.Length; i++)
		{
			var position = new Vector3(4, reversed[i], 0);
			ActivateSpawnSign(position);
			yield return new WaitForSeconds(0.2f);
		}

	}
	private IEnumerator CascadeAttackMeteors(int[] positions, int[] reversed)
	{
		yield return new WaitForSeconds(0.9f);
		for (int i = 0; i < positions.Length; i++)
		{
			var position = new Vector3(4, positions[i], 0);
			ActivateObject(position, Fox.transform.position, StandartMeteorsPool);
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(1);
		for (int i = 0; i < reversed.Length; i++)
		{
			var position = new Vector3(4, reversed[i], 0);
			ActivateObject(position, Fox.transform.position, StandartMeteorsPool);
			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator SpawnEmitters(int[] yPositions)
	{
		for (int j = -1; j <= 1; j+=2)
		{
			for (int i = 0; i < yPositions.Length; i++)
			{
				var obj = Instantiate(_emitterMissile);
				obj.GetComponent<Emitter>().SetDirection(j);
				obj.transform.position = new Vector3(4 * j, yPositions[i], 0);
				yield return new WaitForSeconds(1);
			}
			Array.Reverse(yPositions);
		}
	}

	private IEnumerator SpawnWaveLineAttack(int countInOneLine)
	{
		int[] firstWaveYPos = new int[] { -4, -2, 0, 2, 4};
		int[] secondWaveYPos = new int[] { -3, -1, 1, 3};

		for (int i = 0; i < 2; i++)
		{
			foreach (var yPos in firstWaveYPos)
			{
				var position = new Vector3(4, yPos, 0);
				ActivateSpawnSign(position);
			}
			yield return new WaitForSeconds(0.9f);

			for (int j = 0; j < countInOneLine; j++)
			{
				foreach (var yPos in firstWaveYPos)
				{
					var position = new Vector3(4, yPos, 0);
					ActivateObject(position, new Vector3(-10, yPos, 0), StandartMeteorsPool);
				}
				yield return new WaitForSeconds(0.35f);
			}

			foreach (var yPos in secondWaveYPos)
			{
				var position = new Vector3(4, yPos, 0);
				ActivateSpawnSign(position);
			}
			yield return new WaitForSeconds(0.9f);

			for (int j = 0; j < countInOneLine; j++)
			{
				foreach (var yPos in secondWaveYPos)
				{
					var position = new Vector3(4, yPos, 0);
					ActivateObject(position, new Vector3(-10, yPos, 0), StandartMeteorsPool);
				}
				yield return new WaitForSeconds(0.35f);
			}
		}
	}

	private void SpawnOrbsSigns(int count)
	{
		Vector3[] positions = new Vector3[count];

		for (int i = 0; i < count; i++)
		{
			var pos = GetRandomMapPosition();
			var sign = Instantiate(_orbsSpawnSigns);
			sign.transform.position = pos;
			positions[i] = pos;
		}

		StartCoroutine(SpawnOrbs(count, positions));
	}

	private IEnumerator SpawnOrbs(int count, Vector3[] positions)
	{
		yield return new WaitForSeconds(0.9f);
		for (int i = 0; i < count; i++)
		{
			var obj = Instantiate(_damageOrb);
			obj.GetComponent<MovingAreas>().ChangeDisableTimer(12);
			obj.SetActive(true);
			obj.transform.position = positions[i];
		}
	}

	private void ChangeMeteorsCount()
	{
		SetStandartMeteorsCount(2);
	}

	private void OnDestroy()
	{
		DataManager.GameDataInfo.AchievementsReceived["Keep going!"] = true;
	}
}
