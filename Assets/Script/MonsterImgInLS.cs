using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterImgInLS : MonoBehaviour
{
    public Text nameText;
    public Text statusText;
    public Text explanationText;

    public void Start()
    {
        int rand = Random.RandomRange(0, 15);
        this.transform.GetChild(rand).gameObject.SetActive(true);
        nameText.text = GameManager.instance.monsterManager.MonsterList[rand].name;
        statusText.text = "공격력 " + GameManager.instance.monsterManager.MonsterList[rand].atk + "\n" +
                        "체력 " + GameManager.instance.monsterManager.MonsterList[rand].hp + "\n" +
                        "속력 " + GameManager.instance.monsterManager.MonsterList[rand].speed + "\n" +
                        "경직 " + GameManager.instance.monsterManager.MonsterList[rand].rigidTime + "\n";
        explanationText.text = GameManager.instance.monsterManager.MonsterList[rand].explanation;
    }
}
