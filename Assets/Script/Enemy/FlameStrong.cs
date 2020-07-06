﻿using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrong : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    private void Start()
    {
        skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        string[] name = { "1_1", "1_2", "1_3" };
        skeletonAnimation.skeleton.SetSkin("1");
        StartCoroutine(SetAniCoroutine(name));
    }
    IEnumerator SetAniCoroutine(string[] name)
    {
        for(int i = 0; i<name.Length; i++)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, name[i], false);
            yield return new WaitForSeconds(0f);
        }
    }
}
