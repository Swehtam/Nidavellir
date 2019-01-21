using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {
	public GameObject key;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (KeyController.open)
		{
			if (col.gameObject.tag == "Player")
			{
				StartCoroutine(Wait(0.8f));
			}
		}
	}

	public IEnumerator Wait(float time)
	{
        SoundManagerScript.PlaySound("gate-opening");
        anim.SetBool("GotKey", true);
		yield return new WaitForSeconds(time);
		anim.SetBool("IsOpen", true);
	}
}
