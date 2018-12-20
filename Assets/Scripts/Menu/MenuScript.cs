using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuScript : MonoBehaviour {

    private string sceneName;
    public GameObject player;
    public GameObject menuPanel;
    public GameObject optionsPanel;

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

    public void OpenSettingsMenu()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public IEnumerator GoToScene(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        StartCoroutine(Exit(1.0f));
    }
    
    public IEnumerator Exit(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }
}
