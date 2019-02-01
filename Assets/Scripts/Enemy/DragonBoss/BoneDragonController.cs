﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BoneDragonController : MonoBehaviour
    {
        public float moveSpeed;
        public float attackCoolDown;
        public bool shoot;
        public GameObject fireball;
        public Transform firePoint;
        public float direction;
        public int phase;

        //private bool dragonMoving;
        private Rigidbody2D dragonRB;
        private Animator anim;
        private bool dragonFireball;

        //private float attackTime = 0.5f;
        //private float coolDown;
        //private float attackTimeCoolDown;

        void Start()
        {
            dragonRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            //coolDown = 0f;
            dragonFireball = true;
            phase = 1;
            direction = -1f;
        }

        private void FixedUpdate()
        {
            // Para o inimigo não ser empurrado e continuar deslizando com a inercia
            dragonRB.velocity = new Vector2(0f, 0f);

            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
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
                //anim.SetBool("StartFlying", true);


                anim.SetFloat("FacingX", direction);
            }
            else if (phase == 2)
            {
                if (dragonFireball)
                {
                    if (shoot)
                    {
                        GameObject clone = (GameObject)Instantiate(fireball, firePoint.position, Quaternion.identity, gameObject.transform);
                        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.0f, 0.0f) * 100);
                        dragonFireball = false;
                    }
                    anim.SetBool("FireballAttack", dragonFireball);
                }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                
            }
        }
    }
}