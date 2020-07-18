using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MasicCircleIconBtn : MonoBehaviour
{
    public PlayerController playerController;
    public Transform context;
    public Transform point;
    public Text masicLevel;
    public Text masicName;
    public Text explanation;

    float originPosX;
    float tempMinus;
    Sequence MySequence;


    public void Start()
    {
        originPosX = context.position.x;
        tempMinus = point.position.x - this.transform.position.x;
        MySequence = DOTween.Sequence();
    }

    
    public void OnClickIcon()
    {
        masicLevel.gameObject.SetActive(true);
        masicName.gameObject.SetActive(true);
        explanation.gameObject.SetActive(true);

        MySequence.Kill();
        MySequence.Append(context.DOMoveX(originPosX + tempMinus, .5f).SetEase(Ease.Linear));

        for (int i = 0; i < context.childCount; i++)
        {
            if (context.GetChild(i).GetComponent<Image>() != null)
            {
                context.GetChild(i).GetComponent<Image>().color = new Color(192 / 255f, 192 / 255f, 192 / 255f, 186 / 255f);
            }
        }
        this.GetComponent<Image>().color = Color.white;

        string name = this.gameObject.name;
        List<string> dataList = new List<string>();

        switch (name)
        {
            case "포인트 증가":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(9);
                masicLevel.text = "LV " + playerController.masic_point;
                masicName.text = "포인트획득증가";
                explanation.text = "획득 포인트를 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_point + "</color>% 증가";
                break;
            case "이동속도":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(3);
                masicLevel.text = "LV " + playerController.masic_speed;
                masicName.text = "이동속도증가";
                explanation.text = "이동속도를 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_speed + "</color>% 증가";
                break;
            case "체력구슬":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(6);
                masicLevel.text = "LV " + playerController.masic_recovery;
                masicName.text = "체력회복량증가";
                explanation.text = "체력 회복률을 <color=#00FF30>" + float.Parse(dataList[1]) * playerController.masic_recovery + "</color>% 증가";
                break;
            case "아이템 획득":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(4);
                masicLevel.text = "LV " + playerController.masic_itemdistance;
                masicName.text = "아이템획득범위증가";
                explanation.text = "아이템 획득 범위를 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_itemdistance + "</color>% 증가";
                break;
            case "최대체력":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(0);
                masicLevel.text = "LV " + playerController.masic_maxhp;
                masicName.text = "최대체력증가";
                explanation.text = "최대 체력을 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_maxhp + "</color>% 증가";
                break;
            case "공격속도증가":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(2);
                masicLevel.text = "LV " + playerController.masic_atkspeed;
                masicName.text = "공격속도증가";
                explanation.text = "공격속도를 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_atkspeed + "</color>% 증가";
                break;
            case "방어력증가":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(5);
                masicLevel.text = "LV " + playerController.masic_sheild;
                masicName.text = "방어력증가";
                explanation.text = "방여력을 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_sheild + "</color>% 증가";
                break;
            case "스킬 데미지 증가":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(7);
                masicLevel.text = "LV " + playerController.masic_skilldamage;
                masicName.text = "스킬대미지증가";
                explanation.text = "스킬 데미지를 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_skilldamage + "</color>% 증가";
                break;
            case "공격력증가":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(1);
                masicLevel.text = "LV " + playerController.masic_atk;
                masicName.text = "공격력증가";
                explanation.text = "공격력을 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_atk + "</color>% 증가";
                break;
            case "경험치획득량":
                dataList = GameManager.instance.database.masic_circle_DB.GetRowData(8);
                masicLevel.text = "LV " + playerController.masic_exp;
                masicName.text = "경험치획득증가";
                explanation.text = "경험치 획득률을 <color=#00FF30>" + int.Parse(dataList[1]) * playerController.masic_exp + "</color>% 증가";
                break;
        }
    }

}
