using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject damagePrepab;
    Transform target;
    IEnumerator moveCoroutine;

    public int hp;
    public float moveSpeed;


    public override void TargetSetting(Transform target)
    {
        this.target = target;
        moveCoroutine = MoveCoroutine();
        StartCoroutine(moveCoroutine);
    }

    public override void Hit(string damage)
    {
        base.Hit(damage);
        GameObject tempDamagePrepab = Instantiate(damagePrepab, this.transform.position, Quaternion.identity);
        tempDamagePrepab.GetComponent<Damage>().Open(damage);

        hp -= int.Parse(damage);
        if (hp <= 0) Destroy();
    }
    public override void Destroy()
    {
        base.Destroy();
        if(moveCoroutine != null) StopCoroutine(moveCoroutine);
        Destroy(this.gameObject);
    }

    IEnumerator MoveCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.02f);
        while (true)
        {
            if (target == null) yield break;
            
            if (target.position.x < this.transform.position.x)
            {
                this.transform.Translate(Vector2.left * moveSpeed);
            }
            else
            {
                this.transform.Translate(Vector2.right * moveSpeed);
            }
            yield return waitTime;
        }
    }
}
