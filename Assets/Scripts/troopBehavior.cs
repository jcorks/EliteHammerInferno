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
	
	float baseAttack = 5f;

	float priorSpeed = 1f;

	float fightInterval = 0;

	void makeProperty(float mobilized, float mobilizedMorale) {
		Debug.Log ("mobilized");
		strength = mobilized;
		morale = mobilizedMorale;
	}

	// Use this for initialization
	void Awake () {
		//makeProperty (1000f, 1000f);
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		fightInterval++;
		//Basic Movement
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;

		//If there is a fight, update the status every second
		if (fightInterval == 50) {
			if (fighting == true && troopOwner == Player.PLAYER_1) {
				float attack1 = Mathf.Round (opponent.strength*0.1f*Random.Range(0.8f, 1f) + opponent.morale*0.05f * Random.Range(0.8f, 1f));
				float shock1 = Mathf.Round (opponent.strength*0.075f*Random.Range(0.8f, 1f) + opponent.morale*0.025f*Random.Range(0.8f, 1f));
				float attack2 = Mathf.Round (strength*0.1f*Random.Range(0.8f, 1f) + morale*0.05f * Random.Range(0.8f, 1f));
				float shock2 = Mathf.Round (strength*0.075f*Random.Range(0.8f, 1f) + morale*0.025f*Random.Range(0.8f, 1f));
				strength -= attack1;
				morale -= shock1;
				opponent.strength -= attack2;
				opponent.morale -= shock2;
				Debug.Log("attack:" + attack1 + " shock:" + shock1);
				Debug.Log("morale:" + morale + " morale2:" + opponent.morale);
				Debug.Log("strength:" + strength + " strength2:" + opponent.strength); 
			}
			if (!fighting && garrisoned) { //morale charge
				morale += 10;
			}
			fightInterval = 0;
		}

		if (fighting) {
			if (strength < 0) {
				Destroy (this.gameObject); //Unit destroyed
			}
			if (morale < 0) {
				speed = priorSpeed*-2; // withdraw if morale is low
				fighting = false;
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
		troopBehavior clash = collidedWith.GetComponent<troopBehavior>();
		if (clash.troopOwner != troopOwner) {
			fighting = true;
			opponent = clash;
			Debug.Log ("clash!");
			priorSpeed = speed;
			speed = 0;
		}
		if (clash.troopOwner == troopOwner && clash.fighting /*and heading towards same vector*/) {
			Debug.Log ("merge!");
			clash.morale = Mathf.Round((morale*strength + clash.morale*clash.strength)/(clash.strength+strength));
			clash.strength += strength;
			Debug.Log (clash.strength);
			Debug.Log (clash.morale);
			Destroy (this.gameObject);
		}
	}
}
