using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void GMLoadScene(string sceneName)
    {
        GameManager.instance.LoadScene(sceneName);
    }
}
