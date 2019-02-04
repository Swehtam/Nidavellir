using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class PlayerAttack : MonoBehaviour
    {
        public int dmg;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.isTrigger && collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(dmg);
            }
            else if (!collision.isTrigger && collision.CompareTag("Boss") && collision.name.Equals("BoneDragon"))
            {
                collision.GetComponent<BossHealthManager>().HurtEnemy(dmg, false);
            }
        }
    }
}