using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
	public static string FileName = "data.json";
	public static bool UseEncryption = true;

	public static GameData GameDataInfo;
	public static List<IDataPersistence> DataPersistenceObjects;
	public static FileDataHandler DataHandler;

	public static void NewGame()
	{
		GameDataInfo = new GameData();
	}

	public static void LoadGame()
	{
		GameDataInfo = DataHandler.Load();

		if (GameDataInfo == null)
		{
			Debug.Log("No data was found");
			NewGame();
		}
		foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(GameDataInfo);
		}
	}

	public static void SaveGame()
	{
		DataHandler.Save(GameDataInfo);
	}
}
