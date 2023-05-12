using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoystickOption : MonoBehaviour
{
    [SerializeField] private FoxMover _foxMover;
    [SerializeField] private TMP_Text _speedLabel;
    [SerializeField] private Slider _slider;

	private void OnEnable()
	{
		_slider.value = PlayerPrefs.GetFloat("FoxSpeed", _foxMover.GetCurrentSpeed());

		SetLabelText((int)_slider.value);
	}

	public void ChangeJoystickSensitivity()
	{
		var newValue = _slider.value;
		PlayerPrefs.SetFloat("FoxSpeed", newValue);
		_foxMover.SetNewSpeed();

		int intValue = (int)newValue;

		SetLabelText(intValue);
	}

	private void SetLabelText(int value)
	{
		if (value == _slider.maxValue)
		{
			_speedLabel.text = "Max";
		}
		else if (value == _slider.minValue)
		{
			_speedLabel.text = "Min";
		}
		else
		{
			_speedLabel.text = value.ToString();
		}
	}
}
