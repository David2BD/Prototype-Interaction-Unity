using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class playerSFX : MonoBehaviour
{
    public AudioClip[] tracks; // Reference to the AudioSource component
    // 0 - walk, 1 - shoot, 2 - jump, 3 - arming gun

    public AudioClip[] attacks;
    public AudioClip[] lowHealth;
    public AudioClip[] hit;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource.minDistance = 100;
        audioSource.maxDistance = 300;
        audioSource.spatialBlend = 1.0f;
        
        audioSource.clip = tracks[0];
        
    }

    void Update()
    {
        
    }

    public void stopSound()
    {
        audioSource.Stop();
    }

    public void playVoiceAttack()
    {
        // possible to have no voiceline
        int randomIndex = Random.Range(0, (int)(attacks.Length * 1.3));
        if (attacks.Length > 0 && randomIndex < attacks.Length)
        {
            audioSource.PlayOneShot(attacks[randomIndex]);
        }
    }
    
    public void playVoiceHit()
    {
        // possible to have no voiceline
        int randomIndex = Random.Range(0, (int)(hit.Length * 1.3));
        if (hit.Length > 0 && randomIndex < hit.Length)
        {
            StartCoroutine(delayedAction(hit[randomIndex]));
        }
    }
    
    public void playVoiceLowHealth()
    {
        // possible to have no voiceline
        int randomIndex = Random.Range(0, (int)(lowHealth.Length * 1.3));
        if (lowHealth.Length > 0 && randomIndex < lowHealth.Length)
        {
            StartCoroutine(delayedAction(lowHealth[randomIndex]));
        }
    }

    IEnumerator delayedAction(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length + 1f);
    }
    
    public void playMovement()
    {
        audioSource.clip = tracks[0];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }  
    }

    public void playArming()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(tracks[3]);
        }
    }

    public void playJump()
    {
        audioSource.clip = tracks[2];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void playShoot()
    {
        audioSource.PlayOneShot(tracks[1]);
    }

}
