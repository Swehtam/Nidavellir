using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BoneDragonController : MonoBehaviour
    {
        //variaveis para o dragão se mexer
        public float moveSpeed;
        public float direction;
        
        //Variaveis referentes para a Bola de Gelo
        public float iceballAttackCoolDown;
        public GameObject iceball;
        public Transform shootPoint;
        public bool shoot;
        public bool attackingIceball;

        //Variaveis referentes ao Rugido
        public float roarAttackCoolDown;

        //variaveis referentes ao Rasante
        public float airAttackCoolDown;

        //Variavel para saber a fase do boss
        public int phase;
        
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

        //Componentes usados pelo Boss
        private Rigidbody2D dragonRB;
        private Animator anim;
        private PlayerHealthManager player;
        private BossHealthManager healthManager;

        //Variaveis para movimentação do dragão
        private Transform point;
        private bool dragonFlying;

        //Variaveis referentes à Bola de Gelo
        private readonly float iceballAttackTime = 1f;
        private float iceballCoolDown;
        private float iceballTimeCoolDown;
        private bool fbCreated;

        //Variaveis referentes ao Rugido
        private readonly float roarAttackTime = 1.3f;
        private float roarCoolDown;
        private float roarTimeCoolDown;
        private bool roar;
        private readonly float roarForce = 500f;
        private float roarForceDecrese;

        //Variaveis referentes ao Rasante
        private readonly int airStrikeAttackCount = 3;
        private float airCoolDown;
        private int airStrikeCount;
        private bool airStriking;
        private Transform airAttackPoint;
        
        void Start()
        {
            direction = -1;

            healthManager = GetComponent<BossHealthManager>();
            player = FindObjectOfType<PlayerHealthManager>();
            dragonRB = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            point = null;
            dragonFlying = false;

            iceballCoolDown = 0f;
            iceballTimeCoolDown = 0f;
            fbCreated = false;

            roarCoolDown = 0f;
            roarTimeCoolDown = 0f;
            roar = false;
            roarForceDecrese = 0f;

            airCoolDown = 0f;
            airStrikeCount = 0;
            airStriking = false;
            airAttackPoint = null;
        }

        private void FixedUpdate()
        {
            // Para o inimigo não ser empurrado e continuar deslizando com a inercia
            dragonRB.velocity = new Vector2(0f, 0f);

            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                if (point)
                {
                    anim.SetBool("StartFlying", true);
                    anim.SetBool("Flying", true);
                    transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
                    if (transform.position == point.position)
                    {
                        point = null;
                        dragonFlying = false;
                        anim.SetBool("StartFlying", false);
                        anim.SetBool("Flying", false);
                        direction = player.transform.position.x - transform.position.x;
                    }
                }
                return;
            }

            //Caso ele tenha morrido dê play na animação e pare tudo
            if (died)
            {
                //alterara a variavel que será usada na movimentação do boss, para ele mesmo, ou seja, ele vai parar aonde estiver
                anim.SetBool("AirStriking", false);
                anim.SetBool("StartFlying", false);
                anim.SetBool("Flying", false);
                anim.SetBool("FireballAttack", false);
                anim.SetBool("Dead", true);
                return;
            }

            //Caso esteja vivo e não esteja voando;
            if (phase == 1)
            {
                if (dragonFlying)
                {
                    moveSpeed = 5f;
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
                    moveSpeed = 15f;
                    anim.SetBool("AirStriking", airStriking);
                    transform.position = Vector2.MoveTowards(transform.position, airAttackPoint.position, moveSpeed * Time.deltaTime);
                    if (transform.position == airAttackPoint.position)
                    {
                        airAttackPoint = null;
                        airStriking = false;
                        anim.SetBool("AirStriking", airStriking);
                        FlyToPoint(direction, true);
                    }
                    return;
                }
            }
            else if (phase == 2)
            {
                moveSpeed = 10f;
                airStriking = false;
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
                if (attackingIceball)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.transform.position.y + 2f), moveSpeed * Time.deltaTime);
                }
            }
            else if (phase == 3)
            {
                if (dragonFlying)
                {
                    moveSpeed = 5f;
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
                    moveSpeed = 15f;
                    anim.SetBool("AirStriking", airStriking);
                    transform.position = Vector2.MoveTowards(transform.position, airAttackPoint.position, moveSpeed * Time.deltaTime);
                    if (transform.position == airAttackPoint.position)
                    {
                        airAttackPoint = null;
                        airStriking = false;
                        anim.SetBool("AirStriking", airStriking);
                        FlyToPoint(direction, true);
                    }
                    return;
                }
            }
        }

        void Update()
        {
            //Para os controles dos inimigos caso o dialogo esteja acontecendo
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                if (shoot && !fbCreated)
                {
                    fbCreated = true;
                    direction = player.transform.position.x - transform.position.x;
                    GameObject clone = (GameObject)Instantiate(iceball, shootPoint.transform.position, Quaternion.identity);
                    clone.GetComponent<Rigidbody2D>().AddForce(new Vector2((Mathf.Abs(direction)) / direction, 0.0f) * 100);
                }
                anim.SetFloat("FacingX", direction);
                return;
            }
            if (dragonFlying)
            {    
                return;
            }

            if (phase == 1)
            {
                //boss ja vai estar voando por causa do dialogo então não precisa disso aqui
                if (!airStriking)
                {
                    AirStrikePoint(direction);
                }
            }
            else if (phase == 2)
            {
                if (healthManager.takingDamage)
                {
                    FlyToPoint(direction, false);
                    return;
                }

                if (iceballCoolDown <= 0)
                {
                    attackingIceball = true;
                    iceballCoolDown = iceballAttackCoolDown;
                    iceballTimeCoolDown = iceballAttackTime;
                }

                if (iceballCoolDown > 0)
                {
                    iceballTimeCoolDown -= Time.deltaTime;
                    iceballCoolDown -= Time.deltaTime;
                }

                if (iceballTimeCoolDown < 0)
                {
                    attackingIceball = false;
                    fbCreated = false;
                }
            }
            else if (phase == 3)
            {
                if(airCoolDown > 0 && !airStriking)
                {
                    if (healthManager.takingDamage)
                    {
                        FlyToPoint(direction, false);
                        return;
                    }
                }
                //Caso o CD do Rasante tenha acabado e o contador de Rasantes seja 0, entaõ faça rasante
                if (airCoolDown <= 0 && airStrikeCount == 0)
                {
                    airStrikeCount = airStrikeAttackCount;
                }
                //Caso o Rasante esteja em CD e o contador esteiver em 0 e o CD do Rugido tenha acabado, então faça o Rugido
                else if (airCoolDown > 0 && airStrikeCount == 0 && roarCoolDown <= 0)
                {
                    roar = true;
                    roarForceDecrese = roarForce;
                    roarCoolDown = roarAttackCoolDown;
                    roarTimeCoolDown = roarAttackTime;
                }
                //Caso o Rasante esteja em CD e o contador esteiver em 0 e o Rugido esteja em CD e ele não esteja fazendo o Rugido e o CD da IceBall tenha acabado, então faça o IceBall
                else if (airCoolDown > 0 && airStrikeCount == 0 && roarCoolDown > 0 && roarTimeCoolDown < 0 && iceballCoolDown <= 0)
                {
                    attackingIceball = true;
                    iceballCoolDown = iceballAttackCoolDown;
                    iceballTimeCoolDown = iceballAttackTime;
                }

                if(airStrikeCount > 0 && !airStriking)
                {
                    airStrikeCount -= 1;
                    AirStrikePoint(direction);
                    if(airStrikeCount == 0)
                    {
                        airCoolDown = airAttackCoolDown;
                    }
                }

                if (airCoolDown > 0)
                {
                    airCoolDown -= Time.deltaTime;
                }

                if (roarCoolDown > 0)
                {
                    roarTimeCoolDown -= Time.deltaTime;
                    roarCoolDown -= Time.deltaTime;
                }

                if (iceballCoolDown > 0)
                {
                    iceballTimeCoolDown -= Time.deltaTime;
                    iceballCoolDown -= Time.deltaTime;
                }

                if (roarTimeCoolDown < 0)
                {
                    roar = false;
                }

                if (iceballTimeCoolDown < 0)
                {
                    attackingIceball = false;
                    fbCreated = false;
                }
            }
            if (roar)
            {
                roarForceDecrese -= Time.deltaTime * 100;
                direction = player.transform.position.x - transform.position.x;
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2((Mathf.Abs(direction)) / direction, 0.0f) * roarForceDecrese);
            }

            if (shoot && !fbCreated)
            {
                fbCreated = true;
                direction = player.transform.position.x - transform.position.x;
                GameObject clone = (GameObject)Instantiate(iceball, shootPoint.transform.position, Quaternion.identity);
                clone.GetComponent<Rigidbody2D>().AddForce(new Vector2((Mathf.Abs(direction)) / direction, 0.0f) * 200);
            }

            anim.SetBool("Roar", roar);
            anim.SetBool("AirStriking", airStriking);
            anim.SetBool("FireballAttack", attackingIceball);
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
            anim.SetBool("StartFlying", true);
            anim.SetBool("Flying", true);
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

        //metodo para fazer o Boss voar até um ponto especifico nos arquivos .yarn
        [YarnCommand("flyTo")]
        public void Fly(string pointName)
        {
            moveSpeed = 10f;
            Transform p = null;
            //procura o ponto para onde irá se mover dentro do array
            foreach (var info in pointsToFly)
            {
                if (info.name == pointName)
                {
                    p = info.flyTo;
                    break;
                }
            }

            //se não achar mandar uma mensagem para o console
            if (p == null)
            {
                Debug.LogErrorFormat("Não foi encontrando o point {0}!", pointName);
                return;
            }
            else
            {
                //se achar coloque o point para onde ele deva ir no objeto responsavel por isso
                point = p;
                direction = point.position.x - transform.position.x;
            }
        }

        //Metodo para fazer as animações do Boss
        [YarnCommand("setAnimation")]
        public void SetAnimation(string command)
        {
            anim.SetBool("FireballAttack", false);
            anim.SetBool("AirStriking", false);
            if (command.Equals("Fireball"))
            {
                anim.SetBool("DialogFireball", true);
            }
            else if (command.Equals("StopFireball"))
            {
                anim.SetBool("DialogFireball", false);
            }
        }
    }
}