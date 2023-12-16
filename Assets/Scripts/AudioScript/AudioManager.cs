using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AudioScript
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        // Singleton
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject("AudioManagerSingleton");
                        _instance = singleton.AddComponent<AudioManager>();
                    }
                }

                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                // existe entre les scenes
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public AudioMixer mixer;

        public AudioClip[] menuTracks;
        private AudioSource menuAudioSource;
        private int menuTrackIndex;


        public AudioClip[] gameTracks;
        private AudioSource gameAudioSource;
        private int gameTrackIndex;

        public AudioClip[] dangerTracks;
        private AudioSource dangerAudioSource;
        private int dangerTrackIndex;

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
            gameTrackIndex = Random.Range(0, gameTracks.Length);
            dangerTrackIndex = Random.Range(0, dangerTracks.Length);

            menuAudioSource = gameObject.AddComponent<AudioSource>();
            menuAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
            menuAudioSource.minDistance = 200;
            menuAudioSource.maxDistance = 500;
            menuAudioSource.spatialBlend = 1.0f;

            gameAudioSource = gameObject.AddComponent<AudioSource>();
            gameAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundGame")[0];
            gameAudioSource.minDistance = 200;
            gameAudioSource.maxDistance = 500;
            gameAudioSource.spatialBlend = 1.0f;
            
            dangerAudioSource = gameObject.AddComponent<AudioSource>();
            dangerAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundGame")[0];
            dangerAudioSource.minDistance = 200;
            dangerAudioSource.maxDistance = 500;
            dangerAudioSource.spatialBlend = 1.0f;

            transitionAudioSource = gameObject.AddComponent<AudioSource>();
            transitionAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
            transitionAudioSource.minDistance = 200;
            transitionAudioSource.maxDistance = 500;
            transitionAudioSource.spatialBlend = 1.0f;

            SceneManager.sceneLoaded += OnSceneLoaded;

            StartCoroutine(playMenuBGM());
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "MainMenu":
                    StopCurrentMusic(false);
                    StartCoroutine(playMenuBGM());
                    break;
                case "Game":
                    StopCurrentMusic(true);
                    StartCoroutine(playGameBGM());
                    break;
                // Add cases for other scenes as needed
                default:
                    StopCurrentMusic(false);
                    StartCoroutine(playMenuBGM());
                    break;
            }
        }

        void StopCurrentMusic(bool game)
        {
            if (game)
            {
                menuAudioSource.Stop();
            }
            else
            {
                gameAudioSource.Stop();
            }
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

            gameAudioSource.Stop();
            menuAudioSource.clip = menuTracks[menuTrackIndex];
            
            if (menuAudioSource.clip == null)
            {
                menuTrackIndex = 0;
                menuAudioSource.clip = gameTracks[menuTrackIndex];
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

        IEnumerator playDangerBGM()
        {
            /*
            float originalVolume = gameAudioSource.volume;
            
            gameAudioSource.volume *= 0.3f;
            dangerAudioSource.clip = dangerTracks[0];
            dangerAudioSource.Play();
            */

            yield return null;
        }

        IEnumerator playGameBGM()
        {
            transitionAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundGame")[0];

            menuAudioSource.Stop();
            gameAudioSource.clip = gameTracks[gameTrackIndex];

            if (gameAudioSource.clip == null)
            {
                gameTrackIndex = 0;
                gameAudioSource.clip = gameTracks[gameTrackIndex];
            }

            gameAudioSource.Play();
            gameTrackIndex = (gameTrackIndex + 1) % gameTracks.Length;
            
            while (true)
            {
                yield return new WaitForSeconds(trackInterval);

                if (!isFading && gameTrackIndex < gameTracks.Length)
                {
                    isFading = true;

                    transitionAudioSource.clip = gameTracks[gameTrackIndex];
                    transitionAudioSource.volume = 0f;
                    transitionAudioSource.Play();

                    float timer = 0f;

                    while (timer < fadeDuration)
                    {
                        timer += Time.deltaTime;

                        float progress = timer / fadeDuration;
                        gameAudioSource.volume = Mathf.Lerp(1f, 0f, progress);
                        transitionAudioSource.volume = Mathf.Lerp(0f, 1f, progress);

                        yield return null;
                    }

                    gameAudioSource.Stop();
                    gameAudioSource = transitionAudioSource;
                    gameTrackIndex = (gameTrackIndex + 1) % gameTracks.Length;

                    isFading = false;
                }
            }
        }
    }
}