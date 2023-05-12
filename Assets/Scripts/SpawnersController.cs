using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
	[SerializeField] private Spawner[] _spawnerList;
	[SerializeField] private Spawner _meteorsSpawner;
	[SerializeField] private GameSpeedController _gameSpeedController;

	private void OnEnable()
	{
		_gameSpeedController.BossDeaD += UpMeteorsCount;
	}

	private void OnDisable()
	{
		_gameSpeedController.BossDeaD -= UpMeteorsCount;
	}

	private void UpMeteorsCount()
	{
		_meteorsSpawner.UpOnOneMeteorsCount();
	}

	public void DisableAllSpawners()
	{
		foreach (var spawner in _spawnerList)
		{
			spawner.StopSpawning();
		}
	}

	public void EnableAllSpawners()
	{
		foreach (var spawner in _spawnerList)
		{
			spawner.StartSpawning();
		}
	}
}
