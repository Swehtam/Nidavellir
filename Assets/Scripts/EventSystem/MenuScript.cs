using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuScript : MonoBehaviour {

    private string sceneName;
    public GameObject player;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    void Update()
    {
        if (player)
        {
            if (player.gameObject.GetComponent<PlayerHealthManager>().died == true)
            {
                StartCoroutine(GoToScene(2.5f, "DeathScene"));
            }
        }
        
        if (sceneName == "DeathScene")
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine(GoToScene(1.0f, "Menu"));
            }
        }
    }

    public void LoadIntroScene()
    {
        StartCoroutine(GoToScene(1.0f, "Introduction"));
    }

    public IEnumerator GoToScene(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
