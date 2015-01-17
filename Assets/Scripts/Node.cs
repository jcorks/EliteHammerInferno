using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	private troopBehavior troop;
	private List<Node> neighbors;
	private int resCount = 0;
	private int resourceGain = 0; // mad gains

	public int passiveBonusAttack;
	public int passiveBonusDefense;
	public int playerOwner;

	static public int troopCost = 5;
	public GameObject pathObj;


	/* Unit Management */
	
	// Produces units locally by spending resources from the pool
	public void buildUnits (int numNewUnits) {
		for (int i = 0; i < numNewUnits; ++i) {
			if (PlayerData.Resources[playerOwner] < troopCost) return;
			PlayerData.Resources[playerOwner] -= troopCost;
			troop.strength++;
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

		// gain Resources for the owner player
		if (resCount>100) {
			PlayerData.Resources[playerOwner] += resourceGain;
			resCount = 0;
		}
		resCount++;
	}
}
