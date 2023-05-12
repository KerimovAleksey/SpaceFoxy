using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
	[SerializeField] private ScoreCompanent _scoreComponment;

	private void OnEnable()
	{
		_scoreComponment.ScoreChanged += ChangeLabelScore;
	}

	private void OnDisable()
	{
		_scoreComponment.ScoreChanged -= ChangeLabelScore;
	}

	public void ChangeLabelScore(int scoreValue)
    {
        _label.text = scoreValue.ToString();
    }
}
