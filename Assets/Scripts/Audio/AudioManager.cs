using System.Collections;
using System.Collections.Generic;
// File: AudioManager.cs
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip coinSound;
    public AudioClip bubbleSound;
    public AudioClip backgroundMusic;
    public AudioClip damageSound;
    public AudioClip divingSound;
    public AudioClip drowningSound;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            Debug.Log("Playing SFX: " + clip.name);
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to PlaySFX");
        }
    }

    public void PlayBubble()
    {
        Debug.Log("Playing bubble sound");
        PlaySFX(bubbleSound);
    }

    public void PlayCoin()
    {
        Debug.Log("Playing coin sound");
        PlaySFX(coinSound);
    }

    public void PlayDamage()
    {
        Debug.Log("Playing damage sound");
        PlaySFX(damageSound);
    }

    public void PlayDiving()
    {
        Debug.Log("Playing diving sound");
        PlaySFX(divingSound);
    }

    public void PlayDrowning()
    {
        Debug.Log("Playing drowning sound");
        PlaySFX(drowningSound);
    }


    public void PlayMusic(AudioClip music)
    {
        if (music != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
