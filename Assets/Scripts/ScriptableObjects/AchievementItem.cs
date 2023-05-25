using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements/Create New Achievement")]
public class AchievementItem : ScriptableObject
{
	[SerializeField] private string _name;
	[SerializeField] private Sprite _sprite;
	[SerializeField] private bool _isReceived;
	[SerializeField] private string _description;

	public string Name => _name;
	public Sprite Sprite => _sprite;
	public bool IsReceived => _isReceived;
	public string Description => _description;
}
