using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void TargetSetting(Transform target) { }
    public virtual void Hit(string damage) { }
    public virtual void Destroy() { }
}
