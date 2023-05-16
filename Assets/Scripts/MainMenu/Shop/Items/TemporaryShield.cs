using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryShield : ShopItem
{
	protected override void ItemIsBuyed()
	{
		Debug.Log("Купил");
	}
}
