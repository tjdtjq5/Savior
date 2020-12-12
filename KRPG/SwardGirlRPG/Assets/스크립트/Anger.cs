using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Anger : MonoBehaviour
{
    public static Anger instance;
    
    private void Awake()
    {
        instance = this;
    }
    public float minus;

    public Image foreGage;

    IEnumerator angerCoroutine;

    public void AngerGage(float plus)
    {
        plus = plus / 100;
        foreGage.fillAmount += plus;
    }

    public void OnClickAnger()
    {
        if (foreGage.fillAmount < 1) // 게이지 덜참
        {
            return;
        }
        angerCoroutine = AngerCoroutine();
        StartCoroutine(angerCoroutine);
    }

    IEnumerator AngerCoroutine()
    {
        CharacterController.instance.AngerModeStart();
        WaitForSeconds waitTime = new WaitForSeconds(0.02f);
        while (foreGage.fillAmount > 0)
        {
            foreGage.fillAmount -= minus;
            yield return waitTime;
        }
        CharacterController.instance.AngerModeStop();
    }
}
