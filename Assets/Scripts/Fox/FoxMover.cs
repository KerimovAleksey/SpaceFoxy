using System.Collections;
using UnityEngine;

public class FoxMover : MonoBehaviour
{
	[SerializeField] private float _tapRange;
	[SerializeField] private Joystick _joystick;

	[SerializeField] private float _maxSpeed;
	[SerializeField] private float _minSpeed;

	private float _speed;
	private Rigidbody2D _rigidBody;

	private Vector2 _velocity;
	private bool _isControlled = true;

	private float _startkickSpeed = 60;
	private float _currentKickSpeed = 0;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_speed = PlayerPrefs.GetFloat("FoxSpeed", (_maxSpeed + _minSpeed) / 2f);
	}

	private void FixedUpdate()
	{
		CheckPlayerPosition();
		if (_isControlled == true)
		{
			Vector2 moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
			_velocity = moveInput * _speed;
			_rigidBody.MovePosition(_rigidBody.position + _velocity * Time.deltaTime);
		}
	}

	public void KickOut()
	{
		_isControlled = false;
		StartCoroutine(SpeedDown());
	}

	private IEnumerator SpeedDown()
	{
		_currentKickSpeed = _startkickSpeed;
		while (_currentKickSpeed != 0)
		{
			_rigidBody.velocity = new Vector2(-_currentKickSpeed, 0);
			_currentKickSpeed -= 1;
			yield return new WaitForEndOfFrame();
		}
		_isControlled = true;
		_rigidBody.velocity = new Vector2(0, 0);
	}

	private void CheckPlayerPosition()
	{
		var pos = gameObject.transform.position;
		if (pos.x > 11f || pos.x < -11f || pos.y > 6f || pos.y < -5f)
		{
			gameObject.transform.position = new Vector3(0,0,0);
			_currentKickSpeed = 0;
		}
	}

	public float GetCurrentSpeed()
	{
		return _speed;
	}

	public void SetNewSpeed()
	{
		_speed = PlayerPrefs.GetFloat("FoxSpeed");
	}
}
