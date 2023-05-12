using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossesStandartAttackComponent : MonoBehaviour
{
	[SerializeField] protected ObjectPool SpawnSignsPool;
	[SerializeField] protected GameSpeedController GameSpeedController;

	[SerializeField] protected ObjectPool StandartMeteorsPool;
	[SerializeField] protected GameObject Fox;

	public event UnityAction OnSpecialAttack;

	private int[] _specialAttackTimes = new int[] { 5, 15, 25 };
	private int _standartMeteorsCount = 1;

	private float _time = 0;
	private int _step = 0;
	private float _spawnMeteorDelay = 0.9f;

	private int _specialAttackPostDelay;
	private float _standartAttackDelay;

	private bool _isSpawning = true;


	private void Start()
	{
		StartCoroutine(Spawning());
	}

	protected virtual IEnumerator Spawning()
	{
		while (_isSpawning == true)
		{
			for (int i = 0; i < _standartMeteorsCount; i++)
			{
				var spawnPosition = GetRandomMapPosition();

				ActivateSpawnSign(spawnPosition);
				StartCoroutine(SpawnObjWithDelay(_spawnMeteorDelay, spawnPosition));
			}

			yield return new WaitForSeconds(_standartAttackDelay);

			_time += 1;
			if (_time == _specialAttackTimes[_step])
			{
				MakeSPecialAttack(_step);
				OnSpecialAttack?.Invoke();
				if (_step < _specialAttackTimes.Length - 1)
				{
					_step++;
				}
				else
				{
					_time = 0;
					_step = 0;
				}
				yield return new WaitForSeconds(_specialAttackPostDelay);
			}
		}
	}

	protected IEnumerator SpawnObjWithDelay(float delay, Vector3 spawnPosition)
	{
		yield return new WaitForSeconds(delay);
		ActivateObject(spawnPosition);
	}

	private void ActivateObject(Vector3 spawnPos)
	{
		var obj = StandartMeteorsPool.GetObject();
		obj.SetActive(true);
		var companent = obj.GetComponent<FlyingObject>();
		companent.SetStartValues(spawnPos, Fox.transform.position, GameSpeedController.GetCurrentFlyObjSpeedUp());
	}

	protected void ActivateObject(Vector3 spawnPosition, Vector3 targetPoint, ObjectPool objPool)
	{
		var obj = objPool.GetObject();
		obj.SetActive(true);
		var companent = obj.GetComponent<FlyingObject>();
		companent.SetStartValues(spawnPosition, targetPoint, GameSpeedController.GetCurrentFlyObjSpeedUp());
	}

	protected void ActivateSpawnSign(Vector3 spawnPos)
	{
		var sign = SpawnSignsPool.GetObject();
		sign.SetActive(true);
		sign.transform.position = spawnPos;
	}
	protected void ActivateSpawnSign(Vector3 spawnPos, ObjectPool objPool)
	{
		var sign = objPool.GetObject();
		sign.SetActive(true);
		sign.transform.position = spawnPos;
	}

	protected Vector3 GetRandomMapPosition()
	{
		return new Vector3(Random.Range(-12, 4), Random.Range(-3.5f, 3.5f), 0);
	}

	protected void SetSAPostDelay(int seconds)
	{
		_specialAttackPostDelay = seconds;
	}

	protected void SetStandartAttackDelay(float seconds)
	{
		_standartAttackDelay = seconds;
	}

	protected void SetStandartMeteorsCount(int count)
	{
		_standartMeteorsCount = count;
	}

	protected void SetSpawnMeteorDelay(float delay)
	{
		_spawnMeteorDelay = delay;
	}

	protected void SetSATimes(int[] times)
	{
		_specialAttackTimes = times;
	}

	protected int GetCurrentMeteorCount()
	{
		return _standartMeteorsCount;
	}
	protected abstract void MakeSPecialAttack(int step);

}
