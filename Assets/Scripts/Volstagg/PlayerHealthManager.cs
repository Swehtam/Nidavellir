using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;
	SpriteRenderer m_SpriteRenderer;
	Color m_NewColor;
	Color buffer;
    public bool died;


	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		buffer = m_SpriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SoundManagerScript.PlaySound("volstagg-death");
            died = true;
        }
            
	}

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        SoundManagerScript.PlaySound("volstagg-grunt");
        StartCoroutine(WaitDamage(0.5f));
	}

	public void CurePlayer(int cure)
	{
		currentHealth += cure;
		StartCoroutine(WaitCure(0.5f));
	}

	public IEnumerator WaitDamage(float time)
	{
		m_SpriteRenderer.color = Color.red;		
		yield return new WaitForSeconds(time);		
		m_SpriteRenderer.color = buffer;
	}

    public IEnumerator WaitCure(float time)
    {
        m_SpriteRenderer.color = Color.green;
        yield return new WaitForSeconds(time);
        m_SpriteRenderer.color = buffer;
    }

    public IEnumerator GoToMenuScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Menu");
    }
}
