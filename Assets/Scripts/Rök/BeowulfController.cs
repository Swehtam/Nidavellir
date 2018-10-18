using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BeowulfController : MonoBehaviour
    {

        public float moveSpeed;
        private Animator anim;
        private bool beowulfMoving = true;
        private Vector2 direction;
        private Rigidbody2D beowulfRB;
        private float xDir;
        private float yDir;

        public GameObject point;

        // Use this for initialization
        void Start()
        {
            beowulfRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            //Para os controles de Beowulf caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
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
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
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
    }
}