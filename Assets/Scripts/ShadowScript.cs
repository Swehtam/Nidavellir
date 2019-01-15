using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public Vector2 offset = new Vector2(0.3f, -0.26f);
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
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localScale = new Vector2(1f, 1f);
        transShadow.localRotation = Quaternion.Euler(angle);

        sprRndCaster = GetComponent<SpriteRenderer>();
        sprtRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprtRndShadow.material = shadowMaterial;
        sprtRndShadow.color = shadowColor;
        sprtRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprtRndShadow.sortingOrder = sprRndCaster.sortingOrder - 2;
    }

    void LateUpdate()
    {
        transShadow.position = new Vector2(transCaster.position.x + offset.x, 
            transCaster.position.y + offset.y);

        sprtRndShadow.sprite = sprRndCaster.sprite;
    }
}
