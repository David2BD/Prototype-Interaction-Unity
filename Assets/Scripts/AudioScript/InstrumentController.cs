using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class InstrumentController : MonoBehaviour
{
    public AudioMixer mixer;
    
    // clips for each instruments
    public AudioClip[] PianoClips; // 4
    public AudioClip[] ViolinClips; // 3
    public AudioClip[] BeatClips; // 5
    public AudioClip[] DrumClips; // 3
    public AudioClip[] PauseClips; // 3

    // tracks for each instrument
    private AudioSource _pianoClipsAudioSource; // 4
    private AudioSource _violinClipsAudioSource; // 3
    private AudioSource _beatClipAudioSources; // 5
    private AudioSource _drumClipsAudioSource; // 3
    private List<AudioSource> _pauseAudioSources = new List<AudioSource>(); // 3

    // groups for easier editing of values
    private AudioMixerGroup _beatG;
    private AudioMixerGroup _drumG;
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
        ChangeVolume();
        ChangePitch();
        
        PlayPiano(true);
        PlayViolin(true);
    }

    private void InitializeMixerGroup()
    { 
        _beatG = mixer.FindMatchingGroups("Master/Instrument/Beat")[0]; 
        _pianoG = mixer.FindMatchingGroups("Master/Instrument/Piano")[0]; 
        _violinG = mixer.FindMatchingGroups("Master/Instrument/Violin")[0]; 
        _otherG = mixer.FindMatchingGroups("Master/Instrument/Other")[0]; 
        _drumG = mixer.FindMatchingGroups("Master/Instrument/Drum")[0];
    }

    private void InitializeAudioSource()
    {
        InitializeClipsAudio(PianoClips, ref _pianoClipsAudioSource, _pianoG);
        InitializeClipsAudio(ViolinClips, ref _violinClipsAudioSource, _violinG);
        InitializeClipsAudio(BeatClips, ref _beatClipAudioSources, _beatG);
        InitializeClipsAudio(DrumClips, ref _drumClipsAudioSource, _drumG);
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
        
        // set volumes for all instruments mixerGroup based on volume
        

        if (level != musicLevel) // change of level
        {
            level = musicLevel;
            ChangeVolume();
            ChangePitch();
            PlayBeat(aiming, true);
            PlayDrum(aiming, true);
            PlayPiano(true);
            PlayViolin(true);
        }
        else // change of state
        {
            PlayBeat(aiming, false);
            PlayDrum(aiming, false);
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

    private void PlayPiano(bool levelChange)
    {
        if (level < 4 && levelChange)
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
            PlayPiano(true);
            PlayViolin(true);
        }

    }

    public void StopAll()
    {
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

    private void ChangeVolume()
    {
        // start at 1, decrease by 0.3 -- 0 if level > 3
        float pianoVolume = (level < 3) ? 1f - (0.3f * (level - 1)) : 0f;
        
        // start at 0, increase by 0.2 -- 0 if level < 3
        float violinVolume = (level > 2) ? (0.2f * level) : 0f;
        
        // increase by 0.2 every level
        float beatVolume = 0.15f * level;
        
        // increase by 0.3 -- 0 if level = 1 or level = 5
        float drumVolume = 0f;
        if (level != 1 && level != 5)
        {
            drumVolume = 0.2f * (level - 1);
        }

        ChangeVolumeGroup(_pianoG.name, pianoVolume);
        ChangeVolumeGroup(_violinG.name, violinVolume);
        ChangeVolumeGroup(_beatG.name, beatVolume);
        ChangeVolumeGroup(_drumG.name, drumVolume);
    }
    
    // Change volume of the specified Mixer Group
    private void ChangeVolumeGroup(string mixerGroupName, float volumeValue)
    {
        // Convert volume (0-1) to decibel range
        float volumeDB = Mathf.Log10(volumeValue) * 20;
        mixer.SetFloat(mixerGroupName + "_Volume", volumeDB); // "_Volume" is a parameter in the Mixer Group
    }

    private void ChangePitch()
    {
        // start at 1.2, decrease by 0.2 -- 0 if level > 3
        float pianoPitch = (level < 3) ? 1.0f - (0.2f * (level - 1)) : 1f;
        
        // start at 1, increase by 0.1 -- 0 if level < 3
        float violinPitch = (level > 2) ? 0.8f + (0.1f * (level - 2)) : 0f;
        
        // start slow, increase after increase by 0.2 every level
        float beatPitch = 0.5f + (level * 0.1f);
        
        // start at 0.5, increase by 0.2 -- 0 if level = 1 or level = 5
        float drumPitch = 0.5f;
        if (level != 1 && level != 5)
        {
            drumPitch = 0.5f + (0.1f * (level - 1));
        }

        ChangePitchGroup(_pianoG.name, pianoPitch);
        ChangePitchGroup(_violinG.name, violinPitch);
        ChangePitchGroup(_beatG.name, beatPitch);
        ChangePitchGroup(_drumG.name, drumPitch);
    }

    private void ChangePitchGroup(string mixerGroupName, float pitchValue)
    {
        mixer.SetFloat(mixerGroupName + "_Pitch", pitchValue);
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
