using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchivementTemplate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AchievementItem _achievementItem;
	[SerializeField] private Image _image;
	[SerializeField] private TMP_Text _nameLabel;
	[SerializeField] private GameObject _isRecieved;
	[SerializeField] private GameObject _descriptionPanel;
	[SerializeField] private TMP_Text _descriptionLabel;

	[SerializeField] private string _id;

	private CanvasGroup _canvasGroup;
	private bool _collected;

	public bool Collected => _collected;
	public string ID => _id;
	public string Name => _nameLabel.text;

	[ContextMenu("Generate guid for id")]
	private void GenerateGuid()
	{
		_id = System.Guid.NewGuid().ToString();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_descriptionPanel.SetActive(false);
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		_descriptionPanel.SetActive(true);
	}

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_image.sprite = _achievementItem.Sprite;
		_nameLabel.text = _achievementItem.Name;
		_descriptionLabel.text = _achievementItem.Description;
	}

	private void OnEnable()
	{
		if (_collected)
		{
			GetThisAchievement();
		}
	}

	[ContextMenu("get this")]
	public void GetThisAchievement()
	{
		_collected = true;
		_canvasGroup.alpha = 1;
		_isRecieved.SetActive(true);
	}

	public void ChangeCollectedStatus(bool status)
	{
		_collected = status;
	}
}
