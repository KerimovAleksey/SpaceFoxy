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

	private CanvasGroup _canvasGroup;
	public string Name => _nameLabel.text;

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

	[ContextMenu("get this")]
	public void GetThisAchievement()
	{
		_canvasGroup.alpha = 1;
		_isRecieved.SetActive(true);
	}
}
