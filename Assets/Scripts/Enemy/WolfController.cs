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
	public float attackCoolDown;
	private bool seeker = false;
	private Animator anim;
	private bool enemyMoving;
	private Vector2 direction;	
	private Rigidbody2D myRB;
	private float xDir;
	private float yDir;
	private bool enemyAttacking;
	private PlayerControl thePlayer;
	private float attackTime = 1.0f;
    private float coolDown;
    private float attackTimeCoolDown;
 
	

	// Use this for initialization

	void Start()
	{
		myRB = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerControl>();
		anim = GetComponent<Animator>();
		enemyAttacking = false;
		coolDown = 0f;
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
			if (!enemyAttacking)
			{
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

			if (Vector2.Distance(thePlayer.transform.position, transform.position) <= minRange && coolDown <= 0)
			{
				coolDown = attackCoolDown;
				attackTimeCoolDown = attackTime;
				enemyAttacking = true;
			}

			if (coolDown > 0)
			{
				attackTimeCoolDown -= Time.deltaTime;
				coolDown -= Time.deltaTime;
			}

			if (attackTimeCoolDown <= 0)
			{
				enemyAttacking = false;
			}

			//anim.SetBool("EnemyAttacking", enemyAttacking);
		}
	}
}
