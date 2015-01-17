using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	private troopBehavior troop;
	private List<Node> neighbors;
	private int resCount = 0;
	private int resourceGain = 10; // mad gains

	public GameObject troopsPrefab;
	public int passiveBonusAttack;
	public int passiveBonusDefense;
	public int playerOwner;

	static public int troopCost = 5;
	public GameObject pathObj;


	/* Unit Management */
	
	// Produces units locally by spending resources from the pool
	public void buildUnits (int numNewUnits) {
		if (troop == null) {
			GameObject troopObject = Instantiate (troopsPrefab) as GameObject;
			troop = troopObject.GetComponent<troopBehavior>();
			troop.transform.position = transform.position;
			troop.garrisoned = this;
			troop.speed = 0;
		}
		for (int i = 0; i < numNewUnits; ++i) {
			if (Hammer.PlayerData.players[playerOwner].Resources < troopCost) return;
			Hammer.PlayerData.players[playerOwner].Resources -= troopCost;
			troop.morale = (troop.morale*troop.strength + 100)/(troop.strength+1);
			troop.strength++;
			Debug.Log ("unit produced");
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









	// Set the player owner
	public void setOwner(Player p) {
		playerOwner = (int)p;
	}

	public void setResourceGain(int amt) {

		// Rescale based on amount gain
		transform.localScale += new Vector3 (.5f*(amt / (float)troopCost), 
		                                     .5f*(amt / (float)troopCost), 
		                                     .5f*(amt / (float)troopCost));


		resourceGain = amt;
	}




	// Use this for initialization
	void Awake () {
		neighbors = new List<Node> ();
		playerOwner = (int)Player.AI;
	}
	
	// Update is called once per frame
	void Update () {

		// Test. draws a line between neighboring nodes
		foreach (Node n in neighbors) {
			Debug.DrawLine (transform.position, n.transform.position, new Color(255, 0, 0, 35), 1);
		}
	}

	void FixedUpdate() {
		if (playerOwner == (int)Player.AI)
						return;
		// gain Resources for the owner player
		if (resCount>100) {
			Hammer.PlayerData.players[playerOwner].Resources += resourceGain;
			Hammer.PlayerData.players[playerOwner].addTotal (resourceGain);
			resCount = 0;
		}
		resCount++;
	}
}
