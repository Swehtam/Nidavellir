using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeowulfController : MonoBehaviour {


	
	public float moveSpeed;
	public float maxRange;
    public float minRange;
	private bool seeker = false;
	private Animator anim;
	private bool enemyMoving = true;
	private Vector2 direction;
    private float step;
	private Rigidbody2D myRB;
	private float xDir;
	private float yDir;

	public GameObject point;
	// Use this for initialization

	void Start () {
		myRB = GetComponent<Rigidbody2D>();		
		anim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        if (enemyMoving)
        {
            step = moveSpeed * Time.deltaTime;
			//myRB.velocity = direction;
			transform.position = Vector2.MoveTowards(transform.position, point.transform.position, step);
        }
        
    }

    // Update is called once per frame
    void Update () {       

        //se o personagem entrar na area de ameaça do inimigo, o inimigo irá segui-lo
      

			//vetor para saber qual a posição do inimigo para o player
			xDir = point.transform.position.x - transform.position.x;
			yDir = point.transform.position.y - transform.position.y;
			/*
			//saber se o inimigo olha pra esquerda ou pra direita,
			//se for perto de 0, então desconsidera o x e só considera a posição z (do eixo do mundo)
			if (direction.x >= -0.5f && direction.x <= 0.5f)
            {
                direction.x = 0f;
            }
            else if (direction.x >= 0.5f)
            {
                direction.x = 1.0f;
            }
            else
            {
                direction.x = -1.0f;
            }

            //saber se o inimigo olha pra cima ou pra baixo,
            //se for perto de 0, então desconsidera o z (do eixo do mundo) e só considera a posição x 
            if (direction.y > -1.0f && direction.y < 1.0f)
            {
                direction.y = 0f;
            }
            else if (direction.y >= 0.5f)
            {
                direction.y = 1.0f;
            }
            else
            {
                direction.y = -1.0f;
            }
			*/
            //caso o inimigo fique muito proximo, faço o parar na frente do player e olhar na direção dele
            //isso aqui foi feito para o inimigo n ficar empurrando o player para fora do mapa
            if (Vector2.Distance(point.transform.position, transform.position) <= minRange){
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

