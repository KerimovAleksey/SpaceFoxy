using UnityEngine;

public class FrogsController : MonoBehaviour
{
    [SerializeField] private GameObject _frog;
	[SerializeField] private GameSpeedController _gameSpeedController;
	[SerializeField] private GameObject _frogsContainer;

	private void OnEnable()
	{
		_gameSpeedController.BossDeaD += AddFrog;
	}

	private void OnDisable()
	{
		_gameSpeedController.BossDeaD -= AddFrog;
	}

	private void AddFrog()
	{
		var frog = Instantiate(_frog, _frogsContainer.transform);
		frog.transform.SetParent(_frogsContainer.transform);
	}
}
