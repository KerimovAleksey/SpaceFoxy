using System.Collections;
using UnityEngine;

public class SpawningOrbRadial : SpawningObj
{
	private float _spawnSignRadius = 2;
	private float _targetRadius = 5;
	private int _objCount;
	private int _countOfCircles;
	private float _waveDelay;

	private int _oneTimeWavesCount;

	private float _angle;

	public void SetWavesValues(int objCount, int circlesCount, float waveDelay, int oneTimeWavesCount)
	{
		_objCount = objCount;
		_countOfCircles = circlesCount;
		_waveDelay = waveDelay;
		_oneTimeWavesCount = oneTimeWavesCount;
	}

	protected override IEnumerator SpawnObjects()
	{
		yield return new WaitForSeconds(1.5f);
		for (int j = 0; j < _countOfCircles; j++)
		{
			_angle = Mathf.Deg2Rad * (360f / _objCount);
			StartCoroutine(SpawnObj());
			for (int i = 0; i < _objCount; i++)
			{
				var pos = new Vector3(_spawnSignRadius * Mathf.Cos(_angle * i) + transform.position.x, _spawnSignRadius * Mathf.Sin(_angle * i) + transform.position.y, 0);
				ActivateSpawnSign(pos);
				yield return new WaitForSeconds(0.05f);
			}
			yield return new WaitForSeconds(_waveDelay);
		}
		yield return new WaitForSeconds(3);
		StartCoroutine(OneTimeCircleWaveSigns());

	}

	private IEnumerator SpawnObj()
	{
		yield return new WaitForSeconds(0.9f);
		for (int i = 0; i < _objCount; i++)
		{
			var targetPos = new Vector3(_targetRadius * Mathf.Cos(_angle * i) + transform.position.x, _targetRadius * Mathf.Sin(_angle * i) + transform.position.y, 0);
			var spawnPos = new Vector3(_spawnSignRadius * Mathf.Cos(_angle * i) + transform.position.x, _spawnSignRadius * Mathf.Sin(_angle * i) + transform.position.y, 0);
			ActivateObject(spawnPos, targetPos, _objForSpawnPool);
			yield return new WaitForSeconds(0.05f);
		}
	}

	private IEnumerator OneTimeCircleWaveSigns()
	{
		for (int j = 0; j < _oneTimeWavesCount; j++)
		{
			_angle = Mathf.Deg2Rad * (360f / _objCount);
			StartCoroutine(OneTimeCircleWave());
			for (int i = 0; i < _objCount; i++)
			{
				var pos = new Vector3(_spawnSignRadius * Mathf.Cos(_angle * i) + transform.position.x, _spawnSignRadius * Mathf.Sin(_angle * i) + transform.position.y, 0);
				ActivateSpawnSign(pos);
			}
			yield return new WaitForSeconds(_waveDelay);
		}
		StartCoroutine(Disable(3));
	}

	private IEnumerator OneTimeCircleWave()
	{
		yield return new WaitForSeconds(0.9f);
		for (int i = 0; i < _objCount; i++)
		{
			var targetPos = new Vector3(_targetRadius * Mathf.Cos(_angle * i) + transform.position.x, _targetRadius * Mathf.Sin(_angle * i) + transform.position.y, 0);
			var spawnPos = new Vector3(_spawnSignRadius * Mathf.Cos(_angle * i) + transform.position.x, _spawnSignRadius * Mathf.Sin(_angle * i) + transform.position.y, 0);
			ActivateObject(spawnPos, targetPos, _objForSpawnPool);
		}
	}
}
