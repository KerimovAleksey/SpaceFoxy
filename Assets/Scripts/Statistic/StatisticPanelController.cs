using TMPro;
using UnityEngine;

public class StatisticPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text _chickensCount;
    [SerializeField] private TMP_Text _deathsCount;
    [SerializeField] private TMP_Text _levelOneRecord;
    [SerializeField] private TMP_Text _levelTwoRecord;

	private void Start()
	{
		_chickensCount.text = DataManager.GameDataInfo.ChickenCount.ToString();
		_deathsCount.text = DataManager.GameDataInfo.DeathsCount.ToString();

		_levelOneRecord.text = DataManager.GameDataInfo.MaxLevelOneTimeCount.ToString() + ", " + DataManager.GameDataInfo.MaxLevelOneChickensCount;
		_levelTwoRecord.text = DataManager.GameDataInfo.MaxLevelTwoTimeCount.ToString() + ", " + DataManager.GameDataInfo.MaxLevelTwoChickensCount;
	}
}
