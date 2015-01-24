using UnityEngine;
using System.Collections;

public class SimpleFollowCamera : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;

	[SerializeField]
	private float _targetFollowDistance;

	private float _jitterThreshold = 0.15f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float player1FollowDist = Vector3.Distance(Player1.transform.position,transform.position);
		float player2FollowDist = Vector3.Distance(Player2.transform.position,transform.position);

		float smallestFollowDist = Mathf.Min(player1FollowDist,player2FollowDist);

		if(smallestFollowDist > _targetFollowDistance)
		{
			float diff = smallestFollowDist - _targetFollowDistance;
			transform.position += new Vector3(0,0,diff);
		}
	}
}
