using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class IceBlockHidding : MonoBehaviour
    {
        public bool isVolstaggHidding;
        private IceBlockBreaking iceB;
        private PlayerHealthManager player;
        private BoneDragonController boss;

        // Start is called before the first frame update
        void Start()
        {
            iceB = GetComponentInParent<IceBlockBreaking>();
            boss = FindObjectOfType<BoneDragonController>();
            isVolstaggHidding = false;
            player = FindObjectOfType<PlayerHealthManager>();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (!iceB.destroyed)
            {
                if (!col.isTrigger && col.name.Equals("Volstagg"))
                {
                    float playerDirection = col.transform.position.x - transform.position.x;
                    playerDirection = (Mathf.Abs(playerDirection)) / playerDirection;
                    Debug.Log(playerDirection);

                    float bossDirection = (Mathf.Abs(boss.direction)) / boss.direction;
                    Debug.Log(bossDirection);
                    if (playerDirection == bossDirection)
                    {
                        isVolstaggHidding = true;
                        player.dontTakeDamage = true;
                    }
                }
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (!col.isTrigger && col.name.Equals("Volstagg"))
            {
                isVolstaggHidding = false;
                player.dontTakeDamage = false;
            }
        }
    }
}