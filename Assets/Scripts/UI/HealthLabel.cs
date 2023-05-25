using System.Collections.Generic;
using UnityEngine;

public class HealthLabel : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private HealthCompanent _healthCompanent;
	[SerializeField] private GameObject _healthContainer;

	private List<Heart> _hearthList = new List<Heart>();

	private void OnEnable()
	{
		_healthCompanent.HealthChanged += ChangeHealthsCount;
	}

	private void OnDisable()
	{
		_healthCompanent.HealthChanged -= ChangeHealthsCount;
	}

	private void Start()
	{
		var count = _healthCompanent.GetMaxHealth();
		AddHeart(count);
	}

	private void ChangeHealthsCount(float changeValue)
	{
		if (changeValue > 0)
		{
			AddHeart(changeValue);
		}
		else
		{
			BreakHeart(-changeValue);
		}
	}

	private void BreakHeart(float count)
	{
		for (int i = 0; i < count; i++)
		{
			if (_hearthList.Count > 0)
			{
				_hearthList[0].FadeOut();
				_hearthList.RemoveAt(0);
			}
		}
	}

	private void AddHeart(float count)
	{
		for (int i = 0; i < count; i++)
		{
			var heart = Instantiate(_heartPrefab, _healthContainer.transform);
			var companent = heart.GetComponent<Heart>();
			_hearthList.Add(companent);
			heart.transform.SetParent(_healthContainer.transform);
			companent.FadeIn();
		}
	}
}
