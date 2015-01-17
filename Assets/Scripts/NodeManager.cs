using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {
	public int nodeSpread;
	private static List<GameObject> nodesObjs; 


	public GameObject NodeObject;

	/*
	void Awake() {
		nodesObjs = new List<Node>();
		for (int i = 0; i < 100; ++i) {
			GameObject = (GameObject)Instantiate(NodeObject);
			Node curNode = n.GetComponent<Node>();
			curNode.transform.position = new Vector3(Random.value*nodeSpread - nodeSpread/2.0f,
			                                         0.0f, 
			                                         Random.value*nodeSpread - nodeSpread/2.0f);
			                           	
			nodes.Add(curNode);
			int val = (int) Mathf.Floor( Random.value * nodes.Count);
			print (val);
			curNode.connectNode (nodes[val]); 
		}
	}
	*/





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
