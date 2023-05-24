using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
	[Header("File Storage Config")]
	[SerializeField] private string _fileName;

	[SerializeField] private bool _useEncryption;

    public static DataManager instance { get; private set; }

	private GameData _gameData;
	private List<IDataPersistence> _dataPersistenceObjects;
	private FileDataHandler _dataHandler;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Найдено более 1 контролерра сохранений");
		}
		instance = this;
	}

	private void Start()
	{
		_dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
		_dataPersistenceObjects = FindAllDataPersisnenceObjects();
		LoadGame();
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}

	public void NewGame()
	{
		_gameData = new GameData();
	}

	public void LoadGame()
	{
		_gameData = _dataHandler.Load();

		if (_gameData == null)
		{
			Debug.Log("No data was found");
			NewGame();
		}
		foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(_gameData);
		}
	}

	public void SaveGame()
	{
		foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
		{
			dataPersistenceObj.SaveData(ref _gameData);
		}

		_dataHandler.Save(_gameData);
	}

	private List<IDataPersistence> FindAllDataPersisnenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
