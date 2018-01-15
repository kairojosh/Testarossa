using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public LayerMask outOfBoundsMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Destroys bullet if it collides with any object associated with the OutOfBounds Mask
		if (Physics.CheckSphere (gameObject.transform.position, gameObject.transform.lossyScale.x, outOfBoundsMask)) {
			Destroy (gameObject);
		}


		}
	}

