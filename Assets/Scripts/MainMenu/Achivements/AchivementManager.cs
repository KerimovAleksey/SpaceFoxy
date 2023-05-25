using UnityEngine;

public class AchivementManager : MonoBehaviour
{
	[SerializeField] private AchivementTemplate[] _achivements;

	private void Start()
	{
		for (int i = 0; i < _achivements.Length; i++)
		{
			bool received;
			DataManager.GameDataInfo.AchievementsReceived.TryGetValue(_achivements[i].Name, out received);
			if (received == true)
			{
				_achivements[i].GetThisAchievement();
			}
		}
	}
}
