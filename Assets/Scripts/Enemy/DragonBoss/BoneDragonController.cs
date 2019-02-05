using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BoneDragonController : MonoBehaviour
    {
        public float moveSpeed;
        public float attackCoolDown;
        public GameObject fireball;
        public Transform firePoint;
        public float direction;
        public int phase;
        public bool shoot;

        //Fazendo referencia ao Script BossLane
        public int volstaggLane;

        //Variavel para saber se o boss morreu, ela ta sendo usado no script BossHealthManager
        public bool died;

        //private bool dragonMoving;
        private Rigidbody2D dragonRB;
        private Animator anim;
        private GameObject point;

        private readonly float attackTime = 2f;
        private float coolDown;
        private float attackTimeCoolDown;
        private bool attackingFireball;
        private bool fbCreated;

        void Start()
        {
            point = FindObjectOfType<PlayerControl>().gameObject;
            dragonRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            coolDown = 0f;
            attackingFireball = false;
            died = false;
            phase = 2;
            direction = 1f;
            shoot = false;
            fbCreated = false;
        }

        private void FixedUpdate()
        {
            // Para o inimigo não ser empurrado e continuar deslizando com a inercia
            dragonRB.velocity = new Vector2(0f, 0f);
            if (died)
            {
                //alterara a variavel que será usada na movimentação do boss, para ele mesmo, ou seja, ele vai parar aonde estiver
            }

            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
            }
            if(phase == 1)
            {

            }
            else if (phase == 2)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, point.transform.position.y + 2f), moveSpeed * Time.deltaTime);
            }
            else if (phase == 3)
            {

            }
            /*if (dragonRB)
            /{
                transform.position = Vector2.MoveTowards(transform.position, //Algum Ponto, moveSpeed * Time.deltaTime);
            }*/

        }

        void Update()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
            }

            if (phase == 1)
            {
                //boss ja vai estar voando por causa do dialogo então não precisa disso aqui
                //anim.SetBool("StartFlying", true);


                anim.SetFloat("FacingX", direction);
            }
            else if (phase == 2)
            {
                if (shoot && !fbCreated)
                {
                    fbCreated = true;
                    GameObject clone = (GameObject)Instantiate(fireball, firePoint.position, Quaternion.identity);
                    clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 0.0f) * 200);
                }

                if (coolDown <= 0)
                {
                    attackingFireball = true;
                    coolDown = attackCoolDown;
                    attackTimeCoolDown = attackTime;
                }

                if (coolDown > 0)
                {
                    attackTimeCoolDown -= Time.deltaTime;
                    coolDown -= Time.deltaTime;
                }

                if (attackTimeCoolDown < 0)
                {
                    attackingFireball = false;
                    fbCreated = false;
                }

                anim.SetBool("FireballAttack", attackingFireball);
                anim.SetFloat("FacingX", direction);
            }
            else if (phase == 3)
            {

            }

            /*enemyMoving = false;

            if (!enemyAttacking)
            {
                //se o personagem entrar na area de ameaça do inimigo, o inimigo irá segui-lo
                if (Vector2.Distance(thePlayer.transform.position, transform.position) <= maxRange || seeker)
                {
                    seeker = true;
                    enemyMoving = true;

                    //coordenadas para saber a direção que o player se encontra do inimigo
                    xDir = thePlayer.transform.position.x - transform.position.x;

                    //caso o inimigo fique muito proximo, faço o parar na frente do player e olhar na direção dele
                    //isso aqui foi feito para o inimigo n ficar empurrando o player para fora do mapa
                    if (Vector2.Distance(thePlayer.transform.position, transform.position) <= minRange)
                    {
                        enemyMoving = false;

                        anim.SetFloat("LastMoveX", xDir);
                    }
                    else
                    {
                        anim.SetFloat("MoveX", xDir);
                        anim.SetFloat("MoveY", yDir);
                    }

                    anim.SetBool("EnemyMoving", enemyMoving);
                }
            }

            if (Vector2.Distance(thePlayer.transform.position, transform.position) <= minRange && coolDown <= 0)
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

            anim.SetBool("EnemyAttacking", enemyAttacking);*/
        }
    }
}