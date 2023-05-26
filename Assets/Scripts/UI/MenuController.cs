using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _songList;
    [SerializeField] private AudioSource _songPlayer;
    [SerializeField] private TMP_Text _songLabel;

    private int _currentSongIndex = 0;

    public void ChangePanelEnabled(GameObject menuPanel)
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        Time.timeScale = menuPanel.activeSelf == true ? 0.0f : 1.0f;
    }

	private void Awake()
	{
        RemeshPlayList();
		NextSong();
	}

	private void OnEnable()
	{
		PrintMusicName();
	}

	public void ChangeMusicEnabled()
    {
        if (_songPlayer.isPlaying == true)
        {
            _songPlayer.Pause();
        }
        else
        {
            _songPlayer.Play();
        }
    }

    public void PrevSong()
    {
        if (_currentSongIndex == 0)
        {
            _currentSongIndex = _songList.Length - 1;
        }
        else
        {
            _currentSongIndex--;
        }
        _songPlayer.clip = _songList[_currentSongIndex];
        PrintMusicName();
        _songPlayer.Play();
        CancelInvoke();
		Invoke("NextSong", _songPlayer.clip.length);
	}

    public void NextSong()
    {
        if (_currentSongIndex == _songList.Length - 1)
        {
            _currentSongIndex = 0;
        }
        else
        {
            _currentSongIndex++;
        }
        _songPlayer.clip = _songList[_currentSongIndex];
		PrintMusicName();
		_songPlayer.Play();
		CancelInvoke();
		Invoke("NextSong", _songPlayer.clip.length);
	}

    public void RemeshPlayList()
    {
        for (int i = _songList.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i+1);
            AudioClip temp = _songList[j];
            _songList[j] = _songList[i];
            _songList[i] = temp;
        }
        NextSong();
    }

    private void PrintMusicName()
    {
        _songLabel.text = _songPlayer.clip.name;
    }
	public void HomeButton()
	{
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
