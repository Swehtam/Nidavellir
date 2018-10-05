using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yarn.Unity.Example
{
public class WolfController : MonoBehaviour
{
	public float moveSpeed;
	public float maxRange;
	public float minRange;
	public float dashRange;
	private bool seeker = false;
	private Animator anim;
	private bool enemyMoving;
	private Vector2 direction;	
	private Rigidbody2D myRB;
	private float xDir;
	private float yDir;
	private PlayerControl thePlayer;

	// Use this for initialization

	void Start()
	{
		myRB = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerControl>();
		anim = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
		{
			return;
		}
		if (enemyMoving)
		{
			myRB.velocity = new Vector2(0f, 0f);
			transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
		}
	}

	// Update is called once per frame
	void Update()
	{

		if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
		{
			return;
		}

		enemyMoving = false;
		//se o personagem entrar na area de ameaça do inimigo, o inimigo irá segui-lo
		if (Vector2.Distance(thePlayer.transform.position, transform.position) <= maxRange || seeker)
		{
			seeker = true;
			enemyMoving = true;
			anim.SetBool("Init", true);

			//vetor para saber qual a posição do inimigo para o player
			xDir = thePlayer.transform.position.x - transform.position.x;
			yDir = thePlayer.transform.position.y - transform.position.y;

			if (Vector2.Distance(thePlayer.transform.position, transform.position) <= minRange)
			{
				enemyMoving = false;
				anim.SetFloat("LastMoveX", xDir);
				anim.SetFloat("LastMoveY", yDir);
			}
			else
			{
				anim.SetFloat("MoveX", xDir);
				anim.SetFloat("MoveY", yDir);
			}
			anim.SetBool("EnemyMoving", enemyMoving);
		}
		
	}
}
}