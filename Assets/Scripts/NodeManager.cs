using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class NodeManager : MonoBehaviour {
	/*
	private float nodeSpread			=8.7f; 		// Total range of the map
	private int tryCount				=4000;		// Number of nodes to try to make. May vary from actual nodes 
	private float minTolerance			=1.0f;		// Minimum distance allowable for a node connection
	private float maxTolerance			=2.4f;		// Maximum distance allowable for a node connection
	private int maxNeighbors			=4;			// maximum number of neighbors
	private float minimumNodeProximity = 1.5f;	
	*/

	private float nodeSpread			=4.7f; 		// Total range of the map
	private int tryCount				=4000;		// Number of nodes to try to make. May vary from actual nodes 
	private float minTolerance			=1.0f;		// Minimum distance allowable for a node connection
	private float maxTolerance			=5f;		// Maximum distance allowable for a node connection
	private int maxNeighbors			=2;			// maximum number of neighbors
	private float minimumNodeProximity = 3f;	

	private static List<GameObject> nodeObjs; 
	public GameObject NodeObject;
	public GameObject NodeCursorObj;
	private GameObject[] playerCursors = new GameObject[5];




	void Awake() {

		//TODO remove
		Hammer.PlayerData.init ();



		Hammer.PlayerData.players [0].setHero (Hero.Hero_1);
		Hammer.PlayerData.players [1].setHero (Hero.Hero_2);





		
		generateNodes ();



	}






	// Use this for initialization
	void Start () {

	}

	void FixedUpdate() {

	}

	// Update is called once per frame
	void Update () {
		GameObject n = GameObject.FindGameObjectWithTag ("P1Res");
		n.GetComponent<Text> ().text = "Player1 Resources:\n" + Hammer.PlayerData.players [0].Resources.ToString ();
		n = GameObject.FindGameObjectWithTag ("P2Res");
		n.GetComponent<Text> ().text = "Player2 Resources:\n" + Hammer.PlayerData.players [1].Resources.ToString ();


		processPlayer (Player.PLAYER_1);
		processPlayer (Player.PLAYER_2);
		//processPlayer (Player.PLAYER_3);
		//processPlayer (Player.PLAYER_4);

		

	}



	void processPlayer(Player p) {
	
		if (playerCursors [(int)p].GetComponent<NodeCursor> ().isMoving)
						return;

		if (Hammer.PlayerData.players[(int)p].up()) {

			playerCursors[(int)p].GetComponent<NodeCursor>().goToNode(CursorDirection.UP);	
		}

		if (Hammer.PlayerData.players[(int)p].down()) {
			playerCursors[(int)p].GetComponent<NodeCursor>().goToNode(CursorDirection.DOWN);
		}


		if (Hammer.PlayerData.players[(int)p].left()) {
			playerCursors[(int)p].GetComponent<NodeCursor>().goToNode(CursorDirection.LEFT);
		}

		if (Hammer.PlayerData.players[(int)p].right()) {
			playerCursors[(int)p].GetComponent<NodeCursor>().goToNode(CursorDirection.RIGHT);
		}

	}





	void generateNodes() {
		nodeObjs = new List<GameObject>();

		float leftMost = float.MaxValue;
		float rightMost = float.MaxValue;
		Node rightMostNode = null;
		Node leftMostNode = null;
		Vector3 leftPoint = new Vector3(-6.0f, 0.0f, 0.0f);
		Vector3 rightPoint = new Vector3 (6.0f, 0.0f, 0.0f);

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

			// We also want the rightmost and leftmost nodes
			float dist = Vector3.Distance(newPos, leftPoint);
			if (dist < leftMost) {
				leftMost = dist;
				leftMostNode = curNode;
			}

			dist = Vector3.Distance(newPos, rightPoint);
			if (dist < rightMost) {
				rightMost = dist;
				rightMostNode = curNode;
			}


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





		// Then place the player bases
		GameObject newObj = (GameObject)Instantiate(NodeObject);
		nodeObjs.Add(newObj);
		Node cNode = newObj.GetComponent<Node>();
		cNode.makeBase ();
		cNode.transform.position = new Vector3(6.0f, 0.0f, 0.0f);

		cNode.connectNode (rightMostNode);
		cNode.setOwner (Player.PLAYER_2);
		playerCursors[1] = (GameObject)Instantiate (NodeCursorObj);
		playerCursors[1].GetComponent<NodeCursor> ().setNode (cNode);
		playerCursors[1].GetComponent<NodeCursor> ().setType (Player.PLAYER_2);

		
		newObj = (GameObject)Instantiate(NodeObject);
		nodeObjs.Add(newObj);
		cNode = newObj.GetComponent<Node>();
		cNode.makeBase ();
		cNode.transform.position = new Vector3(-6.0f, 0.0f, 0.0f);

		cNode.connectNode (leftMostNode);
		cNode.setOwner (Player.PLAYER_1);
		playerCursors[0] = (GameObject)Instantiate (NodeCursorObj);
		playerCursors[0].GetComponent<NodeCursor> ().setNode (cNode);
		playerCursors[0].GetComponent<NodeCursor> ().setType (Player.PLAYER_1);
		

		
		print (nodeObjs.Count);
	}
}
