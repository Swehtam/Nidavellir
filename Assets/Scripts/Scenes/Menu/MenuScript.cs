using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Yarn.Unity.Example
{
    public class MenuScript : MonoBehaviour
    {
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
            IntroductionScript.phase = 1;
            LoadingScreenManager.LoadScene("Introduction");
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
}