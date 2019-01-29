using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class MoveToCollider : MonoBehaviour
    {
        //colisão para ele sair da cena do jooj quando chegar no ponto desejado
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger != true)
            {
                //Quando Rök chegar no collider destruir o collider para ter menos objetos na cena
                if (col.name.Equals("Rök") && gameObject.name.Equals("NearGate"))
                {
                    col.GetComponent<Rigidbody2D>().isKinematic = true;
                }

                //Quando Skeleton chegar no collider destruir o collider para ter menos objetos na cena e destruir o Skeleton
                if (col.name.Equals("Skeleton") && gameObject.name.Equals("SkeletonFinalPoint"))
                {
                    StartCoroutine(DestroyCollider());
                    Destroy(col.gameObject);
                }
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if(!gameObject.name.Equals("NearGate"))
            {
                //Quando Volstagg sair do collider mudar sua velocidade e destruir o collider para ter menos objetos na cena
                if (col.name.Equals("Volstagg") && gameObject.name.Equals("VolstaggInitialPoint"))
                {
                    col.GetComponent<PlayerControl>().moveSpeed = 5;
                    StartCoroutine(DestroyCollider());
                }
                if (col.name.Equals("Rök") && gameObject.name.Equals("RökInitialPoint"))
                {
                    col.GetComponent<BeowulfController>().moveSpeed = 4;
                    StartCoroutine(DestroyCollider());
                }
                if (col.name.Equals("Skeleton") && gameObject.name.Equals("SkeletonInitialPoint"))
                {
                    StartCoroutine(DestroyCollider());
                }
            }

        }

        /*private void OnTriggerStay2D(Collider2D col)
        {
            if (col.name.Equals("Volstagg") && gameObject.name.Equals("VolstaggInitialPoint"))
            {
                col.GetComponent<PlayerControl>().moveSpeed = 5;
            }
            if (col.name.Equals("Rök") && gameObject.name.Equals("RökInitialPoint"))
            {
                col.GetComponent<BeowulfController>().moveSpeed = 4;
            }
        }*/

        public IEnumerator DestroyCollider()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}

