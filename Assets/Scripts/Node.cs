using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	private troopBehavior troop = null;
	private List<Node> neighbors;
	private List<Pathway> neighborPaths;
	private int resCount = 0;
	private int resourceGain = 10; // mad gains
	private bool besieged = false;

	public GameObject troopsPrefab;
	public int passiveBonusAttack;
	public int passiveBonusDefense;
	public Player playerOwner;

	private bool base_b = false;

	static public int troopCost = 5;
	public GameObject pathObj;


	/* Unit Management */
	
	// Produces units locally by spending resources from the pool
	public void buildUnits (int numNewUnits) {
		if (troop == null) {
			GameObject troopObject = Instantiate (troopsPrefab) as GameObject;
			troop = troopObject.GetComponent<troopBehavior>();
			troop.transform.position = transform.position;
			troop.transform.localScale  = new Vector3 (4.0f, 4.0f, 4.0f);
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
			Debug.Log ("unit produced:" + troop.strength);
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









	// Set the player owner
	public void setOwner(Player p) {
		playerOwner = p;
	}

	public void setResourceGain(int amt) {

		// Rescale based on amount gain
		transform.localScale += new Vector3 (.2f*(amt / (float)troopCost), 
		                                     .2f*(amt / (float)troopCost), 
		                                     .2f*(amt / (float)troopCost));


		resourceGain = amt;
	}

	public void makeBase() {
		// update visuals
		transform.localScale = new Vector3 (.7f, .7f, .7f);
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
	}

	void FixedUpdate() {

		// gain Resources for the owner player
		/*if (resCount>100 && !besieged) {
			Hammer.PlayerData.players[playerOwner].Resources += resourceGain;
			resCount = 0;
		}

		resCount++;*/
	}

	// returns whether or not a troop exists on this node
	public bool hasTroop() {
		return troop != null;	
	}

	public void moveTroop(Node dest) {
		bool found = false;
		foreach (Node n in neighbors) {
			if (n.GetInstanceID() == dest.GetInstanceID()) found = true;
		}
		if (!found) {
			print("THIS SHOULDNT HAPPEN");
		}



		troop.transform.position = dest.transform.position;
	}

}
