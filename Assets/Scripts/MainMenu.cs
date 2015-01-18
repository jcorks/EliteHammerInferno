using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void buttonsound()
    {
        audio.Play();
    }

    public void quit()
    {
        Application.Quit();
    }

}
