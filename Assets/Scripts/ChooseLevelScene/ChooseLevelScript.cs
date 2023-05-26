using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevelScript : MonoBehaviour
{
    public void ChooseLevel(int levelNumber)
    {
        if (levelNumber == 0)
        {
			SceneManager.LoadScene(0);
		}
        else
        {
            levelNumber += 2;
            PlayerPrefs.SetInt("SceneNumber", levelNumber);
            SceneManager.LoadScene(2);
        }
    }
}
