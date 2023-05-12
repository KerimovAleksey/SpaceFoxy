using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningObj : MonoBehaviour
{
    [SerializeField] protected float _speed;
	[SerializeField] private int _startStage;

	protected int Stage = 0;

    protected ObjectPool _objForSpawnPool;
    protected ObjectPool _signsPool;
	protected GameSpeedController _gameSpeedController;

	protected Rigidbody2D _rigidBody;

	protected Vector3 _targetToMove;

	private SpriteRenderer _sprite;
	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_sprite = GetComponent<SpriteRenderer>();
	}

	public void SetReferences(ObjectPool objForSpawnPool, ObjectPool signsPool, GameSpeedController gameSpeedController)
	{
		_objForSpawnPool = objForSpawnPool;
		_signsPool = signsPool;
		_gameSpeedController = gameSpeedController;
	}

	private void OnEnable()
	{
		Stage = _startStage;
		StartCoroutine(Enable(3));
	}

	protected virtual void FixedUpdate()
	{
		if (Stage == 0)
		{
			if (_targetToMove != null)
			{
				var direction = _targetToMove - transform.position;
				_rigidBody.velocity = direction.normalized * _speed;
			}
		}
		else if (Stage == 1)
		{
			StartCoroutine(SpawnObjects());
			Stage = 2;
		}
	}

	protected abstract IEnumerator SpawnObjects();

	protected void ActivateObject(Vector3 spawnPosition, Vector3 targetPoint, ObjectPool objPool)
	{
		var obj = objPool.GetObject();
		obj.SetActive(true);
		var companent = obj.GetComponent<FlyingObject>();
		companent.SetStartValues(spawnPosition, targetPoint, _gameSpeedController.GetCurrentFlyObjSpeedUp());
	}
	protected void ActivateSpawnSign(Vector3 spawnPos)
	{
		var sign = _signsPool.GetObject();
		sign.SetActive(true);
		sign.transform.position = spawnPos;
	}

	public void SetCurrentTargetToMove(Vector3 pos)
	{
		_targetToMove = pos;
	}

	protected virtual IEnumerator Enable(float time)
	{
		float alpha = 0;
		var delta = 100f / (time * 60f * 60f);
		while (_sprite.color.a < 1)
		{
			alpha += delta;
			_sprite.color = new Color(1, 1, 1, alpha);
			yield return new WaitForFixedUpdate();
		}
	}

	protected virtual IEnumerator Disable(float time)
	{
		float alpha = _sprite.color.a;
		var delta = 100f / (time * 60f * 60f);
		while (_sprite.color.a > 0)
		{
			alpha -= delta;
			_sprite.color = new Color(1, 1, 1, alpha);
			yield return new WaitForFixedUpdate();
		}
		if (_sprite.color.a <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
