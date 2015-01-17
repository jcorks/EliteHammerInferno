using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class troopBehavior : MonoBehaviour {

	public float speed;

	public Player troopOwner;

	public float strength = 0f;

	public float morale = 0f;
	
	public troopBehavior opponent;
	
	public bool fighting = false;

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
		if (fightInterval == 50) {
			if (fighting == true) {
				Debug.Log("morale:" + morale);
				Debug.Log("strength:" + strength);
				strength = Mathf.Round (strength - opponent.strength*0.1f - morale*0.05f);
				morale = Mathf.Round (morale - opponent.strength*0.15f - morale*0.05f);
				if (strength < 0) {
					Destroy (this.gameObject); //Unit destroyed
				}
				else if (morale < 0) {
					speed = priorSpeed*-2; // withdraw if morale is low
					fighting = false;
				}
				if (opponent.morale < 0 || opponent.strength < 0) {
					fighting = false;
					speed = priorSpeed;
				}
			}

			fightInterval = 0;
		}
	}	

	void OnTriggerEnter(Collider coll){
		Debug.Log (coll.gameObject);
		//Find out what hit this basket
		GameObject collidedWith = coll.gameObject;
		troopBehavior clash = collidedWith.GetComponent<troopBehavior>();
		if (clash.troopOwner != troopOwner) {
			fighting = true;
			opponent = collidedWith.GetComponent<troopBehavior>();
			Debug.Log ("clash!");
			priorSpeed = speed;
			speed = 0;
		}
		if (clash.troopOwner == troopOwner && clash.fighting /*and heading towards same vector*/) {

		}
	}
}
