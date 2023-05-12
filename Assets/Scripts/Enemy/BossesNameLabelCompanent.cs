using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossesNameLabelCompanent : MonoBehaviour
{
	[SerializeField] private string _nameOfBoss;
	[SerializeField] private TMP_Text _bossNameLabel;

	private void Start()
	{
		_bossNameLabel.text = _nameOfBoss;
	}

	public string GetBossName()
	{
		return _nameOfBoss;
	}

}
