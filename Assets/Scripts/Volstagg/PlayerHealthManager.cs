using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yarn.Unity.Example
{
    public class PlayerHealthManager : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;
        // Variavel sendo usada em outro script para poder mudar para cena de morte
        public bool died;

        private PlayerControl player;
        private float slowCD;
        private bool normal;

        //Sprite do player
        private SpriteRenderer player_SpriteRenderer;
        //Sprite do shield do player
        private SpriteRenderer shield_SpriteRenderer;
        //Sprite do braço do player
        private SpriteRenderer arm_SpriteRenderer;

        //Buffer da cor do player
        private Color player_buffer;
        //Buffer da cor do braço do player
        private Color arm_buffer;
        //Buffer da cor do shield do player;
        private Color shield_buffer;

        // Use this for initialization
        void Start()
        {
            normal = true;
            slowCD = 0f;
            currentHealth = maxHealth;
            player = GetComponent<PlayerControl>();

            player_SpriteRenderer = GetComponent<SpriteRenderer>();
            player_buffer = player_SpriteRenderer.color;

            shield_SpriteRenderer = transform.Find("Shield").GetComponent<SpriteRenderer>();
            shield_buffer = shield_SpriteRenderer.color;

            arm_SpriteRenderer = transform.Find("Attack-Arm").GetComponent<SpriteRenderer>();
            arm_buffer = arm_SpriteRenderer.color;
        }

        // Update is called once per frame
        void Update()
        {
            if (normal)
            {
                player_SpriteRenderer.color = player_buffer;
                shield_SpriteRenderer.color = shield_buffer;
                arm_SpriteRenderer.color = arm_buffer;
            }
            if (currentHealth <= 0)
            {
                //Se Volstagg morrer vai fazer com que outro Script chame a mudança de cena
                died = true;
                SoundManagerScript.PlaySound("volstagg-death");
                gameObject.SetActive(false);
            }

            if(slowCD >= 0 && !normal)
            {
                player.canRun = false;

                player_SpriteRenderer.color = Color.blue;
                shield_SpriteRenderer.color = Color.blue;
                arm_SpriteRenderer.color = Color.blue;

                slowCD -= Time.deltaTime;
            }
            else
            {
                player.canRun = true;
                normal = true;
            }
        }

        //Dar dano no player
        //Fazer com que tudo fique vermelho se Volstagg tomar dano
        public void HurtPlayer(int damage)
        {
            normal = false;
            currentHealth -= damage;
            SoundManagerScript.PlaySound("volstagg-grunt");
            StartCoroutine(Wait(0.5f, Color.red));
        }

        //Curar no player
        //Fazer com que tudo de Volstagg fique verde ai pegar um coração
        public void CurePlayer(int cure)
        {
            normal = false;
            currentHealth += cure;

            if (currentHealth > 5)
                currentHealth = 5;

            StartCoroutine(Wait(0.5f, Color.green));
        }

        public void SlowPlayer()
        {
            slowCD = 2f;
            normal = false;
        }

        public IEnumerator Wait(float time, Color color)
        {
            player_SpriteRenderer.color = color;
            shield_SpriteRenderer.color = color;
            arm_SpriteRenderer.color = color;

            yield return new WaitForSeconds(time);
        }
    }
}