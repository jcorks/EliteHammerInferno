using UnityEngine;
using System.Collections;

public class TroopStatus : MonoBehaviour {

	public GameObject barPrefab;
	private GameObject barBase;


	// Use this for initialization
	void Start () {

		barBase = (GameObject)Instantiate (barPrefab);
		barBase.GetComponent<TroopStatusBar> ().setMorale(100.0f);

	}

	public void setMorale(float m) {

		barBase.GetComponent<TroopStatusBar>().setMorale (m);
	}
	// Update is called once per frame
	void Update () {
		barBase.transform.position = transform.position + new Vector3(0.0f, 0.0f, -0.4f);

	}

	void OnDestroy() {
		Destroy (barBase);
	}
}
