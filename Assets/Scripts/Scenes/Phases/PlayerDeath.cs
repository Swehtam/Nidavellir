using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yarn.Unity.Example
{
    public class PlayerDeath : MonoBehaviour
    {
        public GameObject player;
        public static int deaths = 0;
        private static string scene = "";

        void Start()
        {
            string actualScene;
            actualScene = SceneManager.GetActiveScene().name;

            if (actualScene.Equals(scene))
            {
                deaths += 1;
                if (scene.Equals("Scene2D"))
                {
                    if(deaths == 1)
                    {
                        FindObjectOfType<DialogueRunner>().StartDialogue("Phase1.Death1");
                    }
                    else
                    {
                        FindObjectOfType<DialogueRunner>().StartDialogue("Phase1.Death2");
                    }
                }
                else if (scene.Equals("BossPhase"))
                {

                }
            }
            else
            {
                deaths = 0;
                scene = actualScene;
                if (scene.Equals("Scene2D"))
                {
                    FindObjectOfType<DialogueRunner>().StartDialogue("Phase1");
                }
                else if (scene.Equals("BossPhase"))
                {
                    //FindObjectOfType<DialogueRunner>().StartDialogue("Phase4");
                }
            }
        }

        void Update()
        {
            if (player)
            {
                if (player.gameObject.GetComponent<PlayerHealthManager>().died == true)
                {
                    LoadingScreenManager.LoadScene("DeathScene");
                }
            }
        }

        [YarnCommand("changeScene")]
        public void ChangeScene(string command)
        {
            LoadingScreenManager.LoadScene(command);
        }

        [YarnCommand("changeNode")]
        public void ChangeNode(string command)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue(command);
        }
    }
}