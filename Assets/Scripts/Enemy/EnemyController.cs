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

        private bool seeker = true;
        private float xDir;
        private float yDir;

        private bool enemyMoving;
        private Rigidbody2D enemyRB;
        private Animator anim;
        private PlayerControl thePlayer;

        private float attackTime = 1.0f;
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
        }

        private void FixedUpdate()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
            }
            if (enemyMoving)
            {
                enemyRB.velocity = new Vector2(0f, 0f);
                transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            }

        }

        // Update is called once per frame
        void Update()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
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
    }
}