using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayOrder : MonoBehaviour
{
    public string layerName;
    private int counter = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            //contar o numero de mobs q entrou no collider
            counter++;
            col.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
            foreach (Transform child in col.transform)
            {
                if (child.GetComponent<SpriteRenderer>())
                {
                    child.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
                }
            }

            //Fazer com q o objeto com o collider se destrua caso o numero de mobs especifico entrar no colider
            if(counter == 4 && gameObject.name.Equals("BehindGate"))
            {
                Destroy(gameObject);
            }

            if(counter == 3 && gameObject.name.Equals("FrontGate"))
            {
                Destroy(gameObject);
            }
        }
    }
}
