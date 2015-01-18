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
	public Hero attached;
	float priorSpeed = 1;
	float fightInterval = 0;

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

		/*Vector3 speedVec = new Vector3 (0.0f, 0.0f, speed);
		speedVec = Vector3.Angle (fromNode.transform.position, toNode.transform.position);
		tranform.position += speedVec;
*/

		//If there is a fight, update the status every second
		if (fightInterval == 50) {
			if (fighting == true && !garrisoned && troopOwner == Player.PLAYER_1) {
				float attack1 = Mathf.Round (opponent.strength*0.1f*Random.Range(0.8f, 1f) + opponent.morale*0.05f * Random.Range(0.8f, 1f));
				float shock1 = Mathf.Round (opponent.strength*0.075f*Random.Range(0.8f, 1f) + opponent.morale*0.025f*Random.Range(0.8f, 1f));
				float attack2 = Mathf.Round (strength*0.1f*Random.Range(0.8f, 1f) + morale*0.05f * Random.Range(0.8f, 1f));
				float shock2 = Mathf.Round (strength*0.075f*Random.Range(0.8f, 1f) + morale*0.025f*Random.Range(0.8f, 1f));
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
				float attack1 = Mathf.Round (opponent.strength*0.1f*Random.Range(0.8f, 1f) + opponent.morale*0.05f * Random.Range(0.8f, 1f));
				float shock1 = Mathf.Round (opponent.strength*0.075f*Random.Range(0.8f, 1f) + opponent.morale*0.025f*Random.Range(0.8f, 1f));
				float attack2 = Mathf.Round (strength*0.1f*Random.Range(0.6f, 0.8f) + morale*0.05f * Random.Range(0.6f, 0.8f));
				float shock2 = Mathf.Round (strength*0.075f*Random.Range(0.4f, 0.6f) + morale*0.025f*Random.Range(0.4f, 0.6f));
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
				float attack1 = Mathf.Round (opponent.strength*0.1f*Random.Range(0.6f, 0.8f) + opponent.morale*0.05f * Random.Range(0.6f, 0.8f));
				float shock1 = Mathf.Round (opponent.strength*0.075f*Random.Range(0.4f, 0.6f) + opponent.morale*0.025f*Random.Range(0.4f, 0.6f));
				float attack2 = Mathf.Round (strength*0.1f*Random.Range(0.8f, 1f) + morale*0.05f * Random.Range(0.8f, 1f));
				float shock2 = Mathf.Round (strength*0.075f*Random.Range(0.8f, 1f) + morale*0.025f*Random.Range(0.8f, 1f));
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
		if (collidedWith.tag == "node" && collidedWith.GetComponent<Node>().playerOwner == null) {
			Debug.Log("node found");
			transform.position=collidedWith.transform.position;
			garrisoned = collidedWith.GetComponent<Node>();
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
