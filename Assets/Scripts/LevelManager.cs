using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameObject startPos;
    GameObject player;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    //Load scene via scene number
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    //Load scene via scene name
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    private void OnLevelWasLoaded(int level)
    {
        if (startPos = GameObject.FindGameObjectWithTag("Spawn"))
            if (player = GameObject.FindGameObjectWithTag("Player"))
                player.transform.position = startPos.transform.position;
    }
}
