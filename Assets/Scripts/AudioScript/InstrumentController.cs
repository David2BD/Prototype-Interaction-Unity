using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class InstrumentController : MonoBehaviour
{
    public AudioMixer mixer;
    
    // clips for each instruments
    public AudioClip[] BassClips; // 3
    public AudioClip[] PianoClips; // 4
    public AudioClip[] ViolinClips; // 3
    public AudioClip[] BeatClips; // 5
    public AudioClip[] DrumClips; // 3
    public AudioClip[] PauseClips; // 3

    // tracks for each instrument
    private AudioSource _bassClipsAudioSource; // 3
    private AudioSource _pianoClipsAudioSource; // 4
    private AudioSource _violinClipsAudioSource; // 3
    private AudioSource _beatClipAudioSources; // 5
    private AudioSource _drumClipsAudioSource; // 3
    private List<AudioSource> _pauseAudioSources = new List<AudioSource>(); // 3

    // groups for easier editing of values
    private AudioMixerGroup _percussionG;
    private AudioMixerGroup _pianoG;
    private AudioMixerGroup _violinG;
    private AudioMixerGroup _otherG;
    
    // for crossfade
    private float fadeTime = 5f;
    
    // levels
    private int level = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeMixerGroup();
        InitializeAudioSource();
        
        PlayPiano(true);
        PlayViolin(true);
    }

    private void InitializeMixerGroup()
    { 
        _percussionG = mixer.FindMatchingGroups("Master/Instrument/Percussion")[0]; 
        _pianoG = mixer.FindMatchingGroups("Master/Instrument/Piano")[0]; 
        _violinG = mixer.FindMatchingGroups("Master/Instrument/Violin")[0]; 
        _otherG = mixer.FindMatchingGroups("Master/Instrument/Other")[0]; 
    }

    private void InitializeAudioSource()
    {
        InitializeClipsAudio(BassClips, ref _bassClipsAudioSource, _percussionG);
        InitializeClipsAudio(PianoClips, ref _pianoClipsAudioSource, _pianoG);
        InitializeClipsAudio(ViolinClips, ref _violinClipsAudioSource, _violinG);
        InitializeClipsAudio(BeatClips, ref _beatClipAudioSources, _percussionG);
        InitializeClipsAudio(DrumClips, ref _drumClipsAudioSource, _percussionG);
        InitializePauseClips();
    }

    private void InitializeClipsAudio(AudioClip[] clips, ref AudioSource audio, AudioMixerGroup group)
    {
        if (audio == null)
        {
            audio = gameObject.AddComponent<AudioSource>();
        }
        audio.outputAudioMixerGroup = group;
        audio.playOnAwake = false;
        audio.loop = true;
        audio.clip = clips[0];

        audio.minDistance = 200;
        audio.maxDistance = 500;
        audio.spatialBlend = 1.0f;
    }

    private void InitializePauseClips()
    {
        foreach (AudioClip clip in PauseClips)
        {
            AudioSource ASource = gameObject.AddComponent<AudioSource>();
            ASource.outputAudioMixerGroup = _otherG;
            ASource.playOnAwake = false;
            ASource.loop = true;
            ASource.clip = clip;

            ASource.minDistance = 200;
            ASource.maxDistance = 500;
            ASource.spatialBlend = 1.0f;

            _pauseAudioSources.Add(ASource);
        }
    }

    public void PlayAll(bool aiming)
    {
        int musicLevel = GameManager.Instance.getMusicLevel();

        if (level != musicLevel) // change of level
        {
            level = musicLevel;
            PlayBeat(aiming, true);
            PlayDrum(aiming, true);
            PlayBass(aiming, true);
            PlayPiano(true);
            PlayViolin(true);
        }
        else // change of state
        {
            PlayBeat(aiming, false);
            PlayDrum(aiming, false);
            PlayBass(aiming, false);
        }
    }
    
    private void PlayBeat(bool aiming, bool levelChange)
    {
        // update clip if changed
        if (levelChange)
        {
            _beatClipAudioSources.Stop();
            _beatClipAudioSources.clip = BeatClips[level - 1];
        }
        
        if (aiming)
        {
            if (!_beatClipAudioSources.isPlaying)
            {
                // if first, initialized at start
                _beatClipAudioSources.Play();
            }
        }
        else
        {
            _beatClipAudioSources.Stop();
        }
        // adapt volume and pitch based on level;
    }
    
    private void PlayDrum(bool aiming, bool levelChange)
    {
        // update clip if changed
        if (level is > 1 and < 5 && levelChange)
        {
            _drumClipsAudioSource.Stop();
            _drumClipsAudioSource.clip = DrumClips[level - 2];
        }
        
        if (aiming && level is > 1 and < 5)
        {
            // adjust volume and pitch
            if (!_drumClipsAudioSource.isPlaying)
            {
                _drumClipsAudioSource.Play();
            }
        }
        else
        {
            _drumClipsAudioSource.Stop();
        }
    }

    private void PlayBass(bool aiming, bool levelChange)
    {
        if (levelChange && level > 2)
        {
            _bassClipsAudioSource.Stop();
            _bassClipsAudioSource.clip = BassClips[level-3];
        }
        
        // in move and only if danger
        if (!aiming && level > 2)
        {
            if (!_bassClipsAudioSource.isPlaying)
            {
                _bassClipsAudioSource.Play();
            }
        }
        else
        {
            _bassClipsAudioSource.Stop();
        }
    }

    private void PlayPiano(bool levelChange)
    {
        if (level < 5 && levelChange)
        {
            _pianoClipsAudioSource.Stop();
            _pianoClipsAudioSource.clip = PianoClips[level - 1];
            _pianoClipsAudioSource.Play();
        }
        else
        {
            _pianoClipsAudioSource.Stop();
        }
    }

    private void PlayViolin(bool levelChange)
    {
        if (level is > 2 and < 6 && levelChange)
        {
            _violinClipsAudioSource.Stop();
            _violinClipsAudioSource.clip = ViolinClips[level-3];
            _violinClipsAudioSource.Play();
        }
        else
        {
            _violinClipsAudioSource.Stop();
        }
    }
    
    public void PlayPause(bool start)
    {
        if (start)
        {
            StopAll();

            foreach (AudioSource audio in _pauseAudioSources)
            {
                audio.Play();
            }

            // specific timestamp
            _pauseAudioSources[1].time = 19f;
        }
        else
        {
            StopAudioSource(_pauseAudioSources);
        }

    }

    public void ChangeIntensity(float intensity)
    {
        _violinClipsAudioSource.volume = intensity; // Adjust volume based on intensity
    }

    public void StopAll()
    {
        _bassClipsAudioSource.Stop();;
        _pianoClipsAudioSource.Stop();
        _violinClipsAudioSource.Stop();
        _beatClipAudioSources.Stop();
        _drumClipsAudioSource.Stop();
        StopAudioSource(_pauseAudioSources);
    }
    
    private static void StopAudioSource(List<AudioSource> audioSources)
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }   
    }

    public void SwitchSegment()
    {
        
    }

    public void SetLevel(int l)
    {
        level = l;
    }

    IEnumerator CrossFade(AudioSource channel, float targetVolume)
    {
        float time = 0;
        float start = channel.volume;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            channel.volume = Mathf.Lerp(start, targetVolume, time / fadeTime);
            yield return null;
        }
        channel.volume = targetVolume;
        yield break;
    }
}
