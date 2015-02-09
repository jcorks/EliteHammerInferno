using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Player {
	PLAYER_1,	
	PLAYER_2,
	AI
};

namespace Hammer {
public class PlayerData {


    public static List<PlayerData> players;

    public static void init()
    {
		//print ("Inited");
		

        players = new List<PlayerData>();

        // initialize all 4 controllers
        players.Add(new PlayerData()
        {
			playerNum = 1,
            Resources = 0,
            Character = 0,
            TotalResources = 0,
            Horizontal = "Horizontal1",
            Vertical = "Vertical1",
            AltHorizontal = "AltHorizontal1",
            AltVertical = "AltVertical1",
            MoveArmy = "MoveArmy1",
            BuildArmy = "BuildArmy1",
            Power = "Power1",			
        });
        players.Add(new PlayerData()
        {
			playerNum =2,
            Resources = 0,
            Character = 0,
            TotalResources = 0,
            Horizontal = "Horizontal2",
            Vertical = "Vertical2",
            AltHorizontal = "AltHorizontal2",
            AltVertical = "AltVertical2",
            MoveArmy = "MoveArmy2",
            BuildArmy = "BuildArmy2",
            Power = "Power2",
        });

		players.Add (new PlayerData ());
    }



    // Processing controls returns bool if pressed/active
    // Usage: if( PlayerData.players[i].left() )
    public bool left()
    {

			if (playerNum == 1) 
			if (Input.GetKeyDown (KeyCode.A))
				return true;


			if (playerNum == 2) 
				if (Input.GetKeyDown (KeyCode.LeftArrow))
					return true;

			return false;
		
    }

    public bool right()
    {
			if (playerNum == 1) 
				if (Input.GetKeyDown (KeyCode.D))
					return true;
			
			
			if (playerNum == 2) 
				if (Input.GetKeyDown (KeyCode.RightArrow))
					return true;
			
			return false;;

    }

    public bool up()
    {
			if (playerNum == 1) 
				if (Input.GetKeyDown (KeyCode.W))
					return true;
			
			
			if (playerNum == 2) 
				if (Input.GetKeyDown (KeyCode.UpArrow))
					return true;
			
			return false;
	
    }

    public bool down()
    {
			if (playerNum == 1) 
				if (Input.GetKeyDown (KeyCode.S))
					return true;
			
			
			if (playerNum == 2) 
				if (Input.GetKeyDown (KeyCode.DownArrow))
					return true;
			
			return false;
		
    }

    public bool move()
    {
			if (playerNum == 1) 
				if (Input.GetKeyDown (KeyCode.F))
					return true;
			
			
			if (playerNum == 2) 
				if (Input.GetKeyDown (KeyCode.N))
					return true;
			
			return false;
    }

    public bool build()
    {
			if (playerNum == 1) 
				if (Input.GetKeyDown (KeyCode.G))
					return true;
			
		
			if (playerNum == 2) 
				if (Input.GetKeyDown (KeyCode.M))
					return true;

			return false;
 
    }

    public bool ability()
    {
		if (Input.GetButton(Power)) {
			if (abilityPressedC > 0) {
				abilityPressedC -= Time.deltaTime;
				return false;
			}
			abilityPressedC = gracePeriodSeconds;
			return true;
		}
		abilityPressedC = -1.0f;
		return false;
    }

    public void addTotal(int add)
    {
        TotalResources += add;
    }

    public int total()
    {
        return TotalResources;
    }

    public void setCharacter(int number)
    {
        Character = number;
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
	
	private Hero hero = Hero.None;
		public Hero getHero() {
			return hero;
		}
		public void setHero(Hero h) {
			hero = h;
		}
    private int Character;
 
	private float upPressedC = 0;
	private float downPressedC = 0;
	private float leftPressedC = 0;
	private float rightPressedC = 0;
	private float movePressedC = 0;
	private float buildPressedC = 0;
	private float abilityPressedC = 0;

	private float gracePeriodSeconds = .17f;

	int playerNum;



    }
}


