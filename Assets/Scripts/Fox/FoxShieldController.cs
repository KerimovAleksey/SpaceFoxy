using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxShieldController : MonoBehaviour
{
    [SerializeField] private GameObject _shieldButton;
    [SerializeField] private GameObject _shield;
    [SerializeField] private FoxHealths _foxHealths;
    [SerializeField] private float _reloadTimeInSec;
    [SerializeField] private float _activeTimeInSec;

    private float _remainReloadTime = 0;
    private SpriteRenderer _shieldSpriteRenderer;
	private Button _buttonCompanent;
	private Image _imageButtonCompanent;

	private void Start()
	{
		_shieldSpriteRenderer = _shield.GetComponent<SpriteRenderer>();
		_buttonCompanent = _shieldButton.GetComponent<Button>();
		_imageButtonCompanent= _shieldButton.GetComponent<Image>();
	}

	public void ActivateShield()
    {
		_shield.SetActive(true);

		if (_remainReloadTime <= 0)
        {
			_buttonCompanent.interactable = false;

			StartCoroutine(AppearCoroutine(_shieldSpriteRenderer));

            _foxHealths.ChangeProtectionStatus(true);

			StartCoroutine(FillButtonImage(_reloadTimeInSec));
            Invoke("ShieldIsReloaded", _reloadTimeInSec);
			Invoke("DeactivateShield", _activeTimeInSec);
        }
    }

    private void DeactivateShield()
    {
		_foxHealths.ChangeProtectionStatus(false);
		StartCoroutine(FadeCoroutine(_shieldSpriteRenderer));
	}

    private void ShieldIsReloaded()
    {
		_buttonCompanent.interactable = true;
	}

	private IEnumerator AppearCoroutine(SpriteRenderer spriteRenderer)
	{
		float alpha = 0;
		spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
		while (alpha < 1)
		{
			alpha += 0.05f;
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator FadeCoroutine(SpriteRenderer spriteRenderer)
	{
		float alpha = 1;
		spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
		while (alpha > 0)
		{
			alpha -= 0.05f;
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
			yield return new WaitForFixedUpdate();
		}
		_shield.SetActive(false);
	}

	private IEnumerator FillButtonImage(float time)
	{
		_imageButtonCompanent.fillAmount = 0;
		while (_imageButtonCompanent.fillAmount < 1)
		{
			Debug.Log(1f / time);
			_imageButtonCompanent.fillAmount += 1f / time;
			yield return new WaitForSeconds(1f / time);
		}
	}
}
