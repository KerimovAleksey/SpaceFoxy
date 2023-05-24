using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Money;

	public SerializableDictionary<string, bool> _itemsBought;
    public SerializableDictionary<string, bool> _achievementsReceived;

    // Стартовые значения для новой игры 
    public GameData()
    {
        Money = 0;
        _achievementsReceived = new SerializableDictionary<string, bool>();
        _itemsBought = new SerializableDictionary<string, bool>();
    }
}
