﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob_nomal : MonoBehaviour
{
    [Header("오브")]
    [Range(15, 30)] public float speed = 15;

    [Header("오디오")]
    public AudioSource nomal_atk_AudioSource;

    [HideInInspector]
    public PlayerController player_status;
    [HideInInspector]
    public List<Transform> targetList = new List<Transform>();
    int iCount;

    bool atk_sound_flag;

    private void FixedUpdate()
    {
        if (targetList.Count < 1)
        {
            Destroy();
            return;
        }

        if (!atk_sound_flag)
        {
            GameManager.instance.audioManager.EnvironVolume_Play(nomal_atk_AudioSource);
            atk_sound_flag = true;
        }

        this.transform.position = Vector2.MoveTowards(this.transform.position, targetList[iCount].position, 0.3f);
        if (Vector2.Distance(transform.position, targetList[iCount].position) == 0)
        {
            if (StageManager.instance.currentStage == "Boss")
                targetList[iCount].GetComponent<Boss>().Hit((int)(player_status.Atk()), true);
            else targetList[iCount].GetComponent<MonsterController>().Hit((int)(player_status.Atk()), true);
            iCount++;
            if (iCount == targetList.Count)
            {
                atk_sound_flag = false;
                iCount = 0;
                Destroy();
            }
        }
    }

  

    private void OnEnable()
    {
        targetList.Clear();
        Invoke("Destroy", 10);
    }

    void Destroy()
    {
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.ob);
    }
}