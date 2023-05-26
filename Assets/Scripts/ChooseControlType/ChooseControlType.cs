using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseControlType : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingLabel;

    public void SetControlType(int number)
    {
        PlayerPrefs.SetInt("ControlType", number);
        _loadingLabel.gameObject.SetActive(true);
        Invoke(nameof(ChangeScene), 0.1f);
	}

    private void ChangeScene()
    {
		SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("SceneNumber"), LoadSceneMode.Single);
	}
}
