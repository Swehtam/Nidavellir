using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BossAirAttack : MonoBehaviour
    {
        private PlayerHealthManager player;
        private bool hitedPlayer;

        private void Start()
        {
            player = FindObjectOfType<PlayerHealthManager>();
            hitedPlayer = false;
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!hitedPlayer)
            {
                if (!col.isTrigger && col.CompareTag("Player"))
                {
                    player.HurtPlayer(1);
                    hitedPlayer = true;
                }
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (!col.isTrigger && col.CompareTag("Player"))
            {
                hitedPlayer = false;
            }
        }
    }
}