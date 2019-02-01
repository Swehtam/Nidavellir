using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    //Posição da sombra com relação ao objeto ligado à este script
    public Vector2 offset = new Vector2(0.3f, -0.26f);
    //Angulo da sombra com relação ao objeto ligado à esse script
    public Vector3 angle = new Vector3(60f, 0, -30f);

    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprtRndShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor;

    void Start()
    {
        transCaster = transform;

        //Cria o objeto da sombra, diz que ela é filha do objeto ligado à esse script,
        // nomeia ela com o nome "shadow", diz que sua escala é igual ao objeto e
        // que seu angulo é igual aquele pre-definido pelo programador ou o padrão.
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localScale = new Vector2(1f, 1f);
        transShadow.localRotation = Quaternion.Euler(angle);

        //Cria um SpriteRenderer na sombra para atualizar suas sprites
        sprRndCaster = GetComponent<SpriteRenderer>();
        sprtRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        //Seta seu material, sua cor, o nome do seu Layer e sua ordem na tela.
        //Coloquei (-2), pois as sombras dos braços dos personagens estavam na
        // frente dos personagens em si.
        sprtRndShadow.material = shadowMaterial;
        sprtRndShadow.color = shadowColor;
        sprtRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprtRndShadow.sortingOrder = sprRndCaster.sortingOrder - 2;
    }

    //Para ser chamado depois de todas as funções Update
    void LateUpdate()
    {
        //Atualização da posição da sombra
        transShadow.position = new Vector2(transCaster.position.x + offset.x, 
            transCaster.position.y + offset.y);

        //Atualização da mudança de sprites da sombra
        sprtRndShadow.sprite = sprRndCaster.sprite;
    }
}
