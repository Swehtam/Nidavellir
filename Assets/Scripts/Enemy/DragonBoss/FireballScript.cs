using System.Collections;

using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class FireballScript : MonoBehaviour
    {
        //variavel para saber se atingiu o bloco de gelo, variavel sendo utilizada no script IceBlockHidding
        public bool iceBlockHit;

        //Componentes da Fireball
        private Animator anim;
        private Rigidbody2D fireRB;

        //Outros game objects
        private BoneDragonController dragon;
        private PlayerHealthManager playerHealth;

        //Variaveis de controle da Fireball
        private readonly float timeToDestroy = 3f;
        private float cooldown;
        private bool fbCreated;
        private readonly int damage = 1; 

        // Start is called before the first frame update
        void Start()
        {
            iceBlockHit = false;
            fireRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            fbCreated = true;
            dragon = FindObjectOfType<BoneDragonController>();
            playerHealth = FindObjectOfType<PlayerHealthManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (fbCreated)
            {
                cooldown = timeToDestroy;
                fbCreated = false;
                anim.SetFloat("FireballDirection", dragon.direction);
            }

            if (iceBlockHit)
            {
                StartCoroutine(DestroyFireball());
            }
            else
            {
                cooldown -= Time.deltaTime;

                if (cooldown <= 0f)
                {
                    StartCoroutine(DestroyFireball());
                }
            }
            
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.isTrigger && col.CompareTag("Player") && col.name.Equals("Volstagg"))
            {
                playerHealth.HurtPlayer(damage);
                playerHealth.SlowPlayer();
                StartCoroutine(DestroyFireball());
                SoundManagerScript.PlaySound("fireballImpact");
            }
            else if (!col.isTrigger && col.CompareTag("Player"))
            {
                StartCoroutine(DestroyFireball());
                SoundManagerScript.PlaySound("fireballImpact");
            }
        }

        private IEnumerator DestroyFireball()
        {
            fireRB.velocity = new Vector2(0f, 0f);
            anim.SetBool("StopFireball", true);
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }
    }
}