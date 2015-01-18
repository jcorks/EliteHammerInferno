using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class troopBehavior : MonoBehaviour {

	public float speed;
	public float strength;
	public float morale;
	public Player troopOwner;
	public troopBehavior opponent;
	public bool fighting = false;
	public Node garrisoned; //tells if the unit is in a province
	public Hero attached; //tells if a hero unit is attached
	public Sprite minion; 
	float priorSpeed = 1f;
	float fightInterval = 0f;
	float heroBonusAttack = 1f;
	float heroBonusShock = 1f;
	float heroBonusOther = 1f;
	// Use this for initialization
	void Awake () {
		//makeProperty (1000f, 1000f);
	}

	void heroAbility(Hero attached) {
		if (attached == Hero.Hero_1) {
			heroBonusOther = 0.1f;
		}
		if (attached == Hero.Hero_2) {

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
	
	// Update is called once per frame
	void FixedUpdate() {
		fightInterval++;
		//Basic Movement
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;

		/*Vector3 speedVec = new Vector3 (0.0f, 0.0f, speed);
		speedVec = Vector3.Angle (fromNode.transform.position, toNode.transform.position);
		tranform.position += speedVec;*/

		//If there is a fight, update the status every second
		if (fightInterval == 50) {
			if (fighting == true && !garrisoned && troopOwner == Player.PLAYER_1) {
				float attack1 = combatResult1(0.1f,0.05f,0.8f,1f,0f);
				float shock1 = combatResult1(0.075f,0.025f,0.8f,1f,0f);
				float attack2 = combatResult2(0.1f,0.05f,0.8f,1f,0f);
				float shock2 =combatResult2(0.075f,0.025f,0.8f,1f,0f);
				strength -= attack1;
				morale -= shock1;
				opponent.strength -= attack2;
				opponent.morale -= shock2;
				Debug.Log("attack:" + attack1 + " shock:" + shock1);
				Debug.Log("attack2:" + attack2 + " shock2:" + shock2);
				Debug.Log("morale:" + morale + " morale2:" + opponent.morale);
				Debug.Log("strength:" + strength + " strength2:" + opponent.strength); 
			}
			if (fighting && garrisoned && troopOwner != Player.PLAYER_1) {
				float attack1 = combatResult1(0.1f,0.05f,0.8f,1f,0f);
				float shock1 = combatResult1(0.075f,0.025f,0.8f,1f,0f);
				float attack2 = combatResult2(0.1f,0.05f,0.6f,0.8f,0f);
				float shock2 =combatResult2(0.075f,0.025f,0.4f,0.6f,0f);
				strength -= attack1;
				morale -= shock1;
				opponent.strength -= attack2;
				opponent.morale -= shock2;
				Debug.Log("attack:" + attack1 + " shock:" + shock1);
				Debug.Log("attack2:" + attack2 + " shock2:" + shock2);
				Debug.Log("morale:" + morale + " morale2:" + opponent.morale);
				Debug.Log("strength:" + strength + " strength2:" + opponent.strength);
			}
			if (fighting && garrisoned && troopOwner == Player.PLAYER_1) {
				float attack1 = combatResult1(0.1f,0.05f,0.6f,0.8f,0f);
				float shock1 = combatResult1(0.075f,0.025f,0.4f,0.6f,0f);
				float attack2 = combatResult2(0.1f,0.05f,0.8f,1f,0f);
				float shock2 =combatResult2(0.075f,0.025f,0.8f,1f,0f);
				strength -= attack1;
				morale -= shock1;
				opponent.strength -= attack2;
				opponent.morale -= shock2;
				Debug.Log("attack:" + attack1 + " shock:" + shock1);
				Debug.Log("attack2:" + attack2 + " shock2:" + shock2);
				Debug.Log("morale:" + morale + " morale2:" + opponent.morale);
				Debug.Log("strength:" + strength + " strength2:" + opponent.strength);
			}
			if (!fighting && garrisoned && morale < 100) { //morale charge
				morale += 10 % 100;
			}
			if (attached == Hero.Hero_1) {
				morale = morale+morale*heroBonusOther; // angel hero give 
			}
			fightInterval = 0;
		}

		if (fighting) {
			if (strength < 0) {
				Destroy (this.gameObject); //Unit destroyed
			}
			if (morale < 0) {
				if (!garrisoned) {
					speed = priorSpeed*-2; // withdraw if morale is low
					fighting = false;
				}
				else {
					Destroy (this.gameObject); //Unit destroyed
				}
			}
			if (opponent.morale < 0 || opponent.strength < 0) {
				fighting = false;
				speed = priorSpeed;
				morale = morale + 30 % 100;
			}
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
			Debug.Log("node found");

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
			if (clash.troopOwner == troopOwner && clash.speed == 0) {
				Debug.Log ("merge!");
				clash.morale = Mathf.Round((morale*strength + clash.morale*clash.strength)/(clash.strength+strength));
				clash.strength += strength;
				Debug.Log (clash.strength);
				Debug.Log (clash.morale);
				Destroy (this.gameObject);
			}
		}
	}
}
