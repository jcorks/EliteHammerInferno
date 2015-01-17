using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {
	private float nodeSpread			=8.7f; 		// Total range of the map
	private int tryCount				=1000;		// Number of nodes to try to make. May vary from actual nodes 
	private float minTolerance			=1.0f;		// Minimum distance allowable for a node connection
	private float maxTolerance			=2.4f;		// Maximum distance allowable for a node connection
	private int maxNeighbors			=4;			// maximum number of neighbors
	private float minimumNodeProximity = 1.5f;	

	private static List<GameObject> nodeObjs; 


	public GameObject NodeObject;



	public GameObject[] playerCursors = new GameObject[5];



	void Awake() {
		generateNodes ();
	}






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		processPlayer (Player.PLAYER_1);
		processPlayer (Player.PLAYER_2);
		//processPlayer (Player.PLAYER_3);
		//processPlayer (Player.PLAYER_4);
	}



	void processPlayer(Player p) {

	}



	void generateNodes() {
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
}
