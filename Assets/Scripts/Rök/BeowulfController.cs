using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BeowulfController : MonoBehaviour
    {

        private Animator anim;
        private bool beowulfMoving;
        private Vector2 direction;
        private Rigidbody2D beowulfRB;
        private float xDir;
        private float yDir;
        private GameObject point;
        private bool timeToMove;

        //array para guardar todos os pontos que Rök pode andar
        [System.Serializable]
        public struct MoveToInfo
        {
            public string name;
            public GameObject moveTo;
        }

        public MoveToInfo[] pointsToMove;
        public float moveSpeed;

        // Use this for initialization
        void Start()
        {
            beowulfRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            timeToMove = false;
        }

        private void FixedUpdate()
        {
            //Para os controles de Beowulf caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true && !timeToMove)
            {
                return;
            }

            if (beowulfMoving)
            {
                beowulfRB.velocity = new Vector2(0f, 0f);
                transform.position = Vector2.MoveTowards(transform.position, point.transform.position, moveSpeed * Time.deltaTime);
            }

        }

        // Update is called once per frame
        void Update()
        {

            //Para os controles de Beowulf caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true && !timeToMove)
            {
                return;
            }

            //vetor para saber qual a posição do inimigo para o player
            beowulfMoving = true;
            xDir = point.transform.position.x - transform.position.x;
            yDir = point.transform.position.y - transform.position.y;

            anim.SetFloat("MoveX", xDir);
            anim.SetFloat("MoveY", yDir);
            anim.SetFloat("LastMoveX", xDir);
            anim.SetFloat("LastMoveY", yDir);
            anim.SetBool("EnemyMoving", beowulfMoving);
        }

        //colisão para ele sair da cena do jooj quando chegar no ponto desejado
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Finish")
                gameObject.SetActive(false);
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
            timeToMove = true;
        }
    }
}