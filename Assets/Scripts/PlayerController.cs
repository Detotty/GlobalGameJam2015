using UnityEngine;
using System.Collections;

public enum Hand
{
	UnknownHand,
	LeftHand,
	RightHand
}

public class PlayerController : MonoBehaviour {

	public Plate plate;

	private Hand lastHandUsed;

	public void EatWithHand(Hand hand)
	{
		bool correctHand = lastHandUsed == Hand.UnknownHand || lastHandUsed != hand;
		if(plate.HasFood() && correctHand)
		{
			GameObject foodBit = plate.PickUpFood();
			Destroy(foodBit);

			lastHandUsed = hand;
		}
	}

}
