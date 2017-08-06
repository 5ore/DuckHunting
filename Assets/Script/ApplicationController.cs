using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour {

    public bool LoadGameOnStart = false;

    public GameObject loadingText;

    [SerializeField]
    private Button optionsButton;

    private void Start()
    {
        if (LoadGameOnStart)
        {
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        loadingText.SetActive(true);
        while(loadingText.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pop-up"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        AsyncOperation ao = SceneManager.LoadSceneAsync("MainMenu");
        ao.allowSceneActivation = false;
        while (ao.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        ao.allowSceneActivation = true;
    }

    public void ResetGame()
    {
        for (int i = 2; i <= 10; i++)
        {
            PlayerPrefs.SetInt("lock" + i, 0);
            PlayerPrefs.SetInt("percentage" + i, 0);
            for (int j = 1; j <= 3; j++)
                PlayerPrefs.SetInt("star" + j + i, 0);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
