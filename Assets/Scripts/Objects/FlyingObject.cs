using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class FlyingObject : MonoBehaviour
{
	[SerializeField] protected float _baseSpeed;
	[SerializeField] protected float _rangeSpread;

	protected float _speed;
	protected Vector2 _direction;
	protected Rigidbody2D _rigidBody2D;

	private void Awake()
	{
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}

	protected abstract void OnTriggerEnter2D(Collider2D collision);

	public void SetStartValues(Vector2 startPosition, Vector3 targetPosition, float currentSpeedUp)
	{
		_speed = _baseSpeed;

		transform.position = startPosition;

		_direction = (targetPosition - transform.position).normalized;

		var _newRangeSpread = Random.Range(-_rangeSpread, _rangeSpread);
		_direction += new Vector2(_newRangeSpread/100f, _newRangeSpread/100f);

		Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, _direction);
		Vector3 newRotate = toRotate.eulerAngles;
		transform.rotation = Quaternion.Euler(newRotate.x, newRotate.y, newRotate.z + 90);

		_speed += currentSpeedUp;
	}

	protected virtual void FixedUpdate()
	{
		_rigidBody2D.velocity = _direction * _speed;
	}
}
