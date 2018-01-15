using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	GridScript grid;
	public Transform AI; 
	public Transform player;

	void Update(){
		FindPath (AI.position, player.position);


	}

	void Awake(){
		//Accesses necessary functions
		grid = GetComponent<GridScript> ();

	}

	void FindPath(Vector3 startPosition, Vector3 targetPosition) {
		Node startNode = grid.PositionConvertNode (startPosition);
		Node targetNode = grid.PositionConvertNode (targetPosition);

		List<Node> openNodes = new List<Node> ();
		List<Node> closedNodes = new List<Node> ();
		openNodes.Add (startNode);

		//Searches through all open nodes to find the node with lowest fCost
		while (openNodes.Count > 0 &&  targetNode.inBounds == true) {

            //Purely here for the first step
			Node activeNode = openNodes [0];

            //The slowest part of the whole code, it crashes it occasionally because it is so inefficient
			for (int i = 1; i < openNodes.Count; i++) {
				if (openNodes [i].fCost < activeNode.fCost || activeNode.fCost == openNodes [i].fCost && openNodes [i].hCost < activeNode.hCost) {
					activeNode = openNodes [i];
				}

			}

			openNodes.Remove (activeNode);
			closedNodes.Add (activeNode);

			if (activeNode == targetNode) {
				ReturnPath (startNode, targetNode);
				return;

			}

			foreach (Node adjacent in grid.GetAdjacent(activeNode)) {
				if (!adjacent.inBounds || closedNodes.Contains (adjacent)) {
					continue;
				}
				int movementCost = activeNode.gCost + FindHCost (activeNode, adjacent);
				if (movementCost < adjacent.gCost || !openNodes.Contains (adjacent)) {
					adjacent.gCost = movementCost;
					adjacent.hCost = FindHCost (adjacent, targetNode);
					adjacent.parentNode = activeNode;

					if (!openNodes.Contains (adjacent)) {
						openNodes.Add (adjacent);
					}

				}
			}


		}
	}

		


	int FindHCost(Node node, Node endNode){
 

		int xDistance = Mathf.Abs(node.gridX - endNode.gridX);
		int yDistance = Mathf.Abs(node.gridY - endNode.gridY);
		int Cost;
        

		if (xDistance > yDistance) {
			Cost = 14 * yDistance + 10 * (xDistance - yDistance);

		} else {
			Cost = 14 * xDistance + 10 * (yDistance - xDistance);
		}
        return Cost;
     

	}

	void ReturnPath(Node startNode, Node endNode){
		List<Node> Path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			Path.Add (currentNode);
			currentNode = currentNode.parentNode;

		}
		Path.Reverse ();
		grid.path = Path;


	}



}