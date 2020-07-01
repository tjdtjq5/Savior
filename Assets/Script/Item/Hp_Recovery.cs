using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_Recovery : MonoBehaviour
{
    [Range(10, 100)] public float recovery_amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            collision.GetComponent<PlayerController>().Hp_Recovery((int)(collision.GetComponent<PlayerController>().MaxHp() * recovery_amount));
            if (this.gameObject.name.Contains("large"))
            {
                ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.hp_marble_large);
            }
            if (this.gameObject.name.Contains("middle"))
            {
                ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.hp_marble_middle);
            }
            if (this.gameObject.name.Contains("small"))
            {
                ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.hp_marble_small);
            }
        }
    }
}
