using UnityEngine;
using System.Collections;

public enum Player {
	PLAYER_1,
	PLAYER_2,
	PLAYER_3,
	PLAYER_4,
	AI
};



public class PlayerData : MonoBehaviour {

	static public int[] Resources = new int[5];
	

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 5; ++i) 
			Resources[i] = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
