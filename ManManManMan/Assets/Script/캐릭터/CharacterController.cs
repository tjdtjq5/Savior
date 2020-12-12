using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public Character[] characters = new Character[4];

    public Image[] cardImg = new Image[4];
    public Sprite nullCardSprite;

    public void SetUI()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                cardImg[i].sprite = nullCardSprite;
            }
            else
            {

            }
        }
    }

    public void SetCharacter(Character character)
    {
        CharacterClass characterClass = character.GetCharacterClass();
        characters[(int)characterClass] = character;
    }
}


