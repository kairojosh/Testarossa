using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public  Transform CameraTarget;
	public float cameraHeight = 10f;
	public float lerpValue = 1f;
	// Use this for initialization
	void Start () {

	}
	

	void FixedUpdate () {
		Vector3 currentPosition = this.transform.position;
		transform.position  = new Vector3 ( (Mathf.Lerp(currentPosition.x, CameraTarget.position.x,lerpValue*Time.deltaTime)) , cameraHeight , (Mathf.Lerp(currentPosition.z,CameraTarget.position.z, lerpValue*Time.deltaTime)));

	}
}
