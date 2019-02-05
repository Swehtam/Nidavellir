using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class IceBlockHidding : MonoBehaviour
    {
        public bool isVolstaggHidding;
        private IceBlockBreaking iceB;

        // Start is called before the first frame update
        void Start()
        {
            iceB = GetComponentInParent<IceBlockBreaking>();
            isVolstaggHidding = false;
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (!iceB.destroyed)
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
    }
}