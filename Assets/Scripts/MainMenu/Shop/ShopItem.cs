using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _price;

	[SerializeField] private TMP_Text _notEnoughMoneyLabel;

	[SerializeField] private TMP_Text _buyButtonLabel;

	[SerializeField] private Image _buyButtonImage;

	[SerializeField] protected ShopManager _shopManager;

	[SerializeField] private GameObject _infoPanel;
	[SerializeField] private string _idName;
	public string IdName => _idName;

	private bool _isBuyed;

	private CanvasGroup _alphaComponent;

	private void Start()
	{
		_alphaComponent = _notEnoughMoneyLabel.GetComponent<CanvasGroup>();
	}

	public void TryBuyItem()
	{
		if (_shopManager.GetMoneyBalance() >= _price)
		{
			_shopManager.OnBuyItem(_price);
			ActionWhenItemIsBuyed();

			DataManager.GameDataInfo.ItemsBought[_idName] = true;
		}
		else
		{
			StartCoroutine(FadeInNotEnoughMoneyLabel());
		}
	}

	public void SetBougthStatus(bool status)
	{
		_isBuyed = status;
		if (_isBuyed)
		{
			_buyButtonImage.color = new Color(1, 1, 1, 1);
			_buyButtonLabel.gameObject.SetActive(false);
			_buyButtonImage.GetComponent<Button>().interactable = false;
		}
	}

	[ContextMenu("Buy This")]
	private void ActionWhenItemIsBuyed()
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

	public void InfoButtonClicked()
	{
		_infoPanel.SetActive(!_infoPanel.activeSelf);
	}
}
