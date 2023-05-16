using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
	[SerializeField] private GameObject _fox;
	[SerializeField] private Joystick _joystick;
	[SerializeField] private GameObject _sencorController;
	private void Start()
	{
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
}
