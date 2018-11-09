using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class PlayerControl : MonoBehaviour
    {

        public float moveSpeed;
        private Rigidbody2D myRB;
        private Vector3 moveInput;

        private Animator anim;
        private bool playerMoving;
        private Vector2 lastMove;

        private float attackTime = 1.0f;
        private float attackCoolDown;
        private bool playerAttacking;

        // Use this for initialization
        void Start()
        {
            myRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //Remove o controle do player caso o Dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
            }

            playerMoving = false;

            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput = moveInput * moveSpeed;

            if (moveInput.x != 0 || moveInput.y != 0)
            {
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetMouseButtonDown(0) && playerAttacking == false)
            {
                attackCoolDown = attackTime;
                playerAttacking = true;
                SoundManagerScript.PlaySound("volAttack-slash");
            }

            if (attackCoolDown > 0)
            {
                attackCoolDown -= Time.deltaTime;
            }

            if (attackCoolDown <= 0)
            {
                playerAttacking = false;
            }

            if (!playerAttacking)
            {
                anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                anim.SetBool("PlayerMoving", playerMoving);
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);
            }
            
            anim.SetBool("PlayerAttacking", playerAttacking);
        }

        private void FixedUpdate()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
            }
            myRB.velocity = moveInput;
        }

        //Metodo para rodas as animações de Volstagg de acordo com dialogo
        [YarnCommand("setAnimation")]
        public void PlayAnimation(string animationName)
        {
            if(animationName == "FaceLeft")
            {
                anim.SetFloat("LastMoveX", -1.0f);
                Debug.Log("olhou pra esquerda");
            }else if (animationName == "FaceRight")
            {
                anim.SetFloat("LastMoveX", 1.0f);
                Debug.Log("olhou pra direita");
            }
            else if (animationName == "FaceDown")
            {
                anim.SetFloat("LastMoveY", -1.0f);
                Debug.Log("olhou pra baixo");
            }

            //anim.CrossFade(animationName, 0, -1,);
        }
    }
}
