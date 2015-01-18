using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    public GameObject image00;
    public GameObject image01;
    public GameObject image10;
    public GameObject image11;

    public GameObject heaven1;
    public GameObject heaven2;
    public GameObject hell1;
    public GameObject hell2;

    public AudioSource click;

    public Animator anim;

    bool player1ready = false;
    bool player2ready = false;

    // total characters
    int totalCharacters = 2;

    public List<Character> characters1 = new List<Character>();
    public List<Character> characters2 = new List<Character>();

    int character1 = 0;
    int character2 = 0;
    
    public void buttonsound()
    {
        audio.Play();
    }

    void Start()
    {
        Hammer.PlayerData.init();

        for(int i = 0; i < totalCharacters; i++) {
            characters1.Add(new Character());
            characters2.Add(new Character());
        }

        characters1[0].setValues(image00, true);
        characters1[1].setValues(image01, false);

        characters2[0].setValues(image10, true);
        characters2[1].setValues(image11, false);


    }

    void changeCharacter1(bool change)
    {
        if (change) {
            character1++;
        }
        else {
            character1--;
        }
        if (character1 > totalCharacters - 1 ) {
            character1 = 0;
        }
        else if (character1 < 0) {
            character1 = totalCharacters - 1;
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
        if (character2 > totalCharacters - 1) {
            character2 = 0;
        }
        else if (character2 < 0) {
            character2 = totalCharacters - 1;
        }
    }

    void Background(bool heaven, int player)
    {
        if (player == 1) {
            if (heaven) {
                heaven1.SetActive(true);
                hell1.SetActive(false);
            }
            else {
                heaven1.SetActive(false);
                hell1.SetActive(true);
            }
        }
        else {
            if (heaven)
            {
                heaven2.SetActive(true);
                hell2.SetActive(false);
            }
            else
            {
                heaven2.SetActive(false);
                hell2.SetActive(true);
            }
        }
    }


    void Update()
    {
        if (Hammer.PlayerData.players[0].left() && !player1ready) {
            anim.SetBool("LeftArrow1", true);
            characters1[character1].Portrait().SetActive(false);
            changeCharacter1(false);
            click.Play();
            Background(characters1[character1].Side(), 1);
            characters1[character1].Portrait().SetActive(true);
            
        }

        if (Hammer.PlayerData.players[0].right() && !player1ready) {
            print("right");
            anim.SetBool("RightArrow1", true);
            characters1[character1].Portrait().SetActive(false);
            changeCharacter1(true);
            click.Play();
            Background(characters1[character1].Side(), 1);
            characters1[character1].Portrait().SetActive(true);
            
        }

        if (Hammer.PlayerData.players[1].left() && !player2ready) {
            anim.SetBool("LeftArrow2", true);
            characters2[character2].Portrait().SetActive(false);
            changeCharacter2(false);
            click.Play();
            Background(characters2[character2].Side(), 2);
            characters2[character2].Portrait().SetActive(true);

        }

        if (Hammer.PlayerData.players[1].right() && !player2ready) {
            anim.SetBool("RightArrow2", true);
            characters2[character2].Portrait().SetActive(false);
            changeCharacter2(true);
            click.Play();
            Background(characters2[character2].Side(), 2);
            characters2[character2].Portrait().SetActive(true);

        }

        if (Hammer.PlayerData.players[0].move() && !player1ready) {
            player1ready = true;

        }

        if (Hammer.PlayerData.players[1].move() && !player2ready)
        {
            player2ready = true;

        }

        if (Hammer.PlayerData.players[0].build() && player1ready)
        {
            player1ready = false;

        }
    }

    public void quit()
    {
        Application.Quit();
    }

}
