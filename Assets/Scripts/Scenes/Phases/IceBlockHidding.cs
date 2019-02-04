using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class IceBlockHidding : MonoBehaviour
    {
        private bool isVolstaggHidding;
        private bool destroyed;
        //private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            //anim = GetComponent<Animator>();
            isVolstaggHidding = false;
            destroyed = false;
        }

        void OnTriggerStay2D(Collider2D col)
        {
            //Caso o bloco de gelo seja destruido, não é mais possivel Volstagg se esconder nele
            if (!destroyed)
            {
                if (!col.isTrigger && col.name.Equals("Volstagg"))
                {
                    isVolstaggHidding = true;
                }
            }
                
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (!col.isTrigger && col.name.Equals("Volstagg"))
            {
                isVolstaggHidding = false;
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger && col.CompareTag("Boss"))
            {
                if (isVolstaggHidding)
                {
                    //Caso o boss bata no bloco de gelo
                    if (col.name.Equals("BoneDragon"))
                    {
                        col.GetComponent<BossHealthManager>().HurtEnemy(1, true);
                        StartCoroutine(DestroyIceBlock());
                    }
                    //Caso seja a fireball
                    else
                    {
                        col.GetComponent<FireballScript>().iceBlockHit = true;
                        StartCoroutine(DestroyIceBlock());
                    }
                }
            }
        }

        public IEnumerator DestroyIceBlock()
        {
            destroyed = true;
            isVolstaggHidding = false;
            //colocar a animação dele quebrando
            //anim.SetBool("Destroy", true);
            yield return new WaitForSeconds(1f);
            //colocar a animação dele quebrado
            //anim.SetBool("Destroy", false);
        }
    }
}