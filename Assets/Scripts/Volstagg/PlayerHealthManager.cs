using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;
	SpriteRenderer m_SpriteRenderer;
	Color m_NewColor;
	Color buffer;


	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		//m_SpriteRenderer.color = Color.blue;
		buffer = m_SpriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentHealth <= 0)
            gameObject.SetActive(false);
	}

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
    }

	public void OnCollisionEnter2D(Collision2D col)
	{
		

		if (col.gameObject.tag == "Enemy")
		{
			StartCoroutine(Wait(0.5f));
		}
	}

	public IEnumerator Wait(float time)
	{
		m_SpriteRenderer.color = Color.red;		
		yield return new WaitForSeconds(time);		
		m_SpriteRenderer.color = buffer;
	}
}
