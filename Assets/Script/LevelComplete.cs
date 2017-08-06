using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour {
    [SerializeField]
    private GameObject Star1;
    [SerializeField]
    private GameObject Star2;
    [SerializeField]
    private GameObject Star3;
    [SerializeField]
    
    // Use this for initialization
    void Start () {
        int per = GamePlay.Kill / GamePlay.LevelDuck * 100;
        Star1.GetComponent<Image>().color = Color.white;
        if (per >= 75)
            Star2.GetComponent<Image>().color = Color.white;
        else
            Star2.GetComponent<Image>().color = Color.gray;

        if (per >=100)
            Star3.GetComponent<Image>().color = Color.white;
        else
            Star3.GetComponent<Image>().color = Color.gray;


    }

    public void NextLevel()
    {
        
    }

    public void BackToLevelSelector()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
