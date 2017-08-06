using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {

    AsyncOperation ao;

    GameObject cameraPositioner;

    public Camera MainCamera;

    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private GameObject loadingText;
    [SerializeField]
    private float CameraMovementSpeed = 20;
    [SerializeField]
    GameObject[] objectsToDestroy;
    [SerializeField]
    GameObject[] objectsToSetActive;
    [SerializeField]
    GameObject optionsMenu;
    [SerializeField]
    GameObject soundMenu;
    [SerializeField]
    GameObject generalOptions;
    [SerializeField]
    GameObject optionsButton;
    [SerializeField]
    private GameObject SelectedLevelShow;

    private bool loadedOnce = false;

    public static bool startGame;

  
    private int clickid = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("lock1", 1);
    }

    public void buttonPlay()
    {
        StartCoroutine(btnPlay());
    }

    public void levelSelect(GameObject selectedLevel)
    {
        if (!SelectedLevelShow.activeInHierarchy)
            SelectedLevelShow.SetActive(true);

        if (clickid == int.Parse(selectedLevel.name) &&
            PlayerPrefs.GetInt("lock" + selectedLevel.name) == 1 && !loadedOnce)
        {
            loadedOnce = true;
            StartCoroutine(LevelSelect(selectedLevel));
        }
        else
        {
            clickid = int.Parse(selectedLevel.name);
            SelectedLevelShow.GetComponent<LevelElement>().ShowLevel(clickid);
        }
       
    }

    public void ClickPlay()
    {
        StartCoroutine(LevelSelect(GameObject.Find(clickid.ToString())));
    }

    ///////////////////////////////////////////////////////
    /// CALLED WHEN SELECTING A LEVEL
    ///////////////////////////////////////////////////////
    public IEnumerator LevelSelect(GameObject selectedLevel)
    {

        if (selectedLevel.GetComponent<Animator>() == null)
        {
            Debug.Log("The 'animator' component in the selected level could not be found.");
            yield break;
        }
        selectedLevel.GetComponent<Animator>().Play("clicked");

        while (selectedLevel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("clicked"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (GameObject.Find("LevelSelector") == null)
        {
            Debug.Log("The level selector object could not be found.");
            yield break;
        }

        if(GameObject.Find("SelectedLevel_Background") == null)
        {
            Debug.Log("The \"SelectedLevel_Background\" object could not be found.");
            yield break;
        }

        GameObject levelSelector = GameObject.Find("LevelSelector");
        GameObject SelectedLevel_Background = GameObject.Find("SelectedLevel_Background");

        if(levelSelector.GetComponent<Animator>() == null)
        {
            Debug.LogWarning("The animator component in the level selector could not be found.");
            yield break;
        }

        if(SelectedLevel_Background.GetComponent<Animator>() == null)
        {
            Debug.LogWarning("The animator component in the " +
                "SelectedLevel_Background\" object could not be found.");
            yield break;
        }

        Animator[] animators = new Animator[]
        {
            levelSelector.GetComponent<Animator>(),
            SelectedLevel_Background.GetComponent<Animator>()
        };

        foreach (Animator animator in animators)
        {
            if (!animator.enabled)
                animator.enabled = true;

            animator.Play("close");
        }

        while(animators[0].GetCurrentAnimatorStateInfo(0).IsName("close") ||
              animators[1].GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (!int.TryParse(selectedLevel.name.ToString(), out GamePlay.GameLevel))
        {
            Debug.LogWarning("Parsing error.");
            yield break;
        }

        loadingText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        ao = Application.LoadLevelAdditiveAsync("GamePlay");

        while (!ao.isDone)
        {
            Debug.Log(ao.progress);
            yield return null;
        }

        if (loadingText.GetComponent<Animator>() == null)
            Debug.Log("The animator for the loading text could not be found.");
        else
        {
            loadingText.GetComponent<Animator>().Play("done");
            while (loadingText.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("done"))
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        loadingText.SetActive(false);
        //Destroy(loadingText);

        LoadGame();

    }

    ///////////////////////////////////////////////////////
    /// CALLED IN THE MAIN MENU
    ///////////////////////////////////////////////////////
    public IEnumerator btnPlay()
    {
        if (playButton.GetComponent<Animator>() == null)
        {
            Debug.Log("The 'animator' component in 'btnPlay' could not be found.");
            yield break;
        }
        playButton.GetComponent<Animator>().Play("pressed");

        while (playButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pressed"))
        {
            yield return null;
        }

        optionsButton.GetComponent<Animator>().Play("move-up");

        yield return new WaitForSeconds(1f);

        foreach(GameObject objectToBeActivated in objectsToSetActive)
        {
            objectToBeActivated.SetActive(true);
            if(objectToBeActivated.GetComponent<Animator>() == null)
            {
                Debug.LogWarning("No animator component attached to" +
                    objectToBeActivated.name);
                objectToBeActivated.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                StartCoroutine(PlayPopUpAnimation(objectToBeActivated));
            }
        }

        yield return new WaitForSeconds(1f);

    }
        
    IEnumerator PlayPopUpAnimation(GameObject objectAnimator)
    {
        Debug.Log(objectAnimator.name);
        objectAnimator.GetComponent<Animator>().Play("pop-up");
        while (objectAnimator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pop-up"))
            yield return null;
        objectAnimator.GetComponent<Animator>().enabled = false;
    }

    void LoadGame()
    {
        if (GameObject.Find("CameraPositioner") == null)
        {
            Debug.Log("The camera positioner could not be found.");
            return;
        }

        cameraPositioner = GameObject.Find("CameraPositioner");

        StartCoroutine(StopSpawning());

        startGame = true;
    }

    IEnumerator StopSpawning()
    {
        ObjectSpawner.ON = false;

        GameObject environment = GameObject.Find("Environment");

        while(environment != null ||
            environment.GetComponentInChildren<Transform>() != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject gameObject  in objectsToDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void CloseQuitDialogBox(Animator animator)
    {
        StartCoroutine(closeQuitDialogBox(animator));
    }

    IEnumerator closeQuitDialogBox(Animator animator)
    {
        if (!animator.enabled)
            animator.enabled = true;

        animator.Play("close");

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("close"))
            yield return null;

        yield return new WaitForSeconds(0.5f);

        animator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!startGame)
        {
            return;
        }
        float currentPosition = MainCamera.transform.position.y;
        MainCamera.transform.position =
            Vector3.MoveTowards(MainCamera.transform.position, cameraPositioner.transform.position, Time.deltaTime * CameraMovementSpeed);
        if (MainCamera.transform.position.y == currentPosition)
        {
            startGame = false;
            ActivateGamePlayGUI();
        }
    }

    void ActivateGamePlayGUI()
    {
        GameObject gamePlayBoard = GameObject.FindGameObjectWithTag("GamePlayBoard").transform.GetChild(0).gameObject;

        if (gamePlayBoard == null)
        {
            Debug.LogWarning("The 'GamePlayBoard' tagged game object could not be found in the 'GamePlay' scene.");
            return;
        }

          if(gamePlayBoard.GetComponent<Button>() == null)
        {
            Debug.LogWarning("The 'GamePlayBoard' tagged game object does not have a 'Button' component attached to it.");
            return;
        }
        gamePlayBoard.SetActive(true);
        gamePlayBoard.GetComponent<Button>().onClick.AddListener(() => closeGamePlayGUI(gamePlayBoard.GetComponent<Animator>()));
    }

    public void closeGamePlayGUI(Animator gamePlayBoardAnimator)
    {
        StartCoroutine(CloseGamePlayGUI(gamePlayBoardAnimator));
    }

    IEnumerator CloseGamePlayGUI(Animator gamePlayBoardAnimator)
    {
        gamePlayBoardAnimator.Play("close");

        while (gamePlayBoardAnimator.GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        gamePlayBoardAnimator.gameObject.SetActive(false);

    }

    ///////////////////////////////////////////////
    // OPTIONS BUTTON
    ///////////////////////////////////////////////
    public void ShowOptions(Animator optionsButtonAnimator)
    {
        if(optionsButtonAnimator.gameObject.GetComponent<Button>() == null)
        {
            Debug.LogError("No button component in the options button.");
            return;
        }

        if (!ValidateOptionsMenu())
        {
            Debug.LogError("The options menu has no animator component attached to it.");
            return;
        }

        if (optionsButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            EnableOptionsMenu();
            optionsButtonAnimator.Play("rotate");
            if (!playButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pressed"))
                playButton.GetComponent<Animator>().Play("close");
        }
        else
        {
            StartCoroutine(DisableOptionsMenu(optionsButtonAnimator));
        }

    }

    void EnableOptionsMenu()
    {
        optionsMenu.SetActive(true);

    }

    IEnumerator DisableOptionsMenu(Animator optionsButtonAnimator)
    {
        optionsMenu.GetComponent<Animator>().Play("close");
        if (!playButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pressed"))
            playButton.GetComponent<Animator>().Play("pop-up");

        while (optionsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        optionsButtonAnimator.Play("idle");
        ResetOptionsMenu();
    }

    void ResetOptionsMenu()
    {
        optionsMenu.SetActive(false);
        soundMenu.SetActive(false);
        generalOptions.SetActive(true);
    }

    /// <summary>
    /// Checks if the options menu has an animator component attached to it.
    /// </summary>
    /// <returns></returns>
    bool ValidateOptionsMenu()  { return (optionsMenu.GetComponent<Animator>() == null) ? false : true; }
}
