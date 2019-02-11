using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BossHealthManager : MonoBehaviour
    {
        //variaveis para a vida do Boss
        private int health;
        public int currentHealth;

        //Componentes do boss
        private SpriteRenderer boss_SpriteRenderer;
        private BoneDragonController boss;
        private Animator anim;

        // Buffer da cor da sprite do Boss
        private Color boss_buffer;

        //variavel para saber se ele ta levando dano, para não levar 2 vezes
        public bool takingDamage;
        
        // Use this for initialization
        void Start()
        {
            health = 18;
            currentHealth = health;
            anim = GetComponent<Animator>();
            boss = GetComponent<BoneDragonController>();
            boss_SpriteRenderer = GetComponent<SpriteRenderer>();
            boss_buffer = boss_SpriteRenderer.color;
            takingDamage = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentHealth > 12 && currentHealth <= 18)
            {
                boss.phase = 1;
            }
            else if (currentHealth > 6 && currentHealth <= 12)
            {
                boss.phase = 2;
            }
            else if (currentHealth > 0 && currentHealth <= 6)
            {
                boss.phase = 3;
            }
            else if (currentHealth <= 0)
            {
                boss.died = true;
                StartCoroutine(BossDeath());
            }
        }

        public void HurtEnemy(int damage, bool iceblock)
        {
            //Essa variavel foi colocada para não tomar dano 2 vezes seguidas
            if (!takingDamage)
            {
                takingDamage = true;
                SoundManagerScript.PlaySound("dragonHit");
                currentHealth -= damage;

                //Se o Boss levar dano do IceBlock então não fazer a animação de ter levado dano
                if (iceblock)
                {
                    StartCoroutine(Wait(1f, true));
                }
                //Caso contrario faça a animação
                else
                {
                    StartCoroutine(Wait(1f, false));
                }
            }
            else
            {
                return;
            }
        }

        public IEnumerator Wait(float time, bool iceblock)
        {
            boss_SpriteRenderer.color = Color.red;
            //Se não foi o bloco de gelo então faça animação
            if (!iceblock)
                anim.SetBool("TakingDamage", true);

            yield return new WaitForSeconds(time);
            anim.SetBool("TakingDamage", false);
            boss_SpriteRenderer.color = boss_buffer;
            takingDamage = false;
        }

        public IEnumerator BossDeath()
        {
            anim.SetBool("Dead", true);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}