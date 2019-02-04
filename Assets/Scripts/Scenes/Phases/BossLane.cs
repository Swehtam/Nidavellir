using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BossLane : MonoBehaviour
    {
        private BoneDragonController boss;
        
        // Start is called before the first frame update
        void Start()
        {
            boss = FindObjectOfType<BoneDragonController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.isTrigger && col.name.Equals("Volstagg"))
            {
                if (gameObject.name.Equals("TopLane"))
                {
                    boss.volstaggLane = 1;
                }
                if (gameObject.name.Equals("MidLane"))
                {
                    boss.volstaggLane = 2;
                }
                if (gameObject.name.Equals("BotLane"))
                {
                    boss.volstaggLane = 3;
                }
            }
            
        }
    }
}

