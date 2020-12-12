using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected string characterName;
    protected CharacterClass characterClass;
    protected int initialAtk;
    protected float initialAtkspeed;
    protected float initialCriticalPercent;
    protected float initialCriticalAtk;
    protected float additionalPercent;
    protected float additionalAtk;

    protected abstract void SetCharacter(string characterName);
    protected abstract void Idle();
    protected abstract void Move();
    protected abstract void Attack();

    public string GetCharacterName() { return characterName; }
    public CharacterClass GetCharacterClass() { return characterClass; }
}
