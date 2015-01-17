using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class troopBehavior : MonoBehaviour {

	public float speed;

	public float strength = 0f;

	public float morale = 0f;

	float priorSpeed = 1f;

	float fightInterval = 0;

	public troopBehavior opponent;

	bool fighting = false;

	void makeProperty(float mobilized, float mobilizedMorale) {
		Debug.Log ("mobilized");
		strength = mobilized;
		morale = mobilizedMorale;
	}

	// Use this for initialization
	void Awake () {
		makeProperty (100f, 100f);
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
				if (opponent.morale < 0) {
					fighting = false;
					speed = priorSpeed;
				}
			}

			fightInterval = 0;
		}
		//if (strength == 0)
			//speed = -2 * priorSpeed;
			//Destroy (this);
		//if (morale < 0) 
			//speed = -2*priorSpeed;
	}	

	void OnTriggerEnter(Collider coll){
		Debug.Log (coll.gameObject);
		//Find out what hit this basket
			GameObject collidedWith = coll.gameObject;
			if (collidedWith.tag == "troop") {
				fighting = true;
				opponent = collidedWith.GetComponent<troopBehavior>();
				Debug.Log ("clash!");
				priorSpeed = speed;
				speed = 0;
			}
	}
}
