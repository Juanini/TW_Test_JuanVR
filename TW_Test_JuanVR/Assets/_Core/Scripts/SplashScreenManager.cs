using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public float splashScreenDelay = 3;
    public GameObject loadingTextObj;

    void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(splashScreenDelay);
        loadingTextObj.gameObject.SetActive(true);
        SceneManager.LoadSceneAsync(GameConstants.S_GAME);

    }
}
