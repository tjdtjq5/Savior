using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    [HideInInspector] public int attack;

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

    bool randomshot;

    private void Start()
    {
        randomshot = false;
    }

    private void FixedUpdate()
    {
        if (randomshot)
        {

        }
    }

    public void Breath()
    {
        //브레스
        //보스 애니메이션에 머리를 앞으로 뻗을 때 브레스 애니메이션 동작하도록, 보스 스프라이트 입에 달아놓을것
        attack = 20;
        GameObject breath = Instantiate(phase1[0], mouth.position, Quaternion.identity);
    }
    
    public void Flame()
    {
        //플레임
        attack = 10;
        GameObject flame = Instantiate(phase1[1], player.position, Quaternion.identity);
    }

    public void Claw()
    {
        //할퀴기
        attack = 15;
        GameObject breath = Instantiate(phase1[0], hand.position, Quaternion.identity);
    }

    public void RandomShot()
    {
        randomshot = true;
        //화염탄 난사
        attack = 10;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float randx = Random.Range(min.x, max.x);
        float randy = Random.Range(min.y, max.y);


        GameObject breath = Instantiate(phase1[0], mouth.position, Quaternion.identity);
    }

    public void BreathBall()
    {
        //브레스탄
        attack = 30;

    }
    public void FlameBomb()
    {
        //화염폭발
        attack = 40;
    }

    public void Spin()
    {
        //꼬리치기
        attack = 20;
    }

    public void FlameStrong()
    {
        //플레임 강화
        attack = 20;
    }

    public void BurningGround()
    {
        //불타는 대지
        attack = 15;
    }

    public void BreathStrong()
    {
        //브레스 강화
        attack = 30;
    }

    public void SpinStrong()
    {
        //꼬리치기 강화
        attack = 25;
    }

}
