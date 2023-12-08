using GameScripts;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using Random = UnityEngine.Random;

namespace AudioScript
{
    public class FoleysController : MonoBehaviour
    {
        public AudioMixer mixer;
        
        public Transform soldierBlue;
        public Transform soldierRed;

        public Transform[] portals;

        public float distanceSound = 3f;
        private int turn = 1;

        // 2 different sound for portals
        public AudioSource[] audioSource;

        public AudioClip[] natureTracks;
        public AudioSource natureSource;
        private int natureIndex = 0;
    
        // Start is called before the first frame update
        void Start()
        {
            natureSource = gameObject.AddComponent<AudioSource>();
            natureSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Foleys")[0];
            //natureIndex = Random.Range(0, natureTracks.Length);
            
            StartCoroutine(playNature());
        }

        // Update is called once per frame
        void Update()
        {
            turn = GameManager.Instance.getTurn();
            portalSound(turn);
            
        }
        
        IEnumerator playNature()
        {
            natureSource.clip = natureTracks[natureIndex];
            natureSource.Play();
            yield return new WaitForSeconds(natureSource.clip.length + 1f); // Wait for the track to finish
            natureIndex = (natureIndex + 1) % natureTracks.Length;
        }

        void portalSound(int turn)
        {
            Transform current = (turn == 1) ? soldierBlue : soldierRed;
            
            Vector3 pos = current.position;
            float distancePortal1 = Vector3.Distance(pos, portals[0].position);
            float distancePortal2 = Vector3.Distance(pos, portals[1].position);
            float distancePortal3 = Vector3.Distance(pos, portals[2].position);
            float distancePortal4 = Vector3.Distance(pos, portals[3].position);
        
            if ((distancePortal1 <= distanceSound || distancePortal2 <= distanceSound) && !audioSource[0].isPlaying)
            {
                PlaySound(audioSource[0]);
            }
            else if ((distancePortal1 > distanceSound && distancePortal2 > distanceSound) && audioSource[0].isPlaying)
            {
                StopSound(audioSource[0]);
            }
            if ((distancePortal3 <= distanceSound || distancePortal4 <= distanceSound) && !audioSource[1].isPlaying)
            {
                PlaySound(audioSource[1]);
            }
            else if ((distancePortal3 > distanceSound && distancePortal4 > distanceSound) && audioSource[1].isPlaying)
            {
                StopSound(audioSource[1]);
            }
        }
                
        void PlaySound(AudioSource audioSource)
        {
            audioSource.Play();
        }

        void StopSound(AudioSource audioSource)
        {
            audioSource.Stop();
        }
    }
}
