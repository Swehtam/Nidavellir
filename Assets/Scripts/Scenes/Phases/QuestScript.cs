using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace Yarn.Unity.Example
{
    public class QuestScript : MonoBehaviour
    {
        public Text quest;

        private readonly float textSpeed = 0.03f;
        private readonly float eraseSpeed = 0f;
        private bool runnig = false;
        private KeyController keyController;

        private void Start()
        {
            keyController = FindObjectOfType<KeyController>();
        }

        void Update()
        {
            if (!runnig)
            {
                if (!keyController.gotKey)
                {
                    string newQuestText = "Encontre uma maneira de abrir o portão.\n\n Vou lher dar uma dica: começa com 'C' e termina com 'have'.";
                    StartCoroutine(NewQuest(newQuestText));
                }
                else if (keyController.gotKey && !keyController.open)
                {
                    string newQuestText = "Opa, vejo que você tem companhia, é melhor se livrar deles.\n\n  Onda: " + keyController.onda + " de 5\n  Inimigos derrotados: " + keyController.killed +
                                          " de " + keyController.needToKill;

                    if (keyController.killed == 0 && quest.text != newQuestText)
                    {
                        StartCoroutine(NewQuest(newQuestText));
                    }
                    else if (keyController.killed > 0 && keyController.killed < 30)
                    {
                        StartCoroutine(UpdateQuest(newQuestText));
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    string newQuestText = " Para um anão até que você não é ruim.\n\n Agora vá e passe pelo portão, você merece continuar sua jornada.";
                    StartCoroutine(NewQuest(newQuestText));
                }
            }
        }

        private IEnumerator NewQuest(string newQuest)
        {
            if (quest.text.Equals(""))
            {
                runnig = true;
                // Mostrar o texto com um caracter por vez
                var stringBuilder = new StringBuilder();

                foreach (char c in newQuest)
                {
                    stringBuilder.Append(c);
                    quest.text = stringBuilder.ToString();
                    yield return new WaitForSeconds(textSpeed);
                }

                runnig = false;
            }
            else if (quest.text.Equals(newQuest))
            {
                yield return null;
            }
            else
            {
                runnig = true;
                string teste = quest.text;
                for (int i = 0; i < teste.Length; i++)
                {
                    quest.text = quest.text.Substring(0, quest.text.Length - 1);
                    yield return new WaitForSeconds(eraseSpeed);
                }

                runnig = false;
            }

        }

        private IEnumerator UpdateQuest(string updatedQuest)
        {
            if (quest.text.Equals(updatedQuest))
            {
                yield return null;
            }
            else
            {
                quest.text = updatedQuest;
                yield return null;
            }
        }
    }
}
