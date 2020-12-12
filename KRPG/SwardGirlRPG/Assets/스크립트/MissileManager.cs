using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileManager : MonoBehaviour
{
    public float speed;
    WaitForSeconds waitTime;
    public GameObject fireBall;

    public void Missile(string damage, Transform target)
    {
        StartCoroutine(MissileCoroutine(damage, target));
    }

    IEnumerator MissileCoroutine(string damage, Transform target)
    {
        GameObject missile = Instantiate(fireBall, this.transform.position, Quaternion.identity);
        missile.transform.position = this.transform.position;
        waitTime = new WaitForSeconds(0.02f);
        while (true)
        {
            if (target == null)
            {
                Destroy(missile);
                yield break;
            }
            missile.transform.position = Vector2.MoveTowards(missile.transform.position, target.position, speed);
            yield return waitTime;

            if (target == null)
            {
                Destroy(missile);
                yield break;
            }

            if (Vector2.Distance(missile.transform.position, target.position) < 0.3f)
            {
                Destroy(missile);

                target.GetComponent<Enemy>().Hit(damage);

                yield break;
            }
        }
    }
}
