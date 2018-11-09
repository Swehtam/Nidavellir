using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

	private GameObject key;
    public int health;
    public int currentHealth;
    private SpriteRenderer m_SpriteRenderer;
    private Color m_NewColor;
    private Color buffer;

    // Use this for initialization
    void Start ()
    {
		key = GameObject.Find("Key");
		currentHealth = health;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        buffer = m_SpriteRenderer.color;
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
        SoundManagerScript.PlaySound("skeleton-bones");
        StartCoroutine(Wait(0.5f));
    }

    public IEnumerator Wait(float time)
    {
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(time);
        m_SpriteRenderer.color = buffer;
    }
}
