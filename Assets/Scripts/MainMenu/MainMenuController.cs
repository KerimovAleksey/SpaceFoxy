using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Sprite _soundsEnabledSprite;
    [SerializeField] private Sprite _soundsDisabledSprite;
    [SerializeField] private Image _allSoundsButton;
	[SerializeField] private GameObject _loadingLabel;

	private bool _appearCoroutineEnabled = false;
    private bool _fadeCoroutineEnabled = false;

	private void OnEnable()
	{
		Time.timeScale = 1;
		if (AudioListener.pause == true)
		{
			_allSoundsButton.sprite = _soundsDisabledSprite;
		}
		else if (AudioListener.pause == false)
		{
			_allSoundsButton.sprite = _soundsEnabledSprite;
		}
	}

	private void Awake()
	{
		DataManager.DataHandler = new FileDataHandler(Application.persistentDataPath, DataManager.FileName, DataManager.UseEncryption);
		DataManager.DataPersistenceObjects = FindAllDataPersisnenceObjects();
		DataManager.LoadGame();

		CheckLongTimeAchivements();
	}

	private void CheckLongTimeAchivements()
	{
		GameData gameData = DataManager.GameDataInfo;
		if (gameData.ChickenCount >= 100)
			gameData.AchievementsReceived["Lover of kisses"] = true;
		if (gameData.ChickenCount >= 1000)
			gameData.AchievementsReceived["KFC-hunter"] = true;

		if (gameData.DeathsCount >= 100)
			gameData.AchievementsReceived["Persistent"] = true;
		if (gameData.DeathsCount >= 1000)
			gameData.AchievementsReceived["Iron Will"] = true;

		if (gameData.AllGameMoneyEarned >= 2000)
			gameData.AchievementsReceived["Hard worker"] = true;
	}

	private List<IDataPersistence> FindAllDataPersisnenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
		return new List<IDataPersistence>(dataPersistenceObjects);
	}

	private void OnApplicationQuit()
	{
		DataManager.SaveGame();
	}

	public void Play()
    {
		_loadingLabel.SetActive(true);
		Invoke(nameof(LoadMainScene), 0.1f);
	}

	private void LoadMainScene()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeSoundsEnabled()
    {
		AudioListener.pause = !AudioListener.pause;
		if (AudioListener.pause == true)
		{
			_allSoundsButton.sprite = _soundsDisabledSprite;
		}
		else if (AudioListener.pause == false)
		{
			_allSoundsButton.sprite = _soundsEnabledSprite;
		}
	}

    public void OpenPanel(GameObject panel)
    {
        if (panel.activeSelf == false && _appearCoroutineEnabled == false)
        {
            panel.SetActive(true);
            StartCoroutine(AppearAlpha(panel.GetComponent<CanvasGroup>()));
        }
    }

    public void ClosePanel(GameObject panel)
    {
		if (panel.activeSelf == true && _fadeCoroutineEnabled == false && _appearCoroutineEnabled == false)
		{
			StartCoroutine(FadeInAlpha(panel.GetComponent<CanvasGroup>()));
		}
	}

    private IEnumerator AppearAlpha(CanvasGroup obj)
    {
        _appearCoroutineEnabled = true;
        obj.alpha = 0;
        while (obj.alpha < 1)
        {
			obj.alpha += 0.025f;
			yield return new WaitForFixedUpdate();
        }
		_appearCoroutineEnabled = false;
	}

	private IEnumerator FadeInAlpha(CanvasGroup obj)
	{
        _fadeCoroutineEnabled = true;
        obj.alpha = 1;
		while (obj.alpha > 0)
		{
			obj.alpha -= 0.025f;
			yield return new WaitForFixedUpdate();
		}
        obj.gameObject.SetActive(false);
		_fadeCoroutineEnabled = false;
	}
}
