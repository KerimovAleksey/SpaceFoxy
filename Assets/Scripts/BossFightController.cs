using UnityEngine;

public class BossFightController : MonoBehaviour
{
    [SerializeField] private GameSpeedController _gameSpeedController;
	[SerializeField] private SpawnersController _spawnersController;
    [SerializeField] private GameObject[] _bossList;
	[SerializeField] private Spawner _spawner;

	private int _currentBossNumber;

	private void OnEnable()
	{
		_gameSpeedController.StartBossFight += StartBossFight;
	}

	private void OnDisable()
	{
		_gameSpeedController.StartBossFight -= StartBossFight;
	}

	private void StartBossFight(int bossNumber)
	{
		_currentBossNumber = bossNumber;
		_spawnersController.DisableAllSpawners();
		Invoke("EnableBoss", 6);
	}

	private void EnableBoss()
	{
		_bossList[_currentBossNumber].SetActive(true);
	}
}
 