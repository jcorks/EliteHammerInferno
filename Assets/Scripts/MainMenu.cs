using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private Animator _animator;
    private CanvasGroup _canvasgroup;

    public void buttonsound()
    {
        audio.Play();
    }

    public void quit()
    {
        Application.Quit();
    }

}
