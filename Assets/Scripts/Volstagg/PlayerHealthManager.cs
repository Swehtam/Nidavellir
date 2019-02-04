﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yarn.Unity.Example
{
    public class PlayerHealthManager : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;

        private PlayerControl player;

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

        // Variavel sendo usada em outro script para poder mudar para cena de morte
        public bool died;

        // Use this for initialization
        void Start()
        {
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
            if (currentHealth <= 0)
            {
                //Se Volstagg morrer vai fazer com que outro Script chame a mudança de cena
                died = true;
                SoundManagerScript.PlaySound("volstagg-death");
                gameObject.SetActive(false);
            }
        }

        //Dar dano no player
        public void HurtPlayer(int damage)
        {
            currentHealth -= damage;
            SoundManagerScript.PlaySound("volstagg-grunt");
            StartCoroutine(WaitDamage(0.5f));
        }

        //Curar no player
        public void CurePlayer(int cure)
        {
            currentHealth += cure;

            if (currentHealth > 5)
                currentHealth = 5;

            StartCoroutine(WaitCure(0.5f));
        }

        public void SlowPlayer()
        {
            player.canRun = false;
            StartCoroutine(WaitSlow(2f));
        }

        //Fazer com que tudo fique vermelho se Volstagg tomar dano
        public IEnumerator WaitDamage(float time)
        {
            player_SpriteRenderer.color = Color.red;
            shield_SpriteRenderer.color = Color.red;
            arm_SpriteRenderer.color = Color.red;

            yield return new WaitForSeconds(time);

            player_SpriteRenderer.color = player_buffer;
            shield_SpriteRenderer.color = shield_buffer;
            arm_SpriteRenderer.color = arm_buffer;
        }

        //Fazer com que tudo de Volstagg fique verde ai pegar um coração
        public IEnumerator WaitCure(float time)
        {
            player_SpriteRenderer.color = Color.green;
            shield_SpriteRenderer.color = Color.green;
            arm_SpriteRenderer.color = Color.green;

            yield return new WaitForSeconds(time);

            player_SpriteRenderer.color = player_buffer;
            shield_SpriteRenderer.color = shield_buffer;
            arm_SpriteRenderer.color = arm_buffer;
        }

        public IEnumerator WaitSlow(float time)
        {
            player_SpriteRenderer.color = Color.blue;
            shield_SpriteRenderer.color = Color.blue;
            arm_SpriteRenderer.color = Color.blue;

            yield return new WaitForSeconds(time);

            player_SpriteRenderer.color = player_buffer;
            shield_SpriteRenderer.color = shield_buffer;
            arm_SpriteRenderer.color = arm_buffer;
            player.canRun = true;
        }
    }
}