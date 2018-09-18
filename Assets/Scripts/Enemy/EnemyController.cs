using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody myRB;
    public float moveSpeed;
	public float range;
	private bool seeker = false;
	private Animator anim;
	private Vector3 moveInput;
	private bool enemyMoving;
	private Vector2 lastMove;


	private PlayerControl thePlayer;
	// Use this for initialization

	void Start () {  
		myRB = GetComponent<Rigidbody>();
		thePlayer = FindObjectOfType<PlayerControl>();
		anim = GetComponent<Animator>();

	}

    private void FixedUpdate()
    {
		if (Vector3.Distance(thePlayer.transform.position, transform.position) <= range || seeker) {
			seeker = true;
			
			myRB.velocity = transform.forward * moveSpeed;
		}
    }

    // Update is called once per frame
    void Update () {
		if (seeker) {

			enemyMoving = false;

			moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
			moveInput = moveInput * moveSpeed;


			if (moveInput.x != 0 || moveInput.z != 0)
			{
				enemyMoving = true;
				lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			}
			anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
			anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
			anim.SetBool("EnemyMoving", enemyMoving);
			anim.SetFloat("LastMoveX", lastMove.x);
			anim.SetFloat("LastMoveY", lastMove.y);

			transform.LookAt (new Vector3 (thePlayer.transform.position.x, transform.position.y, thePlayer.transform.position.z));
		}
	}
}
