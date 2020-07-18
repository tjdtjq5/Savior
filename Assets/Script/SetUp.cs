using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetUp : MonoBehaviour
{
    public GameObject stopPannel;
    public GameObject blackPannel;
    public PlayerController PlayerController;
    public PlayerSkill playerSkill;
    public void StopUIOpen()
    {
        if (!stopPannel.activeSelf)
        {
            TimeManager.instance.SetTime(true);
            stopPannel.SetActive(true);
            blackPannel.SetActive(true);
            Setting();
            PointBtn();
        }
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Play()
    {
        TimeManager.instance.SetTime(false);
        stopPannel.SetActive(false);
        blackPannel.SetActive(false);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Setting()
    {
        Color temp = new Color((float)106/255, (float)74 /255, (float)113 /255, (float)255 /255);
        point_text.GetComponent<Text>().color = temp;
        point_pannel.SetActive(false);
        status_text.GetComponent<Text>().color = temp;
        status_pannel.SetActive(false);
        masicCircle_text.GetComponent<Text>().color = temp;
        masicCircle_pannel.SetActive(false);
        activeSkill_text.GetComponent<Text>().color = temp;
        activeSkill_pannel.SetActive(false);
        artifacts_text.GetComponent<Text>().color = temp;
        artifacts_pannel.SetActive(false);
        sound_text.GetComponent<Text>().color = temp;
        sound_pannel.SetActive(false);
    }

    //패널 버튼
    [Header("패널")]
    public Text point_text;
    public GameObject point_pannel;

    public void PointBtn()
    {
        Setting();
        point_text.GetComponent<Text>().color = Color.white;
        point_pannel.SetActive(true);
        int highScore = 0;
        if (PlayerPrefs.HasKey("Point"))
        {
            highScore = PlayerPrefs.GetInt("Point");
        }
        point_pannel.transform.Find("최고점수").GetChild(0).GetComponent<Text>().text = string.Format("{0:#,###0}", highScore) + "점";
        point_pannel.transform.Find("현재점수").GetChild(0).GetComponent<Text>().text = string.Format("{0:#,###0}", PlayerController.currentPoint) + "점";
        point_pannel.transform.Find("처치 몬스터").GetChild(0).GetComponent<Text>().text = PlayerController.deadMonsterNum.ToString() + "마리";
    }

    public Text status_text;
    public GameObject status_pannel;

    public void StatusBtn()
    {
        Setting();
        status_text.GetComponent<Text>().color = Color.white;
        status_pannel.SetActive(true);
        status_pannel.transform.Find("공격력").GetChild(0).GetComponent<Text>().text = PlayerController.Atk().ToString();
        status_pannel.transform.Find("공격속도").GetChild(0).GetComponent<Text>().text = PlayerController.AtkSpeed().ToString() + "S";
        status_pannel.transform.Find("공격 객체수").GetChild(0).GetComponent<Text>().text = PlayerController.attack_lv_count.ToString();
        status_pannel.transform.Find("체력").GetChild(0).GetComponent<Text>().text = PlayerController.max_hp.ToString();
        status_pannel.transform.Find("스킬 슬롯").GetChild(0).GetComponent<Text>().text = PlayerController.skill_lv_getcount.ToString();
        status_pannel.transform.Find("스킬 공격력").GetChild(0).GetComponent<Text>().text = ((int)(PlayerController.SkillDamage() * 100)).ToString() + "%";
        status_pannel.transform.Find("스킬 쿨타임").GetChild(0).GetComponent<Text>().text = ((int)(PlayerController.SkillCoolTime())).ToString() + "%";
        status_pannel.transform.Find("이동속도").GetChild(0).GetComponent<Text>().text = ((int)(PlayerController.MoveSpeed() * 100)).ToString() + "%";
        status_pannel.transform.Find("경험치").GetChild(0).GetComponent<Text>().text = ((int)(PlayerController.ExpUpPercent() * 100)).ToString() + "%";
    }

    public Text masicCircle_text;
    public GameObject masicCircle_pannel;

    public void MasicCircleBtn()
    {
        Setting();
        masicCircle_text.GetComponent<Text>().color = Color.white;
        masicCircle_pannel.SetActive(true);
    }

    public Text activeSkill_text;
    public GameObject activeSkill_pannel;

    public void ActiveSkillBtn()
    {
        Setting();
        activeSkill_text.GetComponent<Text>().color = Color.white;
        activeSkill_pannel.SetActive(true);
        activeSkill_pannel.transform.Find("분류").gameObject.SetActive(false);
        activeSkill_pannel.transform.Find("스킬이름").gameObject.SetActive(false);
        activeSkill_pannel.transform.Find("스킬설명").gameObject.SetActive(false);

        Transform context = activeSkill_pannel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < context.childCount; i++)
        {
            context.GetChild(i).gameObject.SetActive(false);
            context.GetChild(i).GetComponent<Image>().color = new Color(192 / 255f, 192 / 255f, 192 / 255f, 186 / 255f);
        }
        for (int i = 0; i < playerSkill.player_skill.Count; i++)
        {
            for (int j = 0; j < context.childCount; j++)
            {
                if (playerSkill.player_skill[i] == context.GetChild(j).name)
                {
                    context.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
    }

    public Text artifacts_text;
    public GameObject artifacts_pannel;

    public void ArtifactsBtn()
    {
        Setting();
        artifacts_text.GetComponent<Text>().color = Color.white;
        artifacts_pannel.SetActive(true);
    }

    public Text sound_text;
    public GameObject sound_pannel;

    public void SoundBtn()
    {
        Setting();
        sound_text.GetComponent<Text>().color = Color.white;
        sound_pannel.SetActive(true);
    }
}
