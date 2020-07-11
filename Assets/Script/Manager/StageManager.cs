using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public EnemyManager enemyManager;
    public PlayerController playerController;
    public Background_StageManager background_stageManager;
    public Item_Manager item_Manager;
    public AudioSource next_audio;
    public Text stagetext;
    public Sound_Manager Sound_Manager;

    private void Awake()
    {
        instance = this;
    }

    public string currentStage;
    public int currentStageInt = 1;

    public void NextStage()
    {
        TimeManager.instance.SetTime(true);

        currentStageInt++;

        switch (currentStageInt)
        {
            case 1:
                Sound_Manager.Stage01();
                break;
            case 2:
                Sound_Manager.Stage02();
                break;
            case 3:
                Sound_Manager.Stage03();
                break;
            case 4:
                Sound_Manager.Stage04();
                break;
            case 5:
                Sound_Manager.Stage05();
                break;
            case 6:
                Sound_Manager.Boss();
                break;
        }

        if (currentStageInt == 6)
        {
            currentStage = "Boss";
            playerController.bossstage = true;
            enemyManager.bossstage = true;
            this.GetComponent<Animator>().SetBool("Boss", true);
        }
        else
        {
            currentStage = "Stage0" + currentStageInt;
            this.GetComponent<Animator>().SetBool("Next", true);
            GameManager.instance.audioManager.EnvironVolume_Play(next_audio);
        }
    }

    public void NextStage_Start()
    {
        //장애물없애기, 배경 바꾸기, 몬스터 없애기, 몬스터 변경
        enemyManager.NextStage();
        background_stageManager.NextStage();
        item_Manager.NextStage();
        if (currentStageInt == 6)
            stagetext.text = "BOSS";
        else stagetext.text = "STAGE " + currentStageInt.ToString();
    }

    public void NextStage_End()
    {
        TimeManager.instance.SetTime(false);
        if (currentStageInt == 6)
        {
            this.GetComponent<Animator>().SetBool("Boss", false);
        }
        else  this.GetComponent<Animator>().SetBool("Next", false);
    }

}
