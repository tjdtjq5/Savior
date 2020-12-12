using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public float y;
    public void Open(string damage)
    {
        this.transform.GetChild(0).GetComponent<Text>().text = damage;
        this.transform.DOMoveY(this.transform.position.y + y, 0.4f).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }
}
