using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InstrumentController : MonoBehaviour
{
    public AudioClip[] instruments;
    public AudioMixer mixer;
    
    private List<AudioSource> _audioSources = new List<AudioSource>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioClip clip in instruments)
        {
            AudioSource ASource = gameObject.AddComponent<AudioSource>();;
            ASource.clip = clip;
            ASource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Instrument")[0];
            ASource.minDistance = 200;
            ASource.maxDistance = 500;
            ASource.spatialBlend = 1.0f;
            _audioSources.Add(ASource);
        }
    }

    public void startAll()
    {
        foreach (AudioSource audio in _audioSources)
        {
            audio.Play();
        }
    }

    public void changeIntensity(float intensity)
    {
        foreach (AudioSource audio in _audioSources)
        {
            audio.volume = intensity; // Adjust volume based on intensity
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
