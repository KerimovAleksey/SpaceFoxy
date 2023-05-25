using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
	[SerializeField] private float _timeForChangeAlpha;

    private Image _image;

	private void Awake()
	{
		_image = GetComponent<Image>();
		_image.color = new Color(1, 1, 1, 0);
	}

	public void FadeIn()
	{
		StartCoroutine(FadeInCoroutine());
	}

	public void FadeOut()
	{
		StartCoroutine(FadeOutCoroutine());
	}

	private IEnumerator FadeInCoroutine()
	{
		while (_image.color.a != 1)
		{
			var alpha = _image.color.a + _timeForChangeAlpha / 100;
			if (alpha > 1)
			{
				alpha = 1;
			}
			_image.color = new Color(1, 1, 1, alpha);
			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator FadeOutCoroutine()
	{
		while (_image.color.a != 0)
		{
			var alpha = _image.color.a - _timeForChangeAlpha / 100;
			if (alpha < 0)
			{
				alpha = 0;
			}
			_image.color = new Color(1, 1, 1, alpha);
			yield return new WaitForFixedUpdate();
		}
		Destroy(gameObject);
	}
}
