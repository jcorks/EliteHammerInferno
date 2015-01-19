using UnityEngine;
using System.Collections;

public class TutorialWait : MonoBehaviour {

    bool ready = false;
    float timer = 5;
    bool player1ready = false;
    bool player2ready = false;

    public GameObject player1up;
    public GameObject player2up;
    public GameObject player1set;
    public GameObject player2set;


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

        if (Hammer.PlayerData.players[0].move() && !player1ready)
        {
            player1ready = true;
            player1up.SetActive(false);
            player1set.SetActive(true);

        }

        if (Hammer.PlayerData.players[1].move() && !player2ready)
        {
            player2ready = true;
            player2up.SetActive(false);
            player2set.SetActive(true);

        }

        if (Hammer.PlayerData.players[0].build() && player1ready)
        {
            player1ready = false;
            player1up.SetActive(true);
            player1set.SetActive(false);

        }

        if (Hammer.PlayerData.players[1].build() && player2ready)
        {
            player2ready = false;
            player2up.SetActive(true);
            player2set.SetActive(false);

        }

        // Set playerdata variables for character
        if (player1ready && player2ready)
        {
            Application.LoadLevel("MainGame");
        }

    }
}
