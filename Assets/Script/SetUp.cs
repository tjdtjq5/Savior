using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetUp : MonoBehaviour
{
    public GameObject stopPannel;
    public GameObject blackPannel;
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
    }

    public Text status_text;
    public GameObject status_pannel;

    public void StatusBtn()
    {
        Setting();
        status_text.GetComponent<Text>().color = Color.white;
        status_pannel.SetActive(true);
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
