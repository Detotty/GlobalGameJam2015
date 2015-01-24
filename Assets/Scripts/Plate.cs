using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plate : MonoBehaviour {
	
	private List<GameObject> foodBits;

	[SerializeField]
	private Transform foodBitHolder;

	public void Awake()
	{
		foodBits = new List<GameObject>();
	}

	public void Start()
	{
		foreach(Transform child in foodBitHolder)
		{
			foodBits.Insert(0,child.gameObject);
		}
	}

	public GameObject PickUpFood()
	{
		if(foodBits.Count <= 0)
		{
			Debug.Log("Out of food!");
			return null;
		}

		GameObject foodBit = foodBits[0];
		foodBits.RemoveAt(0);

		return foodBit;
	}

	public bool HasFood()
	{
		return foodBits.Count > 0;
	}
}
