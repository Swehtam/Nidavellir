using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody myRB;
    public float moveSpeed;

    private PlayerControl thePlayer;
	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<PlayerControl>();
	}

    private void FixedUpdate()
    {
        myRB.velocity = transform.forward * moveSpeed;
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(thePlayer.transform.position);
	}
}
