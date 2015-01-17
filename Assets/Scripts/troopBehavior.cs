using UnityEngine;
using System.Collections;

public class troopBehavior : MonoBehaviour {

	public float speed  = 1f;

	public float strength = 100;

	public float morale = 200;

	float priorSpeed = 1f;

	bool fighting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		//Basic Movement
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
		//if (strength == 0)
			//speed = -2 * priorSpeed;
			//Destroy (this);
		//if (morale < 0) 
			//speed = -2*priorSpeed;
	}	

	void OnCollisionEnter(Collision coll){
			//Find out what hit this basket
			GameObject collidedWith = coll.gameObject;
			if (collidedWith.tag == "troop") {
				fighting = true;
				Debug.Log ("clash!");
				priorSpeed = speed;
				speed = 0;
			}
	}

	void OnCollisionStay(Collision coll) {
		if (fighting && strength != 0) {
			strength--;
			Debug.Log (strength);
			//morale-=3;
		}
	}
}
