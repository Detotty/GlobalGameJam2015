using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plate : MonoBehaviour {

	public int StartingFoodBitCount {get; private set;}
	
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

		foodBits.Sort(delegate(GameObject one, GameObject two)
		{
			if (one.transform.position.y == two.transform.position.y) return 0;
			else if (one.transform.position.y > two.transform.position.y) return -1;
			else return 1;
		});
		StartingFoodBitCount = foodBits.Count;
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
