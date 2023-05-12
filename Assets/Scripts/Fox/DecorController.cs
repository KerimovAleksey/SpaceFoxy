using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorController : MonoBehaviour
{
    [SerializeField] private ScoreCompanent _scoreCompanent;
	[SerializeField] private GameObject _images;

	private bool _isCoroutine = false;

	private void OnEnable()
	{
		_scoreCompanent.ScoreChanged += ChangeVisability;
	}
	private void OnDisable()
	{
		_scoreCompanent.ScoreChanged -= ChangeVisability;
	}

	private void ChangeVisability(int score)
	{
		if (_isCoroutine == false)
		{
			StartCoroutine(ChangeVisibleCoroutine());
		}
	}

	private IEnumerator ChangeVisibleCoroutine()
	{
		_isCoroutine = true;
		_images.SetActive(true);
		yield return new WaitForSecondsRealtime(1f);
		_images.SetActive(false);
		_isCoroutine = false;
	}

	
}
