using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPanel : MonoBehaviour
{
    public string sceneToLoadOnRestart;
    public void Restart()
    {
        SceneManager.LoadScene(sceneToLoadOnRestart);
    }
   public void QuitApp()
    {
        Application.Quit();
    }
}
