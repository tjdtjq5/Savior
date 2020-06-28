using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skillinfo : MonoBehaviour
{
    public int skillDamage;

    [HideInInspector] public float randx;
    [HideInInspector] public float randy;
    [HideInInspector] public Transform target;

    [HideInInspector] public bool randomshot;
    [HideInInspector] public bool breathstrong;
    [HideInInspector] public bool phase1;
    [HideInInspector] public bool breathball;
    [HideInInspector] public bool direction;//브레스탄과 플레이어의 x방향구분

    Vector3 min;
    Vector3 max;
    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
        if (phase1 && randomshot)
            Boss_Skill.instance.RandomShot01(this.transform);
        else if (!phase1 && randomshot)
            Boss_Skill.instance.RandomShot02(this.transform);
        randomshot = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (TimeManager.instance.GetTime())
                return;

            collision.GetComponent<PlayerController>().Hit(skillDamage,this.gameObject);
            
        }
    }

    private void Update()
    {
        if (TimeManager.instance.GetTime())
            return;

        float randx = Random.Range(min.x, max.x);
        float randy = Random.Range(min.y, max.y);

        if (randomshot)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(randx,randy), 0.3f);
        }
        if (breathstrong)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, target.position, 3.0f);
        }
        if (breathball)
        {
            if (direction)
                this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x + 40, this.transform.position.y), 7.2f*Time.deltaTime);
            else
                this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x - 40, this.transform.position.y), 7.2f*Time.deltaTime);
        }
    }
}
