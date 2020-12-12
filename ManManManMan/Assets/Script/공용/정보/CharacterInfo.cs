using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo instance;
    public CharacterInfoStruct[] characterList = new CharacterInfoStruct[4];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < characterList.Length; i++)
        {
            characterList[i].characterName = i.ToString();
            characterList[i].characterClass = (CharacterClass)i;
            characterList[i].sprite = Resources.Load<Sprite>("캐릭터/" + characterList[i].characterName) as Sprite;
        }
            
    }

    public CharacterInfoStruct GetCharacterInfo(string characterName)
    {
        for (int i = 0; i < characterList.Length; i++)
        {
            if (characterList[i].characterName == characterName)
            {
                return characterList[i];
            }
        }
        return new CharacterInfoStruct();
    }
}
[System.Serializable]
public struct CharacterInfoStruct
{
    public string characterName;
    public CharacterClass characterClass;
    public int initialAtk;
    public float initialAtkspeed;
    public float initialCriticalPercent;
    public float initialCriticalAtk;
    public float additionalPercent;
    public float additionalAtk;

    public Sprite sprite;
}