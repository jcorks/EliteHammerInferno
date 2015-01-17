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

	static public bool[] confirmInput = new bool[5];
	static public bool[] backInput = new bool[5];
	static public bool[] upInput = new bool[5];
	static public bool[] downInput = new bool[5];
	static public bool[] leftInput = new bool[5];
	static public bool[] rightInput = new bool[5];




	static public bool inputConfirm(Player p) {
		// replace with proper player's input
		return (Input.GetKey (KeyCode.Z));
	}

	
	static public bool inputBack(Player p) {
		// replace with proper player's input
		return (Input.GetKeyDown (KeyCode.X));
	}

	
	static public bool inputUp(Player p) {
		// replace with proper player's input
		if (p == Player.PLAYER_1)
			return (Input.GetKeyDown (KeyCode.UpArrow));

		return false;
	}

	
	static public bool inputDown(Player p) {
		// replace with proper player's input
		if (p == Player.PLAYER_1)
			return (Input.GetKeyDown(KeyCode.DownArrow));

		return false;
	}
	
	static public bool inputLeft(Player p) {
		// replace with proper player's input
		if (p == Player.PLAYER_1)
			return (Input.GetKeyDown (KeyCode.LeftArrow));
		return false;
	}

	
	static public bool inputRight(Player p) {
		// replace with proper player's input
		if (p == Player.PLAYER_1)
			return (Input.GetKeyDown (KeyCode.RightArrow));

		return false;
	}






	// Use this for initialization
	void Start () {
		for(int i = 0; i < 5; ++i) 
			Resources[i] = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


