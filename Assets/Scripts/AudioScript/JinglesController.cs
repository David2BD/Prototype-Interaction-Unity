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
        public AudioClip lowHealth;
        private AudioSource audioSource;

        private float delay = 3f;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/SFX")[0];   
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void lowHealthState()
        {
            audioSource.PlayOneShot(lowHealth);
        }

        public void playWin()
        {
            int i = Random.Range(0, audioWinTracks.Length);
            audioSource.clip = audioWinTracks[i];
            audioSource.Play();
        }

        public void stopSound()
        {
            audioSource.Stop();
        }
    }
}
