using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Rigidbody2D fireballRB;
    private Animator anim;
    private float timeToDestroy = 3f;
    private float cooldown;
    private bool fbCreated;

    // Start is called before the first frame update
    void Start()
    {
        fireballRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fbCreated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fbCreated)
        {
            cooldown = timeToDestroy;
            fbCreated = false;
        }

        cooldown -= Time.deltaTime;

        if(cooldown <= 0f)
        {
            //StartCoroutine(DestroyFireball());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Player"))
        {
            StartCoroutine(DestroyFireball());
        }
    }

    private IEnumerator DestroyFireball()
    {
        anim.SetBool("StopFireball", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
