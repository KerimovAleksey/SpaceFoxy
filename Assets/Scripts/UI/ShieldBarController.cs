using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarController : MonoBehaviour
{
    [SerializeField] private Image _shieldBar;
    [SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private BlackHoleHealths _objHealths;

	private float _maxHealth;

    public void Enable()
    {
		_maxHealth = _objHealths.GetCurrentMaxHealth();
		_shieldBar.fillAmount = 1;
		StartCoroutine(AppearAlpha(3));
    }

    public void Disable()
    {
		StartCoroutine(FadeIn(3));
    }

    public void ChangeValue(float damageValue)
    {
		var delta = -damageValue / _maxHealth;
		_shieldBar.fillAmount -= delta;
	}

	private IEnumerator AppearAlpha(float time)
	{
		float alpha = 0;
		var delta = 100f / (time * 60f * 60f);
		while (_canvasGroup.alpha < 1)
		{
			alpha += delta;
			_canvasGroup.alpha = alpha;
			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator FadeIn(float time)
	{
		float alpha = _canvasGroup.alpha;
		var delta = 100f / (time * 60f * 60f);
		while (_canvasGroup.alpha > 0)
		{
			alpha -= delta;
			_canvasGroup.alpha = alpha;
			yield return new WaitForFixedUpdate();
		}
	}
}
