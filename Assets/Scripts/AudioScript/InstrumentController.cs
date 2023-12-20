using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioScript
{
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
        private float _crossFadeTime = 3f;
        private float _startFadeTime = 2f;
        private float _endFadeTime = 2f;
        private bool _isFading;
        private bool _isStarting;
        private bool _isEnding;
    
        // levels
        private int _level = 1;
    
        // Start is called before the first frame update
        void Start()
        {
            InitializeMixerGroup();
            InitializeAudioSource();
            ChangeVolume();
            ChangePitch();
        
            _pianoClipsAudioSource.clip = PianoClips[_level - 1];
            StartCoroutine(StartSlowly(_pianoClipsAudioSource));

            _isFading = false;
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

        private void InitializeClipsAudio(AudioClip[] clips, ref AudioSource source, AudioMixerGroup group)
        {
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
            }
            source.outputAudioMixerGroup = group;
            source.playOnAwake = false;
            source.loop = true;
            source.clip = clips[0];

            source.minDistance = 200;
            source.maxDistance = 500;
            source.spatialBlend = 1.0f;
        }

        private void InitializePauseClips()
        {
            foreach (AudioClip clip in PauseClips)
            {
                AudioSource aSource = gameObject.AddComponent<AudioSource>();
                aSource.outputAudioMixerGroup = _otherG;
                aSource.playOnAwake = false;
                aSource.loop = true;
                aSource.clip = clip;

                aSource.minDistance = 200;
                aSource.maxDistance = 500;
                aSource.spatialBlend = 1.0f;

                _pauseAudioSources.Add(aSource);
            }
        }

        public void PlayAll(bool aiming)
        {
            int musicLevel = GameManager.Instance.getMusicLevel();
        
            // set volumes for all instruments mixerGroup based on volume
        

            if (_level != musicLevel) // change of level
            {
                _level = musicLevel;
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
                // not supposed to happen
                if (_beatClipAudioSources.isPlaying)
                {
                    _beatClipAudioSources.Stop();
                }
                _beatClipAudioSources.clip = BeatClips[_level - 1];
            }
        
            if (aiming)
            {
                StartCoroutine(StartSlowly(_beatClipAudioSources));
            }
            else
            {
                StartCoroutine(EndSlowly(_beatClipAudioSources));
            }
            // adapt volume and pitch based on level;
        }
    
        private void PlayDrum(bool aiming, bool levelChange)
        {
            // update clip if changed
            if (_level is > 1 and < 5 && levelChange)
            {
                // wouldnt normally happen
                if (_drumClipsAudioSource.isPlaying)
                {
                    _drumClipsAudioSource.Stop();
                }
                _drumClipsAudioSource.clip = DrumClips[_level - 2];
                
            }
        
            if (aiming && _level is > 1 and < 5)
            {
                // adjust volume and pitch
                StartCoroutine(StartSlowly(_drumClipsAudioSource));
            }
            else
            {
                StartCoroutine(EndSlowly(_drumClipsAudioSource));
            }
        }

        private void PlayPiano(bool levelChange)
        {
            if (_level < 4 && levelChange)
            {
                if (!_pianoClipsAudioSource.isPlaying)
                {
                    StartCoroutine(StartSlowly(_pianoClipsAudioSource));
                }
                else // transition
                {
                    StartCoroutine(CrossFade(_pianoG, PianoClips[_level - 1]));
                }
            }
            else
            {
                StartCoroutine(EndSlowly(_pianoClipsAudioSource));
            }
        }

        private void PlayViolin(bool levelChange)
        {
            if (_level is > 2 and < 6 && levelChange)
            {
                if (!_violinClipsAudioSource.isPlaying)
                {
                    StartCoroutine(StartSlowly(_violinClipsAudioSource));
                }
                else // transition of violin
                {
                    StartCoroutine(CrossFade(_violinG, ViolinClips[_level - 3]));
                }
            }
            else
            {
                StartCoroutine(EndSlowly(_violinClipsAudioSource));
            }
        }
    
        public void PlayPause(bool start)
        {
            if (start)
            {
                StopAllSlowly();
                
                foreach (AudioSource source in _pauseAudioSources)
                {
                    StartCoroutine(StartSlowly(source));
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

        public void StopAllSudden()
        {
            _pianoClipsAudioSource.Stop();
            _violinClipsAudioSource.Stop();
            _beatClipAudioSources.Stop();
            _drumClipsAudioSource.Stop();
        }
        
        public void StopAllSlowly()
        {
            StartCoroutine(EndSlowly(_pianoClipsAudioSource));
            StartCoroutine(EndSlowly(_violinClipsAudioSource));
            StartCoroutine(EndSlowly(_beatClipAudioSources));
            StartCoroutine(EndSlowly(_drumClipsAudioSource));
        }
    
        private void StopAudioSource(List<AudioSource> audioSources)
        {
            foreach (AudioSource source in audioSources)
            {
                StartCoroutine(EndSlowly(source));
            }   
        }

        // change volume in audioMixerGroup - General function
        private void ChangeVolume()
        {
            // start at 1, decrease by 0.3 -- 0 if level > 3
            float pianoVolume = (_level < 3) ? 1f - (0.3f * (_level - 1)) : 0f;
        
            // start at 0, increase by 0.2 -- 0 if level < 3
            float violinVolume = (_level > 2) ? (0.2f * _level) : 0f;
        
            // increase by 0.2 every level
            float beatVolume = 0.15f * _level;
        
            // increase by 0.3 -- 0 if level = 1 or level = 5
            float drumVolume = 0f;
            if (_level != 1 && _level != 5)
            {
                drumVolume = 0.2f * (_level - 1);
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

        // Change Pitch for mixerGroup
        private void ChangePitch()
        {
            // start at 1.2, decrease by 0.2 -- 0 if level > 3
            float pianoPitch = (_level < 3) ? 1.0f - (0.2f * (_level - 1)) : 1f;
        
            // start at 1, increase by 0.1 -- 0 if level < 3
            float violinPitch = (_level > 2) ? 0.8f + (0.1f * (_level - 2)) : 0f;
        
            // start slow, increase after increase by 0.2 every level
            float beatPitch = 0.5f + (_level * 0.1f);
        
            // start at 0.5, increase by 0.2 -- 0 if level = 1 or level = 5
            float drumPitch = 0.5f;
            if (_level != 1 && _level != 5)
            {
                drumPitch = 0.5f + (0.1f * (_level - 1));
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

        private AudioSource GetAudioSource(AudioMixerGroup group)
        {
            switch (group.name)
            {
                case "Beat":
                    return _beatClipAudioSources;
                case "Piano":
                    return _pianoClipsAudioSource;
                case "Violin":
                    return _violinClipsAudioSource;
                case "Drum":
                    return _drumClipsAudioSource;
                default:
                    return null;
            }
        }


        IEnumerator StartSlowly(AudioSource source)
        {
            bool test = false;
            if (!test)
            {
                test = true;
                source.volume = 0f;
                source.Play();
                float timer = 0f;

                while (timer < _startFadeTime)
                {
                    timer += Time.unscaledDeltaTime;

                    float progress = timer / _startFadeTime;
                    source.volume = Mathf.Lerp(0f, 1f, progress);
                    yield return null;
                }
                test = false;
            }
        }
    
        // End track slowly
        IEnumerator EndSlowly(AudioSource source)
        {
            bool test = false;
            if (!test)
            {
                test = true;
                float timer = 0f;

                while (timer < _endFadeTime)
                {
                    timer += Time.unscaledDeltaTime;

                    float progress = timer / _endFadeTime;
                    source.volume = Mathf.Lerp(1f, 0f, progress);
                    yield return null;
                }

                source.Stop();
                test = false;
            }

        }
    
        // CrossFade between two Tracks with AudioSource
        // Volume variation is specific to said audioSource
        IEnumerator CrossFade(AudioMixerGroup group, AudioClip clip)
        {
            if (!_isFading)
            {
                AudioSource channel = GetAudioSource(group);
                _isFading = true;

                AudioSource transitionAudioSource = gameObject.AddComponent<AudioSource>();
                transitionAudioSource.outputAudioMixerGroup = group;
            
                transitionAudioSource.minDistance = 200;
                transitionAudioSource.maxDistance = 500;
                transitionAudioSource.spatialBlend = 1.0f;
            
                transitionAudioSource.playOnAwake = false;
                transitionAudioSource.loop = true;
                transitionAudioSource.clip = clip;
            
                transitionAudioSource.volume = 0f;
                transitionAudioSource.Play();
            
                float timer = 0f;

                while (timer < _crossFadeTime)
                {
                    timer += Time.deltaTime;

                    float progress = timer / _crossFadeTime;
                    channel.volume = Mathf.Lerp(1f, 0f, progress);
                    transitionAudioSource.volume = Mathf.Lerp(0f, 1f, progress);
                    yield return null;
                }
            
                Destroy(channel);

                switch (group.name)
                {
                    case "Beat":
                        _beatClipAudioSources = transitionAudioSource;
                        break;
                    case "Piano":
                        _pianoClipsAudioSource = transitionAudioSource;
                        break;
                    case "Violin":
                        _violinClipsAudioSource = transitionAudioSource;
                        break;
                    case "Drum":
                        _drumClipsAudioSource = transitionAudioSource;
                        break;
                }
                _isFading = false;
            }
        }
    }
}
