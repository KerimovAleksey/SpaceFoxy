using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovingAreas : MonoBehaviour
{
    [SerializeField] protected float _startSpeed;
	[SerializeField] protected float _changeTargetTimer;
	[SerializeField] protected float _disableDelay;

    protected float _speed;
	protected Vector3 _targetPoint;
	protected float _time = 0;
	protected Rigidbody2D _rigidBody;

	protected SpriteRenderer _sprite;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_sprite = GetComponent<SpriteRenderer>();
	}

	protected virtual void Start()
	{
		_targetPoint = GetRandomMapPoint();
		StartCoroutine(AppearAlpha(3));
	}

	protected virtual void OnEnable()
	{
		_speed = _startSpeed;
		if (_disableDelay != 0)
		{
			Invoke("DisableObject", _disableDelay);
		}
	}

	protected virtual void FixedUpdate()
	{
		_time += Time.fixedDeltaTime;

		var velocity = (_targetPoint - transform.position);
		if (_time >= _changeTargetTimer)
		{
			_targetPoint = GetRandomMapPoint();
			_time = 0;
		}
		else if (velocity.magnitude < 0.5f)
		{
			_targetPoint = GetRandomMapPoint();
		}
		else
		{
			_rigidBody.velocity = velocity.normalized * _speed;
		}
	}

	protected virtual Vector3 GetRandomMapPoint()
    {
		return new Vector3(Random.Range(-8, 8), Random.Range(-4.5f, 4.5f), 0);
	}
	protected IEnumerator AppearAlpha(float time)
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

	private IEnumerator FadeIn(float time)
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

	public void DisableObject()
	{
		if (gameObject.activeSelf == true)
		{
			StartCoroutine(FadeIn(3));
		}
	}
	public void ChangeDisableTimer(float time)
	{
		_disableDelay = time;
	}

	protected abstract void OnTriggerStay2D(Collider2D collision);
}
