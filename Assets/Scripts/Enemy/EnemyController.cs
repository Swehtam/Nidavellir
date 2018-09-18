using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    

    public float moveSpeed;
	public float maxRange;
    public float minRange;
	private bool seeker = false;
	private Animator anim;
	private bool enemyMoving;
	private Vector2 direction;
    private float step;

	private PlayerControl thePlayer;
	// Use this for initialization

	void Start () {  
		thePlayer = FindObjectOfType<PlayerControl>();
		anim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        if (enemyMoving)
        {
            step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(thePlayer.transform.position.x, transform.position.y, thePlayer.transform.position.z), step);
        }
        
    }

    // Update is called once per frame
    void Update () {
        enemyMoving = false;

        //se o personagem entrar na area de ameaça do inimigo, o inimigo irá segui-lo
        if (Vector3.Distance(thePlayer.transform.position, transform.position) <= maxRange || seeker){
            seeker = true;
            enemyMoving = true;

            //vetor para saber qual a posição do inimigo para o player
            direction = new Vector2((thePlayer.transform.position.x - transform.position.x), (thePlayer.transform.position.z - transform.position.z));

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

            //caso o inimigo fique muito proximo, faço o parar na frente do player e olhar na direção dele
            //isso aqui foi feito para o inimigo n ficar empurrando o player para fora do mapa
            if (Vector3.Distance(thePlayer.transform.position, transform.position) <= minRange){
                enemyMoving = false;

                anim.SetFloat("LastMoveX", direction.x);
                anim.SetFloat("LastMoveY", direction.y);
            }
            else
            {
                anim.SetFloat("MoveX", direction.x);
                anim.SetFloat("MoveY", direction.y);
            }	
            
			anim.SetBool("EnemyMoving", enemyMoving);
        }
	}
}
