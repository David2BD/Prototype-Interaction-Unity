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
        
        public AudioClip[] buttonsSound;
        private AudioSource buttonAudioSource;

        // fondu croise
        private float fadeDuration = 10.0f;
        private bool isFading;
        private float trackInterval = 45.0f;

        private AudioSource transitionAudioSource;

        // Start is called before the first frame update
        void Start()
        {
            menuTrackIndex = Random.Range(0, menuTracks.Length);

            menuAudioSource = gameObject.AddComponent<AudioSource>();
            menuAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
            menuAudioSource.minDistance = 200;
            menuAudioSource.maxDistance = 500;
            menuAudioSource.spatialBlend = 1.0f;

            transitionAudioSource = gameObject.AddComponent<AudioSource>();
            transitionAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
            transitionAudioSource.minDistance = 200;
            transitionAudioSource.maxDistance = 500;
            transitionAudioSource.spatialBlend = 1.0f;
            
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

            buttonAudioSource.PlayOneShot(buttonsSound[i]);
        }

        IEnumerator playMenuBGM()
        {
            transitionAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
            
            menuAudioSource.clip = menuTracks[menuTrackIndex];
            
            if (menuAudioSource.clip == null)
            {
                menuTrackIndex = 0;
                menuAudioSource.clip = menuTracks[menuTrackIndex];
            }

            menuAudioSource.Play();

            menuTrackIndex = (menuTrackIndex + 1) % menuTracks.Length;
            while (true)
            {
                yield return new WaitForSeconds(trackInterval);

                if (!isFading && menuTrackIndex < menuTracks.Length)
                {
                    isFading = true;

                    transitionAudioSource.clip = menuTracks[menuTrackIndex];
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

                    menuAudioSource.Stop();
                    menuAudioSource = transitionAudioSource;
                    menuTrackIndex = (menuTrackIndex + 1) % menuTracks.Length;

                    isFading = false;
                }
            }
        }
    }
}