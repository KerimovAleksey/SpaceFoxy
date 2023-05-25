using UnityEngine;

public class DeadZone : MonoBehaviour
{
	[SerializeField] private GameObject _fox;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject != _fox)
		{
			collision.gameObject.SetActive(false);
		}
	}
}
