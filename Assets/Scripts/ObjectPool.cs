using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] private GameObject _prefab;
	[SerializeField] private GameObject _objectPool;

	[SerializeField] private int _objectCount;

	private List<GameObject> _objects = new List<GameObject>();

	private void Start()
	{
		InstantiateObjects(_objectCount);
	}

	private void InstantiateObjects(int count)
	{
		for (int i = 0; i < count; i++)
		{
			var obj = Instantiate(_prefab);
			obj.SetActive(false);
			_objects.Add(obj);
		}
	}

	private GameObject InstantiateObject()
	{
		var obj = Instantiate(_prefab);
		obj.SetActive(false);
		_objects.Add(obj);
		return obj;
	}

	public GameObject GetObject()
	{
		for (int i = 0; i < _objects.Count; i++)
		{
			if (_objects[i].activeSelf == false)
			{
				return _objects[i];
			}
		}
		return InstantiateObject();
	}
}
