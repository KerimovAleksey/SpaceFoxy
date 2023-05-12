using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private int _price;

	[SerializeField] private TMP_Text _notEnoughMoneyLabel;
	[SerializeField] private TMP_Text _priceLabel;
	[SerializeField] private TMP_Text _buyButtonLabel;
	[SerializeField] private TMP_Text _nameLabel;

	[SerializeField] private Image _buyButtonImage;
	[SerializeField] private Image _imageContainer;

	[SerializeField] protected ShopManager _shopManager;

	private bool _isBuyed = false;
	private CanvasGroup _alphaComponent;

	private void OnEnable()
	{
		_imageContainer.sprite = _sprite;
		if (_isBuyed == true)
		{
			ActionWhenItemIsBuyed();
		}
		_priceLabel.text = _price.ToString();
		_nameLabel.text = _name;

		_alphaComponent = _notEnoughMoneyLabel.GetComponent<CanvasGroup>();
	}

	public void TryBuyItem()
	{
		if (_shopManager.GetMoneyBalance() > _price)
		{
			_shopManager.OnBuyItem(_price);
			ActionWhenItemIsBuyed();
			ItemIsBuyed();
		}
		else
		{
			StartCoroutine(FadeInNotEnoughMoneyLabel());
		}
	}

	protected abstract void ItemIsBuyed();

	protected void ActionWhenItemIsBuyed()
	{
		_isBuyed = true;
		_buyButtonImage.color = new Color(1, 1, 1, 1);
		_buyButtonLabel.gameObject.SetActive(false);
		_buyButtonImage.GetComponent<Button>().interactable = false;
	}

	private IEnumerator FadeInNotEnoughMoneyLabel()
	{
		_alphaComponent.alpha = 1;
		yield return new WaitForSeconds(1);
		while (_alphaComponent.alpha > 0)
		{
			_alphaComponent.alpha -= 0.025f;
			yield return new WaitForFixedUpdate();
		}
	}
}
