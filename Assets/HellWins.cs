using UnityEngine;
using System.Collections;

public class HellWins : MonoBehaviour {

    bool ready = false;
    float timer = 5;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            ready = true;
        }

        if (ready && Input.GetButton("Accept"))
        {
            Application.LoadLevel("Main Menu");
        }
    }
}
