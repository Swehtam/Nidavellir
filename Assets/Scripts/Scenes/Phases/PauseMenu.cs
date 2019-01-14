using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yarn.Unity.Example
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;

        public GameObject pauseMenuUI;
        public GameObject settingsMenu;
        public GameObject hudMenu;

        // Update is called once per frame
        void Update()
        {
            // Pausar e despausar quando aperta ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        // Metodo para despausar o jogo quando aperta ESC novamente ou quando aperta no botão
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            settingsMenu.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        // Metodo para pausar o jogo quando pressionar o botão ESC
        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        // Metodo para o botão de mudar de cena para o Menu
        public void LoadMenu()
        {
            Time.timeScale = 1f;
            LoadingScreenManager.LoadScene("Menu");
        }

        // Metodo para o botão de sair do jogo
        public void QuitGame()
        {
            Application.Quit();
        }

        // Metodo para o botão de abir as opções
        public void OpenSettings()
        {
            settingsMenu.SetActive(true);
            pauseMenuUI.SetActive(false);
        }

        // Metodo para o botão de fechar as opções
        public void CloseSettings()
        {
            settingsMenu.SetActive(false);
            pauseMenuUI.SetActive(true);
        }

        // Metodo para mostrar o HUD quando for chamado no dialogo
        [YarnCommand("showHUD")]
        public void ShowHUD()
        {
            hudMenu.SetActive(true);
        }
    }
}