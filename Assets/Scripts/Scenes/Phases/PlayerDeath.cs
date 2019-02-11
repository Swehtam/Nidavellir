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
        public static string scene = "";
        private LevelChanger levelChanger;

        void Start()
        {
            levelChanger = FindObjectOfType<LevelChanger>();
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
                    if (deaths == 1)
                    {
                        FindObjectOfType<DialogueRunner>().StartDialogue("Phase4.Death1");
                    }
                    else if (deaths == 2)
                    {
                        FindObjectOfType<DialogueRunner>().StartDialogue("Phase4.Death2");
                    }
                    else
                    {
                        FindObjectOfType<DialogueRunner>().StartDialogue("Phase4.Death3");
                    }
                }
            }
            else
            {
                deaths = 0;
                scene = actualScene;
            }
        }

        void Update()
        {
            if (player)
            {
                if (player.gameObject.GetComponent<PlayerHealthManager>().died == true)
                {
                    StartCoroutine(Death());
                }
            }
        }

        [YarnCommand("changeScene")]
        public void ChangeScene(string command)
        {
            levelChanger.FadeToLevel(command);
        }

        [YarnCommand("changeNode")]
        public void ChangeNode(string command)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue(command);
        }

        //Quando Volstagg morrer demorar para ele fazer o barulho de morte
        public IEnumerator Death()
        {
            yield return new WaitForSeconds(3f);
            levelChanger.FadeToLevel("DeathScene");
        }
    }
}