using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour {

    public GameObject menuPanel;
    public GameObject optionsPanel;
    public AudioMixer audioMixer;

    void Start()
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 0f)) * 20);

        audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0f)) * 20);

        audioMixer.SetFloat("soundEffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("SoundEffectsVolume", 0f)) * 20);
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
