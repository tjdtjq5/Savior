using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    [Header("아이템")]
    public Transform instantiate_pos;  
    public GameObject ob_prefab = null;
    public Queue<GameObject> ob_queue = new Queue<GameObject>();
    public GameObject smoke_prefab = null;
    public Queue<GameObject> smoke_queue = new Queue<GameObject>();
    public GameObject nomal_damage_prefab = null;
    public Queue<GameObject> nomal_damage_queue = new Queue<GameObject>();
    public GameObject critical_damage_prefab = null;
    public Queue<GameObject> critical_damage_queue = new Queue<GameObject>();
    public GameObject hp_marble_large_prefab = null;
    public Queue<GameObject> hp_marble_large_queue = new Queue<GameObject>();
    public GameObject hp_marble_middle_prefab = null;
    public Queue<GameObject> hp_marble_middle_queue = new Queue<GameObject>();
    public GameObject hp_marble_small_prefab = null;
    public Queue<GameObject> hp_marble_small_queue = new Queue<GameObject>();

    [Header("몬스터")]
    public GameObject monster_stage01_01_pripab = null;
    public Queue<GameObject> monster_stage01_01_queue = new Queue<GameObject>();
    public GameObject monster_stage01_02_pripab = null;
    public Queue<GameObject> monster_stage01_02_queue = new Queue<GameObject>();
    public GameObject monster_stage01_03_pripab = null;
    public Queue<GameObject> monster_stage01_03_queue = new Queue<GameObject>();
    public GameObject monster_stage02_01_pripab = null;
    public Queue<GameObject> monster_stage02_01_queue = new Queue<GameObject>();
    public GameObject monster_stage02_02_pripab = null;
    public Queue<GameObject> monster_stage02_02_queue = new Queue<GameObject>();
    public GameObject monster_stage02_03_pripab = null;
    public Queue<GameObject> monster_stage02_03_queue = new Queue<GameObject>();
    public GameObject monster_stage03_01_pripab = null;
    public Queue<GameObject> monster_stage03_01_queue = new Queue<GameObject>();
    public GameObject monster_stage03_02_pripab = null;
    public Queue<GameObject> monster_stage03_02_queue = new Queue<GameObject>();
    public GameObject monster_stage03_03_pripab = null;
    public Queue<GameObject> monster_stage03_03_queue = new Queue<GameObject>();
    public GameObject monster_stage04_01_pripab = null;
    public Queue<GameObject> monster_stage04_01_queue = new Queue<GameObject>();
    public GameObject monster_stage04_02_pripab = null;
    public Queue<GameObject> monster_stage04_02_queue = new Queue<GameObject>();
    public GameObject monster_stage04_03_pripab = null;
    public Queue<GameObject> monster_stage04_03_queue = new Queue<GameObject>();
    public GameObject monster_stage05_01_pripab = null;
    public Queue<GameObject> monster_stage05_01_queue = new Queue<GameObject>();
    public GameObject monster_stage05_02_pripab = null;
    public Queue<GameObject> monster_stage05_02_queue = new Queue<GameObject>();
    public GameObject monster_stage05_03_pripab = null;
    public Queue<GameObject> monster_stage05_03_queue = new Queue<GameObject>();

    [Header("경험치구슬")]
    public GameObject exp_marble_small_prefab = null;
    public Queue<GameObject> exp_marble_small_queue = new Queue<GameObject>();
    public GameObject exp_marble_middle_prefab = null;
    public Queue<GameObject> exp_marble_middle_queue = new Queue<GameObject>();
    public GameObject exp_marble_large_prefab = null;
    public Queue<GameObject> exp_marble_large_queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        // ob  
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(ob_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            ob_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // smoke
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(smoke_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            smoke_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // nomal_damage
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(nomal_damage_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            nomal_damage_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // exp_marble_small
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(exp_marble_small_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            exp_marble_small_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // exp_marble_middle
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(exp_marble_middle_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            exp_marble_middle_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // exp_marble_large
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(exp_marble_large_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            exp_marble_large_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // hp_marble_large
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(hp_marble_large_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            hp_marble_large_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // hp_marble_middle
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(hp_marble_middle_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            hp_marble_middle_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // hp_marble_small
        for (int i = 0; i < 100; i++)
        {
            GameObject t_object = Instantiate(hp_marble_small_prefab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            hp_marble_small_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        // monster_stage01_01
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage01_01_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage01_01_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage01_01_pripab.SetActive(false);
        // monster_stage01_02
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage01_02_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage01_02_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage01_02_pripab.SetActive(false);
        // monster_stage01_03
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage01_03_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage01_03_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage01_03_pripab.SetActive(false);
        // monster_stage02_01
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage02_01_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage02_01_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage02_01_pripab.SetActive(false);
        // monster_stage02_02
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage02_02_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage02_02_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage02_02_pripab.SetActive(false);
        // monster_stage02_03
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage02_03_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage02_03_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage02_03_pripab.SetActive(false);
        // monster_stage03_01
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage03_01_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage03_01_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage03_01_pripab.SetActive(false);
        // monster_stage03_02
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage03_02_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage03_02_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage03_02_pripab.SetActive(false);
        // monster_stage03_03
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage03_03_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage03_03_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage03_03_pripab.SetActive(false);
        // monster_stage04_01
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage04_01_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage04_01_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage04_01_pripab.SetActive(false);
        // monster_stage04_02
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage04_02_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage04_02_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage04_02_pripab.SetActive(false);
        // monster_stage04_03
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage04_03_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage04_03_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage04_03_pripab.SetActive(false);
        // monster_stage05_01
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage05_01_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage05_01_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage05_01_pripab.SetActive(false);
        // monster_stage05_02
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage05_02_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage05_02_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage05_02_pripab.SetActive(false);
        // monster_stage05_03
        for (int i = 0; i < 150; i++)
        {
            GameObject t_object = Instantiate(monster_stage05_03_pripab, new Vector2(3000, 3000), Quaternion.identity, instantiate_pos);
            monster_stage05_03_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        monster_stage05_03_pripab.SetActive(false);
    }

    // 사용한 오브젝트를 다시 큐에 집어 넣는 함수
    public void InsertQueue(GameObject p_object, ObjectKind obj)
    {

        if (obj == ObjectKind.ob)
            ob_queue.Enqueue(p_object);

        if (obj == ObjectKind.smoke)
            smoke_queue.Enqueue(p_object);

        if (obj == ObjectKind.nomal_damage)
            nomal_damage_queue.Enqueue(p_object);

        if (obj == ObjectKind.critical_damage)
            critical_damage_queue.Enqueue(p_object);

        if (obj == ObjectKind.exp_marble_small)
            exp_marble_small_queue.Enqueue(p_object);

        if (obj == ObjectKind.exp_marble_middle)
            exp_marble_middle_queue.Enqueue(p_object);

        if (obj == ObjectKind.exp_marble_large)
            exp_marble_large_queue.Enqueue(p_object);

        if (obj == ObjectKind.hp_marble_large)
            hp_marble_large_queue.Enqueue(p_object);

        if (obj == ObjectKind.hp_marble_middle)
            hp_marble_middle_queue.Enqueue(p_object);

        if (obj == ObjectKind.hp_marble_small)
            hp_marble_small_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage01_01)
            monster_stage01_01_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage01_02)
            monster_stage01_02_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage01_03)
            monster_stage01_03_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage02_01)
            monster_stage02_01_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage02_02)
            monster_stage02_02_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage02_03)
            monster_stage02_03_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage03_01)
            monster_stage03_01_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage03_02)
            monster_stage03_02_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage03_03)
            monster_stage03_03_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage04_01)
            monster_stage04_01_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage04_02)
            monster_stage04_02_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage04_03)
            monster_stage04_03_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage05_01)
            monster_stage05_01_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage05_02)
            monster_stage05_02_queue.Enqueue(p_object);

        if (obj == ObjectKind.monster_stage05_03)
            monster_stage05_03_queue.Enqueue(p_object);

        p_object.SetActive(false);
    }

    // 사용하기위해 큐에서 오브젝트를 꺼내오는 함수
    public GameObject GetQueue(ObjectKind obj)
    {
        GameObject t_object = null;

        if (obj == ObjectKind.ob)
            t_object = ob_queue.Dequeue();

        if (obj == ObjectKind.smoke)
            t_object = smoke_queue.Dequeue();

        if (obj == ObjectKind.nomal_damage)
            t_object = nomal_damage_queue.Dequeue();

        if (obj == ObjectKind.critical_damage)
            t_object = critical_damage_queue.Dequeue();

        if (obj == ObjectKind.exp_marble_small)
            t_object = exp_marble_small_queue.Dequeue();

        if (obj == ObjectKind.exp_marble_middle)
            t_object = exp_marble_middle_queue.Dequeue();

        if (obj == ObjectKind.exp_marble_large)
            t_object = exp_marble_large_queue.Dequeue();

        if (obj == ObjectKind.hp_marble_large)
            t_object = hp_marble_large_queue.Dequeue();

        if (obj == ObjectKind.hp_marble_middle)
            t_object = hp_marble_middle_queue.Dequeue();

        if (obj == ObjectKind.hp_marble_small)
            t_object = hp_marble_small_queue.Dequeue();

        if (obj == ObjectKind.monster_stage01_01)
            t_object = monster_stage01_01_queue.Dequeue();

        if (obj == ObjectKind.monster_stage01_02)
            t_object = monster_stage01_02_queue.Dequeue();

        if (obj == ObjectKind.monster_stage01_03)
            t_object = monster_stage01_03_queue.Dequeue();

        if (obj == ObjectKind.monster_stage02_01)
            t_object = monster_stage02_01_queue.Dequeue();

        if (obj == ObjectKind.monster_stage02_02)
            t_object = monster_stage02_02_queue.Dequeue();

        if (obj == ObjectKind.monster_stage02_03)
            t_object = monster_stage02_03_queue.Dequeue();

        if (obj == ObjectKind.monster_stage03_01)
            t_object = monster_stage03_01_queue.Dequeue();

        if (obj == ObjectKind.monster_stage03_02)
            t_object = monster_stage03_02_queue.Dequeue();

        if (obj == ObjectKind.monster_stage03_03)
            t_object = monster_stage03_03_queue.Dequeue();

        if (obj == ObjectKind.monster_stage04_01)
            t_object = monster_stage04_01_queue.Dequeue();

        if (obj == ObjectKind.monster_stage04_02)
            t_object = monster_stage04_02_queue.Dequeue();

        if (obj == ObjectKind.monster_stage04_03)
            t_object = monster_stage04_03_queue.Dequeue();

        if (obj == ObjectKind.monster_stage05_01)
            t_object = monster_stage05_01_queue.Dequeue();

        if (obj == ObjectKind.monster_stage05_02)
            t_object = monster_stage05_02_queue.Dequeue();

        if (obj == ObjectKind.monster_stage05_03)
            t_object = monster_stage05_03_queue.Dequeue();

        t_object.SetActive(true);
        return t_object;
    }
}

public enum ObjectKind
{
    ob,
    smoke,
    nomal_damage,
    critical_damage,
    exp_marble_small,
    exp_marble_middle,
    exp_marble_large,
    hp_marble_large,
    hp_marble_middle,
    hp_marble_small,
    obstacle,
    monster_stage01_01,
    monster_stage01_02,
    monster_stage01_03,
    monster_stage02_01,
    monster_stage02_02,
    monster_stage02_03,
    monster_stage03_01,
    monster_stage03_02,
    monster_stage03_03,
    monster_stage04_01,
    monster_stage04_02,
    monster_stage04_03,
    monster_stage05_01,
    monster_stage05_02,
    monster_stage05_03

}
