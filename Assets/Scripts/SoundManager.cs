using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;

    public AudioClip shotSound;
    public AudioClip hitSound;

    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    public void PlayAudio(AudioClip a)
    {
        audioSource.PlayOneShot(a);
    }
    
}
