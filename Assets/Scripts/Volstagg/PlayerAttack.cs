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
            if (collision.isTrigger != true && collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(dmg);
            }
        }
    }
}