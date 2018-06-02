using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUI : MonoBehaviour {
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("lv");
        PlayerPrefs.DeleteKey("smallTimer");
        PlayerPrefs.DeleteKey("bigTmer");
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("costIndex");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    public void OldGame()
    {
        UnityEngine. SceneManagement.SceneManager.LoadScene(1);
    }


    public void GameExit()
    {
        Application.Quit();
    }
}
