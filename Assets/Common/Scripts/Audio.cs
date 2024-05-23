using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Inst { get; private set; }
    void Awake() => Inst = this;

    public AudioSource audioSource;
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

    void Start()
    {
        audioSource.mute = false;
        audioSource.volume = CostManager.volume;
        audioSource.loop = true;
    }

    public void playBurnout()
    {
        audioSource.PlayOneShot(DayBurnout);
    }

    public void playBurst()
    {
        audioSource.PlayOneShot(DayBurst);
    }

    public void playDraw()
    {
        audioSource.PlayOneShot(DayDraw);
    }

    public void playSlide()
    {
        audioSource.PlayOneShot(DayDraw);
    }

    public void playCardOnTarget()
    {
        audioSource.PlayOneShot(NightCardOnTarget);
    }

    public void playSceneChange()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(SceneChange);
    }

    public void playSettings()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Settings);
    }
    public void playPack()
    {
        audioSource.PlayOneShot(Settings);
    }

    public void playDayBackground()
    {
        audioSource.clip = DayBackground;
        audioSource.Play();
    }

    public void playNightBackground()
    {
        audioSource.clip = NightBackground;
        audioSource.Play();
    }

    public void playBurnOutCut()
    {
        audioSource.clip = BurnOutCut;
        audioSource.Play();
    }

    public void playFireEnding()
    {
        audioSource.clip = FiredEnding;
        audioSource.Play();
    }

    public void playNormalEnding()
    {
        audioSource.clip = NormalEnding;
        audioSource.Play();
    }

    public void playNormalEndingCut()
    {
        audioSource.clip = NormalEndingCut;
        audioSource.Play();
    }

    public void playTitleBackground()
    {
        audioSource.clip = TitleBackground;
        audioSource.Play();
    }

    public void playEndingStamp()
    {
        audioSource.PlayOneShot(EndingStamp);
    }

    public void playEndingWrite()
    {
        audioSource.PlayOneShot(EndingWrite);
    }
}
