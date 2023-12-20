using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AudioScript
{
    public class MenuBGMController : MonoBehaviour
    {
        public AudioMixer mixer;

        public AudioClip[] menuTracks;
        private AudioSource menuAudioSource;
        private int menuTrackIndex;
        
        // 0 - start game, 1 - confirm, 2 - change, 3 - back, 4 - slider
        public AudioClip[] buttonsSound;
        private AudioSource buttonAudioSource;

        // fondu croise
        private float fadeDuration = 15.0f;
        private bool isFading;
        private float trackInterval = 45.0f;

        // Start is called before the first frame update
        void Start()
        {
            menuTrackIndex = Random.Range(0, menuTracks.Length);

            if (menuAudioSource == null)
            {
                menuAudioSource = gameObject.AddComponent<AudioSource>();
            }
            menuAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
            menuAudioSource.minDistance = 200;
            menuAudioSource.maxDistance = 500;
            menuAudioSource.spatialBlend = 1.0f;
            menuAudioSource.loop = true;
            
            menuAudioSource.clip = menuTracks[menuTrackIndex];
            menuAudioSource.Play();
            
            StartCoroutine(playMenuBGM());
        }

        void StopCurrentMusic()
        {
            menuAudioSource.Stop();
        }

        public void playButtonSound(int i)
        {
            if (buttonAudioSource == null)
            {
                buttonAudioSource = gameObject.AddComponent<AudioSource>();
                buttonAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Buttons")[0];
            }

            if (i != 4)
            {
                buttonAudioSource.PlayOneShot(buttonsSound[i]);
            }
            else
            {
                if (!buttonAudioSource.isPlaying)
                {
                    buttonAudioSource.PlayOneShot(buttonsSound[i]);
                }
            }


        }

        IEnumerator playMenuBGM()
        {
            while (true)
            {
                yield return new WaitForSeconds(trackInterval - fadeDuration);

                if (!isFading)
                {
                    isFading = true;
                    int nextTrackIndex = (menuTrackIndex + 1) % menuTracks.Length;

                    AudioSource transitionAudioSource = gameObject.AddComponent<AudioSource>();
                    transitionAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
                    transitionAudioSource.minDistance = 200;
                    transitionAudioSource.maxDistance = 500;
                    transitionAudioSource.spatialBlend = 1.0f;  
                    
                    transitionAudioSource.clip = menuTracks[nextTrackIndex];
                    transitionAudioSource.volume = 0f;
                    transitionAudioSource.Play();

                    float timer = 0f;

                    while (timer < fadeDuration)
                    {
                        timer += Time.deltaTime;

                        float progress = timer / fadeDuration;
                        menuAudioSource.volume = Mathf.Lerp(1f, 0f, progress);
                        transitionAudioSource.volume = Mathf.Lerp(0f, 1f, progress);

                        yield return null;
                    }
                    
                    Destroy(menuAudioSource);
                    menuAudioSource = transitionAudioSource;
                    menuTrackIndex = nextTrackIndex;

                    isFading = false;
                }
            }
        }
    }
}