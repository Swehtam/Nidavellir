using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    public Toggle fullscreen;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        // Loop para criar as strings do dropdown da resolução
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Condição para descobrir o tamanho de tela atual do player
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Adicionar as opções de resolução e setar o tamanho atual da tela do player
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        // Setar o grafico inicial do jogo (ULTRA)
        graphicsDropdown.value = PlayerPrefs.GetInt("Graphics", 5);
        graphicsDropdown.RefreshShownValue();

        fullscreen.isOn = Convert.ToBoolean(PlayerPrefs.GetString("Fullscreen", "true"));

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
    }

    // Metodo para mudar a resolução quando selecionar um valor no dropdown
    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
        Resolution resolution = resolutions[PlayerPrefs.GetInt("Resolution")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Metodo para mudar o volume geral do jogo quando mudar o slidebar
    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
    }

    // Metodo para mudar o volume da música do jogo quando mudar o slidebar
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
    }

    // Metodo para mudar o volume dos efeitos sonoros do jogo quando mudar o slidebar
    public void SetSoundEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
        audioMixer.SetFloat("soundEffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("SoundEffectsVolume")) * 20);
    }

    // Metodo para mudar a qualidade quando selecionar um valor no dropdown
    public void SetQuality(int qualityIndex)
    {
        PlayerPrefs.SetInt("Graphics", qualityIndex);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics"));
    }

    // Metodo para colocar a tela fullscreen ou não
    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetString("Fullscreen", isFullscreen.ToString());
        Screen.fullScreen = isFullscreen;
    }
}