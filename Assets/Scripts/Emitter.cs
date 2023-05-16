using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Emitter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _obj;
	[SerializeField] private int _count;

	private float _time = 0;
	private Rigidbody2D _rigidBody;
	private int _direction = 1;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	public void SetDirection(int direction)
	{
		_direction = direction;
	}

	private void FixedUpdate()
	{
		if (_time < 2)
		{
			_time += Time.fixedDeltaTime;
			_rigidBody.velocity = Vector2.left * _speed * _direction;
		}
		else
		{
			SpawnMeteorites();
			Destroy(gameObject);
		}
	}

	private void SpawnMeteorites()
	{
		Vector3 position;

		for (int i = -_count/2; i < _count/2; i++)
		{
			position = new Vector3(-10, i, 0);
			ActivateObject(transform.position, position);
		}
	}

	private void ActivateObject(Vector3 spawnPos, Vector3 destinationPos)
	{
		var obj = Instantiate(_obj);
		var companent = obj.GetComponent<FlyingObject>();
		companent.SetStartValues(spawnPos, destinationPos, 3);
	}
}
