using System.Collections;
using UnityEngine;

public class SecondPhase : BossesStandartAttackComponent
{
    [SerializeField] private AudioSource _secondPhaseEnabledSound;
	[SerializeField] private BlackHoleHealths _blackHoleHealths;

	[SerializeField] private GameObject _blackHole;

	[SerializeField] private float _blackHoleSpawnInterval;
	[SerializeField] private ObjectPool _followAreasPool;

	[SerializeField] private GameObject _enterTeleport;
	[SerializeField] private GameObject _exitTeleport;

	[SerializeField] private ObjectPool _spawningOrbsPool;
	[SerializeField] private float _spawningOrbsCount;
	[SerializeField] private float _spawningOrbsDelay;

	[SerializeField] private ObjectPool _exposionOrbsPool;

	private BossHealthCompanent _healthComponent;

	private void Awake()
	{	
		_healthComponent = GetComponent<BossHealthCompanent>();
		SetSAPostDelay(3);
		SetStandartAttackDelay(1.25f);
		SetStandartMeteorsCount(2);
		SetSATimes(new int[] { 5, 25, 40, 80, 100});
	}

	private void OnEnable()
	{
		_blackHoleHealths.OnTheEndEvent += DelayInvokeOfBlackHole;
		_secondPhaseEnabledSound.Play();
		ActivateBlackHole();
		Debug.Log("2 фаза");
	}

	private void OnDisable()
	{
		_blackHoleHealths.OnTheEndEvent -= DelayInvokeOfBlackHole;
	}

	private void DelayInvokeOfBlackHole()
	{
		Invoke("ActivateBlackHole", _blackHoleSpawnInterval);
	}

	private void ActivateBlackHole()
	{
		_blackHole.SetActive(true);
	}
	protected override void MakeSPecialAttack(int step)
	{
		switch (step)
		{
			case 0:
				SetSAPostDelay(20);
				StartCoroutine(SpawnFollowDamageSigns(new int[] {6, 10, 14, 18}));
				break;
			case 1:
				SetSAPostDelay(3);
				SpawnTelepots();
				break;
			case 2:
				SetStandartMeteorsCount(GetCurrentMeteorCount() + 1);
				break;
			case 3:
				SetSAPostDelay(40);
				StartCoroutine(ActivateSpawningOrbs());
				break;
			case 4:
				SetSAPostDelay(8);
				StartCoroutine(SpawnExplosionOrbs(15));
				break;
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

	private void SpawnTelepots()
	{
		var exitTeleport = Instantiate(_exitTeleport);
		_exitTeleport.transform.position = GetRandomMapPosition();
		var enterTeleport = Instantiate(_enterTeleport);
		_enterTeleport.transform.position = GetRandomMapPosition();
		enterTeleport.GetComponent<EnterTeleport>().SetExitTeleport(exitTeleport);
	}

	private IEnumerator ActivateSpawningOrbs()
	{
		for (int i = 0; i < _spawningOrbsCount; i++)
		{
			var pos = GetRandomMapPosition();
			ActivateSpawnSign(pos);
			yield return new WaitForSeconds(0.9f);
			SpawnSpawningOrb(pos, 15 + 5 * i, i + 1);
			yield return new WaitForSeconds(_spawningOrbsDelay);
		}
	}

	private void SpawnSpawningOrb(Vector3 pos, int objCount, int oneTimeWavesCount)
	{
		var obj = _spawningOrbsPool.GetObject();
		obj.SetActive(true);
		obj.GetComponent<SpawningObj>().SetReferences(StandartMeteorsPool, SpawnSignsPool, GameSpeedController);
		obj.GetComponent<SpawningOrbRadial>().SetWavesValues(objCount, 3, 1, oneTimeWavesCount);
		obj.transform.position = pos;
	}

	private IEnumerator SpawnExplosionOrbs(int count)
	{
		SpawnOrb();
		yield return new WaitForSeconds(3);
		for (int i = 0; i < count - 1; i++)
		{
			SpawnOrb();
			yield return new WaitForSeconds(Random.Range(0.1f, 2f));
		}
	}

	private void SpawnOrb()
	{
		var obj = _exposionOrbsPool.GetObject();
		obj.SetActive(true);
		obj.transform.position = GetRandomMapPosition();
	}

	private void OnDestroy()
	{
		if (_healthComponent.GetCurentHealth() <= 0)
		{
			DataManager.GameDataInfo.AchievementsReceived["Purposeful"] = true;
			DataManager.GameDataInfo.AchievementsReceived["A small step, a big way"] = true;
		}
	}
}
