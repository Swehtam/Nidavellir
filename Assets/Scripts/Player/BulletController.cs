using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed;

    public float lifeTime;
    public int damage;
    
	void FixedUpdate () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            Destroy(gameObject);
        }
    }


}
