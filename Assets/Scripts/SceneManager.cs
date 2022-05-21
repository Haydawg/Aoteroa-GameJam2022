using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    SceneManager sceneManager;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //Load scene via scene number
    public void LoadScene(int scene)
    {
        sceneManager.LoadScene(scene);
    }
    //Load scene via scene name
    public void LoadScene(string scene)
    {
        sceneManager.LoadScene(scene);
    }
}
