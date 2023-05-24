using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Money;
    public int AllGameMoneyEarned;
    public int ChickenCount;
    public int DeathsCount;

	public SerializableDictionary<string, bool> ItemsBought;
    public SerializableDictionary<string, bool> AchievementsReceived;

    // Стартовые значения для новой игры 
    public GameData()
    {
        Money = 0;
        AllGameMoneyEarned = 0;
	    ChickenCount = 0;
        DeathsCount = 0;

        AchievementsReceived = new SerializableDictionary<string, bool>();
        ItemsBought = new SerializableDictionary<string, bool>();
    }
}
