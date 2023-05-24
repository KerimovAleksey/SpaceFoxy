using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchivementManager : MonoBehaviour, IDataPersistence
{
	[SerializeField] private AchivementTemplate[] _achivements;

	private void Start()
	{
		if (ScenesBridge.Achivements.Count > 0)
		{
			for (int i = 0; i < _achivements.Length; i++)
			{
				if (ScenesBridge.Achivements.Contains(_achivements[i].Name))
				{
					_achivements[i].GetThisAchievement();
				}
			}
		}
		ScenesBridge.Achivements.Clear();
	}

	public void LoadData(GameData data)
	{
		for (int i = 0; i < _achivements.Length; i++)
		{
			bool collected;
			data._achievementsReceived.TryGetValue(_achivements[i].ID, out collected);
			if (collected)
			{
				_achivements[i].ChangeCollectedStatus(true);
			}
		}
	}

	public void SaveData(ref GameData data)
	{
		for (int i = 0; i < _achivements.Length; i++)
		{
			if (data._achievementsReceived.ContainsKey(_achivements[i].ID))
			{
				data._achievementsReceived.Remove(_achivements[i].ID);
			}
			data._achievementsReceived.Add(_achivements[i].ID, _achivements[i].Collected);
		}
	}
}
