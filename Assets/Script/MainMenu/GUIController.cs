using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour {

    [SerializeField]
    private GameObject QuitDialogBox;
    [SerializeField]
    private AudioClip buttonSoundClip;

    public static bool paused = false;

    public static bool canPause = true;

    public void PlayButtonSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(buttonSoundClip);
    }

	// Update is called once per frame
	void Update () {

        ///////////////////////////////////////////
        //          THE QUIT SCREEN 
        ///////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Escape) && canPause){

            QuitDialogBox.GetComponentInChildren<TextGenerator>().GenerateText();

            QuitDialogBox.SetActive(true);

            if (QuitDialogBox.GetComponent<Animator>() == null)
            {
                Debug.Log("No animator attached to the quit dialog box.");
                QuitDialogBox.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                return;
            }

            StartCoroutine(PopUpDialogBox());
        }
	}

    /// <summary>
    /// Pops up the quit dialog box -Lukaz
    /// </summary>
    IEnumerator PopUpDialogBox()
    {
        Animator quitDialogBoxAnimator = QuitDialogBox.GetComponent<Animator>();

        quitDialogBoxAnimator.Play("pop-up");

        while (quitDialogBoxAnimator.GetCurrentAnimatorStateInfo(0).IsName("pop-up"))
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
