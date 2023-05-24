using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
	[SerializeField] private GameObject _fox;
	[SerializeField] private Joystick _joystick;
	[SerializeField] private GameObject _sencorController;

	[SerializeField] private GameObject _shield;

	[SerializeField] private Spawner _goldenChickenSpawner;
	[SerializeField] private GameObject _goldenChickenObjPool;

	private void Start()
	{
		LoadShopItems();
		AudioListener.volume = PlayerPrefs.GetFloat("GlobalVolume", 0.5f);
		var controlType = PlayerPrefs.GetInt("ControlType");
		switch (controlType)
		{
			case 0:
				_fox.AddComponent<FingerHoldMover>();
				break;
			case 1:
				_fox.AddComponent<TapDirectionMover>();
				break;
			case 2:
				_sencorController.SetActive(true);
				_joystick.gameObject.SetActive(true);
				_fox.AddComponent<JoystickMover>();
				_fox.GetComponent<JoystickMover>().SetReferences(_joystick);
				break;
		}
	}

	private void LoadShopItems()
	{
		bool shieldEnabled;
		DataManager.GameDataInfo.ItemsBought.TryGetValue("TemporaryShield", out shieldEnabled);
		if (shieldEnabled)
		{
			_shield.SetActive(true);
		}

		bool goldenChickenEnabled;
		DataManager.GameDataInfo.ItemsBought.TryGetValue("GoldenChicken", out goldenChickenEnabled);
		if (goldenChickenEnabled)
		{
			_goldenChickenObjPool.SetActive(true);
			_goldenChickenSpawner.enabled = true;
		}
	}
}
