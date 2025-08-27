using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // bien luu tru audio source
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    //tao bien luu tru auto clip
    public AudioClip musicClip;
    public AudioClip attackClip;
    public AudioClip dieClip;
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip);
    }
}
