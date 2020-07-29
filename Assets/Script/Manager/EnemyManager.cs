﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("플레이어 타겟")]
    public Transform player_transform;
    [Header("적 스폰")]
    public Transform enemy_spawn;
    [Header("초기 스폰 타임")]
    public float spawn_time = 1f;

    float middle = 35;
    float strong = 15;

    [HideInInspector] public bool bossstage;

    int currentStage;
    public int enemyCount = 0;

    private void Start()
    {
        currentStage = 1;
        Spawn();
    }

    void Spawn()
    {
        if (TimeManager.instance.GetTime())
        {
            if (!bossstage)
            {
                Invoke("Spawn", spawn_time);
                return;
            }
        }

        // 플레이어 주위 150m 이내에 몬스터수 50마리 이상일경우 반환 
        if (enemyCount > 50)
        {
            Invoke("Spawn", spawn_time);
            return;
        }
        enemyCount++;


        MonsterType monsterType = MonsterType.약한객체;
        int random = Random.RandomRange(0, 100);

        if (random < strong)
        {
            monsterType = MonsterType.강한객체;
        }
        if (monsterType != MonsterType.강한객체 && random < middle)
        {
            monsterType = MonsterType.중간객체;
        }

        ObjectKind monster = ObjectKind.monster_stage01_01;

        switch (currentStage)
        {
            case 1:
                switch (monsterType)
                {
                    case MonsterType.약한객체:
                        monster = ObjectKind.monster_stage01_01;
                        break;
                    case MonsterType.중간객체:
                        monster = ObjectKind.monster_stage01_02;
                        break;
                    case MonsterType.강한객체:
                        monster = ObjectKind.monster_stage01_03;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (monsterType)
                {
                    case MonsterType.약한객체:
                        monster = ObjectKind.monster_stage02_01;
                        break;
                    case MonsterType.중간객체:
                        monster = ObjectKind.monster_stage02_02;
                        break;
                    case MonsterType.강한객체:
                        monster = ObjectKind.monster_stage02_03;
                        break;
                }
                break;
            case 3:
                switch (monsterType)
                {
                    case MonsterType.약한객체:
                        monster = ObjectKind.monster_stage03_01;
                        break;
                    case MonsterType.중간객체:
                        monster = ObjectKind.monster_stage03_02;
                        break;
                    case MonsterType.강한객체:
                        monster = ObjectKind.monster_stage03_03;
                        break;
                }
                break;
            case 4:
                switch (monsterType)
                {
                    case MonsterType.약한객체:
                        monster = ObjectKind.monster_stage04_01;
                        break;
                    case MonsterType.중간객체:
                        monster = ObjectKind.monster_stage04_02;
                        break;
                    case MonsterType.강한객체:
                        monster = ObjectKind.monster_stage04_03;
                        break;
                }
                break;
            case 5:
                switch (monsterType)
                {
                    case MonsterType.약한객체:
                        monster = ObjectKind.monster_stage05_01;
                        break;
                    case MonsterType.중간객체:
                        monster = ObjectKind.monster_stage05_02;
                        break;
                    case MonsterType.강한객체:
                        monster = ObjectKind.monster_stage05_03;
                        break;
                }
                break;
            default:
                break;
        }

        GameObject enemy_obj = ObjectPoolingManager.instance.GetQueue(monster);
        int rand_pos_num = Random.RandomRange(0, enemy_spawn.childCount);
        enemy_obj.transform.position = enemy_spawn.GetChild(rand_pos_num).transform.position;


        if (!bossstage)
            Invoke("Spawn", spawn_time);
    }

    public void NextStage()
    {
        currentStage++;
        RaycastHit2D[] enemy_hit_list = Physics2D.CircleCastAll(player_transform.position, 100, Vector2.zero);
        enemyCount = 0;
        for (int i = 0; i < enemy_hit_list.Length; i++)
        {
            if (enemy_hit_list[i] && enemy_hit_list[i].transform.tag.Contains("Enemy"))
            {
                ObjectPoolingManager.instance.InsertQueue(enemy_hit_list[i].transform.gameObject, (ObjectKind)System.Enum.Parse(typeof(ObjectKind), enemy_hit_list[i].transform.GetComponent<MonsterController>().objectKind_string));
            }
        }
    }

}
