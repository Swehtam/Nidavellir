using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int health;
    public int currentHealth;

    //Bool para saber se o inimigo é o esqueleto
    public bool isSkeleton;

    //Sprite do inimigo
    private SpriteRenderer enemy_SpriteRenderer;
    //Sprite do braço do inimigo, se necessário
    private SpriteRenderer arm_SpriteRenderer;

    // Buffer da cor da sprite do inimigo
    private Color enemy_buffer;
    // Buffer da cor da sprite do braço do inimig
    private Color arm_buffer;

    private GameObject key;

    // Use this for initialization
    void Start ()
    {
		key = GameObject.Find("Key");
		currentHealth = health;
        enemy_SpriteRenderer = GetComponent<SpriteRenderer>();
        enemy_buffer = enemy_SpriteRenderer.color;

        if (isSkeleton)
        {
            arm_SpriteRenderer = transform.Find("Skeleton-Arm").GetComponent<SpriteRenderer>();
            arm_buffer = arm_SpriteRenderer.color;
        }
            
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (currentHealth <= 0)
		{
			key.GetComponent<KeyController>().killed++;
            Destroy(gameObject);
		}
	}

    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
        if (isSkeleton)
            SoundManagerScript.PlaySound("skeleton-bones");
        StartCoroutine(Wait(0.5f));
    }

    public IEnumerator Wait(float time)
    {
        enemy_SpriteRenderer.color = Color.red;
        if (isSkeleton)
            arm_SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(time);
        enemy_SpriteRenderer.color = enemy_buffer;
        if (isSkeleton)
            arm_SpriteRenderer.color = arm_buffer;
    }
}
