using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MenuScripts
{
    public class Audio : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject audioMenu;
    
        public AudioMixer mixer;
        public Slider[] slidersVolume;

        private bool getValue = false;
    
        // Start is called before the first frame update
        void Start()
        {
            getVolume();
        }
    
        public void SetVolume(int i)
        {
            if (!getValue)
            {
                // Convert volume (0-1) to decibel range
                float volumeDB = Mathf.Log10(slidersVolume[i].value) * 20;
                
                switch (i)
                {
                    case 0:
                        mixer.SetFloat("BackgroundMenu_Volume", volumeDB);
                        break;
                    case 1:
                        mixer.SetFloat("Instrument_Volume", volumeDB);
                        break;
                    case 2:
                        mixer.SetFloat("SFX_Volume", volumeDB);
                        break;
                    case 3:
                        mixer.SetFloat("Foleys_Volume", volumeDB);
                        break;
                    case 4:
                        mixer.SetFloat("Buttons_Volume", volumeDB);
                        break;
                }
            }
        }
    
        // Utility function to convert volume value to slider value
        float VolumeToSliderValue(float volumeDB)
        {
            // Convert decibel range to slider value range (0.0 to 1.0)
            float volumeLevel = Mathf.Pow(10, volumeDB / 20);
            return volumeLevel;
        }

        // Update is called once per frame
        void Update()
        {
        }

        void getVolume()
        {
            getValue = true;
            float bgVolumeMenu;
            mixer.GetFloat("BackgroundMenu_Volume", out bgVolumeMenu);
            slidersVolume[0].value = VolumeToSliderValue(bgVolumeMenu);
            
            float sfxVolume;
            mixer.GetFloat("SFX_Volume", out sfxVolume);
            slidersVolume[2].value = VolumeToSliderValue(sfxVolume);
            
            float foleysVolume;
            mixer.GetFloat("Foleys_Volume", out foleysVolume);
            slidersVolume[3].value = VolumeToSliderValue(foleysVolume);
            getValue = false; 
            
            float buttonsVolume;
            mixer.GetFloat("Buttons_Volume", out buttonsVolume);
            slidersVolume[4].value = VolumeToSliderValue(buttonsVolume);
            getValue = false; 
            
            float bgVolumeGame;
            mixer.GetFloat("Instrument_Volume", out bgVolumeGame);
            slidersVolume[1].value = VolumeToSliderValue(bgVolumeGame);
        }
    
        public void Return()
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
            audioMenu.SetActive(false);
            Debug.Log("Player go back to options menu");
        }
    }
}
