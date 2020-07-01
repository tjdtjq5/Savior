using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Exp_Up : MonoBehaviour
{
    private int exp_small_amount;
    private int exp_middle_amount;
    private int exp_large_amount;

    string exp_marble_num;

    private void Start()
    {
        exp_small_amount = 100;
        exp_middle_amount = 200;
        exp_large_amount = 400;
        exp_marble_num = gameObject.GetComponent<Item>().marble_num;    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            switch (exp_marble_num)
            {
                case "exp_marble_small":
                    collision.GetComponent<PlayerController>().Exp_Up(exp_small_amount);
                    ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.exp_marble_small);
                    break;
                case "exp_marble_middle":
                    collision.GetComponent<PlayerController>().Exp_Up(exp_middle_amount);
                    ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.exp_marble_middle);
                    break;
                case "exp_marble_large":
                    collision.GetComponent<PlayerController>().Exp_Up(exp_large_amount);
                    ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.exp_marble_large);
                    break;
                default:
                    break;
            }
        }
    }
}
