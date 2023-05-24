using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_Text _moneyTextLabel;
    [SerializeField] private ShopItem[] _shopItems;
    [SerializeField] DataManager _dataManager;

    private int _money = 0;

	private void Start()
	{
        CheckTemproraryMoney();
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
        UpdateCurrentMoneyValue();
	}

	public void LoadData(GameData data)
	{
        _money = data.Money;

        for (int i = 0; i < _shopItems.Length; i++)
        {
            bool isBougth;
            data._itemsBought.TryGetValue(_shopItems[i].IdName, out isBougth);
            _shopItems[i].SetBougthStatus(isBougth);


			if (ScenesBridge.ShopItems.ContainsKey(_shopItems[i].IdName))
			{
				ScenesBridge.ShopItems.Remove(_shopItems[i].IdName);
			}
			ScenesBridge.ShopItems.Add(_shopItems[i].IdName, isBougth);
		}
	}

	public void SaveData(ref GameData data)
	{
		data.Money = _money;

        for (int i = 0; i < _shopItems.Length; i++)
        {
            if (data._itemsBought.ContainsKey(_shopItems[i].IdName))
            {
                data._itemsBought.Remove(_shopItems[i].IdName);
            }
            data._itemsBought.Add(_shopItems[i].IdName, _shopItems[i].IsBougth);
        }
	}

    private void CheckTemproraryMoney()
    {
        try
        {
            var balance = PlayerPrefs.GetFloat("balanceTemp", 0);
            if (balance > 0)
            {
				_money += (int)balance;
            }

		    UpdateCurrentMoneyValue();
            _dataManager.SaveGame();
			PlayerPrefs.SetFloat("balanceTemp", 0);
        }
        catch
        {
            Debug.Log("Wait");
            Invoke("CheckTemproraryMoney", 1f);
        }
	}
}
