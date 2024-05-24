using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Inst { get; private set; }
    void Awake() => Inst = this;
    public AudioSource backgroundAudioSource;
    public AudioSource popAudioSource;
    public AudioClip DayBurnout;
    public AudioClip DayBurst;
    public AudioClip DayDraw;
    public AudioClip NightCardOnTarget;
    public AudioClip SceneChange;
    public AudioClip Settings;
    public AudioClip DayBackground;
    public AudioClip NightBackground;
    public AudioClip BurnOutCut;
    public AudioClip FiredEnding;
    public AudioClip NormalEnding;
    public AudioClip NormalEndingCut;
    public AudioClip TitleBackground;
    public AudioClip EndingStamp;
    public AudioClip EndingWrite;
    public AudioClip FireDoorOpen;
    public AudioClip FireDoorClose;
    public float backgroundVolume = 0.5f;
    public float popVolume = 0.5f;

    void Start()
    {
        backgroundAudioSource.mute = false;
        backgroundAudioSource.volume = backgroundVolume;
        backgroundAudioSource.loop = true;
        popAudioSource.mute = false;
        popAudioSource.volume = popVolume;
        popAudioSource.loop = false;
    }

    void Update()
    {
        backgroundAudioSource.volume = backgroundVolume;
        popAudioSource.volume = popVolume;
    }

    public void playBurnout()
    {
        popAudioSource.PlayOneShot(DayBurnout);
    }

    public void playBurst()
    {
        popAudioSource.PlayOneShot(DayBurst);
    }

    public void playDraw()
    {
        popAudioSource.PlayOneShot(DayDraw);
    }

    public void playSlide()
    {
        popAudioSource.PlayOneShot(DayDraw);
    }

    public void playCardOnTarget()
    {
        popAudioSource.PlayOneShot(NightCardOnTarget);
    }

    public void playSceneChange()
    {
        popAudioSource.PlayOneShot(SceneChange);
    }

    public void playSettings()
    {
        popAudioSource.PlayOneShot(Settings);
    }

    public void playPack()
    {
        popAudioSource.PlayOneShot(Settings);
    }

    public void playDayBackground()
    {
        backgroundAudioSource.clip = DayBackground;
        backgroundAudioSource.Play();
    }

    public void playNightBackground()
    {
        backgroundAudioSource.clip = NightBackground;
        backgroundAudioSource.Play();
    }

    public void playBurnOutCut()
    {
        backgroundAudioSource.clip = BurnOutCut;
        backgroundAudioSource.Play();
    }

    public void playFireEnding()
    {
        backgroundAudioSource.clip = FiredEnding;
        backgroundAudioSource.Play();
    }

    public void playNormalEnding()
    {
        backgroundAudioSource.clip = NormalEnding;
        backgroundAudioSource.Play();
    }

    public void playNormalEndingCut()
    {
        backgroundAudioSource.clip = NormalEndingCut;
        backgroundAudioSource.Play();
    }

    public void playTitleBackground()
    {
        backgroundAudioSource.clip = TitleBackground;
        backgroundAudioSource.Play();
    }

    public void playEndingStamp()
    {
        popAudioSource.PlayOneShot(EndingStamp);
    }

    public void playEndingWrite()
    {
        popAudioSource.PlayOneShot(EndingWrite);
    }
}
