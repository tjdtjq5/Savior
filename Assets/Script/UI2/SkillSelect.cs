using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    public UI2_Manager uI2_Manager;
    public Animator UI2_Ani;
    public PlayerController playerController;
    public Camera ui2_camera;

    [Header("SelectPannel")]
    public GameObject select01;
    public GameObject select02;
    public Transform select02_canvas;
    [Header("02선택카드")]
    public Sprite[] characterPassive;
    public Sprite[] attackPassive;
    public Sprite[] skillPassive;

    [Header("사운드")]
    public AudioSource selectSound;

    int select_num01;
    int select_num02;

    bool select_flag;

    int maxLv_character_lv_hp;
    int maxLv_character_lv_speed;
    int maxLv_character_lv_exp;
    int maxLv_attack_lv_atk;
    int maxLv_attack_lv_speed;
    int maxLv_attack_lv_count;
    int maxLv_skill_lv_atk;
    int maxLv_skill_lv_cooltime;
    int maxLv_skill_lv_getcount;

    private void Start()
    {
        maxLv_character_lv_hp = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(0)[1]);
        maxLv_character_lv_speed = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(3)[1]);
        maxLv_character_lv_exp = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(5)[1]);
        maxLv_attack_lv_atk = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(1)[1]);
        maxLv_attack_lv_speed = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(2)[1]);
        maxLv_attack_lv_count = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(4)[1]);
        maxLv_skill_lv_atk = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(6)[1]);
        maxLv_skill_lv_cooltime = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(7)[1]);
        maxLv_skill_lv_getcount = int.Parse(GameManager.instance.database.skillCard_DB.GetRowData(8)[1]);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !select_flag)
        {
            Vector2 mousePos = ui2_camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10);
            if (hit)
            {
                switch (hit.transform.name)
                {
                    case "캐릭터패시브":
                        if (playerController.character_lv_hp >= maxLv_character_lv_hp &&
                            playerController.character_lv_exp >= maxLv_character_lv_exp &&
                            playerController.character_lv_speed >= maxLv_character_lv_speed)
                        {
                            return;
                        }
                        select_num01 = 1;
                        GameManager.instance.audioManager.EnvironVolume_Play(selectSound);
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        StartCoroutine(Ani_Coroutine("SkillSelect01_FadeOut", .9F, Select01Btn));
                        break;
                    case "공격패시브 ":
                        if (playerController.attack_lv_atk >= maxLv_attack_lv_atk &&
                        playerController.attack_lv_speed >= maxLv_attack_lv_speed &&
                        playerController.attack_lv_count >= maxLv_attack_lv_count)
                        {
                            return;
                        }
                        select_num01 = 2;
                        GameManager.instance.audioManager.EnvironVolume_Play(selectSound);
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        StartCoroutine(Ani_Coroutine("SkillSelect01_FadeOut", .9F, Select01Btn));
                        break;
                    case "스킬패시브":
                        if (playerController.skill_lv_atk >= maxLv_skill_lv_atk &&
                     playerController.skill_lv_cooltime >= maxLv_skill_lv_cooltime &&
                     playerController.skill_lv_getcount >= maxLv_skill_lv_getcount)
                        {
                            return;
                        }
                        select_num01 = 3;
                        GameManager.instance.audioManager.EnvironVolume_Play(selectSound);
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        StartCoroutine(Ani_Coroutine("SkillSelect01_FadeOut", .9F, Select01Btn));
                        break;
                    case "능력치선택1":
                        select_num02 = 1;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        Invoke("Select02Btn", 0.35f);
                        break;
                    case "능력치선택2":
                        select_num02 = 2;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        Invoke("Select02Btn", 0.35f);
                        break;
                    case "능력치선택3":
                        select_num02 = 3;
                        hit.transform.GetComponent<Animator>().SetBool("click", true);
                        Invoke("Select02Btn", 0.35f);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    IEnumerator Ani_Coroutine(string ani, float time, System.Action Callback = null)
    {
        select_flag = true;
        UI2_Ani.SetBool(ani, true);
        yield return new WaitForSeconds(time);
        UI2_Ani.SetBool(ani, false);
        select_flag = false;
        if (Callback != null)
        {
            Callback();
        }
    }

    void Select01Btn()
    {
        select02_canvas.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            select02_canvas.GetChild(i).GetComponent<Text>().color = new Color(1, 1, 1, 0);
            select02_canvas.GetChild(i).GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0);
        }

        switch (select_num01)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                    select02.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = characterPassive[i];

                select02_canvas.GetChild(0).GetComponent<Text>().text = "<color=#FFFC00>LV"+ playerController.character_lv_hp + "</color>" + "   체력";
                if (maxLv_character_lv_hp  == playerController.character_lv_hp)
                    select02_canvas.GetChild(0).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";
                else
                    select02_canvas.GetChild(0).GetChild(0).GetComponent<Text>().text = "캐릭터 최대 체력\n" + "<color=#2EFF00>" + GameManager.instance.database.skillCard_DB.GetRowData(0)[2] + "%" + "</color>" + " 증가";
                
                select02_canvas.GetChild(1).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.character_lv_speed + "</color>" + "   이동속도";
                if (maxLv_character_lv_speed != playerController.character_lv_speed)
                    select02_canvas.GetChild(1).GetChild(0).GetComponent<Text>().text = "캐릭터 이동속도\n" + "<color=#2EFF00>" + GameManager.instance.database.skillCard_DB.GetRowData(3)[2] + "%" + "</color>" + " 증가";
                else
                    select02_canvas.GetChild(1).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";
                
                select02_canvas.GetChild(2).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.character_lv_exp + "</color>" + "   경험치상승";
                if (maxLv_character_lv_exp  != playerController.character_lv_exp)
                    select02_canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = "캐릭터 경험치 상승량\n" + "<color=#2EFF00>" +GameManager.instance.database.skillCard_DB.GetRowData(5)[2] +  "%" + "</color>" + " 증가";
                else
                    select02_canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                    select02.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = attackPassive[i];

                select02_canvas.GetChild(0).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.attack_lv_atk + "</color>" + "   공격력";
                if (maxLv_attack_lv_atk  != playerController.attack_lv_atk)
                    select02_canvas.GetChild(0).GetChild(0).GetComponent<Text>().text = "일반 공격력 증가\n" + "<color=#2EFF00>"+ GameManager.instance.database.skillCard_DB.GetRowData(1)[2] + "%" + "</color>" + " 증가";
                else
                    select02_canvas.GetChild(0).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";

                select02_canvas.GetChild(1).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.attack_lv_speed + "</color>" + "   공격속도";
                if (maxLv_attack_lv_speed  != playerController.attack_lv_speed)
                    select02_canvas.GetChild(1).GetChild(0).GetComponent<Text>().text = "일반 공격속도\n" + "<color=#2EFF00>" + GameManager.instance.database.skillCard_DB.GetRowData(2)[2] + "%" + "</color>" + " 감소";
                else
                    select02_canvas.GetChild(1).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";

                select02_canvas.GetChild(2).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.attack_lv_count + "</color>" + "   개체수 증가";
                if (maxLv_attack_lv_count  != playerController.attack_lv_count)
                    select02_canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = "적 공격 개체수\n" + "<color=#2EFF00>"+ GameManager.instance.database.skillCard_DB.GetRowData(4)[2] + "</color>" + " 증가";
                else
                    select02_canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                    select02.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = skillPassive[i];

                select02_canvas.GetChild(0).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.skill_lv_atk + "</color>" + "   스킬공격력";
                if (maxLv_skill_lv_atk  != playerController.skill_lv_atk)
                    select02_canvas.GetChild(0).GetChild(0).GetComponent<Text>().text = "모든 스킬 공격력\n" + "<color=#2EFF00>" + GameManager.instance.database.skillCard_DB.GetRowData(6)[2] + "%" + "</color>" + " 증가";
                else
                    select02_canvas.GetChild(0).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";

                select02_canvas.GetChild(1).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.skill_lv_cooltime + "</color>" + "   쿨타임";
                if (maxLv_skill_lv_cooltime  != playerController.skill_lv_cooltime)
                    select02_canvas.GetChild(1).GetChild(0).GetComponent<Text>().text = "모든 스킬 쿨타임\n" + "<color=#2EFF00>"+ GameManager.instance.database.skillCard_DB.GetRowData(7)[2] + "%" + "</color>" + " 감소";
                else
                    select02_canvas.GetChild(1).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";

                select02_canvas.GetChild(2).GetComponent<Text>().text = "<color=#FFFC00>LV" + playerController.skill_lv_getcount + "</color>" + "   스킬 소지개수";
                if (maxLv_skill_lv_getcount != playerController.skill_lv_getcount)
                    select02_canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = "소지할 수 있는 스킬의 수\n" + "<color=#2EFF00>"+ GameManager.instance.database.skillCard_DB.GetRowData(8)[2] + "</color>" + " 증가";
                else
                    select02_canvas.GetChild(2).GetChild(0).GetComponent<Text>().text = "<color=#FF0000>MaxLv 입니다.</color>";
                break;
        }
        
        select01.SetActive(false);
        Invoke("Select02BugInvoke", 0.15f);
        Invoke("Select02CanvasColor", 0.6f);
        StartCoroutine(Ani_Coroutine("SkillSelect02_FadeIn", 1));
    }

    void Select02BugInvoke()
    {
        select02.SetActive(true);
     
    }

    void Select02CanvasColor()
    {
        select02_canvas.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            select02_canvas.GetChild(i).GetComponent<Text>().DOFade(1, 1);
            select02_canvas.GetChild(i).GetChild(0).GetComponent<Text>().DOFade(1, 1);
        }
    }

    void Select02Btn()
    {
        switch (select_num01)
        {
            case 1:
                switch (select_num02)
                {
                    case 1:
                        if (maxLv_character_lv_hp == playerController.character_lv_hp)
                            return;
                        playerController.character_lv_hp++;
                        playerController.Chracter_LvUp_MaxHp();
                        break;
                    case 2:
                        if (maxLv_character_lv_speed == playerController.character_lv_speed)
                            return;
                        playerController.character_lv_speed++;
                        break;
                    case 3:
                        if (maxLv_character_lv_exp  == playerController.character_lv_exp)
                            return;
                        playerController.character_lv_exp++;
                        break;
                }
                break;
            case 2:
                switch (select_num02)
                {
                    case 1:
                        if (maxLv_attack_lv_atk  == playerController.attack_lv_atk)
                            return;
                        playerController.attack_lv_atk++;
                        break;
                    case 2:
                        if (maxLv_attack_lv_speed  == playerController.attack_lv_speed)
                            return;
                        playerController.attack_lv_speed++;
                        break;
                    case 3:
                        if (maxLv_attack_lv_count  == playerController.attack_lv_count)
                            return;
                        playerController.attack_lv_count++;
                        break;
                }
                break;
            case 3:
                switch (select_num02)
                {
                    case 1:
                        if (maxLv_skill_lv_atk == playerController.skill_lv_atk)
                            return;
                        playerController.skill_lv_atk++;
                        break;
                    case 2:
                        if (maxLv_skill_lv_cooltime == playerController.skill_lv_cooltime)
                            return;
                        playerController.skill_lv_cooltime++;
                        break;
                    case 3:
                        if (maxLv_skill_lv_getcount == playerController.skill_lv_getcount)
                            return;
                        playerController.skill_lv_getcount++;
                        break;
                }
                break;
        }
        uI2_Manager.SkillSelect_Exit();
    }
}
