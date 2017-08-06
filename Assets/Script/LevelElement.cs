using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelElement : MonoBehaviour {

    public   Text LevelName;
    public  Text Percentage;
    public  GameObject Star1;
    public  GameObject Star2;
    public  GameObject Star3;
    public  GameObject All;
    public  GameObject Lock;
    public  GameObject imgPlay;

	// Use this for initialization
    public void ShowLevel(int level) {
       

   
            if (PlayerPrefs.GetInt("lock" + level.ToString()) > 0)
            {
                All.SetActive(true);
                Lock.SetActive(false);

            }
            else
            {
                All.SetActive(false);
                Lock.SetActive(true);
            }
      
            
            LevelName.text = "Level " + level.ToString();
            if (PlayerPrefs.GetInt("percentage" + level.ToString()) > 50)
            {
                Percentage.enabled = true;
                imgPlay.SetActive(false);
                Percentage.text = PlayerPrefs.GetInt("percentage" + level.ToString()).ToString() + "%";
            }
            else
            {
            Percentage.enabled = false;
                imgPlay.SetActive(true);
            }

            
           

            if (PlayerPrefs.GetInt("star1" + level.ToString()) > 0)
                Star1.GetComponent<Image>().color = Color.white;
            else
                Star1.GetComponent<Image>().color = Color.gray;

            if (PlayerPrefs.GetInt("star2" + level.ToString()) > 0)
                Star2.GetComponent<Image>().color = Color.white;
            else
                Star2.GetComponent<Image>().color = Color.gray;

            if (PlayerPrefs.GetInt("star3" + level.ToString()) > 0)
                Star3.GetComponent<Image>().color = Color.white;
            else
                Star3.GetComponent<Image>().color = Color.gray;


        
    
    }

}
