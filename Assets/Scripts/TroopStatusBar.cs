using UnityEngine;
using System.Collections;

public class TroopStatusBar : MonoBehaviour {

	public float moraleRatio = 100.0f;


	// Use this for initialization
	void Update () {


			


	}

	public void setBase() {
		//GetComponent<MeshRenderer> ().renderer.material.color = new Color (0, 255, 0, 255);
	}
		
	public void setMorale(float m) {
		moraleRatio = m ;
		string moraleStr = "[";
		
		for (int i = 0; i < moraleRatio/10; ++i) {
			moraleStr += '-';
		}
		moraleStr += "]";

		GetComponent<TextMesh> ().text = moraleStr;
		if (moraleRatio < 30) {
			GetComponent<MeshRenderer> ().renderer.material.color = new Color (255, 0, 0, 255);
		} else {
			GetComponent<MeshRenderer> ().renderer.material.color = new Color (0, 255, 0, 255);
		}
	}
	
	// Update is called once per frame
	void Start () {
		GetComponent<MeshRenderer> ().renderer.material.color = new Color (0, 255, 0, 255);
	}
}
