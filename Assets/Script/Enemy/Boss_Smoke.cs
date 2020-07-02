using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Smoke : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void Insert()
    {
        ObjectPoolingManager.instance.InsertQueue(this.gameObject, ObjectKind.skillrange);
    }
}
