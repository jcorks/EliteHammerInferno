using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class troopBehavior : MonoBehaviour {

	public float speed;
	public float strength;
	public float morale = 100.0f;
	public Player troopOwner;
	public troopBehavior opponent;
	public bool fighting = false;
	public Node garrisoned; //tells if the unit is in a province
	public Hero attached; //tells if a hero unit is attached
	public Sprite minion; 

	public Vector3 angleVector = new Vector3(0f,0f,0f);

	float priorSpeed = 1f;
	float fightInterval = 0f;
	float heroBonusAttack = 1f;
	float heroBonusShock = 1f;
	float heroBonusOther = 1f;
	float heroBonusMorale = 0f;
	int fightTurn = 0;
	
	public Sprite devil_minion;
	public Sprite angel_minion;
	public Sprite hero_1;
	public Sprite hero_2;
	public GameObject statusPrefab;
	private GameObject statusObj;
	public GameObject statusBoxPrefab;
	// Use this for initialization
	void Awake () {
		//makeProperty (1000f, 1000f);
		statusObj = (GameObject)Instantiate (statusPrefab);
	}

	void heroAbility(Hero attached) {
		if (attached == Hero.Hero_1) {
			heroBonusOther = 0.1f;
		}
		if (attached == Hero.Hero_2) {
			heroBonusOther = 2f;
		}
		if (attached == Hero.Hero_3) {
		}
		if (attached == Hero.Hero_4) {
		}
		if (attached == Hero.None) {
		}
	}
	
	float combatResult1(float strengthModifier, float moraleModifier, float randMin, float randMax, float other) {
		return Mathf.Round (opponent.strength*strengthModifier*Random.Range(randMin, randMax)
		                    + opponent.morale*moraleModifier * Random.Range(randMin, randMax)+other);
	}

	float combatResult2(float strengthModifier, float moraleModifier, float randMin, float randMax, float other) {
		return Mathf.Round (strength*strengthModifier*Random.Range(randMin, randMax)
		                    + morale*moraleModifier * Random.Range(randMin, randMax)+other);
	}

	void Update() {
		statusObj.transform.position = transform.position + new Vector3(0.0f, .4f, -.4f);
		statusObj.GetComponent<TextMesh> ().text = "Units: " + strength.ToString ();
		statusObj.GetComponent<TroopStatus> ().setMorale (morale);
	}


	/*void heroCombat (Hero attached, int value){
		if (attached == Hero.Hero_1) {
			value = value*1.2;
		}
	}

	void heroPassive (Hero attached, int value) {
		if (attached == Hero.Hero_1) {
			value = va0.1;
		}
	}*/
	
		// Update is called once per frame
	void FixedUpdate() {
		fightInterval++;
		//Basic Movement
		/*Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;*/

		Vector3 speedVec = angleVector*speed;
		transform.position += speedVec;

		//If there is a fight, update the status every second
		if (fightInterval == 50) {
			if (fighting) {
				float attack1 = 0f;
				float shock1 = 0f;
				float attack2 = 0f;
				float shock2 = 0f;
				//neither troop is not garrisoned
				if (!garrisoned && !opponent.garrisoned && troopOwner == Player.PLAYER_1) { //
					attack1 = combatResult1(0.1f,0.05f,0.8f,1f,0f);
					shock1 = combatResult1(0.075f,0.025f,0.8f,1f,0f);
					attack2 = combatResult2(0.1f,0.05f,0.8f,1f,0f);
					shock2 =combatResult2(0.075f,0.025f,0.8f,1f,0f);
				}
				//this troop is not garrisoned and opponent is
				if (!garrisoned && opponent.garrisoned && troopOwner == Player.PLAYER_1) { 
					attack1 = combatResult1(0.1f,0.05f,0.8f,1f,0f);
					shock1 = combatResult1(0.075f,0.025f,0.8f,1f,0f);
					attack2 = combatResult2(0.1f,0.05f,0.6f,0.8f,0f);
					shock2 =combatResult2(0.075f,0.025f,0.4f,0.6f,0f);
				}
				//this troop is garrisoned and opponent is not
				if (garrisoned && !opponent.garrisoned && troopOwner == Player.PLAYER_1) {
					attack1 = combatResult1(0.1f,0.05f,0.6f,0.8f,0f);
					shock1 = combatResult1(0.075f,0.025f,0.4f,0.6f,0f);
					attack2 = combatResult2(0.1f,0.05f,0.8f,1f,0f);
					shock2 = combatResult2(0.075f,0.025f,0.8f,1f,0f);
				}
				//Hero combat bonus
				if (attached == Hero.Hero_1 && fightTurn == 0) {
					Debug.Log("first charge!");
					shock1 = Mathf.Round(shock1*1.2f); // angel hero1 gives bonus morale damage first engagement
				}
				if (attached == Hero.Hero_2 && fightTurn == 0) {
					Debug.Log("first charge!");
					shock1 = Mathf.Round(shock1*1.2f); // angel hero1 gives bonus morale damage first engagement
				}
				strength -= attack1;
				morale -= shock1;
				opponent.strength -= attack2;
				opponent.morale -= shock2;
				Debug.Log("attack:" + attack1 + " shock:" + shock1);
				Debug.Log("attack2:" + attack2 + " shock2:" + shock2);
				Debug.Log("morale:" + morale + " morale2:" + opponent.morale);
				Debug.Log("strength:" + strength + " strength2:" + opponent.strength);
				fightTurn++;
			}
			if (!fighting && garrisoned && morale < 100) { //morale charge in
				morale += 10 % 100;
			}
			if (attached == Hero.Hero_1) {
				morale = morale+morale*heroBonusOther; // angel hero give boost to morale every fight turn
			}
			fightInterval = 0;
		}

		if (fighting) {
			if (strength < 0) {
				Debug.Log ("destroy!!");
				Destroy (this.gameObject); //Unit destroyed
			}
			if (morale < 0) {
				if (!garrisoned) {
					speed = priorSpeed*-2; // withdraw if morale is low
					fighting = false;
					fightTurn = 0;
				}
				else {
					Debug.Log ("destroy!!");
					Destroy (this.gameObject); //Unit destroyed if besiefed
				}
			}
			if (opponent.morale < 0 || opponent.strength < 0) { //if opponent loses move on
				fighting = false;
				fightTurn = 0;
				speed = priorSpeed;
				morale = morale + 30 % 100;
			}
		}

	}	

	void OnDestroy() {
		Destroy (statusObj);
	}

	public void setOwner(Player p) {
		troopOwner = p;
		if (Hammer.PlayerData.players [(int)p].getHero () == Hero.Hero_1) {
			GetComponent<SpriteRenderer>().sprite = angel_minion;
		} else if (Hammer.PlayerData.players [(int)p].getHero () == Hero.Hero_2) {
			GetComponent<SpriteRenderer>().sprite = devil_minion;
		}
	}

	void OnTriggerEnter(Collider coll){
		Debug.Log (coll.gameObject);

		//Find out what hit this troop
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "node" && (collidedWith.GetComponent<Node>().playerOwner == troopOwner||collidedWith.GetComponent<Node>().playerOwner == Player.AI)){
			Debug.Log("node found");




			transform.position=collidedWith.transform.position;
			garrisoned = collidedWith.GetComponent<Node>();
			collidedWith.GetComponent<Node>().setOwner(troopOwner);
			transform.Rotate (0f, 0f ,0f);
			speed = 0;

		}
		else {
			troopBehavior clash = collidedWith.GetComponent<troopBehavior>();

			if (clash.troopOwner != troopOwner) {
				fighting = true;
				opponent = clash;
				Debug.Log ("clash!");
				priorSpeed = speed;
				speed = 0;
				if (garrisoned) {
				}
			}
			if (clash.troopOwner == this.troopOwner && clash.garrisoned) {
				Debug.Log ("merge!");
				this.morale = Mathf.Round((morale*strength + clash.morale*clash.strength)/(clash.strength+strength));
				this.strength += clash.strength;
				Debug.Log (strength);
				Debug.Log (morale);
				Debug.Log ("destroy!");
				Destroy (this.gameObject);
			}
		}
	}
}
