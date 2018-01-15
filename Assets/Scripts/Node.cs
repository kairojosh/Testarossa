using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class doe not inherit from monobehaviour
public class Node {
    

	public bool inBounds;
	public int gridX, gridY, parentX, parentY;
	public Vector3 position;
	public Node parentNode;
	public int gCost, hCost;
	public int fCost {
		get {
			return gCost + hCost;
		}

	}



	//Instantiater Method, underscore before the identifier is convention, example of encapsulation
	public Node(bool _inBounds, Vector3 _position, int _gridX, int _gridY) {
		inBounds = _inBounds;
		position = _position;
		gridX = _gridX;
		gridY = _gridY;

	}



}
