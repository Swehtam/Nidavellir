using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class IceBlockBreaking : MonoBehaviour
    {
        public bool destroyed;
        private IceBlockHidding iceB;
        private Animator anim;
        private float direction;

        // Start is called before the first frame update
        void Start()
        {
            iceB = GetComponentInChildren<IceBlockHidding>();
            direction = 0f;
            anim = GetComponent<Animator>();
            destroyed = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger && col.CompareTag("Boss"))
            {
                if (iceB.isVolstaggHidding)
                {
                    //Caso o boss bata no bloco de gelo
                    if (col.name.Equals("BoneDragon"))
                    {
                        direction = col.transform.position.x - gameObject.transform.position.x;
                        col.GetComponent<BossHealthManager>().HurtEnemy(1, true);
                        StartCoroutine(DestroyIceBlock());
                    }
                    //Caso seja a fireball
                    else
                    {
                        direction = col.transform.position.x - gameObject.transform.position.x;
                        col.GetComponent<FireballScript>().iceBlockHit = true;
                        StartCoroutine(DestroyIceBlock());
                    }
                }
            }
        }

        public IEnumerator DestroyIceBlock()
        {
            destroyed = true;
            iceB.isVolstaggHidding = false;
            anim.SetFloat("Direction", direction);
            anim.SetBool("Breaking", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("Breaking", false);
        }
    }
}
