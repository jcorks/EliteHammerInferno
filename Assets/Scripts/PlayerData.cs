using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Player {
	PLAYER_1,
	PLAYER_2,
	PLAYER_3,
	PLAYER_4,
	AI
};

namespace Hammer {
public class PlayerData : MonoBehaviour {

    public static List<PlayerData> players;

    public static void init()
    {
		print ("Inited");

        players = new List<PlayerData>();

        // initialize all 4 controllers
        players.Add(new PlayerData()
        {
            Resources = 0,
            TotalResources = 0,
            Horizontal = "Horizontal1",
            Vertical = "Vertical1",
            AltHorizontal = "AltHorizontal1",
            AltVertical = "AltVertical1",
            MoveArmy = "MoveArmy1",
            BuildArmy = "BuildArmy1",
            Power = "Power1"
        });
        players.Add(new PlayerData()
        {
            Resources = 0,
            TotalResources = 0,
            Horizontal = "Horizontal2",
            Vertical = "Vertical2",
            AltHorizontal = "AltHorizontal2",
            AltVertical = "AltVertical2",
            MoveArmy = "MoveArmy2",
            BuildArmy = "BuildArmy2",
            Power = "Power2"
        });
        players.Add(new PlayerData()
        {
            Resources = 0,
            TotalResources = 0,
            Horizontal = "Horizontal3",
            Vertical = "Vertical1",
            AltHorizontal = "AltHorizontal3",
            AltVertical = "AltVertical3",
            MoveArmy = "MoveArmy3",
            BuildArmy = "BuildArmy3",
            Power = "Power3"
        });
        players.Add(new PlayerData()
        {
            Resources = 0,
            TotalResources = 0,
            Horizontal = "Horizontal4",
            Vertical = "Vertical4",
            AltHorizontal = "AltHorizontal4",
            AltVertical = "AltVertical4",
            MoveArmy = "MoveArmy4",
            BuildArmy = "BuildArmy4",
            Power = "Power4"
        });
    }

    // Processing controls returns bool if pressed/active
    // Usage: if( PlayerData.players[i].left() )
    public bool left()
    {
        return (Input.GetAxis(Horizontal) < 0 || Input.GetAxis(AltHorizontal) < 0);
    }

    public bool right()
    {
        return (Input.GetAxis(Horizontal) > 0 || Input.GetAxis(AltHorizontal) > 0);
    }

    public bool up()
    {
        return (Input.GetAxis(Vertical) > 0 || Input.GetAxis(AltVertical) > 0);
    }

    public bool down()
    {
        return (Input.GetAxis(Vertical) < 0 || Input.GetAxis(AltVertical) < 0);
    }

    public bool move()
    {
        return (Input.GetButton(MoveArmy));
    }

    public bool build()
    {
        return (Input.GetButton(BuildArmy));
    }

    public bool ability()
    {
        return (Input.GetButton(Power));
    }

    public void addTotal(int add)
    {
        TotalResources += add;
    }

    public int total()
    {
        return TotalResources;
    }

    //Variables
    public int Resources;
    private int TotalResources;
    private string Horizontal;
    private string Vertical;
    private string AltHorizontal;
    private string AltVertical;
    private string MoveArmy;
    private string BuildArmy;
    private string Power;

/*
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
 */

}
}


