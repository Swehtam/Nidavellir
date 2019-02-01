using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class FireballScript : MonoBehaviour
    {
        //Componente da Fireball
        private Animator anim;

        //Outros game objects
        private BoneDragonController dragon;
        private PlayerHealthManager playerHealth;

        //Variaveis de controle da Fireball
        private readonly float timeToDestroy = 3f;
        private float cooldown;
        private bool fbCreated;
        private readonly int damage = 2; 

        // Start is called before the first frame update
        void Start()
        {
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

            cooldown -= Time.deltaTime;

            if (cooldown <= 0f)
            {
                StartCoroutine(DestroyFireball());
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger != true && col.CompareTag("Player"))
            {
                playerHealth.HurtPlayer(damage);
                StartCoroutine(DestroyFireball());
            }
        }

        private IEnumerator DestroyFireball()
        {
            anim.SetBool("StopFireball", true);
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }
    }
}