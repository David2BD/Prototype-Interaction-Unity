using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts;
using UnityEngine;

public class playerSFX : MonoBehaviour
{
    public AudioClip[] tracks; // Reference to the AudioSource component
    // 0 - shoot, 1 - walk, 2 - jump

    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = tracks[0];
        
    }

    void Update()
    {
        
    }

    public void stopSound()
    {
        audioSource.Stop();
    }
    public void playMovement()
    {
        audioSource.clip = tracks[0];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }  
    }

    public void playJump()
    {
        audioSource.clip = tracks[2];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        //play jump sound
    }

    public void playShoot()
    {
        audioSource.clip = tracks[1];
        audioSource.Play();
    }

}
