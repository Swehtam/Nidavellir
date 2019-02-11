using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class EnemyAttackTrigger : MonoBehaviour
    {
        public int dmg;
        public bool isSkeleton;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.isTrigger != true && collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(dmg);
                if(isSkeleton)
                    SoundManagerScript.PlaySound("sword-slash-attack");
                if(!isSkeleton)
                    SoundManagerScript.PlaySound("wolfAttack");
            }
        }
    }
}

