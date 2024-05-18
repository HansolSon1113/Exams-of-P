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
    public AudioClip DaySlide;
    public AudioClip NightCardOnTarget;
    public AudioClip SceneChange;
    public AudioClip Settings;


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
        audioSource.PlayOneShot(DaySlide);
    }

    public void playCardOnTarget()
    {
        audioSource.PlayOneShot(NightCardOnTarget);
    }

    public void playSceneChange()
    {
        audioSource.PlayOneShot(SceneChange);
    }

    public void playSettings()
    {
        audioSource.PlayOneShot(Settings);
    }
    public void playPack()
    {
        audioSource.PlayOneShot(Settings);
    }
}
