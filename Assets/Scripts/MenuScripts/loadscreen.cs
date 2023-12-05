using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuScripts
{
    public class loadscreen : MonoBehaviour
    {

        public Image loadingBar;
    
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine((LoadScreenAsync()));
            loadingBar.fillAmount = 0;
        }

        IEnumerator LoadScreenAsync()
        {
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");

            while (!asyncOperation.isDone)
            {
                loadingBar.fillAmount = asyncOperation.progress;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
