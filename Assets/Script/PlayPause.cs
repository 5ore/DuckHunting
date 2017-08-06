using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayPause : MonoBehaviour {

    [SerializeField]
    private AdMob Ads;

	public static bool gamestart=false;

	[SerializeField]
	private GameObject dog;

    private bool paused = false;

	public void GameStart(){
        paused = false;
		GamePlay.WarClick = true;
		gamestart = true;
		Time.timeScale = 1;
		Debug.Log (gamestart);

	}


	public void GamePause(){
        paused = true;
		gamestart = false;
	}

	public void AnimStartGame(){
        dog.GetComponent<Animator>().enabled = true;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamestart)
            Application.Quit();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            GameStart();
        else if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            GamePause();
    }
}
