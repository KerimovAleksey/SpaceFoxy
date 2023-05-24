using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	[SerializeField] private ScoreCompanent _scoreComponent;
	[SerializeField] private GameSpeedController _gameSpeedController;

	[SerializeField] private TMP_Text _scoreLabel;
	[SerializeField] private TMP_Text _timeLabel;

	private void OnEnable()
	{
		SetResultValues();
	}

	private void SetResultValues()
    {
		var scoreValue = _scoreComponent.GetCurrentScore();
		var timeValue = _gameSpeedController.GetCurrentTime();
		_scoreLabel.text = scoreValue.ToString();
		_timeLabel.text = timeValue.ToString();

		var balance = PlayerPrefs.GetFloat("balanceTemp", 0);
		PlayerPrefs.SetFloat("balanceTemp", balance + (int)(timeValue / 6) + (scoreValue * 5));
		Debug.Log(PlayerPrefs.GetFloat("balanceTemp"));
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
