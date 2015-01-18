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
<<<<<<< HEAD

        
=======
		
		
>>>>>>> 9918c3bc38ccd7fc541d8406988f67eead0c016b

        players = new List<PlayerData>();

        // initialize all 4 controllers
        players.Add(new PlayerData()
        {
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
            Resources = 0,
            Character = 0,
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
            Character = 0,
            TotalResources = 0,
            Horizontal = "Horizontal3",
            Vertical = "Vertical3",
            AltHorizontal = "AltHorizontal3",
            AltVertical = "AltVertical3",
            MoveArmy = "MoveArmy3",
            BuildArmy = "BuildArmy3",
            Power = "Power3"
        });
        players.Add(new PlayerData()
        {
            Resources = 0,
            Character = 0,
            TotalResources = 0,
            Horizontal = "Horizontal4",
            Vertical = "Vertical4",
            AltHorizontal = "AltHorizontal4",
            AltVertical = "AltVertical4",
            MoveArmy = "MoveArmy4",
            BuildArmy = "BuildArmy4",
            Power = "Power4"
        });

		players.Add (new PlayerData ());
    }



    // Processing controls returns bool if pressed/active
    // Usage: if( PlayerData.players[i].left() )
    public bool left()
    {
		//	return false;
		//return (Input.GetAxis(AltHorizontal) < 0 || Input.GetAxis (Horizontal) < 0);
		


			if (Input.GetAxis(AltHorizontal) < 0 || Input.GetAxis (Horizontal) < -.4) {
				if (leftPressedC > 0) {
					leftPressedC -= Time.deltaTime;
					return false;
				}
				leftPressedC = gracePeriodSeconds;
				return true;
			}
			leftPressedC = -1.0f;
			return false;



		
    }

    public bool right()
    {
		//	return false;
		//return (Input.GetAxis(AltHorizontal) > 0 || Input.GetAxis (Horizontal) > 0);
		
			if (Input.GetAxis(AltHorizontal) > 0 || Input.GetAxis(Horizontal) > .4) {
				if (rightPressedC > 0) {
					rightPressedC -= Time.deltaTime;
					return false;
				}
				rightPressedC = gracePeriodSeconds;
				return true;
		}
		rightPressedC = -1.0f;
		return false;

    }

    public bool up()
    {
	

		//return (Input.GetAxis(AltVertical) > 0 || Input.GetAxis (Vertical) > 0);
		
			if (Input.GetAxis(AltVertical) > 0 || Input.GetAxis(Vertical) > .4) {
				if (upPressedC > 0) {
					upPressedC -= Time.deltaTime;
					return false;
				}
				upPressedC = gracePeriodSeconds;
				return true;
		}
			upPressedC = -1.0f;
		return false;
	
    }

    public bool down()
    {
		//	return false;
		//return (Input.GetAxis(AltVertical) < 0 || Input.GetAxis (Vertical) < 0);
		
		if (Input.GetAxis(AltVertical) < 0 || Input.GetAxis(Vertical) < -.4) {
				if (downPressedC > 0) {
					downPressedC -= Time.deltaTime;
					return false;
				}
				downPressedC = gracePeriodSeconds;
				return true;
		}
		downPressedC = -1.0f;
		return false;
		
    }

    public bool move()
    {
		if (Input.GetButton(MoveArmy)) {
			if (movePressedC > 0) {
				movePressedC -= Time.deltaTime;
				return false;
			}
			movePressedC = gracePeriodSeconds;
			return true;
		}
		movePressedC = -1.0f;
		return false;
    }

    public bool build()
    {
		if (Input.GetButton(BuildArmy)) {
			if (buildPressedC > 0) {
				buildPressedC -= Time.deltaTime;
				return false;
			}
			buildPressedC = gracePeriodSeconds;
			return true;
		}
		buildPressedC = -1.0f;
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
	
	public Hero hero = Hero.Hero_1;

    private int Character;
 
	private float upPressedC = 0;
	private float downPressedC = 0;
	private float leftPressedC = 0;
	private float rightPressedC = 0;
	private float movePressedC = 0;
	private float buildPressedC = 0;
	private float abilityPressedC = 0;

	private float gracePeriodSeconds = .17f;


    }
}


