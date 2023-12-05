using GameScripts;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace AudioScript
{
    public class JinglesController : MonoBehaviour
    {
        public AudioMixer mixer;

        public Transform soldier1;
        public Transform soldier2;
    
        public AudioClip[] audioWinTracks;
        private AudioSource audioWinSource;
        // Start is called before the first frame update
        void Start()
        {
            audioWinSource = gameObject.AddComponent<AudioSource>();
            audioWinSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/SFX")[0];   
        }

        // Update is called once per frame
        void Update()
        {
            if (soldier1.GetComponent<Soldier>().getHealth() <= 0 ||
                soldier2.GetComponent<Soldier>().getHealth() <= 0)
            {
                int i = Random.Range(0, audioWinTracks.Length);
                audioWinSource.clip = audioWinTracks[i];
            }
        }
    }
}
