using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void SwitchScene(string sceneName){
         SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
