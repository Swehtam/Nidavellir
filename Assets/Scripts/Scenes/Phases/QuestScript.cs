using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace Yarn.Unity.Example
{
    public class QuestScript : MonoBehaviour
    {
        //Onde o texto vai ser escrito
        public Text quest;

        //Velocidade do texto apagando e escrevendo
        private readonly float textSpeed = 0.03f;
        private readonly float eraseSpeed = 0f;

        //Variavel para saber se o texto ta sendo escrito ou apagado
        private bool runnig = false;

        //Componentes usados caso esteja na primeira ou na quarta fase
        private KeyController keyController;
        private BoneDragonController boss;

        //Variavel para aparece o tutorial do jogo na primeira fase
        private int tutorial;

        private void Start()
        {
            if (PlayerDeath.scene.Equals("Scene2D"))
            {
                tutorial = 1;
                keyController = FindObjectOfType<KeyController>();
            }
            else if (PlayerDeath.scene.Equals("BossPhase"))
            {
                tutorial = 4;
                boss = FindObjectOfType<BoneDragonController>();
            }
        }

        void Update()
        {
            if (!runnig)
            {
                if (keyController)
                {
                    if (tutorial < 4)
                    {
                        if(tutorial == 1)
                        {
                            string newQuestText = "-------Tutorial-------\n\n Uso as setas do teclado ou os botões W,A,S,D para se movimentar.";
                            StartCoroutine(NewQuest(newQuestText));
                            if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                            {
                                tutorial = 2;
                            }
                        }
                        else if (tutorial == 2)
                        {
                            string newQuestText = "-------Tutorial-------\n\n Segure qualquer um dos Shift's para correr.";
                            StartCoroutine(NewQuest(newQuestText));
                            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            {
                                tutorial = 3;
                            }
                        }
                        else if (tutorial == 3)
                        {
                            string newQuestText = "-------Tutorial-------\n\n Aperte o botão esquerdo do mouse para atacar.";
                            StartCoroutine(NewQuest(newQuestText));
                            if (Input.GetMouseButtonDown(0))
                            {
                                tutorial = 4;
                            }
                        }

                    }
                    else
                    {
                        if (!keyController.gotKey)
                        {
                            string newQuestText = " Parabéns agora você não tem desculpa caso morra. Encontre uma maneira de abrir o portão.\n\n Vou lhe dar uma dica: começa com 'c' e termina com 'have'.";
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
                else if (boss)
                {
                    if(boss.phase == 1)
                    {
                        string newQuestText = " Proteja-se!\n\n Ele vai te dar uma cabeçada!\n Cuiidado para não cair da ponte.";
                        StartCoroutine(NewQuest(newQuestText));
                    }
                    else if (boss.phase == 2)
                    {
                        string newQuestText = " Agora você vai ter que lutar. \n\n Não se preocupe de ser congelado, sorte sua de ser peludo.";
                        StartCoroutine(NewQuest(newQuestText));
                    }
                    else if (boss.phase == 3)
                    {
                        string newQuestText = " IH....\n Agora deu ruim...\n Quer dizer, você vai se sair bem, só desvie de tudo.";
                        StartCoroutine(NewQuest(newQuestText));
                    }
                    else if (boss.phase == 4)
                    {
                        string newQuestText = " Não acredito que você conseguiu...\n\n Meus parabéns!";
                        StartCoroutine(NewQuest(newQuestText));
                    }
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
