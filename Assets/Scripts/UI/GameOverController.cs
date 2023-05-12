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
		_scoreLabel.text = _scoreComponent.GetCurrentScore().ToString();
		_timeLabel.text = _gameSpeedController.GetCurrentTime().ToString();
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
