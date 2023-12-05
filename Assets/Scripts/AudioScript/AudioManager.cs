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

        public AudioClip[] buttonsSound;
        private AudioSource buttonAudioSource;
    
        // Start is called before the first frame update
        void Start()
        {
            menuTrackIndex = Random.Range(0, menuTracks.Length);
            gameTrackIndex = Random.Range(0, gameTracks.Length);
        
            menuAudioSource = gameObject.AddComponent<AudioSource>();
            menuAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundMenu")[0];
        
            gameAudioSource = gameObject.AddComponent<AudioSource>();
            gameAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/BackgroundGame")[0];
        
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
            menuAudioSource.clip = menuTracks[menuTrackIndex];
            menuAudioSource.Play();
            yield return new WaitForSeconds(menuAudioSource.clip.length + 1f); // Wait for the track to finish
            menuTrackIndex = (menuTrackIndex + 1) % menuTracks.Length;
        }
    
        IEnumerator playGameBGM()
        {
            gameAudioSource.clip = gameTracks[gameTrackIndex];
            gameAudioSource.Play();
            yield return StartCoroutine(WaitForSoundToEnd(gameAudioSource.clip)); // Wait for the track to finish
            gameTrackIndex = (gameTrackIndex + 1) % gameTracks.Length;
        }

        IEnumerator WaitForSoundToEnd(AudioClip clip)
        {
            yield return new WaitForSeconds(clip.length + 1f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
