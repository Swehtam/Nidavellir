using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
            SoundManagerScript.PlaySound("pickup-heart");
            this.gameObject.transform.position = new Vector3(99f, 99f, -0.278f);
			col.gameObject.GetComponent<PlayerHealthManager>().CurePlayer(1);
		}
	}
}
