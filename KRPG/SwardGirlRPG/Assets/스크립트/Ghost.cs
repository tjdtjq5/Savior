using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public void CatchGhost()
    {
        Anger.instance.AngerGage(10);
        Destroy(this.gameObject);
    }
}
