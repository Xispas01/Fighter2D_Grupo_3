using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterselectionmenu2 : MonoBehaviour
{
    public static List<Sprite> spritePersonajes;
    public int selectedCharacter = 0;
    public int selectedCharacter2 = 0;
    public SpriteRenderer display1;
    public SpriteRenderer display2;
    public Sprite[] sprites; 

    void Start()
    { 
        display1 = GameObject.Find("displaycharacter1").GetComponent<SpriteRenderer>();
        display2 = GameObject.Find("displaycharacter2").GetComponent<SpriteRenderer>();
    }

    public void Fight()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextCharacter()
    {
        selectedCharacter++;
        if (selectedCharacter >= sprites.Length)
        {
            selectedCharacter = 0;
        }
        display1.sprite = sprites[selectedCharacter];
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = sprites.Length-1;
        }
        display1.sprite = sprites[selectedCharacter];
    }

    public void NextCharacter2()
    {
        selectedCharacter2++;
        if (selectedCharacter2 >= sprites.Length)
        {
            selectedCharacter2 = 0;
        }
        display2.sprite = sprites[selectedCharacter2];
    }

    public void PreviousCharacter2()
    {
        selectedCharacter2--;
        if (selectedCharacter2 < 0)
        {
            selectedCharacter2 = sprites.Length-1;
        }
        display2.sprite = sprites[selectedCharacter2];
    }
}
