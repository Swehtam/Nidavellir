using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BoneDragonController : MonoBehaviour
    {
        public float moveSpeed;
        public float attackCoolDown;
        //So que de gelo
        public GameObject fireball;
        public Transform firePoint;
        public float direction;
        public int phase;
        public bool shoot;

        //Pontos para onde o dragão vai voar
        [System.Serializable]
        public struct FlyToInfo
        {
            public string name;
            public Transform flyTo;
        }
        public FlyToInfo[] pointsToFly;

        //Fazendo referencia ao Script BossLane
        public int volstaggLane;

        //Variavel para saber se o boss morreu, ela ta sendo usado no script BossHealthManager
        public bool died;

        private bool dragonFlying;
        private Rigidbody2D dragonRB;
        private Animator anim;
        private Transform point;
        private PlayerHealthManager player;
        private BossHealthManager healthManager;

        private readonly float attackTime = 1f;
        private float coolDown;
        private float attackTimeCoolDown;
        private bool attackingFireball;
        private bool fbCreated;
        private bool airStriking;
        private Transform airAttackPoint;

        void Start()
        {
            healthManager = GetComponent<BossHealthManager>();
            player = FindObjectOfType<PlayerHealthManager>();
            point = null;
            airAttackPoint = null;
            dragonRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            coolDown = 0f;
            attackingFireball = false;
            died = false;
            phase = 2;
            direction = -1f;
            shoot = false;
            fbCreated = false;
            airStriking = false;
            dragonFlying = false;
            anim.SetBool("StartFlying", true);
            anim.SetBool("Flying", true);
            volstaggLane = 2;
        }

        private void FixedUpdate()
        {
            // Para o inimigo não ser empurrado e continuar deslizando com a inercia
            dragonRB.velocity = new Vector2(0f, 0f);

            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
                if (transform.position == point.position)
                {
                    point = null;
                    dragonFlying = false;
                    direction = player.transform.position.x - transform.position.x;
                }
                return;
            }

            //Caso ele tenha morrido dê play na animação e pare tudo
            if (died)
            {
                //alterara a variavel que será usada na movimentação do boss, para ele mesmo, ou seja, ele vai parar aonde estiver
                anim.SetBool("Dead", true);
                return;
            }

            //Caso esteja vivo e não esteja voando;
            if (phase == 1)
            {
                if (dragonFlying)
                {
                    transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
                    if (transform.position == point.position)
                    {
                        point = null;
                        dragonFlying = false;
                        direction = player.transform.position.x - transform.position.x;
                    }
                    return;
                }
                if (airStriking)
                {
                    anim.SetBool("AirStriking", true);
                    transform.position = Vector2.MoveTowards(transform.position, airAttackPoint.position, moveSpeed * Time.deltaTime);
                    if (transform.position == airAttackPoint.position)
                    {
                        airAttackPoint = null;
                        airStriking = false;
                        anim.SetBool("AirStriking", false);
                        FlyToPoint(direction, true);
                    }
                    return;
                }
            }
            else if (phase == 2)
            {
                if (dragonFlying)
                {
                    transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
                    if (transform.position == point.position)
                    {
                        point = null;
                        dragonFlying = false;
                        anim.SetBool("StartFlying", false);
                        anim.SetBool("Flying", false);
                        direction = player.transform.position.x - transform.position.x;
                    }
                    return;
                }
                if (attackingFireball)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.transform.position.y + 2f), moveSpeed * Time.deltaTime);
                }
            }
            else if (phase == 3)
            {

            }
        }

        void Update()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                anim.SetFloat("FacingX", direction);
                return;
            }
            if (dragonFlying)
            {
                moveSpeed = 7f;
                return;
            }

            if (phase == 1)
            {
                //boss ja vai estar voando por causa do dialogo então não precisa disso aqui
                if (!airStriking)
                {
                    AirStrikePoint(direction);
                }
                else
                {
                    moveSpeed = 15f;
                }

                anim.SetFloat("FacingX", direction);
            }
            else if (phase == 2)
            {
                if (healthManager.takingDamage)
                {
                    FlyToPoint(direction, false);
                    return;
                }
                if (shoot && !fbCreated)
                {
                    fbCreated = true;
                    direction = player.transform.position.x - transform.position.x;
                    GameObject clone = (GameObject)Instantiate(fireball, firePoint.transform.position, Quaternion.identity);
                    clone.GetComponent<Rigidbody2D>().AddForce(new Vector2((Mathf.Abs(direction)) / direction, 0.0f) * 200);
                }

                if (coolDown <= 0)
                {
                    attackingFireball = true;
                    coolDown = attackCoolDown;
                    attackTimeCoolDown = attackTime;
                }

                if (coolDown > 0)
                {
                    attackTimeCoolDown -= Time.deltaTime;
                    coolDown -= Time.deltaTime;
                }

                if (attackTimeCoolDown < 0)
                {
                    attackingFireball = false;
                    fbCreated = false;
                }

                anim.SetBool("FireballAttack", attackingFireball);

            }
            else if (phase == 3)
            {

            }

            anim.SetFloat("FacingX", direction);
        }

        //Função para fazer o dragão voar
        private void FlyToPoint(float directionToFly, bool outPoint)
        {
            dragonFlying = true;
            //Usado na primeira fase
            if (outPoint)
            {
                //se o player estiver na direita, é pq o dragão acabou de passar por ele, 
                //então ele deve voar para o lado esquerdo fora do mapa
                if (directionToFly < 0)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("Left"))
                        {
                            point = info.flyTo;
                        }
                    }
                }
                //caso o player esteja a esquerda,
                //então o dragão deve voar para o lado direito do mapa
                else
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("Right"))
                        {
                            point = info.flyTo;
                        }
                    }
                }
            }
            //Usado na segunda fase
            else
            {
                anim.SetBool("StartFlying", true);
                anim.SetBool("Flying", true);

                if (directionToFly > 0)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("RightMidLane"))
                        {
                            point = info.flyTo;
                        }
                    }
                }
                else
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("LeftMidLane"))
                        {
                            point = info.flyTo;
                        }
                    }
                }
            }

        }

        private void AirStrikePoint(float flyingDirection)
        {
            dragonFlying = true;
            airStriking = true;
            int lane = volstaggLane;

            //Vai estar no topo
            if (flyingDirection > 0)
            {
                if (lane == 1)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("LeftTopLane"))
                        {
                            point = info.flyTo;
                        }
                        if (info.name.Equals("RightTopLane"))
                        {
                            airAttackPoint = info.flyTo;
                        }
                    }
                }
                else if (lane == 2)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("LeftMidLane"))
                        {
                            point = info.flyTo;
                        }
                        if (info.name.Equals("RightMidLane"))
                        {
                            airAttackPoint = info.flyTo;
                        }
                    }
                }
                else if (lane == 3)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("LeftBotLane"))
                        {
                            point = info.flyTo;
                        }
                        if (info.name.Equals("RightBotLane"))
                        {
                            airAttackPoint = info.flyTo;
                        }
                    }
                }
            }
            else
            {
                if (lane == 1)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("RightTopLane"))
                        {
                            point = info.flyTo;
                        }
                        if (info.name.Equals("LeftTopLane"))
                        {
                            airAttackPoint = info.flyTo;
                        }
                    }
                }
                else if (lane == 2)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("RightMidLane"))
                        {
                            point = info.flyTo;
                        }
                        if (info.name.Equals("LeftMidLane"))
                        {
                            airAttackPoint = info.flyTo;
                        }
                    }
                }
                else if (lane == 3)
                {
                    foreach (var info in pointsToFly)
                    {
                        if (info.name.Equals("RightBotLane"))
                        {
                            point = info.flyTo;
                        }
                        if (info.name.Equals("LeftBotLane"))
                        {
                            airAttackPoint = info.flyTo;
                        }
                    }
                }
            }
        }
    }
}