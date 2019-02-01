using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BrokenFloor : MonoBehaviour
    {
        public Sprite holeSprite;

        private PlayerHealthManager playerHealth;
        private BoneDragonController dragon;
        private bool isBroken;
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            playerHealth = FindObjectOfType<PlayerHealthManager>();
            dragon = FindObjectOfType<BoneDragonController>();
            isBroken = false;
            anim = GetComponent<Animator>();
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if(dragon.phase == 2)
            {
                if (!col.isTrigger && col.CompareTag("Player"))
                {
                    if (!isBroken)
                    {
                        StartCoroutine(HoleAnimation());
                    }
                    else
                    {
                        playerHealth.HurtPlayer(5);
                    }

                }
            }
        }

        public IEnumerator HoleAnimation()
        {
            anim.SetBool("FloorBreaking", true);
            yield return new WaitForSeconds(0.6f);
            anim.SetBool("FloorBreaking", false);
            isBroken = true;
        }
    }
}
