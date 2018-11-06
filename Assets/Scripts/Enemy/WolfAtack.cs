using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAtack : MonoBehaviour {

	public int dmg;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.isTrigger != true && collision.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(dmg);
		}
	}
}
