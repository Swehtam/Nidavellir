using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BeowulfController : MonoBehaviour
    {
        //array para guardar todos os pontos que Rök pode andar e variavel para velocidade
        [System.Serializable]
        public struct MoveToInfo
        {
            public string name;
            public Transform moveTo;
        }
        public MoveToInfo[] pointsToMove;
        public float moveSpeed;
        
        //Componentes de Rök
        private Animator anim;
        private Rigidbody2D rökRB;

        //Variaveis para fazer Rök se mexer
        private float xDir;
        private float yDir;
        private Transform point;
        private bool dialogueMove = false;

        // Use this for initialization
        void Start()
        {
            rökRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            rökRB.velocity = new Vector2(0f, 0f);
            //Para os controles de Beowulf caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                if (dialogueMove)
                {
                    transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
                    if (transform.position == point.position)
                    {
                        point = null;
                        dialogueMove = false;
                    }
                }
                return;
            }

        }

        // Update is called once per frame
        void Update()
        {

            //Para os controles de Rök caso o dialogo esteja acontecendo ou não precise se mover
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                //Se estiver rodando o dialogo e o player precisar andar
                if (dialogueMove)
                {
                    //vetor para saber qual a posição do ponto para o player
                    xDir = point.position.x - transform.position.x;
                    yDir = point.position.y - transform.position.y;

                    anim.SetFloat("MoveX", xDir);
                    anim.SetFloat("MoveY", yDir);
                    anim.SetFloat("LastMoveX", xDir);
                    anim.SetFloat("LastMoveY", yDir);
                }

                anim.SetBool("RökMoving", dialogueMove);

                //Retorna pois ainda tem o dialogo rodando
                return;
            }
        }

        //metodo para fazer Rök andar até um ponto especifico nos arquivos .yarn
        [YarnCommand("moveTo")]
        public void MovePoint(string pointName)
        {
            Transform p = null;
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
            else
            {
                //se achar coloque o point para onde ele deva ir no objeto responsavel por isso
                point = p;
                dialogueMove = true;
            }
        }

        //Metodo para rodar as animações de Rök de acordo com dialogo
        [YarnCommand("setAnimation")]
        public void PlayAnimation(string animationName)
        {  
            if (animationName == "FaceRight")
            {
                anim.SetFloat("LastMoveX", 1.0f);
                anim.SetFloat("LastMoveY", 0f);
            }
            if (animationName == "FaceLeft")
            {
                anim.SetFloat("LastMoveX", -1.0f);
                anim.SetFloat("LastMoveY", 0f);
            }
            else if (animationName == "FaceUp")
            {
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", 1.0f);
            }
            else if (animationName == "FaceDown")
            {
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", -1.0f);
            }
            else if (animationName == "Attack")
            {
                anim.SetBool("RökAttacking", true);
                SoundManagerScript.PlaySound("rokAttackingGate");
            }
            else if (animationName == "StopAttack")
            {
                anim.SetBool("RökAttacking", false);
            }
            else if (animationName == "UnFrozen")
            {
                anim.SetBool("Freeze", false);
                anim.SetBool("KeepFrozen", false);
            }
            else if (animationName == "RunningForAttack")
            {
                anim.SetBool("RunningForAttack", true);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.isTrigger && col.CompareTag("Boss"))
            {
                anim.SetBool("RunningForAttack", false);
                anim.SetBool("Freeze", true);
                anim.SetBool("KeepFrozen", true);
            }
        }
    }
}