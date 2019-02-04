using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class HController : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                SoundManagerScript.PlaySound("pickup-heart");
                this.gameObject.transform.position = new Vector3(99f, 99f, -0.278f);
                col.gameObject.GetComponent<PlayerHealthManager>().CurePlayer(1);
            }
        }
    }
}