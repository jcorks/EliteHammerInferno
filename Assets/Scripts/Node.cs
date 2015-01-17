using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	private int numUnits = 0;
	private List<Node> neighbors;

	public int passiveBonusAttack;
	public int passiveBonusDefense;
	public Player playerOwner;


	// Adds a number of units to this node
	public void addUnits(int num) {
		numUnits += num;
	}

	// Removes a specific number of nodes
	public void remUnits(int num) {
		numUnits -= num;
		if (numUnits < 0) numUnits = 0;
	}

	// Gets the current count of units
	public int unitCount() {
		return numUnits;
	}


	// Node attaching. Nodes that are attached are neighbors.
	// Neighbors can move units to other neighbors.
	public void connectNode(Node n) {
		if (!n || n.GetInstanceID () == GetInstanceID ()) return;
		neighbors.Add (n);
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





	// Use this for initialization
	void Awake () {
		neighbors = new List<Node> ();
	}
	
	// Update is called once per frame
	void Update () {

		foreach (Node n in neighbors) {
			Debug.DrawLine (transform.position, n.transform.position, new Color(255, 0, 0, 255), 1);
		}
	}
}
