using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	[SerializeField] private ScoreCompanent _scoreComponent;
	[SerializeField] private GameSpeedController _gameSpeedController;
	[SerializeField] private GameObject _healthContainer;

	[SerializeField] private TMP_Text _scoreLabel;
	[SerializeField] private TMP_Text _timeLabel;

	private void OnEnable()
	{
		SetResultValues();
	}

	private void SetResultValues()
    {
		_healthContainer.SetActive(false);
		var scoreValue = _scoreComponent.GetCurrentScore();
		var timeValue = _gameSpeedController.GetCurrentTime();
		_scoreLabel.text = scoreValue.ToString();
		_timeLabel.text = timeValue.ToString();

		int newMoneyValue = (int)(timeValue / 6) + (scoreValue * 5);

		DataManager.GameDataInfo.Money += newMoneyValue;
		DataManager.GameDataInfo.AllGameMoneyEarned += newMoneyValue;
		DataManager.GameDataInfo.ChickenCount += scoreValue;
		DataManager.GameDataInfo.DeathsCount += 1;

		DataManager.SaveGame();
	}

	public void RestartGame()
    {
        Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

    public void HomeButton()
    {
		SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
