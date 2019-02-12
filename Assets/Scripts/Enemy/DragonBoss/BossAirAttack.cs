using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BossAirAttack : MonoBehaviour
    {
        private PlayerHealthManager player;
        private bool hitedPlayer;
        private BoneDragonController boss;

        private void Start()
        {
            player = FindObjectOfType<PlayerHealthManager>();
            hitedPlayer = false;
            boss = FindObjectOfType<BoneDragonController>();
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!hitedPlayer)
            {
                if (!col.isTrigger && col.CompareTag("Player") && col.name.Equals("Volstagg") && boss.airStriking)
                {
                    player.HurtPlayer(1);
                    SoundManagerScript.PlaySound("dragonFlyBy");
                    hitedPlayer = true;
                }
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (!col.isTrigger && col.CompareTag("Player") && col.name.Equals("Volstagg"))
            {
                SoundManagerScript.PlaySound("dragonFlyBy");
                hitedPlayer = false;
            }
        }
    }
}