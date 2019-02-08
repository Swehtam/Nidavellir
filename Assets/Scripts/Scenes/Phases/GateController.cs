using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class GateController : MonoBehaviour
    {
        public KeyController key;
        private Animator anim;
        public BoxCollider2D childCollider;

        // Use this for initialization
        void Start()
        {
            key = FindObjectOfType<KeyController>();
            anim = GetComponent<Animator>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (key.open)
            {
                if (col.gameObject.tag == "Player")
                {
                    FindObjectOfType<DialogueRunner>().StartDialogue("Gate");
                }
            }
        }

        public IEnumerator Open()
        {
            SoundManagerScript.PlaySound("gate-opening");
            anim.SetBool("GotKey", true);
            yield return new WaitForSeconds(1.0f);
            anim.SetBool("IsOpen", true);
            childCollider.isTrigger = true;
            
        }

        public IEnumerator Close()
        {
            SoundManagerScript.PlaySound("gate-opening");
            anim.SetBool("IsOpen", false);
            yield return new WaitForSeconds(1.0f);
            anim.SetBool("GotKey", false);
            childCollider.isTrigger = false;
        }

        [YarnCommand("animation")]
        public void PlayAnimation(string animationName)
        {
            if (animationName.Equals("open"))
            {
                StartCoroutine(Open());
            }
            else if (animationName.Equals("close"))
            {
                StartCoroutine(Close());
            }
        }
    }
}

