using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class PlayerControl : MonoBehaviour
    {
        //array para guardar todos os pontos que Volstagg pode andar durante dialogo
        [System.Serializable]
        public struct MoveToInfo
        {
            public string name;
            public GameObject moveTo;
        }
        public MoveToInfo[] pointsToMove;

        //Variaveis de velocidade de Volstagg
        public float moveSpeed;
        public bool canRun;

        //Componentes de Volstagg
        private Rigidbody2D myRB;
        private Animator anim;

        //Variaveis para o movimento de Volstagg
        private Vector3 moveInput;
        private GameObject point;
        private bool playerMoving;
        private Vector2 lastMove;
        private bool dialogueMove;

        //Tempo q Vosltagg leva para fazer a animação
        //Variaveis para o ataque dele
        private readonly float attackTime = 0.55f;
        private float attackCoolDown;
        private bool playerAttacking;
        
        // Use this for initialization
        void Start()
        {
            canRun = true;
            myRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //Para não fazer nenhum animação quando estiver pausado
            if (Time.timeScale == 0)
                return;

            //Remove o controle do player caso o Dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                //Se estiver rodando o dialogo e o player precisar andar
                if (dialogueMove)
                {
                    //vetor para saber qual a posição do ponto para o player
                    lastMove = new Vector2(point.transform.position.x - transform.position.x, point.transform.position.y - transform.position.y);

                    anim.SetFloat("MoveX", lastMove.x);
                    anim.SetFloat("MoveY", lastMove.y);
                    anim.SetFloat("LastMoveX", lastMove.x);
                    anim.SetFloat("LastMoveY", lastMove.y);
                }
                
                anim.SetBool("PlayerMoving", dialogueMove);

                //Retorna pois ainda tem o dialogo rodando
                return;
            }

            playerMoving = false;

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && canRun)
            {
                moveSpeed = 5f;
            }
            else
            {
                moveSpeed = 2f;
            }

            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput = moveInput * moveSpeed;

            if (moveInput.x != 0 || moveInput.y != 0)
            {
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetMouseButtonDown(0) && playerAttacking == false)
            {
                attackCoolDown = attackTime;
                playerAttacking = true;
                SoundManagerScript.PlaySound("volAttack-slash");
            }

            if (attackCoolDown > 0)
            {
                attackCoolDown -= Time.deltaTime;
            }

            if (attackCoolDown <= 0)
            {
                playerAttacking = false;
            }

            if (!playerAttacking)
            {
                anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                anim.SetBool("PlayerMoving", playerMoving);
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);
            }
            
            anim.SetBool("PlayerAttacking", playerAttacking);
        }

        private void FixedUpdate()
        {
            //Para os controles do player caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                myRB.velocity = new Vector2(0f, 0f);
                //Caso o dialogo esteja acontecendo e o player precise mover para algum lugar
                if (dialogueMove)
                {
                    transform.position = Vector2.MoveTowards(transform.position, point.transform.position, moveSpeed * Time.deltaTime);

                    //Quando chegar para de mover
                    if (transform.position == point.transform.position)
                    {
                        dialogueMove = false;
                    }
                }
                //Retorna pois ainda esta acontecendo o dialogo
                return;
            }

            //Mover o player atraves do teclado caso nao tenha dialogo
            myRB.velocity = moveInput;
        }

        //Metodo para rodas as animações de Volstagg de acordo com dialogo
        [YarnCommand("setAnimation")]
        public void PlayAnimation(string animationName)
        {
            if(animationName == "FaceLeft")
            {
                anim.SetFloat("LastMoveX", -1.0f);
                anim.SetFloat("LastMoveY", 0f);
            }
            else if (animationName == "FaceRight")
            {
                anim.SetFloat("LastMoveX", 1.0f);
                anim.SetFloat("LastMoveY", 0f);
            }
            else if (animationName == "FaceDown")
            {
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", -1.0f);
            }
            else if (animationName == "FaceUp")
            {
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", 1.0f);
            }
        }

        //metodo para fazer Volstagg andar até um ponto especifico durante o dialogo
        [YarnCommand("moveTo")]
        public void MovePoint(string pointName)
        {
            moveSpeed = 2f;
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
