using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("플레이어위치")]
    public Transform player;

    [Header("메인카메라")]
    public Transform main_cam;

    [Header("카운트다운")]
    public CountdownManager countdown;

    [Header("보스hp")]
    public Image boss_hp;
    public GameObject bosshpbar;

    [Header("오디오")]
    public AudioSource hit_nomal_atk_AudioSource;

    [Header("스파인 정보")]
    SkeletonAnimation skeletonAnimation;

    [Header("몬스터 정보")]
    public string name;
    string currentSpineName;

    //연출
    [HideInInspector] public float Boss_hp;
    /*public Transform startpos; //보스연출 처음 등장하는 곳
    bool boss_start;
    public GameObject smoke;//연출 연기*/

    int atk;
    int hp;
    float speed;

    string phase_num;

    bool attack_flag;
    bool hit_flag;
    bool move_flag;

    [Header("범위")]
    public float range;

    Animator Boss_ani;

    Vector3 player_pos;

    private void Start()
    {
        Boss_ani = this.GetComponent<Animator>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        this.gameObject.transform.position = new Vector2(player.position.x+20,player.position.y+10);
        atk = GameManager.instance.monsterManager.GetMonster(name).atk;
        hp = GameManager.instance.monsterManager.GetMonster(name).hp;
        speed = GameManager.instance.monsterManager.GetMonster(name).speed;
        Boss_hp = hp;
        Boss_Skill.instance.phase_01 = true;
        bosshpbar.SetActive(true);
        phase_num = "Phase_03";
        countdown.time = 300;

        player_pos = player.transform.position;
    }


    private void FixedUpdate()
    {
        //카메라 영역제한
        float cam_x = Mathf.Clamp(main_cam.position.x, player_pos.x - 6, player_pos.x + 6);
        float cam_y = Mathf.Clamp(main_cam.position.y, player_pos.y - 10, player_pos.y + 10);

        main_cam.position = new Vector3(cam_x, cam_y, main_cam.position.z);


        boss_hp.fillAmount = (float)Boss_hp / hp;
        if (countdown.remainTime > 0 && Boss_hp <= 0)
            Victory();
        else if (countdown.remainTime <= 0)
            GameOver();
        Move();

    }

    void GameOver()
    {

    }

    void Victory()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Move()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (!hit_flag && !attack_flag)
        {
            if (move_flag)
            {
                Spine_Ani(AniKind.move);
                this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed/100f);

                if (player.position.x - this.transform.position.x > 0)
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                else
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);


                RaycastHit2D[] isplayer = Physics2D.CircleCastAll(this.transform.position, range, Vector2.zero);

                for(int i = 0; i<isplayer.Length; i++)
                {
                    if (isplayer[i].transform.gameObject.tag.Contains("Player"))
                        StartCoroutine("Attack_flag_Couroutine");
                }
            }
            if (attack_flag)
            {
                Invoke(phase_num,0.1f);
            }
            else
            {
                StartCoroutine("Move_flag_Couroutine");
            }
        }

    }

    IEnumerator Move_flag_Couroutine()
    {
        move_flag = true;
        yield return new WaitForSeconds(1);
        move_flag = false;
    }

    IEnumerator Attack_flag_Couroutine()
    {
        attack_flag = true;
        yield return new WaitForSeconds(5);
        attack_flag = false;
    }


    public void Phase_01()
    {
        if (TimeManager.instance.GetTime())
        {
            return;
        }
        int range = Random.Range(0, 100);
        if (range < 35)
            //보스 브레스 애니메이션 동작
            Spine_Ani(AniKind.Breath);
        else if (35 <= range && range < 70)
            //보스 플레임 애니메이션 동작
            Spine_Ani(AniKind.Flame);
        else
            //보스 할퀴기 동작
            Spine_Ani(AniKind.Claw);
        if(Boss_hp <= hp*0.7)
        {
            //보스 화염탄 난사 애니메이션 동작
            Spine_Ani(AniKind.RandomShot);
            Boss_Skill.instance.phase_01 = false;
            phase_num = "Phase_02";
        }
    }

    public void Phase_02()
    {
        if (TimeManager.instance.GetTime())
        {
            return;
        }
        int range = Random.Range(0, 100);
        if (range < 30)
            //보스 브레스탄 애니메이션 동작
            Spine_Ani(AniKind.Breath);
        else if (30 <= range && range < 60)
            //보스 화염폭발 애니메이션 동작
            Spine_Ani(AniKind.FlameBomb);
        else if (60 <= range && range < 80)
            //보스 꼬리치기 동작
            Spine_Ani(AniKind.Spin);
        else
            //플레임 동작
            Spine_Ani(AniKind.Flame);
        if (Boss_hp <= hp*0.3)
        {
            //보스 화염탄 난사 애니메이션 동작
            Spine_Ani(AniKind.RandomShot);
            Boss_Skill.instance.phase_01 = false;
            phase_num = "Phase_03";
        }

    }

    public void Phase_03()
    {
        if (TimeManager.instance.GetTime())
        {
            return;
        }
        int range = Random.Range(0, 100);
        if (range < 20)
            Spine_Ani(AniKind.Spin);
            //보스 플레임 강화 애니메이션 동작
        else if (20 <= range && range < 45)
            Spine_Ani(AniKind.Spin);
            //보스 불타는 대지 애니메이션 동작
        else if (45 <= range && range < 70)
            //보스 브레스 강화 동작
            Spine_Ani(AniKind.Spin);
        else if (70 <= range && range < 80)
        {
            //보스 화염탄 난사 동작
            Spine_Ani(AniKind.Spin);
            Boss_Skill.instance.phase_01 = false;
        }
        else
            //꼬리치기 강화 동작
            Spine_Ani(AniKind.Spin);
    }

    public void Hit(int damage, bool sound_flag = false)
    {
        if (TimeManager.instance.GetTime())
            return;

        GameObject damage_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.nomal_damage);
        damage_obj.transform.position = this.transform.position;
        damage_obj.GetComponent<Damage>().DamageSet(damage);

        Boss_hp -= damage;

        if(Boss_hp>0)
        {
            if (damage > 100)
            {
                StartCoroutine(Hit_Coroutine());
                Spine_Ani(AniKind.hit);
            }
            /*else
                GetComponent<Animator>().SetTrigger("hit");*/
            /*if (sound_flag)
            {
                hit_nomal_atk_AudioSource.Play();
            }*/
        }
    }
    IEnumerator Hit_Coroutine()
    {
        hit_flag = true;
        yield return new WaitForSeconds(0.3f);
        hit_flag = false;
    }

    void Spine_Ani(AniKind ani)
    {
        string aniName = "";
        switch (ani)
        {
            case AniKind.move:
                aniName = "Dragon_move";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                skeletonAnimation.AnimationState.SetAnimation(0, aniName, true);
                skeletonAnimation.timeScale = 2;
                break;
            case AniKind.hit:
                aniName = "Dragon_hit";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                GetComponent<Animator>().SetTrigger("hit");
                skeletonAnimation.AnimationState.SetAnimation(0, aniName, false);
                skeletonAnimation.timeScale = 1;
                break;
            case AniKind.Breath:
                aniName = "Dragon_Breath";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] Breath_name = { "Dragon_Breath_R", "Dragon_Breath_A", "Dragon_Breath_E" };
                float[] Breath_time = { 1, 2.5f, 1 };
                StartCoroutine(setAni_Coroutine(Breath_name, Breath_time, "Breath"));
                break;
            case AniKind.BurningGround:
                aniName = "Dragon_BurningGround";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] Burning_name = { "Dragon_BurningGround_R", "Dragon_BurningGround_A", "Dragon_BurningGround_E" };
                float[] Burning_time = { 1, 2.5f, 1 };
                StartCoroutine(setAni_Coroutine(Burning_name, Burning_time, "BurningGround"));
                break;
            case AniKind.Claw:
                aniName = "Dragon_Claw";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] Claw_name = { "Dragon_Claw_R", "Dragon_Claw_A", "Dragon_Claw_E" };
                float[] Claw_time = { 1, 1, 1 };
                StartCoroutine(setAni_Coroutine(Claw_name, Claw_time,"Claw"));
                break;
            case AniKind.FlameBomb:
                aniName = "Dragon_FlameBomb";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] FlameBomb_name = { "Dragon_FlameBomb_R", "Dragon_FlameBomb_A", "Dragon_FlameBomb_E" };
                float[] FlameBomb_time = { 1, 1, 1 };
                StartCoroutine(setAni_Coroutine(FlameBomb_name, FlameBomb_time,"FlameBomb"));
                skeletonAnimation.timeScale = 1;
                break;
            case AniKind.Flame:
                aniName = "Dragon_Flame";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] Flame_name = { "Dragon_Flame_R", "Dragon_Flame_A", "Dragon_Flame_E" };
                float[] Flame_time = { 1, 1, 1 };
                StartCoroutine(setAni_Coroutine(Flame_name, Flame_time,"Flame"));
                break;
            case AniKind.RandomShot:
                aniName = "Dragon_RandomShot";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] RandomShot_name = { "Dragon_RandomShot_R", "Dragon_RandomShot_A", "Dragon_RandomShot_E" };
                float[] RandomShot_time = { 1, 1, 1 };
                StartCoroutine(setAni_Coroutine(RandomShot_name, RandomShot_time,"RandomShot"));
                break;
            case AniKind.Spin:
                aniName = "Dragon_Spin";
                if (aniName == currentSpineName)
                    return;
                currentSpineName = aniName;
                string[] Spin_name = { "Dragon_Spin_R", "Dragon_Spin_A", "Dragon_Spin_E" };
                float[] Spin_time = { 1, 1, 0.5f };
                StartCoroutine(setAni_Coroutine(Spin_name, Spin_time,"Spin"));
                break;
            default:
                break;
        }
    }

    IEnumerator setAni_Coroutine(string[] name, float[] time, string type)
    {
        for(int i = 0; i<name.Length; i++)
        {
            skeletonAnimation.AnimationState.SetAnimation(0,name[i],false);
            skeletonAnimation.AnimationState.TimeScale = 1f;

            float randx = Random.Range(-5,5);
            Vector3 pos = new Vector3(player.position.x+randx,player.position.y);
            if (i == 1)
            {
                switch (type)
                {
                    case "Breath":
                            if (phase_num == "Phase_01")
                                Boss_Skill.instance.Breath();
                            else if (phase_num == "Phase_02")
                                Boss_Skill.instance.BreathBall();
                            else
                                Boss_Skill.instance.BreathStrong();
                        break;
                    case "Claw":
                        Boss_Skill.instance.Claw();
                        break;
                    case "BurningGround":
                        Boss_Skill.instance.BurningGround();
                        break;
                    case "FlameBomb":
                        Boss_Skill.instance.FlameBomb(pos);
                        break;
                    case "Flame":
                        if (phase_num == "Phase_03")
                            Boss_Skill.instance.FlameStrong(pos);
                        else
                            Boss_Skill.instance.Flame(pos);
                        break;
                    case "RandomShot":
                        if (phase_num == "Phase_01")
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                StartCoroutine("randshotCoroutine");
                            }
                        }
                        else
                            for(int j = 0; j<8; j++)
                            {
                                StartCoroutine("randshotCoroutine");
                            }
                        break;
                    case "Spin":
                        if (phase_num == "Phase_03")
                            Boss_Skill.instance.SpinStrong();
                        else
                            Boss_Skill.instance.Spin();
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(time[i]);
        }
    }

    IEnumerator randshotCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        Boss_Skill.instance.RandomShot();
    }

    enum AniKind
    {
        move,
        hit,
        Breath,
        BurningGround,
        Claw,
        FlameBomb,
        Flame,
        RandomShot,
        Spin
    }
}
