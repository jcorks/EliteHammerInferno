﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {
	public float nodeSpread; 		// Total range of the map
	public int tryCount;		// Number of nodes to try to make. May vary from actual nodes 
	public float minTolerance;	// Minimum distance allowable for a node connection
	public float maxTolerance;	// Maximum distance allowable for a node connection
	public int maxNeighbors;	// maximum number of neighbors
	public float minimumNodeProximity;

	private static List<GameObject> nodeObjs; 


	public GameObject NodeObject;


	void Awake() {
		nodeObjs = new List<GameObject>();
		for (int i = 0; i < tryCount; ++i) {
			// First get the position of the next node
			bool tooClose = false;
			Vector3 newPos = new Vector3(Random.value*nodeSpread - nodeSpread/2.0f,
			                     0.0f, 
			                     Random.value*nodeSpread - nodeSpread/2.0f);
			for(int k = 0; k < nodeObjs.Count; ++k) {
				if (Vector3.Distance (newPos, nodeObjs[k].transform.position) < 
				    minimumNodeProximity) {
					tooClose = true;
					break;;
				}
			}
			// If the node conflicted with a current position based on the restrictions
			// then we have a failed node.
			if (tooClose) continue;


			GameObject n = (GameObject)Instantiate(NodeObject);
			nodeObjs.Add(n);
			Node curNode = n.GetComponent<Node>();
			curNode.transform.position = newPos;
		}

		// connect them based on proximity
		for (int i = 0; i < nodeObjs.Count; ++i) {
			int numConnected = 0;


			for(int k = 1; k < nodeObjs.Count; ++k) {

				float thisDist = Vector3.Distance(nodeObjs[k].transform.position,
				                                  nodeObjs[i].transform.position);

				if (k != i && thisDist > minTolerance && thisDist < maxTolerance) {
					nodeObjs[i].GetComponent<Node>().connectNode(
						nodeObjs[k].GetComponent<Node>()
					);
					numConnected++;
					if (numConnected == maxNeighbors) break;
	
				}

			}


		}


		// Finally determine the node gain amounts
		Vector3 origin = new Vector3 (0.0f, 0.0f, 0.0f);
		for (int i = 0; i < nodeObjs.Count; ++i) {

			// Determined by roximity to the origin
			int dist = (int)(Vector3.Distance (nodeObjs[i].transform.position, origin));
			
			nodeObjs[i].GetComponent<Node>().
				setResourceGain((int)(nodeSpread/2.0 - dist)); 
		}

		print (nodeObjs.Count);



	}






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
