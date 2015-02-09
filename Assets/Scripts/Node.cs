using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	private List<Node> neighbors;
	private List<Pathway> neighborPaths;
	private int resCount = 0;
	private int resourceGain = 100; // mad gains
	private bool heroFirst = false;

	public troopBehavior troop = null;
	public GameObject troopsPrefab;
	public int passiveBonusAttack;
	public int passiveBonusDefense;
	public Player playerOwner;

	private bool base_b = false;

	static public int troopCost = 5;
	public GameObject pathObj;

	private float moveTimer = 0f;
	private bool moveTime = false;

	/* Unit Management */
	
	// Produces units locally by spending resources from the pool
	public void buildUnits (int numNewUnits) {
		if (moveTime || (troop && troop.fighting) )
			return;
		// && Hammer.PlayerData.players[(int)playerOwner].Resources > 0
		if (troop == null && Hammer.PlayerData.players[(int)playerOwner].Resources > 0) {
			/*if (heroFirst == false) {
				troop.attached = Hero.Hero_1;
				heroFirst = true;
			}*/
			Debug.Log ("unit made");
			GameObject troopObject = Instantiate (troopsPrefab) as GameObject;
			troop = troopObject.GetComponent<troopBehavior>();
			troop.transform.position = transform.position;
			troop.transform.localScale  = new Vector3 (2.0f, 2.0f, 2.0f);
			troop.garrisoned = this;
			troop.attached = Hero.None;
			troop.speed = 0;
			troop.setOwner(playerOwner);
		}
		for (int i = 0; i < numNewUnits; ++i) {
			if (Hammer.PlayerData.players[(int)playerOwner].Resources < troopCost) return;
			Hammer.PlayerData.players[(int)playerOwner].Resources -= troopCost;
			troop.morale = (troop.morale*troop.strength + 100)/(troop.strength+1);
			troop.strength++;
			//Debug.Log ("unit produced:" + troop.strength);
		}
	}





	/* Node Management */

	// Node attaching. Nodes that are attached are neighbors.
	// Neighbors can move units to other neighbors.
	public void connectNode(Node n) {
		if (!n || n.GetInstanceID () == GetInstanceID ()) return;
		foreach (Node i in neighbors) {
			// return if already connected
			if (i.GetInstanceID () == n.GetInstanceID ()) return;
		}
		neighbors.Add (n);
		n.neighbors.Add(this);

		GameObject path = (GameObject)Instantiate (pathObj);
		Pathway newPath = path.GetComponent<Pathway> ();
		newPath.setPath (transform.position, n.transform.position);
		neighborPaths.Add (newPath);
		n.neighborPaths.Add (newPath);
	}

	// Removes a neighbor
	public void removeNode(Node rem) {
		if (!rem) return;
		for(int n = 0; n < neighbors.Count; ++n) {
			if (neighbors[n].GetInstanceID() == rem.GetInstanceID()) {
				neighbors.RemoveAt(n);
			}
		}
	}

	// Gets all the neighbors of a node
	public List<Node> getNeighbors() {
		return neighbors;
	}

	public List<Pathway> getNeighborPaths() {
		return neighborPaths;
	}



	public void activateSpecial() {

	}





	// Set the player owner
	public void setOwner(Player p) {
		playerOwner = p;
		if (p == Player.PLAYER_1) {
			GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 255, 255);
		} else {
			GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0, 255);
		}
	}

	public void setResourceGain(int amt) {

		// Rescale based on amount gain

		/*
		GetComponentInChildren<SpriteRenderer>().localScale += new Vector3 (.2f*(amt / (float)troopCost), 
		                                     .2f*(amt / (float)troopCost), 
		                                     .2f*(amt / (float)troopCost));
		*/


		resourceGain = amt;
	}

	public void makeBase() {
		// update visuals
		//transform.localScale = new Vector3 (.7f, .7f, .7f);
		base_b = true;
	}

	public bool isBase() {
		return base_b;
	}



	// Use this for initialization
	void Awake () {
		neighbors = new List<Node> ();
		neighborPaths = new List<Pathway> ();
		playerOwner = Player.AI;
	}

	/*void OnTriggerEnter(Collider coll){
		Debug.Log (coll.gameObject);
		GameObject collidedWith = coll.gameObject;
		troopBehavior clash = collidedWith.GetComponent<troopBehavior>();

		if (clash.troopOwner == playerOwner) {
			fighting = true;
			opponent = clash;
			Debug.Log ("clash!");
			priorSpeed = speed;
			speed = 0;
		}
	}*/

	// Update is called once per frame
	void Update () {

		// Test. draws a line between neighboring nodes
		foreach (Node n in neighbors) {
			Debug.DrawLine (transform.position, n.transform.position, new Color(255, 0, 0, 35), 1);
		}

		if (moveTime) {
			moveTimer += Time.deltaTime;
			if (moveTimer > 2) {
				moveTime = false;
				moveTimer = 0f;
			}
		}


	}

	void FixedUpdate() {

		// gain Resources for the owner player
		if (resCount>100) {
			Hammer.PlayerData.players[(int)playerOwner].Resources += resourceGain;
			resCount = 0;
		}

		resCount++;
	}

	// returns whether or not a troop exists on this node
	public bool hasTroop() {
		return troop != null;	
	}

	public void moveTroop(Node dest) {
		if (!hasTroop ())
						return;
		bool found = false;
		Vector3 directionVector;
		foreach (Node n in neighbors) {
			if (n.GetInstanceID() == dest.GetInstanceID()) {
				found = true;
				directionVector = n.gameObject.transform.position - this.gameObject.transform.position; 
				//troop.gameObject.transform.Rotate (new Vector3(Vector3.Angle(new Vector3(1f, 0f, 0f),
				                                                            // directionVector), 0f ,0f));
				troop.gameObject.transform.Rotate (0f, 0f ,0f);
				troop.angleVector = directionVector;
				troop.speed = 0.005f;
				//dest.troop = troop;
				troop.garrisoned = null;
				troop = null;
			}
		}
		moveTime = true;
		if (!found) {
			print("THIS SHOULDNT HAPPEN");
		}




	}

}
