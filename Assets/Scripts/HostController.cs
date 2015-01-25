using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HostController : MonoBehaviour {

	public Camera HostCamera;
	public GameObject UIPanel;
	public Text DialogText;
	public Text ContinueText;
	public GameController gameController;
	
	public void PerformActionForStage()
	{
		switch(gameController.CurrentStage)
		{
		case GameStage.StageIntro1:
		case GameStage.StageIntro2:
		case GameStage.StageIntro3:
		case GameStage.StageIntro4:
		case GameStage.StageIntro5:
		case GameStage.StageTurn:
		case GameStage.StageTurn2:
		case GameStage.StageBathroomZoomIn:
		case GameStage.StageRacingSetup:
		case GameStage.StageRacingSetup2:
		case GameStage.StageRacingResolution:
			SetVisible(true);
			DialogText.text = DialogForStage(gameController.CurrentStage);
			break;
		default:
			SetVisible(false);
			return;
		}
	}

	private void SetVisible(bool visible)
	{
		HostCamera.enabled = visible;
		UIPanel.SetActive(visible);
	}

	private string DialogForStage(GameStage stage)
	{
		switch(stage)
		{
		case GameStage.StageIntro1:
			return "Welcome to the Chowdown Showdown! I'm your host Chef Dickie. Go grab a friend to play with you. The game is simple: whoever can eat the most food wins";
		case GameStage.StageIntro2:
			return "The game is simple: there is a large plate of junk food sitting between the two of you. Whoever eats the most amount of food in the shortest time is crowned our winner!";
		case GameStage.StageIntro3:
			return "Player 1 you are on the left. Alternate pressing the A and S keys to shovel food into your fat mouth. Player 2 you are on the right. Same instructions but use the K and L keys.";
		case GameStage.StageIntro4:
			return "If you don't have a friend handy, don't worry, you can control both players yourself! I've heard if you lay on your arm until you hand falls asleep it feels like someone else is doing it.";
		case GameStage.StageIntro5:
			return "Are you ready for the Chowdown Showdown?";
		case GameStage.StageTurn:
			return "Don't celebreate quite yet "+gameController.EatingWinnerName+" What I didn't tell you at the start was that all of that food was made with an experimental new SUPER LAXATIVE.";
		case GameStage.StageTurn2:
			return "And the restaurant only has...";
		case GameStage.StageBathroomZoomIn:
			return "ONE BATHROOM";
		case GameStage.StageRacingSetup:
			return "Looks like its a foot race now. "+gameController.EatingLoserName+" ate less so they will be faster. Use the same buttons as before to waddle your way to the bathroom";
		case GameStage.StageRacingSetup2:
			return "Remember the buttons you used to shovel all that laxative food into your mouth? Use those same ones to run to the bathroom!";
		case GameStage.StageRacingResolution:
			return ""+gameController.RaceLoserName+" is our loser. Join us next time on Chowdown Showdown";
		default:
			return "";
		}
	}
}
