using UnityEngine;
using System.Collections;

public enum GameStage
{
	StageStart,
	StageIntro1,
	StageIntro2,
	StageIntro3,
	StageIntro4,
	StageIntro5,
	StageCountdown,
	StageEating,
	StageEatingResolution,
	StageTurn,
	StageTurn2,
	StageBathroomZoomIn,
	StageBathroomZoomOut,
	StageRacingSetup,
	StageRacingSetup2,
	StageCountdown2,
	StageRacing,
	StageRacingResolution,
	StageGameOver
}
public class GameController : MonoBehaviour {

	public Plate plate;
	public PlayerController player1;
	public PlayerController player2;
	public HostController host;
	public GameObject cameraGO;
	public Countdown countdown;

	public GameStage CurrentStage {get; private set;}

	public string EatingWinnerName {get; private set;}
	public string EatingLoserName {get; private set;}

	public string RaceLoserName {get; private set;}

	private float raceWithThreshold = 12.0f;

	private bool zoomedOutYet;
	private float cameraZoomStartZCoord;

	private PlayerController raceLoser;
	private PlayerController raceWinnner;

	private PlayerController eatingLoser;
	private PlayerController eatingWinnner;

	private bool continueLocked;

	// Use this for initialization
	void Start () {
		StartStage(GameStage.StageStart);
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentStage == GameStage.StageEating)
		{
			if(!plate.HasFood())
			{
				EatingWinnerName = player1.FoodCounter > player2.FoodCounter ? "player 1" : "player 2";
				EatingLoserName = player1.FoodCounter > player2.FoodCounter ? "Player 2" : "Player 1";
				eatingWinnner = player1.FoodCounter > player2.FoodCounter ? player1 : player2;
				eatingLoser = player1.FoodCounter > player2.FoodCounter ? player2 : player1;
				StartStage(GameStage.StageEating + 1);
			}
		}
		else if(CurrentStage == GameStage.StageRacing)
		{
			//Resolve race
			float player1Dist = Vector3.Distance(player1.transform.position,player1.BodyTarget.transform.position);
			float player2Dist = Vector3.Distance(player2.transform.position,player2.BodyTarget.transform.position);

			if(player1Dist <= raceWithThreshold)
			{
				RaceLoserName = "Player 2";
				raceLoser = player2;
				raceWinnner = player1;
				StartStage(CurrentStage+1);
			}
			else if(player2Dist <= raceWithThreshold)
			{
				RaceLoserName = "Player 1";
				raceLoser = player1;
				raceWinnner = player2;
				StartStage(CurrentStage+1);
			}
		}
		else if(CurrentStage == GameStage.StageEatingResolution)
		{
			//NO-OP wait for invoke
		}
		else if(CurrentStage == GameStage.StageCountdown2)
		{
			//NO-OP wait for event
		}
		else if(CurrentStage == GameStage.StageCountdown)
		{
			//NO-OP wait for event
		}
		else
		{
			if(Input.GetKeyDown("space") && !continueLocked)
			{
				StartStage(CurrentStage+1);
			}
		}
	}

	void StartStage(GameStage newStage)
	{
		Debug.Log("Start stage "+newStage);
		CurrentStage = newStage;
		host.PerformActionForStage();
		switch(newStage)
		{
		case GameStage.StageIntro1:
		case GameStage.StageIntro3:
		case GameStage.StageIntro4:
		case GameStage.StageIntro5:
		case GameStage.StageEating:
		case GameStage.StageTurn2:
		case GameStage.StageRacingSetup:
			break;
		case GameStage.StageGameOver:
			//Show credits
			//Restart scene?
			Application.LoadLevel (0);
			break;
		case GameStage.StageEatingResolution:
			eatingWinnner.PerformEatingWinnerAnimation();
			eatingLoser.PerformEatingLoserAnimation();
			Invoke("MoveToNextStage",3.0f);
			break;
		case GameStage.StageRacing:
			player1.UpdateSpeedForFatness();
			player2.UpdateSpeedForFatness();
			break;
		case GameStage.StageTurn:
			break;
		case GameStage.StageBathroomZoomIn:
			continueLocked = true;
			player1.PerformStageAction();
			player2.PerformStageAction();
			cameraGO.GetComponent<SimpleFollowCamera>().enabled = false;
			cameraZoomStartZCoord = cameraGO.transform.position.z;
			cameraGO.GetComponent<CameraZoom>().OnZoomComplete += HandleOnZoomComplete;
			cameraGO.GetComponent<CameraZoom>().Zoom(50,75);
			break;
		case GameStage.StageBathroomZoomOut:
			cameraGO.GetComponent<CameraZoom>().Zoom(cameraZoomStartZCoord,-100);
			break;
		case GameStage.StageRacingResolution:
			raceWinnner.WalkIntoBathroom();
			raceLoser.PerformRaceLosingAnimation();
			break;
		case GameStage.StageCountdown:
			countdown.OnCountdownComplete += HandleOnCountdownComplete;
			countdown.StartCountdown();
			break;
		case GameStage.StageCountdown2:
			countdown.StartCountdown();
			break;
		default:
			StartStage(newStage+1);
			return;
		}
	}

	void HandleOnCountdownComplete ()
	{
		Debug.Log("CountdownComplete");
		MoveToNextStage();
	}

	void MoveToNextStage()
	{
		StartStage(CurrentStage+1);
	}

	void HandleOnZoomComplete ()
	{
		if(!zoomedOutYet)
		{
			zoomedOutYet = true;
			continueLocked = false;
		}
		else
		{
			cameraGO.GetComponent<SimpleFollowCamera>().enabled = true;
			StartStage(CurrentStage+1);
		}
	}
}
