using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class MoveToCollider : MonoBehaviour
    {
        public bool stop = false;
        //colisão para ele sair da cena do jooj quando chegar no ponto desejado
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger != true)
            {
                //Quando Volstagg chegar no collider destruir o collider para ter menos objetos na cena
                if (col.name.Equals("Volstagg") && gameObject.name.Equals("VolstaggInitialPoint"))
                {
                    col.GetComponent<PlayerControl>().moveSpeed = 5;
                    stop = true;
                    StartCoroutine(DestroyCollider());
                }

                //Quando Rök chegar no collider destruir o collider para ter menos objetos na cena
                if (col.name.Equals("Rök") && (gameObject.name.Equals("RökInitialPoint") || gameObject.name.Equals("NearGate")))
                {
                    if (gameObject.name.Equals("RökInitialPoint"))
                        col.GetComponent<BeowulfController>().moveSpeed = 5;

                    if (gameObject.name.Equals("NearGate"))
                        col.GetComponent<Rigidbody2D>().mass = 10;

                    stop = true;
                    StartCoroutine(DestroyCollider());
                }

                //Quando Skeleton chegar no collider destruir o collider para ter menos objetos na cena e destruir o Skeleton
                if (col.name.Equals("Skeleton") && (gameObject.name.Equals("SkeletonInitialPoint") || gameObject.name.Equals("SkeletonFinalPoint")))
                {
                    if (gameObject.name.Equals("SkeletonFinalPoint"))
                        Destroy(col.gameObject);

                    stop = true;
                    StartCoroutine(DestroyCollider());
                }
            }
        }

        public IEnumerator DestroyCollider()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}

