using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyTextLabel;
    [SerializeField] private ShopItem[] _shopItems;

    private int _money = 0;

	private void Start()
	{
		for (int i = 0; i < _shopItems.Length; i++)
		{
			bool isBougth;
			DataManager.GameDataInfo.ItemsBought.TryGetValue(_shopItems[i].IdName, out isBougth);
			_shopItems[i].SetBougthStatus(isBougth);
		}
        _money = DataManager.GameDataInfo.Money;
        UpdateCurrentMoneyValue();
	}

	public void UpdateCurrentMoneyValue()
    {
        _moneyTextLabel.text = _money.ToString();
    }

    public int GetMoneyBalance()
    {
        return _money;
    }
    
    public void OnBuyItem(int price)
    {
        _money -= price;
        DataManager.GameDataInfo.Money = _money;
        UpdateCurrentMoneyValue();
        DataManager.SaveGame();
	}
}
