using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptions : MonoBehaviour {

    [SerializeField]
    GameObject optionsMenu;
    [SerializeField]
    GameObject generalOptions;
    [SerializeField]
    GameObject soundMenu;

    public void ShowOptions(Animator optionsButtonAnimator)
    {
        if (optionsButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            EnableOptionsMenu();
            optionsButtonAnimator.Play("rotate");
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
}
