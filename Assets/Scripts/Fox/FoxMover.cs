using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class FoxMover : MonoBehaviour
{
	protected float _speed;
	protected Rigidbody2D _rigidBody;

	protected Vector2 _velocity;
	protected bool _isControlled = true;

	private float _startkickSpeed = 60;
	private float _currentKickSpeed = 0;

	public event UnityAction SpeedChanged;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	protected abstract void Update();


	public void KickOut()
	{
		if (_isControlled == true)
		{
			_isControlled = false;
			StartCoroutine(SpeedDown());
		}
	}

	private IEnumerator SpeedDown()
	{
		_currentKickSpeed = _startkickSpeed;
		while (_currentKickSpeed > 0)
		{
			_rigidBody.velocity = new Vector2(-_currentKickSpeed, 0);
			_currentKickSpeed -= 1;
			yield return new WaitForEndOfFrame();
		}
		_isControlled = true;
		_rigidBody.velocity = new Vector2(0, 0);
	}

	protected void CheckPlayerPosition()
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
		SpeedChanged?.Invoke();
	}
}
