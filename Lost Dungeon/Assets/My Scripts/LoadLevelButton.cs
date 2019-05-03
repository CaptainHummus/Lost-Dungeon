using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelButton : MonoBehaviour
{
    public void GMLoadScene(string sceneName)
    {
        GameManager.instance.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
