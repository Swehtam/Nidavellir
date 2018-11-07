using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class RökIntroduction : MonoBehaviour
    {

        [System.Serializable]
        public struct SidePointsInfo
        {
            public string name;
            public GameObject sidePoint;
        }

        //private Animator anim;

        public SidePointsInfo[] sidePoint;
        // Use this for initialization
        void Start()
        {
            //anim = GetComponent<Animator>();
        }

        [YarnCommand("show")]
        public void SidePosition(string sideName)
        {
            bool sideFound = false;
            //procura o lado que ele deve ficar
            foreach (var info in sidePoint)
            {
                if (info.name == sideName)
                {
                    gameObject.transform.position = info.sidePoint.transform.position;
                    sideFound = true;
                    break;
                }
            }

            //se não achar mandar uma mensagem para o console
            if (!sideFound)
            {
                Debug.LogErrorFormat("Não foi encontrando o sidePoint {0}!", sideName);
                return;
            }
        }
    }
}
