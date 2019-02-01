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
        private SpriteRenderer floorSR;

        // Start is called before the first frame update
        void Start()
        {
            playerHealth = FindObjectOfType<PlayerHealthManager>();
            dragon = FindObjectOfType<BoneDragonController>();
            floorSR = GetComponent<SpriteRenderer>();
            isBroken = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(dragon.phase == 1)
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
            floorSR.sprite = holeSprite;
            //Animação do buraco abrindo
            yield return new WaitForSeconds(1f);
        }
    }
}
