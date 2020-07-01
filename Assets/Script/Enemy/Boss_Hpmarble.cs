using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hpmarble : MonoBehaviour
{
    [Header("구슬 스폰 위치")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;

    [Header("구슬 생성 최대 갯수")]
    public int max_num;

    GameObject hp_marble;

    /*private void Start()
    {
        hp_marble_Spawn();
    }

    void hp_marble_Spawn()
    {
        float marble_num = Random.value;
        ObjectKind marble_type = ObjectKind.hp_marble_large;
        if (marble_num < 0.15f)
            marble_type = ObjectKind.hp_marble_large;
        else if (marble_num < 0.5f)
            marble_type = ObjectKind.hp_marble_middle;
        else
            marble_type = ObjectKind.hp_marble_small;

        hp_marble = ObjectPoolingManager.instance.GetQueue(marble_type);
        hp_marble.GetComponent<Item>().player = player;
        float X = Random.Range(player.position.x - range, player.position.x + range);
        float Y = Random.Range(player.position.y - range, player.position.y + range);
        hp_marble.transform.position = new Vector3(X, Y, 0);
        Invoke("hp_marble_Spawn", spawn_time);
    }*/
}
