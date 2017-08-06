using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public void SelectLevel()
    {
        if(PlayerPrefs.GetInt("lock"+LevelSelect.SelectedLevel.ToString()) > 0)
        { 
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GamePlay");
        GamePlay.GameLevel = LevelSelect.SelectedLevel;
        }
    }

    public void LoadLevelSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LevelSelect");

    }

    public void Load(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        
    }

}
