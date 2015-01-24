using UnityEngine;
using System.Collections;

public enum GameStage
{
	StageStart,
	StageIntro1,
	StageIntro2,
	StageCoutdown,
	StageEating,
	StageTurn,
	StageTurn2,
	StageBathroomZoom,
	StageCountdown2,
	StageRacing,
	StageRacingResolution,
	StageGameOver
}
public class GameController : MonoBehaviour {

	public Plate plate;
	public PlayerController player1;
	public PlayerController player2;

	public GameStage CurrentStage {get; private set;}

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
				StartStage(GameStage.StageEating + 1);
			}
		}
	}

	void StartStage(GameStage newStage)
	{
		Debug.Log("Start stage "+newStage);
		CurrentStage = newStage;
		switch(newStage)
		{
		case GameStage.StageRacing:
			break;
		case GameStage.StageEating:
			break;
		case GameStage.StageBathroomZoom:
			player1.PerformStageAction();
			player2.PerformStageAction();
			StartStage(newStage+1);
			break;
		default:
			StartStage(newStage+1);
			return;
		}
	}
}
