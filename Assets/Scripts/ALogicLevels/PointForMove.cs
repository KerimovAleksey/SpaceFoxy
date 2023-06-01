using LogicLevels;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LogicLevels
{
	public class PointForMove : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private FoxMover _foxMover;
		[SerializeField] private PointForMove[] _exitPoints;

		private Animator _animator;
		private SpriteRenderer _sprite;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_sprite = GetComponent<SpriteRenderer>();
		}

		private void OnEnable()
		{
			_animator.Play("Idle");
			
			StartCoroutine(AppearAlpha());
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_foxMover.GetCurrentPoint().CheckNextPoint(this) == true)
			{
				if (_foxMover.SetNewCurrentPoint(this) == true)
				{
					PlayClickedAnimation();
				}
			}
		}

		private bool CheckNextPoint(PointForMove nextPoint)
		{
			return _exitPoints.Contains(nextPoint);
		}

		public void PlayClickedAnimation()
		{
			_animator.Play("Clicked");
		}

		public void ChangeEnabled()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}

		public void MarkAvailablePoints()
		{
			foreach (var point in _exitPoints)
			{
				point.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
			}
		}

		public void MarkUnAvailablePoints()
		{
			foreach (var point in _exitPoints)
			{
				point.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
			}
		}

		private IEnumerator AppearAlpha()
		{
			float alpha = 0f;
			_sprite.color = new Color(1, 1, 1, alpha);
			while (_sprite.color.a < 1)
			{
				_sprite.color = new Color(1,1,1,alpha);
				alpha += Time.fixedDeltaTime / 2;
				yield return new WaitForFixedUpdate();
			}
		}
	}
}
