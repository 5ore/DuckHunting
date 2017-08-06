using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    [SerializeField]
   private  LevelElement BigInfo;
    [SerializeField]
   private  LevelElement[] LevelInfo = new LevelElement[5];
    public static int  SelectedLevel;

    [SerializeField]
    private GameObject btnNext;
    [SerializeField]
    private GameObject btnPrev;

    

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("lock1", 1);
        btnPrev.GetComponent<Button>().interactable = false;
        SelectedLevel = 1;
       // SetLevel();
       
     /*   PlayerPrefs.SetInt("star11", 0);
        PlayerPrefs.SetInt("star21", 0);
        PlayerPrefs.SetInt("star31", 0);
        PlayerPrefs.SetInt("percentage1", 0);
        PlayerPrefs.SetInt("lock2", 0);*/
		
	}
 /*   public void SetLevel()
    {
        BigInfo.ShowLevel(SelectedLevel);
        LevelInfo[0].ShowLevel(SelectedLevel - 2);
        LevelInfo[1].ShowLevel(SelectedLevel - 1);
        LevelInfo[2].ShowLevel(SelectedLevel);
        LevelInfo[3].ShowLevel(SelectedLevel + 1);
        LevelInfo[4].ShowLevel(SelectedLevel + 2);
    }
    */

    public void NextLevel() {
        SelectedLevel++;
       // SetLevel();
        if (SelectedLevel >= 2)
            btnPrev.GetComponent<Button>().interactable = true;
        if (SelectedLevel == 10)
            btnNext.GetComponent<Button>().interactable = false;
    

    }
    public void PrevLevel()
    {
        
        SelectedLevel--;
   //     SetLevel();
        if (SelectedLevel == 1)
            btnPrev.GetComponent<Button>().interactable = false;
        if (SelectedLevel <= 9)
            btnNext.GetComponent<Button>().interactable = true;
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
