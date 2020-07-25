using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("STATUS")]
    [Range(0,100)] public float atk;
    public int max_hp;
    [Range(0.1f, 3f)] public float atkspeed;
    [Range(0.04f, 0.1f)] public float speed;
    [Range(1f, 5f)] public float item_range;
    [Range(0,100)] public float sheild;
    [Range(3, 10)] public float range;
    [Range(1.5f, 3f)] public float dash_move;

    [HideInInspector]
    public int currentPoint;
    public int deadMonsterNum;
    int maxLv;

    [HideInInspector]
    public int current_hp;
    [Header("카메라")]
    public Transform theCam;
    [Header("조이스틱")]
    public Transform joystic_foreground;
    Vector2 joystic_localpos;
    [Header("체력 이미지")]
    public Image hp_image;
    [Header("대쉬, 장애물, 스킬아이템 버튼")]
    public GameObject tel_obj;
    public GameObject dash_btn;
    public GameObject treasure_btn;
    public GameObject skill_item_btn;
    GameObject treasure_obj;
    GameObject skill_item_obj;

    [Header("UI")]
    public Text lv_up;
    public Image exp_full;
    [Header("UI2")]
    public UI2_Manager uI2_Manager;

    [Header("오디오")]
    public AudioSource dash_AudioSource;
    public AudioSource levelUp_AudioSource;
    public AudioSource move_AudioSource;

    /// <summary>
    ///  스파인
    /// </summary>
    [Header("스파인")]
    public GameObject idle_down;
    public GameObject down_ver;
    public GameObject ver;
    public GameObject up;
    public GameObject up_ver;
    public GameObject dead;

    [Header("GameEnd")]
    public GameObject GameOverPannel;


    //플레이어 레벨 업 변수 정보
    [HideInInspector] public int character_lv_hp;
    [HideInInspector] public int character_lv_speed;
    [HideInInspector] public int character_lv_exp;

    [HideInInspector] public int attack_lv_atk;
    [HideInInspector] public int attack_lv_speed;
    [HideInInspector] public int attack_lv_count;

    [HideInInspector] public int skill_lv_atk;
    [HideInInspector] public int skill_lv_cooltime;
    [HideInInspector] public int skill_lv_getcount;

    [HideInInspector] public bool bossstage;

    //메인화면 마법진 정보 
    [HideInInspector] public int masic_maxhp; // 최대체력
    [HideInInspector] public int masic_atk; // 공격력
    [HideInInspector] public int masic_atkspeed; // 공격속도
    [HideInInspector] public int masic_speed; // 이동속도
    [HideInInspector] public int masic_itemdistance; // 아이템 거리
    [HideInInspector] public int masic_sheild; // 방어력
    [HideInInspector] public int masic_recovery; // 회복량
    [HideInInspector] public int masic_skilldamage; // 스킬 데미지
    [HideInInspector] public int masic_exp; // 경험치 획득
    [HideInInspector] public int masic_point; // 포인트 획득

    Rigidbody2D rigidbody2D;

    [HideInInspector]
    public int player_exp;

    [HideInInspector]
    public int player_lv;

    [HideInInspector] public bool boss_start;//보스스테이지 시작시 잠시 멈추게

    private void Start()
    {
        player_exp = 0;
        MaxHpSetting();
        current_hp = max_hp;
        lv_up.text = "LV" + player_lv;
        hp_image.fillAmount = current_hp / max_hp;
        rigidbody2D = GetComponent<Rigidbody2D>();
        exp_full.fillAmount = (float)player_exp / 100;

        character_lv_hp = 1;
        character_lv_speed = 1;
        character_lv_exp = 1;

        attack_lv_atk = 1;
        attack_lv_speed = 1;
        attack_lv_count = 1;

        skill_lv_atk = 1;
        skill_lv_cooltime = 1;
        skill_lv_getcount = 1;

        bossstage = false;

        masic_maxhp = GameManager.instance.userinfo.GetMasicMaxHP();
        masic_atk = GameManager.instance.userinfo.GetMasicAtk();
        masic_atkspeed = GameManager.instance.userinfo.GetMasicAtkspeed();
        masic_speed = GameManager.instance.userinfo.GetMasicSpeed();
        masic_itemdistance = GameManager.instance.userinfo.GetMasicItemDistance();
        masic_sheild = GameManager.instance.userinfo.GetMasicShield();
        masic_recovery = GameManager.instance.userinfo.GetMasicRecovery();
        masic_skilldamage = GameManager.instance.userinfo.GetMasicSkillDamage();
        masic_exp = GameManager.instance.userinfo.GetMasicExp();
        masic_point = GameManager.instance.userinfo.GetMasicPoint();

        speed = MoveSpeed();

        int tempMaxLv = -1;
        for (int i = 0; i < GameManager.instance.database.skillCard_DB.GetLineSize(); i++)
        {
            tempMaxLv += int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(i)[1]);
        }
        maxLv = tempMaxLv;
    }

    public float Atk()
    {
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(1);
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(1);
        float tempAtk = atk + (atk * masic_atk * float.Parse(masic_db[1]) / 100) + (atk * attack_lv_atk * float.Parse(skillcard_db[2]) / 100);
        return tempAtk;
    }

    public void MaxHpSetting()
    {
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(0);
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(0);
        max_hp = (int)(max_hp + (max_hp * masic_maxhp * float.Parse(masic_db[1]) / 100) + (max_hp * character_lv_hp * float.Parse(skillcard_db[2]) / 100));
    }

    public float AtkSpeed()
    {
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(2); 
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(2);
        float tempAtkSpeed = atkspeed - ((atkspeed * masic_maxhp * float.Parse(masic_db[1]) / 100) + (atkspeed * attack_lv_speed * float.Parse(skillcard_db[2]) / 100));
        return tempAtkSpeed;
    }

    public float MoveSpeed()
    {
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(3);
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(3);
        float tempMoveSpeed = speed + (speed * masic_speed * float.Parse(masic_db[1]) / 100) + (speed * character_lv_speed * float.Parse(skillcard_db[2]) / 100);
        return tempMoveSpeed;
    }

    public float ItemRange()
    {
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(4);
        float tempItem_range = item_range + (item_range * masic_itemdistance * float.Parse(masic_db[1]) / 100);
        return tempItem_range;
    }

    public float Sheild()
    {
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(5);
        float tempSheild = sheild + (sheild * masic_sheild * float.Parse(masic_db[1]) / 100);
        return tempSheild;
    }

    // % 형식
    public float Recovery()
    {
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(6);
        float tempRecovery = masic_recovery * float.Parse(masic_db[1]);
        return tempRecovery / 100;
    }
    // % 형식
    public float SkillDamage()
    {
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(6);
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(7);
        float tempSkillDamage = masic_skilldamage * float.Parse(masic_db[1]) + (skill_lv_atk * float.Parse(skillcard_db[2]));
        return tempSkillDamage / 100;
    }
    // % 형식
    public float ExpUpPercent()
    {
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(5);
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(8);
        float tempExp = (masic_exp * float.Parse(masic_db[1])) + (character_lv_exp * float.Parse(skillcard_db[2]));
        return tempExp / 100;
    }
    // % 형식
    public float PointUpPercent()
    {
        List<string> masic_db = GameManager.instance.database.masic_circle_DB.GetRowData(9);
        float tempPoint = masic_point * float.Parse(masic_db[1]);
        return tempPoint / 100;
    }
    // 캐릭터 카드로 렙업 했을 때
    public void Chracter_LvUp_MaxHp()
    {
        List<string> skillcard_db = GameManager.instance.database.skillCard_DB.GetRowData(0);
        float tempCurrentMaxHp01 = max_hp;
        MaxHpSetting();
        float tempCurrentMaxHp02 = max_hp;
        current_hp += (int)(tempCurrentMaxHp02 - tempCurrentMaxHp01);
        if (max_hp < current_hp)
            current_hp = max_hp;

        hp_image.fillAmount = (float)current_hp / max_hp;
    }

    public void PointUp(int up)
    {
        currentPoint += up + (int)(up * PointUpPercent());
        if (!PlayerPrefs.HasKey("Point"))
        {
            PlayerPrefs.SetInt("Point", currentPoint);
        }
        else
        {
            if (PlayerPrefs.GetInt("Point") < currentPoint)
            {
                PlayerPrefs.SetInt("Point", currentPoint);
            }
        }
    }

    public float SkillCoolTime()
    {
        return skill_lv_cooltime * 3;
    }

    private void FixedUpdate()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (daedFlag)
        {
            return;
        }

        joystic_localpos = joystic_foreground.GetComponent<RectTransform>().localPosition;

        if (joystic_localpos != Vector2.zero)
        {
            if (!move_AudioSource.isPlaying)
                GameManager.instance.audioManager.EnvironVolume_Play(move_AudioSource);

            if (!treasure_flag)
            {
                if (!dash_flag&&!boss_start) // 대쉬중에는 막아둔다
                {
                    // 조이스틱 위치에 따른 플레이어 위치 
                    this.transform.position = new Vector2(
                    this.transform.position.x + (joystic_localpos.x * Time.fixedDeltaTime * speed),
                    this.transform.position.y + (joystic_localpos.y * Time.fixedDeltaTime * speed));
                    
                    if (bossstage)
                    {
                        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
                        viewPos.x = Mathf.Clamp01(viewPos.x);
                        viewPos.y = Mathf.Clamp01(viewPos.y);
                        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
                     }

                }

                Vector2 dir = Vector2.zero - joystic_localpos;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;

                if (angle > 337.5f && angle <= 360 || angle > 0 && angle <= 22.5f) // 오
                    Spine_Ani(AniKind.right);
                if (angle > 22.5f && angle <= 67.5f) // 위_왼
                    Spine_Ani(AniKind.up_right);
                if (angle > 67.5f && angle <= 112.5f) // 위
                    Spine_Ani(AniKind.up);
                if (angle > 112.5f && angle <= 157.5f) // 위_오
                    Spine_Ani(AniKind.up_left);
                if (angle > 157.5f && angle <= 202.5f) // 왼
                    Spine_Ani(AniKind.left);
                if (angle > 202.5f && angle <= 247.5f) // 아래_오른쪽
                    Spine_Ani(AniKind.down_right);
                if (angle > 247.5f && angle <= 292.5f) // 아래 
                    Spine_Ani(AniKind.down);
                if (angle > 292.5f && angle <= 337.5f) // 아래_왼쪽 
                    Spine_Ani(AniKind.down_left);
            }
        }
        else
        {
            // idle
            Spine_Ani(AniKind.idle);

            if (move_AudioSource.isPlaying)
                move_AudioSource.Stop();
        }
        //new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z);
        // 카메라 플레이어 따라가기 
        theCam.position = Vector3.SmoothDamp(theCam.position, new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z), ref velocity, 0.3f);
    }
    Vector3 velocity = Vector3.zero;

    string currentAniName;
    string currentSkinName;
    void Spine_Ani(AniKind ani)
    {
        string aniName = "";
        switch (ani)
        {
            case AniKind.idle:
                aniName = "character001_idle_down";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = Vector2.zero; down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
                idle_down.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_idle_down", true);
                currentAniName = "character001_idle_down";
                idle_down.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.up:
                aniName = "character001_move_up";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = Vector2.zero; up_ver.transform.localPosition = new Vector2(2000, 2000);

                up.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_up", true);
                currentAniName = "character001_move_up";

                up.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.up_left:
                aniName = "character001_move_upL";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = Vector2.zero;
                up_ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,"character001_move_upL", true);
                currentAniName = "character001_move_upL";
                up_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_upL");
                up_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                up_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                currentSkinName = "character001_upL";
                up_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.up_right:
                aniName = "character001_move_upR";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = Vector2.zero;
                up_ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_upR", true);
                currentAniName = "character001_move_upR";
                currentSkinName = "character001_upR";
                up_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_upR");
                up_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                up_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                up_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AniKind.left:
                aniName = "character001_move_left";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = Vector2.zero; up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
            
                ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_left", true);
                currentAniName = "character001_move_left";
                ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_left");
                currentSkinName = "character001_left";
                ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                ver.GetComponent<SkeletonAnimation>().LateUpdate();
                ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AniKind.right:
                aniName = "character001_move_right";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = Vector2.zero; up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
         
                ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_right", true);
                currentAniName = "character001_move_right";
                ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_right");
                currentSkinName = "character001_right";
                ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                ver.GetComponent<SkeletonAnimation>().LateUpdate();
                ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.down:
                aniName = "character001_move_down";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = Vector2.zero; down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);

                idle_down.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_down", true);
                currentAniName = "character001_move_down";

                idle_down.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.down_left:
                aniName = "character001_move_downleft";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = Vector2.zero; ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
                down_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_downleft");
                currentSkinName = "character001_downleft";
                down_ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,"character001_move_downleft",true);
                down_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                down_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                currentAniName = "character001_move_downleft";

                down_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.down_right:
                aniName = "character001_move_downright";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = Vector2.zero; ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
                down_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_downright");
                currentSkinName = "character001_downright";
                down_ver.GetComponent<SkeletonAnimation>().AnimationName = aniName;
                down_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                down_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                currentAniName = "character001_move_downright";

                down_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AniKind.hit:
                idle_down.SetActive(false); down_ver.SetActive(false); ver.SetActive(false); up.SetActive(false); up_ver.SetActive(false);
                break;
            case AniKind.item:
                idle_down.SetActive(false); down_ver.SetActive(false); ver.SetActive(false); up.SetActive(false); up_ver.SetActive(false);
                break;
            case AniKind.dead:
                idle_down.SetActive(false); down_ver.SetActive(false); ver.SetActive(false); up.SetActive(false); up_ver.SetActive(false);
                break;
            default:
                break;
        }
    }

    enum AniKind
    {
        idle,
        up,
        up_left,
        up_right,
        left,
        right,
        down,
        down_left,
        down_right,
        hit,
        item,
        dead
    }

    public void OnClick_Dash()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (daedFlag)
        {
            return;
        }

        if (Mathf.Abs(joystic_localpos.x) < 45 && Mathf.Abs(joystic_localpos.y) < 45)
        {
            return;
        }

        if (dash_btn.transform.GetChild(0).GetComponent<Image>().fillAmount != 1)
        {
            return;
        }

        dash_btn.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        dash_btn.transform.GetChild(0).GetComponent<Image>().DOFillAmount(1, 3f); // 쿨타임 

        StartCoroutine(DashCoroutine());
    }

    bool dash_flag;
    IEnumerator DashCoroutine()
    {
        dash_flag = true;

        GameManager.instance.audioManager.EnvironVolume_Play(dash_AudioSource);

        switch (currentAniName)
        {
            case "character001_move_up":
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + dash_move);
                break;
            case "character001_move_upL":
                this.transform.position = new Vector2(this.transform.position.x - dash_move, this.transform.position.y + dash_move);
                break;
            case "character001_move_upR":
                this.transform.position = new Vector2(this.transform.position.x + dash_move, this.transform.position.y + dash_move);
                break;
            case "character001_move_left":
                this.transform.position = new Vector2(this.transform.position.x - dash_move, this.transform.position.y);
                break;
            case "character001_move_right":
                this.transform.position = new Vector2(this.transform.position.x + dash_move, this.transform.position.y);
                break;
            case "character001_move_down":
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - dash_move);
                break;
            case "character001_move_downright":
                this.transform.position = new Vector2(this.transform.position.x - dash_move, this.transform.position.y - dash_move);
                break;
            case "character001_move_downleft":
                this.transform.position = new Vector2(this.transform.position.x + dash_move, this.transform.position.y - dash_move);
                break;
            default:
                break;
        }
        yield return null;

        Instantiate(tel_obj, new Vector2(this.transform.position.x , this.transform.position.y + 0.25f), Quaternion.identity, this.transform);
        dash_flag = false;

    }

    bool treasure_flag;
    public void OnClick_Treasure()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (daedFlag)
        {
            return;
        }

        if (treasure_flag)
            return;

        StartCoroutine(Treasure_Coroutine());

    }
    IEnumerator Treasure_Coroutine()
    {
        treasure_flag = true;
        treasure_obj.GetComponent<Treasure>().TreasureOpen();
        yield return new WaitForSeconds(0.7f);
        treasure_flag = false;
    }


    bool hit_flag;
    public void Hit(int damage, GameObject enemy)
    {
        if (TimeManager.instance.GetTime())
            return;

        if (hit_flag)
            return;

        if (daedFlag)
        {
            return;
        }

        float tempDamage = (float)damage - Sheild();
        if (tempDamage < 0)
        {
            tempDamage = 1;
        }

        current_hp -= (int)tempDamage;
        hp_image.fillAmount = (float)current_hp / max_hp;

        //죽음
        if(current_hp <= 0)
        {
            // Game_Over();
            StartCoroutine(GameOverCoroutine());
        }
        Vector3 dir = enemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;

        float draft = 10f;

        if (angle > 315 || angle <= 360 && angle > 0 || angle <= 45) // right   
            rigidbody2D.velocity = new Vector2(draft, 0);
        if (angle > 225 && angle <= 315) // under
            rigidbody2D.velocity = new Vector2(0, -draft);
        if (angle > 135 && angle <= 225) // left
            rigidbody2D.velocity = new Vector2(-draft, 0);
        if (angle > 45 && angle <= 135) // up
            rigidbody2D.velocity = new Vector2(0, draft);

        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        hit_flag = true;
        int countTime = 0;
        idle_down.GetComponent<Animator>().SetBool("hit", true);
        down_ver.GetComponent<Animator>().SetBool("hit", true);
        ver.GetComponent<Animator>().SetBool("hit", true);
        up.GetComponent<Animator>().SetBool("hit", true);
        up_ver.GetComponent<Animator>().SetBool("hit", true);
        yield return new WaitForSeconds(0.15f);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        idle_down.GetComponent<Animator>().SetBool("hit", false);
        down_ver.GetComponent<Animator>().SetBool("hit", false);
        ver.GetComponent<Animator>().SetBool("hit", false);
        up.GetComponent<Animator>().SetBool("hit", false);
        up_ver.GetComponent<Animator>().SetBool("hit", false);
        hit_flag = false;
    }

    bool daedFlag = false;
    IEnumerator GameOverCoroutine()
    {
        daedFlag = true;

        idle_down.transform.localPosition = new Vector2(2000, 2000);
        down_ver.transform.localPosition = new Vector2(2000, 2000);
        ver.transform.localPosition = new Vector2(2000, 2000);
        up.transform.localPosition = new Vector2(2000, 2000);
        up_ver.transform.localPosition = new Vector2(2000, 2000);
        dead.transform.localPosition = Vector2.zero;

        dead.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,"character001_dead" ,false);
        currentSkinName = "character001_dead";
        dead.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
        dead.GetComponent<SkeletonAnimation>().LateUpdate();
        currentAniName = "character001_dead";

        yield return new WaitForSeconds(1f);

        GameOverPannel.SetActive(true);
    }

    public void Hp_Recovery(int up)
    {
        if (TimeManager.instance.GetTime())
            return;

        if (daedFlag)
        {
            return;
        }

        float tempRecovery = up + (up * Recovery());

        current_hp += (int)tempRecovery;
        if (max_hp < current_hp)
            current_hp = max_hp;

        hp_image.fillAmount = (float)current_hp / max_hp;
    }

    public void Exp_Up(int up)
    {
        if (TimeManager.instance.GetTime())
            return;

        if (maxLv == player_lv)
            return;

        if (daedFlag)
        {
            return;
        }

        float tempExpUp = up + (up * ExpUpPercent());
        player_exp += (int)tempExpUp;
        //int.Parse(GameManager.instance.database.exp_DB.GetRowData(player_lv-1)[0]);
        int needExp = 1;
        if (player_exp >= needExp)
        {
            uI2_Manager.SkillSelect();
            player_exp = 0;
            player_lv++;
            lv_up.text = "LV" + player_lv;
            GameManager.instance.audioManager.EnvironVolume_Play(levelUp_AudioSource);
            TimeManager.instance.SetTime(true);

            if (maxLv == player_lv)
            {
                exp_full.fillAmount = 1;
            }
        }
        exp_full.fillAmount = (float)player_exp / needExp;
    }

    bool skill_item_flag = false;
    public void OnClick_SkillItem()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (skill_item_flag)
            return;

        if (daedFlag)
        {
            return;
        }

        StartCoroutine(Skill_item_Coroutine());

    }
    IEnumerator Skill_item_Coroutine()
    {
        skill_item_flag = true;
        string skill_name = skill_item_obj.GetComponent<Skill_Item>().skillname;
        this.GetComponent<PlayerSkill>().Get(skill_name);
        Destroy(skill_item_obj);
        yield return new WaitForSeconds(0.7f);
        skill_item_flag = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Treasure"))
        {
            treasure_obj = collision.gameObject;
            dash_btn.SetActive(false);
            treasure_btn.SetActive(true);
        }
    }
    //스킬구슬 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("Skill_Item"))
        {
            dash_btn.SetActive(false);
            skill_item_btn.SetActive(true);
            skill_item_obj = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Treasure"))
        {
            dash_btn.SetActive(true);
            treasure_btn.SetActive(false);
        }

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("Skill_Item"))
        {
            dash_btn.SetActive(true);
            skill_item_btn.SetActive(false);
        }
    }

}
