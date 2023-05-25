using System.Collections;
using UnityEngine;

public class ExplosionOrb : MovingAreas
{
	[SerializeField] private float _chargeTime;
	[SerializeField] private float _explosionDelay;
	[SerializeField] private int _damage;

	private Animator _animator;
	private int _stage = 0;
	private FoxHealths _foxHealths;
	private float _targetScaleValue;
	private Vector3 _targetScale;

	protected override void Start()
	{
		_animator = GetComponent<Animator>();
	}

	protected override void OnEnable()
	{
		transform.localScale = new Vector3(0.3f, 0.3f, 0);

		_targetScaleValue = Random.Range(0.5f, 1.3f);
		_targetScale = new Vector3(_targetScaleValue, _targetScaleValue, 0);

		_sprite.color = new Color(1, 1, 1, 0);
		_stage = 0;
		StartCoroutine(AppearAlpha(3));
		StartCoroutine(UpScale());
	}

	protected override void FixedUpdate()
	{
		if (transform.localScale == _targetScale && _stage == 0)
		{
			_stage = 1;
			Invoke("Explode", _explosionDelay);
		}
	}

	private void Explode()
	{
		_animator.SetInteger("Stage", 2);
	}

	private IEnumerator UpScale()
	{
		float step = (1 - transform.localScale.x) / _chargeTime * Time.fixedDeltaTime;

		while (transform.localScale != _targetScale)
		{
			transform.localScale = transform.localScale + new Vector3(step, step, 0);
			if (transform.localScale.x > _targetScale.x)
			{
				transform.localScale = _targetScale;
			}
			yield return new WaitForFixedUpdate();
		}
	}

	public void TryDealDamage()
	{
		if (_foxHealths != null)
		{
			_foxHealths.GetDamage(_damage);
			Debug.Log("damage");
		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FoxHealths foxHealths))
		{
			_foxHealths = foxHealths;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out FoxHealths foxHealths))
		{
			_foxHealths = null;
		}
	}
}
