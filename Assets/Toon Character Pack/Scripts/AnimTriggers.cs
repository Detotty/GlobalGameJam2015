using UnityEngine;
using System.Collections;

public class AnimTriggers : MonoBehaviour
{	

	private const float EATING_IDLE_TIMEOUT = 0.6f;
	private const float RACING_IDLE_TIMEOUT = 0.5f;

	// Create a reference to the animator component
	private Animator animator;

	private float eatingIdleTimer;
	private float racingIdleTimer;
	
	void Start ()
	{
		// initialise the reference to the animator component
		animator = gameObject.GetComponent<Animator>();
	}
	
	// check for colliders with a Trigger collider
	// if we are entering something called JumpTrigger, set a bool parameter called JumpDown to true..
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "JumpTrigger")
		{
			animator.SetBool("JumpDown", true);	
		}
	}
	
	// ..and when leaving the trigger, reset it to false
	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.name == "JumpTrigger")
		{
			animator.SetBool("JumpDown", false);
		}
	}

	public void IsEating(bool eating)
	{
		animator.SetBool("Eating", eating);
		if(eating)
		{
			eatingIdleTimer = EATING_IDLE_TIMEOUT;
		}
	}
	
	public void IsRacing(bool racing)
	{
		animator.SetBool("Racing", racing);
		if(racing)
		{
			racingIdleTimer = RACING_IDLE_TIMEOUT;
		}
	}

	public void Update()
	{
		if(eatingIdleTimer > 0.0f)
		{
			eatingIdleTimer -= Time.deltaTime;
			if(eatingIdleTimer <= 0.0f)
			{
				IsEating(false);
				eatingIdleTimer = 0.0f;
			}
		}

		if(racingIdleTimer > 0.0f)
		{
			racingIdleTimer -= Time.deltaTime;
			if(racingIdleTimer <= 0.0f)
			{
				IsRacing(false);
				racingIdleTimer = 0.0f;
			}
		}
	}
}
