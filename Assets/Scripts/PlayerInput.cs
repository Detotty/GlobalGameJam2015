using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public KeyCode leftHandKey;
	public KeyCode rightHandKey;

	public PlayerController playerController;

	public void Update()
	{
		if(Input.GetKeyDown(leftHandKey))
		{
			playerController.EatWithHand(Hand.LeftHand);
		}
		else if(Input.GetKeyDown(rightHandKey))
		{
			playerController.EatWithHand(Hand.RightHand);
		}
	}
}
