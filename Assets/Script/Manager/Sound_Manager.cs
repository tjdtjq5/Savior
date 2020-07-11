using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    [Header("Bgm")] public AudioSource bgm_AudioSource; public bool bgm_isStart;
    [Header("기타")] public AudioSource _AudioSource;
    [Header("스테이지별 사운드")]
    public AudioClip[] stageSound;

    public void Play(string audio_name)
    {
        _AudioSource.clip = GameManager.instance.audioManager.GetAudioClip(audio_name);
        GameManager.instance.audioManager.EnvironVolume_Play(_AudioSource);
    }

    public void BgmPlay()
    {
        GameManager.instance.audioManager.Bgm_Play(bgm_AudioSource);
    }

    private void Start()
    {
        if(bgm_isStart)
            BgmPlay();
    }

    private void Update()
    {
        bgm_AudioSource.volume = GameManager.instance.audioManager.GetBgmVolume();
    }

    public void Stage01()
    {
        bgm_AudioSource.clip = stageSound[0];
        BgmPlay();
    }

    public void Stage02()
    {
        bgm_AudioSource.clip = stageSound[1];
        BgmPlay();
    }

    public void Stage03()
    {
        bgm_AudioSource.clip = stageSound[2];
        BgmPlay();
    }

    public void Stage04()
    {
        bgm_AudioSource.clip = stageSound[3];
        BgmPlay();
    }

    public void Stage05()
    {
        bgm_AudioSource.clip = stageSound[4];
        BgmPlay();
    }

    public void Boss()
    {
        bgm_AudioSource.clip = stageSound[5];
        BgmPlay();

    }
}
