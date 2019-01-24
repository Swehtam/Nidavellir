using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class GateController : MonoBehaviour
    {
        public GameObject key;
        private Animator anim;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (KeyController.open)
            {
                if (col.gameObject.tag == "Player")
                {
                    StartCoroutine(Open());
                }
            }
        }

        public IEnumerator Open()
        {
            SoundManagerScript.PlaySound("gate-opening");
            anim.SetBool("GotKey", true);
            yield return new WaitForSeconds(1.0f);
            anim.SetBool("IsOpen", true);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        public IEnumerator Close()
        {
            SoundManagerScript.PlaySound("gate-opening");
            anim.SetBool("IsOpen", false);
            yield return new WaitForSeconds(1.0f);
            anim.SetBool("GotKey", false);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        [YarnCommand("animation")]
        public void PlayAnimation(string animationName)
        {
            if (animationName == "open")
            {
                StartCoroutine(Open());
            }
            else if (animationName == "close")
            {
                StartCoroutine(Close());
            }
        }
    }
}

