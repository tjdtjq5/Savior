using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantSwordsman : Character
{
    private void Start()
    {
        SetCharacter("2");
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Idle()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetCharacter(string characterName)
    {
        this.characterName = characterName;
        characterClass = CharacterInfo.instance.GetCharacterInfo(characterName).characterClass;
        initialAtk = CharacterInfo.instance.GetCharacterInfo(characterName).initialAtk;
        initialAtkspeed = CharacterInfo.instance.GetCharacterInfo(characterName).initialAtkspeed;
        initialCriticalPercent = CharacterInfo.instance.GetCharacterInfo(characterName).initialCriticalPercent;
        initialCriticalAtk = CharacterInfo.instance.GetCharacterInfo(characterName).initialCriticalAtk;
        additionalPercent = CharacterInfo.instance.GetCharacterInfo(characterName).additionalPercent;
        additionalAtk = CharacterInfo.instance.GetCharacterInfo(characterName).additionalAtk;
    }
}
