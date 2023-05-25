using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]

public class ButtonsSoundsComponent : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip _enterSound;
    [SerializeField] private AudioClip _clickSound;

	private AudioSource _source;

	private void Awake()
	{
		_source = GetComponent<AudioSource>();
		_source.playOnAwake = false;
		_source.volume = 0.15f;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_source.clip = _clickSound;
		_source.Play();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_source.clip = _enterSound;
		_source.Play();
	}

	public void PlayClickSound()
	{
		_source.clip = _clickSound;
		_source.Play();
	}
}
