using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Hand
{
	UnknownHand,
	LeftHand,
	RightHand
}

public class PlayerController : MonoBehaviour {

	public Plate plate;
	public Text counterText;
	public AnimTriggers animTriggers;

	private Hand lastHandUsed;
	private int foodCounter;

	public void EatWithHand(Hand hand)
	{
		bool correctHand = lastHandUsed == Hand.UnknownHand || lastHandUsed != hand;
		if(plate.HasFood() && correctHand)
		{
			GameObject foodBit = plate.PickUpFood();
			Destroy(foodBit);

			foodCounter++;
			counterText.text = foodCounter.ToString();

			animTriggers.IsEating(true);

			lastHandUsed = hand;
		}
	}

}
