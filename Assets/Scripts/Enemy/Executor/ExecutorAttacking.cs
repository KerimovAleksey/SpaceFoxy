using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutorAttacking : BossesStandartAttackComponent
{
	[SerializeField] private HealthCompanent _healthCompanent;

	[SerializeField] private ObjectPool _waveFormObjPool;
	[SerializeField] private ObjectPool _waveFormSpawnSignPool;

	[SerializeField] private ObjectPool _followAreasPool;

	[SerializeField] private SecondPhase _secondPhase;

	private void OnEnable()
	{
		_healthCompanent.OnHalfOfHealth += ChangeBossPhase;
	}

	private void OnDisable()
	{
		_healthCompanent.OnHalfOfHealth -= ChangeBossPhase;
	}

	private void Awake()
	{
		SetSAPostDelay(3);
		SetStandartAttackDelay(1);
		SetStandartMeteorsCount(3);
		SetSpawnMeteorDelay(0.7f);
		SetSATimes(new int[] { 10, 30, 50});
	}

	protected override void MakeSPecialAttack(int step)
	{
		switch (step)
		{
			case 0:
				int[] yPos = new int[] { 3, 0, -3 };
				SetSAPostDelay(9);
				StartCoroutine(InvokeWaveFormAttack(5, yPos, 2));
				break;
			case 1:
				SetSAPostDelay(12);
				StartCoroutine(MeteorRain(10));
				break;
			case 2:
				SetSAPostDelay(20);
				StartCoroutine(SpawnFollowDamageSigns(new int[] { 8, 12, 16 }));
				break;
		}
	}

	private IEnumerator InvokeWaveFormAttack(int lineCount, int[] yPos, int waveCount)
	{
		for (int j = 0; j < waveCount; j++)
		{
			for (int i = 0; i < lineCount; i++)
			{
				foreach (var y in yPos)
				{
					ActivateSpawnSign(new Vector3(4, y, 0), _waveFormSpawnSignPool);
					ActivateObject(new Vector3(4, y, 0), new Vector3(-4, y, 0), _waveFormObjPool);
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(1);
			yPos = new int[] { -4, -1, 2};
		}
	}

	private IEnumerator MeteorRain(int wavesCount)
	{
		float[] xPos = new float[] { -12.5f,-11, -9.5f, -8, -6.5f, -5, -3.5f, -2, -0.5f, 1, 2.5f, 4};

		foreach (var x in xPos)
		{
			ActivateSpawnSign(new Vector3(x, -5, 0));
		}

		yield return new WaitForSeconds(0.9f);

		for (int i = 0; i < wavesCount; i++)
		{
			int skipWaveNumber = Random.Range(0, xPos.Length);
			for (int j = 0; j < xPos.Length; j++)
			{
				if (j != skipWaveNumber && j != xPos.Length)
				{
					ActivateObject(new Vector3(xPos[j], -7, 0), new Vector3(xPos[j], 5, 0), StandartMeteorsPool);
				}
			}
			yield return new WaitForSeconds(1);
		}
	}

	private IEnumerator SpawnFollowDamageSigns(int[] objCounts)
	{
		for (int j = 0; j < objCounts.Length; j++)
		{
			Vector3[] positions = new Vector3[objCounts[j]];

			for (int i = 0; i < objCounts[j]; i++)
			{
				var pos = GetRandomMapPosition();
				positions[i] = pos;
				ActivateSpawnSign(pos);
			}
			StartCoroutine(SpawnFollowDamageAreas(0.5f, positions));
			yield return new WaitForSeconds(4);
		}
	}

	private IEnumerator SpawnFollowDamageAreas(float delay, Vector3[] spawnPositions)
	{
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < spawnPositions.Length; i++)
		{
			var obj = _followAreasPool.GetObject();
			obj.SetActive(true);
			obj.GetComponent<FollowDamageArea>().SetTarget(Fox.gameObject);
			obj.transform.position = spawnPositions[i];
		}
	}

	private void ChangeBossPhase()
	{
		_secondPhase.enabled = true;
		StopAllCoroutines();
		enabled = false;
	}
}
