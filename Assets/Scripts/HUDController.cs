using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public Sprite[] hPSprites;
	public Sprite[] cervaSprites;

	public Image hPUI;
	public Image cervaUI;

	public GameObject player;
	public GameObject key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int hp = player.GetComponent<PlayerHealthManager>().currentHealth;
		int honra = key.GetComponent<KeyController>().onda;

		if (hp == 0) {
			hPUI.sprite = hPSprites[0];
		}
		if (hp == 1)
		{
			hPUI.sprite = hPSprites[1];
		}
		if (hp == 2)
		{
			hPUI.sprite = hPSprites[2];
		}
		if (hp == 3)
		{
			hPUI.sprite = hPSprites[3];
		}
		if (hp == 4)
		{
			hPUI.sprite = hPSprites[4];
		}
		if (hp == 5)
		{
			hPUI.sprite = hPSprites[5];
		}

		if (honra == 0)
		{
			cervaUI.sprite = cervaSprites[0];
		}
		if (honra == 1)
		{
			cervaUI.sprite = cervaSprites[0];
		}
		if (honra == 2)
		{
			cervaUI.sprite = cervaSprites[1];
		}
		if (honra == 3)
		{
			cervaUI.sprite = cervaSprites[2];
		}
		if (honra == 4)
		{
			cervaUI.sprite = cervaSprites[3];
		}
		if (honra == 5)
		{
			cervaUI.sprite = cervaSprites[4];
		}
		if (honra == 6)
		{
			cervaUI.sprite = cervaSprites[5];
		}
	}
}
