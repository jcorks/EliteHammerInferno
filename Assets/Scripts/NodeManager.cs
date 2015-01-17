using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {
	public int nodeSpread; 		// Total range of the map
	public int tryCount;		// Number of nodes to try to make. May vary from actual nodes 
	public int minTolerance;	// Minimum distance allowable for a node connection
	public int maxTolerance;	// Maximum distance allowable for a node connection
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



	}






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
