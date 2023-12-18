using System.Collections;
using GameScripts;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace AudioScript
{
    public class JinglesController : MonoBehaviour
    {
        public AudioMixer mixer;
    
        public AudioClip[] audioWinTracks;
        public AudioClip[] ballMissTracks;
        public AudioClip[] ballHitTracks;
        public AudioClip lowHealth;
        
        private AudioSource audioSource;
        private AudioSource ballSource;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/SFX")[0]; 
            
            audioSource.minDistance = 100;
            audioSource.maxDistance = 300;
            audioSource.spatialBlend = 1.0f;
            
            ballSource = gameObject.AddComponent<AudioSource>();
            ballSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/SFX")[0]; 
            
            ballSource.minDistance = 100;
            ballSource.maxDistance = 300;
            ballSource.spatialBlend = 1.0f;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void lowHealthState()
        {
            audioSource.pitch = 0.8f;
            audioSource.volume = 0.6f;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(lowHealth);
            }
        }

        public void playWin()
        {
            audioSource.pitch = 1f;
            audioSource.volume = 1f;
            int i = Random.Range(0, audioWinTracks.Length);
            audioSource.clip = audioWinTracks[i];
            audioSource.Play();
        }

        public void playBallMiss()
        {
            ballSource.pitch = 2f;
            int i = Random.Range(0, ballMissTracks.Length);
            ballSource.clip = ballMissTracks[i];
            ballSource.Play();
        }
        
        public void playBallHit()
        {
            ballSource.pitch = 2f;
            int i = Random.Range(0, ballHitTracks.Length);
            ballSource.clip = ballHitTracks[i];
            ballSource.Play();
        }

        public void stopSound()
        {
            audioSource.Stop();
        }
    }
}
