using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class EnemyController : MonoBehaviour
    {
        public float moveSpeed;
        public float maxRange;
        public float minRange;
        public float attackCoolDown;

        //Bool para saber se o inimigo é o esqueleto
        public bool isSkeleton;

        //array para guardar todos os pontos que Rök pode andar
        [System.Serializable]
        public struct MoveToInfo
        {
            public string name;
            public GameObject moveTo;
        }
        public MoveToInfo[] pointsToMove;

        //Variaveis para fazer os inimigos se mexerem
        private bool seeker = true;
        private float xDir;
        private float yDir;
        private GameObject point;
        private bool enemyMoving;
        private bool dialogueMove;

        //Componentes dos inimigos
        private Rigidbody2D enemyRB;
        private Animator anim;
        private PlayerControl thePlayer;

        //Variaveis para ataque dos inimigos
        private float attackTime = 0.5f;
        private float coolDown;
        private float attackTimeCoolDown;
        private bool enemyAttacking;

        // Use this for initialization
        void Start()
        {
            enemyRB = GetComponent<Rigidbody2D>();
            thePlayer = FindObjectOfType<PlayerControl>();
            anim = GetComponent<Animator>();
            coolDown = 0f;
            enemyAttacking = false;

            if (isSkeleton)
            {
                attackTime = 0.4f;
            }
        }

        private void FixedUpdate()
        {
            // Para o inimigo não ser empurrado e continuar deslizando com a inercia
            enemyRB.velocity = new Vector2(0f, 0f);

            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                if (dialogueMove)
                {
                    transform.position = Vector2.MoveTowards(transform.position, point.transform.position, moveSpeed * Time.deltaTime);

                    if (transform.position == point.transform.position)
                    {
                        dialogueMove = false;
                    }
                }

                //Retorna pois ainda tem o dialogo rodando
                return;
            }

            //Se nao estiver com dialogo
            if (enemyMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                //Se estiver rodando o dialogo e o player precisar andar
                if (dialogueMove)
                {
                    //vetor para saber qual a posição do ponto para o player
                    xDir = point.transform.position.x - transform.position.x;
                    yDir = point.transform.position.y - transform.position.y;
                }

                anim.SetFloat("MoveX", xDir);
                anim.SetFloat("MoveY", yDir);
                anim.SetFloat("LastMoveX", xDir);
                anim.SetFloat("LastMoveY", yDir);
                anim.SetBool("EnemyMoving", dialogueMove);

                //Retorna pois ainda tem o dialogo rodando
                return;
            }

            enemyMoving = false;

            if (!enemyAttacking)
            {
                //se o personagem entrar na area de ameaça do inimigo, o inimigo irá segui-lo
                if (Vector2.Distance(thePlayer.transform.position, transform.position) <= maxRange || seeker)
                {
                    seeker = true;
                    enemyMoving = true;

                    //coordenadas para saber a direção que o player se encontra do inimigo
                    xDir = thePlayer.transform.position.x - transform.position.x;
                    yDir = thePlayer.transform.position.y - transform.position.y;

                    //caso o inimigo fique muito proximo, faço o parar na frente do player e olhar na direção dele
                    //isso aqui foi feito para o inimigo n ficar empurrando o player para fora do mapa
                    if (Vector2.Distance(thePlayer.transform.position, transform.position) <= minRange)
                    {
                        enemyMoving = false;

                        anim.SetFloat("LastMoveX", xDir);
                        anim.SetFloat("LastMoveY", yDir);
                    }
                    else
                    {
                        anim.SetFloat("MoveX", xDir);
                        anim.SetFloat("MoveY", yDir);
                    }

                    anim.SetBool("EnemyMoving", enemyMoving);
                }
            }

            if(Vector2.Distance(thePlayer.transform.position, transform.position) <= minRange && coolDown <= 0)
            {
                coolDown = attackCoolDown;
                attackTimeCoolDown = attackTime;
                enemyAttacking = true;
            }

            if (coolDown > 0)
            {
                attackTimeCoolDown -= Time.deltaTime;
                coolDown -= Time.deltaTime;
            }

            if (attackTimeCoolDown <= 0)
            {
                enemyAttacking = false;
            }

            anim.SetBool("EnemyAttacking", enemyAttacking);
        }

        //metodo para fazer Rök andar até um ponto especifico nos arquivos .yarn
        [YarnCommand("moveTo")]
        public void MovePoint(string pointName)
        {
            GameObject p = null;
            //procura o ponto para onde irá se mover dentro do array
            foreach (var info in pointsToMove)
            {
                if (info.name == pointName)
                {
                    p = info.moveTo;
                    break;
                }
            }

            //se não achar mandar uma mensagem para o console
            if (p == null)
            {
                Debug.LogErrorFormat("Não foi encontrando o point {0}!", pointName);
                return;
            }

            //se achar coloque o point para onde ele deva ir no objeto responsavel por isso
            point = p;
            dialogueMove = true;
        }
    }
}