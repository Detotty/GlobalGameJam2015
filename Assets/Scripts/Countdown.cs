using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void CountdownFinished();

public class Countdown : MonoBehaviour {

	public event CountdownFinished OnCountdownComplete;

	public Text countdownText;
	
	public void StartCountdown()
	{
		StartCoroutine(CountdownRoutine());
	}

	private IEnumerator CountdownRoutine()
	{
		int counter = 3;
		countdownText.enabled = true;
		while(true)
		{
			if(counter > 0)
			{
				countdownText.text = counter.ToString();
				counter--;
				yield return new WaitForSeconds(1.0f);
			}
			else
			{
				countdownText.text = "Go";
				if(OnCountdownComplete != null)
				{
					OnCountdownComplete();
				}
				yield return new WaitForSeconds(0.5f);
				countdownText.enabled = false;
				break;
			}
		}
	}
}
