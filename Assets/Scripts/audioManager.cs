using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    public AudioSource audioSource;

    public AudioClip jumpAudio;
    public AudioClip collectAudio;
    public AudioClip winAudio;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    public void CollectAudio()
    {
        audioSource.clip = collectAudio;
        audioSource.Play();
    }
    public void WinAudio()
    {
        audioSource.clip = winAudio;
        audioSource.Play();
    }

}
