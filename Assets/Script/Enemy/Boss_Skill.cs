﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_Skill : MonoBehaviour
{
    public static Boss_Skill instance;
    [HideInInspector] public int attack;
    [HideInInspector] public bool phase_01;

    [Header("플레이어위치")]
    public Transform player;

    [Header("보스스킬")]
    public GameObject[] phase1;
    public GameObject[] phase2;
    public GameObject[] phase3;

    [Header("생성위치")]
    public Transform mouth;
    public Transform hand;
    public Transform tail;
    public Transform randshot;

    private void Awake()
    {
        instance = this;
    }

    public void Breath()
    {
        //브레스
        GameObject breath = Instantiate(phase1[0], mouth.position, Quaternion.identity);
        if (player.position.x - this.transform.position.x > 0)
            breath.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            breath.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void Flame(Vector3 pos)
    {
        //플레임
        GameObject flame = Instantiate(phase1[1], pos, Quaternion.identity);
    }

    public void Claw()
    {
        //할퀴기
        //부채꼴 범위 추가
        GameObject claw = Instantiate(phase1[2], hand.position, Quaternion.identity);
        if (player.position.x - this.transform.position.x > 0)
            claw.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            claw.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void RandomShot()
    {
        //화염탄 난사
        GameObject randomskill = Instantiate(phase1[3], randshot.position, Quaternion.identity);
        if (player.position.x - this.transform.position.x > 0)
            randomskill.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            randomskill.transform.rotation = Quaternion.Euler(0, 0, 0);
        randomskill.GetComponent<Boss_Skillinfo>().randomshot = true;
        randomskill.GetComponent<Boss_Skillinfo>().phase1 = phase_01;
    }

    public void RandomShot01(Transform pos)
    {
        //화염탄 난사1
        GameObject randomshot01 = Instantiate(phase1[4], pos.position, Quaternion.identity);
    }

    public void RandomShot02(Transform pos)
    {
        //화염탄 난사2
        GameObject randomshot02 = Instantiate(phase2[5], pos.position, Quaternion.identity);
    }

    public void BreathBall()
    {
        //브레스탄
        GameObject breathball01 = Instantiate(phase2[0], mouth.position, Quaternion.identity);
        GameObject breathball02 = Instantiate(phase2[0], new Vector3(mouth.position.x, mouth.position.y - 1), Quaternion.identity);
        GameObject breathball03 = Instantiate(phase2[0], new Vector3(mouth.position.x, mouth.position.y + 1), Quaternion.identity);
        breathball01.GetComponent<Boss_Skillinfo>().breathball = true;
        breathball02.GetComponent<Boss_Skillinfo>().breathball = true;
        breathball03.GetComponent<Boss_Skillinfo>().breathball = true;
        if (player.position.x - this.transform.position.x > 0)
        {
            breathball01.GetComponent<Boss_Skillinfo>().direction = true;
            breathball02.GetComponent<Boss_Skillinfo>().direction = true;
            breathball03.GetComponent<Boss_Skillinfo>().direction = true;
            breathball01.transform.rotation = Quaternion.Euler(0, 180, 0);
            breathball02.transform.rotation = Quaternion.Euler(0, 180, 0);
            breathball03.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            breathball01.GetComponent<Boss_Skillinfo>().direction = false;
            breathball02.GetComponent<Boss_Skillinfo>().direction = false;
            breathball03.GetComponent<Boss_Skillinfo>().direction = false;
            breathball01.transform.rotation = Quaternion.Euler(0, 0, 0);
            breathball02.transform.rotation = Quaternion.Euler(0, 0, 0);
            breathball03.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void FlameBomb(Vector3 pos)
    {
        //화염폭발
        GameObject flamebomb = Instantiate(phase2[1], pos, Quaternion.identity);
    }

    public void Spin()
    {
        //꼬리치기
        GameObject spin = Instantiate(phase2[2], tail.position, Quaternion.identity);
    }

    public void FlameStrong(Vector3 pos)
    {
        //플레임 강화
        GameObject flamestrong = Instantiate(phase3[0], pos, Quaternion.identity);
    }

    public void BurningGround()
    {
        Vector2 pos;
        if (player.position.x - this.transform.position.x > 0)
            pos = new Vector2(hand.position.x + 2, hand.position.y);
        else
            pos = new Vector2(hand.position.x - 2, hand.position.y);
        //불타는 대지
        GameObject burning01 = Instantiate(phase3[1], pos, Quaternion.identity);
        GameObject burning02 = Instantiate(phase3[1], new Vector3(pos.x, hand.position.y - 1), Quaternion.identity);
        GameObject burning03 = Instantiate(phase3[1], new Vector3(pos.x, hand.position.y + 1), Quaternion.identity);
    }

    public void BreathStrong()
    {
        //브레스 강화
        GameObject breathstrong = Instantiate(phase3[2], mouth.position, Quaternion.identity);
        breathstrong.GetComponent<Boss_Skillinfo>().breathstrong = true;
        breathstrong.GetComponent<Boss_Skillinfo>().target = player;
    }

    public void SpinStrong()
    {
        //꼬리치기 강화
        GameObject spin = Instantiate(phase3[5], tail.position, Quaternion.identity);
    }

}
