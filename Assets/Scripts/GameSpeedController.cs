using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameSpeedController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerLabel;
	[SerializeField] private int[] _bossStartsTimings = new int[] {5, 10, 10};
	[SerializeField] private Animator _backGroundAnimator;
	[SerializeField] private Animator _foxAnimator;

	public event UnityAction<int> StartBossFight;
	public event UnityAction BossDeaD;

	private int _currentBossNumber = 0;

	private float _flyingObjSpeedUp = 0;
	private float _time = 0;
	private float _bossesTimer = 0;
	private float speedUpDelta;

	private bool _bossEnabled = false;

	private void Start()
	{
		speedUpDelta = Time.fixedDeltaTime / 150;
	}

	public float GetCurrentTime()
	{
		return (int)_time;
	}

	private void FixedUpdate()
	{
		_time += Time.fixedDeltaTime;
		if (_bossEnabled == false)
		{
			_bossesTimer += Time.fixedDeltaTime;
		}

		var intTime = (int)_time;

		if (_currentBossNumber < _bossStartsTimings.Length)
		{
			if ((int)_bossesTimer == _bossStartsTimings[_currentBossNumber] && _bossEnabled == false) 
			{
				_bossEnabled = true;
				StartBossFight?.Invoke(_currentBossNumber);
				Debug.Log("Start Boss");
			} 
		}

		_timerLabel.text = intTime.ToString();

		SpeedUpObjects();
	}

	public float GetCurrentFlyObjSpeedUp()
	{
		return _flyingObjSpeedUp;
	}
	
	public void OnBossDead()
	{
		_bossEnabled = false;
		_bossesTimer = 0;
		BossDeaD?.Invoke();
		_currentBossNumber++;
	}
	
	private void SpeedUpObjects()
	{
		_flyingObjSpeedUp += speedUpDelta;
		_backGroundAnimator.speed += speedUpDelta;
		_foxAnimator.speed += speedUpDelta;
	}
}
