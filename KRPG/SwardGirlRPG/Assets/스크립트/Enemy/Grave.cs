using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : Enemy
{
    public GameObject damagePrepab;
    public override void Hit(string damage)
    {
        base.Hit(damage);
        GameObject tempDamagePrepab = Instantiate(damagePrepab, this.transform.position, Quaternion.identity);
        tempDamagePrepab.GetComponent<Damage>().Open(damage);
    }
    public override void Destroy()
    {
        base.Destroy();
        Destroy(this.gameObject);
    }
}
