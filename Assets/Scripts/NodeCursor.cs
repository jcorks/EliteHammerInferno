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


	private Node currentNode = null;
	private Player owner;
	private Hero hero;

	void easeTo(Vector3 v) {
		Vector3 pos = transform.position;
		pos = (pos + currentNode.transform.position) / 2.0f;
		transform.position = pos;
	}




	// Moves the cursor to the nearest candidate node in a directoin
	public void goToNode(CursorDirection dir) {

		List<Node> nodes = currentNode.getNeighbors ();
		float bestHeight = helper_getCurrentPlacement(dir);
		int bestIndex = -1;
		for (int i = 0; i < nodes.Count; ++i) {


			Debug.DrawLine(currentNode.transform.position, nodes[i].transform.position, new Color(255, 255, 0), 1, false);

			float thisHeight = helper_getThisDist(dir, nodes[i]);
			if (nodes[i].playerOwner == owner && helper_compareDist(dir, thisHeight,bestHeight)) {
				bestIndex = i;
				bestHeight = thisHeight;
			}
		}
		if (bestIndex!=-1)
			setNode (nodes [bestIndex]);
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


		if (Hammer.PlayerData.players[(int)owner].build () || Input.GetKeyDown(KeyCode.A) ) {
			Debug.Log ("unit produced");
			currentNode.buildUnits (500);
		}
	}
}
