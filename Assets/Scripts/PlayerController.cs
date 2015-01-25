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
	public GameController gameController;

	//values that will be set in the Inspector
	public Transform BodyTarget;
	public float BodyRotationSpeed;

	public int FoodCounter {get; private set;}

	[SerializeField]
	private int playerNumber;

	[SerializeField]
	private float forwardDeceleration;

	[SerializeField]
	private float forwardAcceleration;

	[SerializeField]
	private float forwardVelocityMax;

	private float forwardVelocity;

	private Quaternion bodyLookRotation;
	private Vector3 bodyDirection;

	private Hand lastHandUsed;

	private bool isRaceWinner;

	public void ActionWithHand(Hand hand)
	{
		bool correctHand = lastHandUsed == Hand.UnknownHand || lastHandUsed != hand;
		if(correctHand)
		{
			PerformStageAction();
			lastHandUsed = hand;
		}
	}

	public void Update()
	{
		if(BodyTarget != null)
		{
			Vector3 bodyTargetPos = BodyTarget.transform.position;
			bodyTargetPos.y = transform.position.y;
			//find the vector pointing from our position to the target
			bodyDirection = (bodyTargetPos - transform.position).normalized;
			
			//create the rotation we need to be in to look at the target
			bodyLookRotation = Quaternion.LookRotation(bodyDirection);
			
			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, bodyLookRotation, Time.deltaTime * BodyRotationSpeed);
		}

		if(forwardVelocity > 0.0f)
		{
			if(!isRaceWinner)
			{
				transform.position += new Vector3(0,0,forwardVelocity * Time.deltaTime);
				forwardVelocity = Mathf.Max(0.0f,forwardVelocity - forwardDeceleration * Time.deltaTime);
			}
			else
			{
				int multi = playerNumber == 2 ? -1 : 1;
				transform.position += new Vector3(multi * forwardVelocity * Time.deltaTime,0,forwardVelocity * Time.deltaTime);
				animTriggers.IsRacing(true);
			}
		}
	}

	public void UpdateSpeedForFatness()
	{
		float speedMutliplier = Mathf.Abs(plate.StartingFoodBitCount - FoodCounter) / (float)plate.StartingFoodBitCount;
		forwardVelocityMax = forwardVelocityMax + speedMutliplier * forwardVelocityMax;
		forwardAcceleration = forwardAcceleration + speedMutliplier * forwardAcceleration;
	}

	public void PerformStageAction()
	{
		if(gameController.CurrentStage == GameStage.StageEating)
		{
			if(plate.HasFood())
			{
				GameObject foodBit = plate.PickUpFood();
				Destroy(foodBit);
				
				FoodCounter++;
				counterText.text = FoodCounter.ToString();
				
				animTriggers.IsEating(true);
			}
			else
			{
				animTriggers.IsEating(false);
			}
		}
		else if(gameController.CurrentStage == GameStage.StageRacing)
		{
			animTriggers.IsRacing(true);
			forwardVelocity = Mathf.Min(forwardVelocityMax, forwardVelocity + forwardAcceleration);
		}
		else if(gameController.CurrentStage == GameStage.StageBathroomZoomIn)
		{
			BodyTarget = GameObject.Find("Player"+playerNumber+"Goal").transform;
		}
	}

	public void WalkIntoBathroom()
	{
		forwardVelocity = forwardVelocityMax;
		BodyTarget = GameObject.Find("Player"+playerNumber+"WinnerTarget").transform;
		isRaceWinner = true;
	}

	public void PerformRaceLosingAnimation()
	{
		BodyTarget = GameObject.Find("Main Camera").transform;
		animTriggers.TriggerOneOffAnimation("Cold");
	}
}
