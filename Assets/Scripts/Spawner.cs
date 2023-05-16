using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _fox;
    [SerializeField] private ObjectPool _objPool;
	[SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private GameSpeedController _gameSpeedController;

	[SerializeField] private float _spawnDelay;

    private float _currentTimeDelay;
    private bool _canSpawn = true;
    private int _objCountInOneTime = 1;

	private void Start()
	{
        _currentTimeDelay = _spawnDelay;
		StartSpawning();
	}
    public void StartSpawning()
    {
        _canSpawn = true;
		StartCoroutine(Spawn());
	}

    public void StopSpawning()
    {
		_canSpawn = false;
    }

    private IEnumerator Spawn()
    {
        while (_canSpawn == true)
        {
            for (int i = 0; i < _objCountInOneTime; i++)
            {
                ActivateObject();
            }
            if (_currentTimeDelay > 1f)
			    _currentTimeDelay = _spawnDelay - _gameSpeedController.GetCurrentTime() / 200f;
            yield return new WaitForSeconds(_currentTimeDelay);
        }
    }

	private void ActivateObject()
    {
        var obj = _objPool.GetObject();
        obj.SetActive(true);
        var companent = obj.GetComponent<FlyingObject>();
        companent.SetStartValues(GetRandomStartPosition(), _fox.transform.position, _gameSpeedController.GetCurrentFlyObjSpeedUp());
    }

    private Vector2 GetRandomStartPosition()
    {
        Vector2 spawnPos;

        var side = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        if (side.position.x == 0)
        {
            spawnPos = new Vector2(Random.Range(-12, 12), side.position.y);
        }
        else
        {
            spawnPos = new Vector2(side.position.x, Random.Range(-8,8));
        }
        return spawnPos;
    }

    public void UpOnOneMeteorsCount()
    {
        _objCountInOneTime++;
	}
}
