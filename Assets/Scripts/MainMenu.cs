using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    // character sprites
    public GameObject image00;
    public GameObject image01;
    public GameObject image10;
    public GameObject image11;

    // backgrounds
    public GameObject heaven1;
    public GameObject heaven2;
    public GameObject hell1;
    public GameObject hell2;

    // ready up images
    public GameObject player1up;
    public GameObject player2up;
    public GameObject player1set;
    public GameObject player2set;

    public AudioSource click;
    public AudioSource title;

    public Animator anim;

    bool player1ready = false;
    bool player2ready = false;

    public AudioSource choral;
    public AudioSource roar;

    // total characters
    int totalCharacters = 2;

    public List<Character> characters1 = new List<Character>();
    public List<Character> characters2 = new List<Character>();

    int character1 = 0;
    int character1prev;
    int character2 = 0;
    int character2prev;
    
    public void buttonsound()
    {
        audio.Play();
    }

    void Start()
    {
        character1prev = totalCharacters - 1;
        character2prev = totalCharacters - 1;
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
            character1prev = character1;
            character1++;
        }
        else if(!change) {
            character1prev = character1;
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
        print("START " + character2);
        print("PREV " + character2prev);
        print(characters2[0].Side());
        print(characters2[1].Side());

        if (change) {
            character2prev = character2;
            character2++;
        }
        else if (!change) {
            character2prev = character2;
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
            if (heaven) {
                heaven2.SetActive(true);
                hell2.SetActive(false);
            }
            else {
                heaven2.SetActive(false);
                hell2.SetActive(true);
            }
        }
    }

    void lockin(bool side) {
        if (side) {
            choral.Play();
        }
        if (!side) {
            roar.Play();
        }
    }

    void Update()
    {
        if (Hammer.PlayerData.players[0].left() && !player1ready) {
            anim.SetBool("LeftArrow1", true);
            changeCharacter1(false);
            characters1[character1prev].Portrait().SetActive(false);
            click.Play();
            Background(characters1[character1].Side(), 1);
            characters1[character1].Portrait().SetActive(true);
            
            
        }

        if (Hammer.PlayerData.players[0].right() && !player1ready) {
            anim.SetBool("RightArrow1", true);
            changeCharacter1(true);
            characters1[character1prev].Portrait().SetActive(false);
            click.Play();
            Background(characters1[character1].Side(), 1);
            characters1[character1].Portrait().SetActive(true);
            
        }

        if (Hammer.PlayerData.players[1].left() && !player2ready) {
            anim.SetBool("LeftArrow2", true);
            changeCharacter2(true);
            characters2[character2prev].Portrait().SetActive(false);
            click.Play();
            Background(characters2[character2].Side(), 2);
            characters2[character2].Portrait().SetActive(true);

        }

        if (Hammer.PlayerData.players[1].right() && !player2ready) {
            anim.SetBool("RightArrow2", true);
            changeCharacter2(true);
            characters2[character2prev].Portrait().SetActive(false);
            click.Play();
            Background(characters2[character2].Side(), 2);
            characters2[character2].Portrait().SetActive(true);

        }

        if (Hammer.PlayerData.players[0].move() && !player1ready && anim.GetBool("character")) {


            player1ready = true;
            player2ready = true;

            if (characters1[character1].Side()) {
                if (characters2[character2].Side()) {
                    changeCharacter2(true);
                    characters2[character2prev].Portrait().SetActive(false);
                    Background(characters2[character2].Side(), 2);
                    characters2[character2].Portrait().SetActive(true);
                }
            }

            if (!characters1[character1].Side())
            {
                if (!characters2[character2].Side())
                {
                    changeCharacter2(true);
                    characters2[character2prev].Portrait().SetActive(false);
                    Background(characters2[character2].Side(), 2);
                    characters2[character2].Portrait().SetActive(true);
                }
            }

            player1up.SetActive(false);
            player1set.SetActive(true);

            player2up.SetActive(false);
            player2set.SetActive(true);

            lockin(characters1[character1].Side());
        }

        if (Hammer.PlayerData.players[1].move() && !player2ready && anim.GetBool("character"))
        {
            player2ready = true;
            player1ready = true;

            if (characters2[character2].Side())
            {
                if (characters1[character1].Side())
                {
                    changeCharacter1(true);
                    characters1[character1prev].Portrait().SetActive(false);
                    Background(characters1[character1].Side(), 2);
                    characters2[character1].Portrait().SetActive(true);
                }
            }

            if (!characters2[character2].Side())
            {
                if (!characters1[character1].Side())
                {
                    changeCharacter1(true);
                    characters1[character1prev].Portrait().SetActive(false);
                    Background(characters1[character1].Side(), 2);
                    characters2[character1].Portrait().SetActive(true);
                }
            }

            player1up.SetActive(false);
            player1set.SetActive(true);

            player2up.SetActive(false);
            player2set.SetActive(true);

            lockin(characters2[character2].Side());
        }

        if (Hammer.PlayerData.players[0].build() && player1ready)
        {
            player1ready = false;
            player2ready = false;
            player1up.SetActive(true);
            player1set.SetActive(false);
            player2up.SetActive(true);
            player2set.SetActive(false);

        }

        if (Hammer.PlayerData.players[1].build() && player2ready)
        {
            player2ready = false;
            player1ready = false;
            player2up.SetActive(true);
            player2set.SetActive(false);
            player1up.SetActive(true);
            player1set.SetActive(false);

        }

        // Set playerdata variables for character
        if (player1ready && player2ready) {
            if (characters1[character1].Side() == true) {
                Hammer.PlayerData.players[0].hero = Hero.Hero_1;
            }
            if (characters1[character1].Side() == false) {
                Hammer.PlayerData.players[0].hero = Hero.Hero_2;
            }
            if (characters2[character2].Side() == true) {
                Hammer.PlayerData.players[1].hero = Hero.Hero_1;
            }
            if (characters2[character2].Side() == false) {
                Hammer.PlayerData.players[1].hero = Hero.Hero_2;
            }

            StartCoroutine("wait");
        }
    }

    IEnumerator wait() {
        print(Hammer.PlayerData.players[0].hero);
        print(Hammer.PlayerData.players[1].hero);
        yield return new WaitForSeconds(3.5f);
        Application.LoadLevel("Tutorial");
    }

    public void quit()
    {
        Application.Quit();
    }

}
