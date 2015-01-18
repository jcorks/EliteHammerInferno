using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum CursorDirection {
	UP,
	DOWN,
	LEFT,
	RIGHT
};

public class NodeCursor : MonoBehaviour {

	public GameObject NodeObj;
	public Sprite angelTex;
	public Sprite devilTex;
	public GameObject arrow;
	private GameObject arrowObj;


	private Node currentNode = null;
	private Player owner;
	private Hero hero;
	private Pathway lastDirPath; // path way of the last node found via getNext / Pre node
	public bool isMoving = false;

	void easeTo(Vector3 v) {
		Vector3 pos = transform.position;
		pos = (pos + currentNode.transform.position) / 2.0f;
		transform.position = pos;
	}




	// Moves the cursor to the nearest candidate node in a directoin
	public void goToNode(CursorDirection dir) {
		Node goTo = getDirNode (dir, true);
		if (goTo) setNode (goTo);
	}

	Node getDirNode(CursorDirection dir, bool restrict) {
		
		List<Node> nodes = currentNode.getNeighbors ();
		float bestHeight = helper_getCurrentPlacement(dir);
		int bestIndex = -1;
		for (int i = 0; i < nodes.Count; ++i) {
			
			
			Debug.DrawLine(currentNode.transform.position, nodes[i].transform.position, new Color(255, 255, 0), 1, false);
			
			float thisHeight = helper_getThisDist(dir, nodes[i]);
			if (helper_compareDist(dir, thisHeight,bestHeight)) {
				if (restrict && nodes[i].playerOwner != owner) continue; 
				bestIndex = i;
				bestHeight = thisHeight;
			}
		}

		// get best one and set lastDirPath
		if (bestIndex != -1) {
			return nodes [bestIndex];
		}
		return null;
	}
	float helper_getCurrentPlacement(CursorDirection c) {
		switch (c) {
			case CursorDirection.UP:
			case CursorDirection.DOWN:
				return transform.position.z;
			case CursorDirection.LEFT:
			case CursorDirection.RIGHT:
				return transform.position.x;
			default: return 0.0f;
		};
	}

	bool helper_compareDist(CursorDirection c, float here, float best) {
		switch (c) {
		case CursorDirection.UP:
		case CursorDirection.RIGHT:
			return here > best;
		case CursorDirection.LEFT:
		case CursorDirection.DOWN:
			return here < best;
		default: return false;
		};
	}

	float helper_getThisDist(CursorDirection c, Node n) {
		switch (c) {
		case CursorDirection.UP:
		case CursorDirection.DOWN:
			return n.transform.position.z;
		case CursorDirection.LEFT:
		case CursorDirection.RIGHT:
			return n.transform.position.x;
		default: return 0.0f;	
		};
	}




	public void setNode(Node n) {
		currentNode = n;

	}



	public void setType(Player p) {
		owner = p;
		hero = Hammer.PlayerData.players [(int)p].hero;
		if (hero == Hero.Hero_1) {
			print ("Set Hero1");	
			GetComponent<SpriteRenderer>().sprite = angelTex;
		} else if (hero == Hero.Hero_2) {
			print ("Set Hero2");
			GetComponent<SpriteRenderer>().sprite = devilTex;
		}
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		Debug.DrawLine (new Vector3 (0.0f, 30.0f, 0.0f), transform.position, new Color(255, 255, 0, 255));

		if (currentNode)
			easeTo (currentNode.transform.position);


		if (isMoving) {
			handleMove();
			return;
		}

		// Begin move mode
		if (Hammer.PlayerData.players [(int)owner].move () && 
		    currentNode.hasTroop()) {

			moveSelected = null;
			arrowObj = (GameObject) Instantiate (arrow);
			arrowObj.transform.position = currentNode.transform.position;

			
			print ("started move");
			isMoving = true;
		}




		if (Hammer.PlayerData.players[(int)owner].build () || Input.GetKeyDown(KeyCode.A) ) {
			Debug.Log ("unit produced");
			currentNode.buildUnits (500);
		}
	}




	Vector3 arrowPos;
	Node moveSelected;
	float arrowCounter = 0;
	int moveNodeIndex = 0;

	Node getNextNode() {
		moveNodeIndex++;
		List<Node> nl = currentNode.getNeighbors ();
		if (moveNodeIndex >= nl.Count) {
			moveNodeIndex = 0;
		}
		lastDirPath = currentNode.getNeighborPaths () [moveNodeIndex];
		return nl [moveNodeIndex];
	}

	Node getPrevNode() {
		moveNodeIndex--;
		List<Node> nl = currentNode.getNeighbors ();
		if (moveNodeIndex < 0 ) {
			moveNodeIndex = nl.Count - 1;
		}
		lastDirPath = currentNode.getNeighborPaths () [moveNodeIndex];
		return nl [moveNodeIndex];
	}

	void handleMove() {

		if (lastDirPath)
		    lastDirPath.setSelected(false);
		    


		if (Hammer.PlayerData.players [(int)owner].up ()) {
			moveSelected = getNextNode();
		}
		if (Hammer.PlayerData.players [(int)owner].down ()) {
			moveSelected = getPrevNode();
		}
		if (Hammer.PlayerData.players [(int)owner].left ()) {
			moveSelected = getNextNode();
		}
		if (Hammer.PlayerData.players [(int)owner].right ()) {
			moveSelected = getPrevNode();
		}



		if (moveSelected) {
			if (lastDirPath)
				lastDirPath.setSelected(true);
			arrowPos = moveSelected.transform.position;
			arrowObj.transform.position = arrowPos + 
				new Vector3(0.0f, 0.4f ,.2f * Mathf.Sin (arrowCounter)*.8f + .9f);
			arrowCounter += .1f;
		}



		if (Hammer.PlayerData.players[(int)owner].move () && moveSelected) {
			// do move stuff
			currentNode.moveTroop(moveSelected);


				

			// end move
			isMoving = false;
			Destroy(arrowObj);
			print ("move end");
			lastDirPath.setSelected(false);
			return;
		}
	

	}


}



