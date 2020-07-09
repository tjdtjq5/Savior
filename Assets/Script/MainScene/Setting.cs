using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider entireVolume;
    public Slider bgmVolume;
    public Slider environVolume;

    private void OnEnable()
    {
        entireVolume.value = GameManager.instance.audioManager.entireVolume;
        bgmVolume.value = GameManager.instance.audioManager.BgmVolume;
        environVolume.value = GameManager.instance.audioManager.environVolume;
    }

    private void OnDisable()
    {
        GameManager.instance.audioManager.entireVolume = entireVolume.value;
        GameManager.instance.audioManager.BgmVolume = bgmVolume.value;
        GameManager.instance.audioManager.environVolume = environVolume.value;
    }
}
