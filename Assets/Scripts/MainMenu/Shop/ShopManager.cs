using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyTextLabel;

    private int _money = 0;

	private void OnEnable()
	{
        UpdateCurrentMoneyValue();
	}

	private void UpdateCurrentMoneyValue()
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
}
