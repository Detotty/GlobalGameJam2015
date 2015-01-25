using UnityEngine;
using System.Collections;

public delegate void CameraZoomed();

public class CameraZoom : MonoBehaviour {

	public event CameraZoomed OnZoomComplete;

	private float _stopThreshold = 1.0f;

	public void Zoom(float zCoord, float speed)
	{
		StartCoroutine(ZoomRoutine(zCoord,speed));
	}

	private IEnumerator ZoomRoutine(float zCoord, float speed)
	{
		float currZCoord = transform.position.z;
		while(Mathf.Abs(currZCoord - zCoord) > _stopThreshold)
		{
			transform.position += new Vector3(0,0,speed*Time.deltaTime);
			currZCoord = transform.position.z;
			yield return new WaitForEndOfFrame();
		}

		if(OnZoomComplete != null)
		{
			OnZoomComplete();
		}
	}
}
