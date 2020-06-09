using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("플레이어위치")]
    public Transform player;

    [Header("메인카메라")]
    public Transform main_cam;

    [HideInInspector] public float Boss_hp;

    string phase_num;

    float rigidTime;
    int atk;
    int hp;
    float speed;

    bool attack_flag;
    bool hit_flag;
    bool move_flag;

    Animator Boss_ani;

    Vector3 player_pos;

    private void Start()
    {
        Boss_ani = this.GetComponent<Animator>();
        this.gameObject.transform.position = player.position;
        rigidTime = GameManager.instance.monsterManager.GetMonster(name).rigidTime;
        atk = GameManager.instance.monsterManager.GetMonster(name).atk;
        hp = GameManager.instance.monsterManager.GetMonster(name).hp;
        speed = GameManager.instance.monsterManager.GetMonster(name).speed;
        Boss_hp = hp;

        phase_num = "Phase_01";

        player_pos = player.transform.position;

        CameraShake();
    }

    private void CameraShake()
    {
        main_cam.transform.DOShakePosition(2.0f);
    }

    private void FixedUpdate()
    {
        //카메라 영역제한
        float cam_x = Mathf.Clamp(main_cam.position.x, player_pos.x - 6, player_pos.x + 6);
        float cam_y = Mathf.Clamp(main_cam.position.y, player_pos.y - 10, player_pos.y + 10);

        main_cam.position = new Vector3(cam_x, cam_y, main_cam.position.z);

        Move();
    }

    public void Move()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (!hit_flag && !attack_flag)
        {
            if (move_flag)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed / 120f);

                if (player.position.x - this.transform.position.x > 0)
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                else
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else
            {
                //보스 idle 애니메이션 적용
            }

            //보스 이동 애니메이션 적용
            /*if (currentSpineName != move)
            {
                currentSpineName = move;
                skeletonAnimation.AnimationState.SetAnimation(0, move, true);
                skeletonAnimation.timeScale = 2;
            }*/
        }

    }
    public void Phase_01()
    {
        int range = Random.Range(0, 100);
        if (range < 35)
            //보스 브레스 애니메이션 동작
            ;
        else if (35 <= range && range < 70)
            //보스 플레임 애니메이션 동작
            ;
        else
            //보스 할퀴기 동작
            ;
        if(Boss_hp <= 70)
        {
            //보스 화염탄 난사 애니메이션 동작
            phase_num = "Phase_02";
        }
    }

    public void Phase_02()
    {
        int range = Random.Range(0, 100);
        if (range < 30)
            //보스 브레스탄 애니메이션 동작
            ;
        else if (30 <= range && range < 60)
            //보스 화염폭발 애니메이션 동작
            ;
        else if (60 <= range && range < 80)
            //보스 꼬리치기 동작
            ;
        else
            //플레임 동작
            ;
        if (Boss_hp <= 30)
        {
            //보스 화염탄 난사 애니메이션 동작
            phase_num = "Phase_03";
        }

    }

    public void Phase_03()
    {
        int range = Random.Range(0, 100);
        if (range < 20)
            //보스 플레임 강화 애니메이션 동작
            ;
        else if (20 <= range && range < 45)
            //보스 불타는 대지 애니메이션 동작
            ;
        else if (45 <= range && range < 70)
            //보스 브레스 강화 동작
            ;
        else if (70 <= range && range < 80)
            //보스 화염탄 난사 동작
            ;
        else
            //꼬리치기 강화 동작
            ;
    }

}
