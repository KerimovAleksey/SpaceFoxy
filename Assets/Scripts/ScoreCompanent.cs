using UnityEngine;
using UnityEngine.Events;

public class ScoreCompanent : MonoBehaviour
{
    [SerializeField] private HealthCompanent _foxHealthCompanent;
    [SerializeField] private AudioSource _chickenSound;
    private int _score = 0;

    public event UnityAction<int> ScoreChanged;

    public int GetCurrentScore()
    {
        return _score;
    }

    public void GetScore(int scoreValue)
    {
        _score += scoreValue;
        _foxHealthCompanent.GetHeal(scoreValue);

        ScoreChanged?.Invoke(_score);
        _chickenSound.Play();
    }
}
