using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Damage : MonoBehaviour
{

    [Header("랜덤 horizontal 범위")] public float random_horizontal;
    [Header("높이")] public float height;

    [Header("빨강")] public Sprite[] red_num;
    [Header("파랑")] public Sprite[] blue_num;
    [Header("흰색")] public Sprite[] white_num;

    public void DamageSet(int damage)
    {
        Vector2 temp_pos = this.transform.position;
        char[] damageChar = damage.ToString().ToCharArray();
        for (int i = 0; i < damageChar.Length; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
            this.transform.GetChild(i).transform.DOScale(new Vector2(1.5f, 1.5f), 0.3f);
            if (damageChar.Length < 3) // 흰색
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = white_num[int.Parse(damageChar[i].ToString())];
            }
            else if(damageChar.Length < 5) // 파란색
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = blue_num[int.Parse(damageChar[i].ToString())];
            }
            else // 빨강색
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = red_num[int.Parse(damageChar[i].ToString())];
            }
        }
        float randomX = Random.RandomRange(-random_horizontal, random_horizontal);
        this.transform.DOMove(new Vector2(temp_pos.x + randomX, temp_pos.y + height), 0.3f).OnComplete(()=>
        {
            ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.nomal_damage);
        });
       
    }

    private void OnDisable()
    {
        this.transform.GetChild(0).transform.localScale = new Vector2(1, 1);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

   
}
