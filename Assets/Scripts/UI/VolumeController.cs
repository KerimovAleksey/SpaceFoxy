using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _backGroungSource;
    [SerializeField] private Sprite _enabledAllSoundsSprite;
    [SerializeField] private Sprite _disabledAllSoundsSprite;
    [SerializeField] private Image _allSoundsButton;
    [SerializeField] private Image _backgroundMusicButton;
    [SerializeField] private Slider _sliderValue;

	private void OnEnable()
	{
		if (AudioListener.pause == true)
		{
			_allSoundsButton.sprite = _disabledAllSoundsSprite;
		}
		else if (AudioListener.pause == false)
		{
			_allSoundsButton.sprite = _enabledAllSoundsSprite;
		}
	}

	public void ToggleAllSounds()
    {
        AudioListener.pause = !AudioListener.pause;
        if (AudioListener.pause == true)
        {
            _allSoundsButton.sprite = _disabledAllSoundsSprite;
        }
        else if (AudioListener.pause == false)
        {
			_allSoundsButton.sprite = _enabledAllSoundsSprite;
        }
    }

    public void ToggleBackGroundMusic()
    {
        _backGroungSource.mute = !_backGroungSource.mute;
        if (_backGroungSource.mute == true)
        {
            _backgroundMusicButton.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
			_backgroundMusicButton.color = new Color(1, 1, 1, 1);
		}
    }

    public void ChangeSliderValue()
    {
        AudioListener.volume = _sliderValue.value;
    }
}
