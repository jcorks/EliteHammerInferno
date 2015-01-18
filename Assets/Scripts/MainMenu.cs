using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    public GameObject image00;
    public GameObject image01;
    public GameObject image10;
    public GameObject image11;

    public AudioSource click;

    public Animation left1;
    public Animation right1;
    public Animation left2;
    public Animation right2;

    // total characters minus 1
    public int totalCharacters = 1;

    public List<GameObject> characters1 = new List<GameObject>();
    public List<GameObject> characters2 = new List<GameObject>();

    int character1 = 0;
    int character2 = 0;
    


    public void buttonsound()
    {
        audio.Play();
    }

    void Start()
    {
        Hammer.PlayerData.init();
        characters1.Add(image00);
        characters1.Add(image01);
        characters2.Add(image10);
        characters2.Add(image11);
    }

    void changeCharacter1(bool change)
    {
        if (change) {
            character1++;
        }
        else {
            character1--;
        }
        if (character1 > totalCharacters) {
            character1 = 0;
        }
        else if (character1 < 0) {
            character1 = totalCharacters;
        }
    }

    void changeCharacter2(bool change)
    {
        if (change) {
            character2++;
        }
        else {
            character2--;
        }
        if (character2 > totalCharacters) {
            character2 = 0;
        }
        else if (character2 < 0) {
            character2 = totalCharacters;
        }
    }

    void Update()
    {
        if (Hammer.PlayerData.players[0].left()) {
            print("left");
            //animation.Play("left1");
            characters1[character1].SetActive(false);
            changeCharacter1(false);
            click.Play();
            characters1[character1].SetActive(true);
            
        }

        if (Hammer.PlayerData.players[0].right()) {
            print("right");
            //animation.Play("right1");
            characters1[character1].SetActive(false);
            changeCharacter1(true);
            click.Play();
            characters1[character1].SetActive(true);
            
        }

        if (Hammer.PlayerData.players[1].left()) {
            //animation.Play("left2");
            characters2[character2].SetActive(false);
            changeCharacter2(false);
            click.Play();
            characters2[character2].SetActive(true);

        }

        if (Hammer.PlayerData.players[1].right()) {
            //animation.Play("right2");
            characters2[character2].SetActive(false);
            changeCharacter2(true);
            click.Play();
            characters2[character2].SetActive(true);

        }

        if (Hammer.PlayerData.players[0].move()) { 
            
        }

    }

    public void quit()
    {
        Application.Quit();
    }

}
