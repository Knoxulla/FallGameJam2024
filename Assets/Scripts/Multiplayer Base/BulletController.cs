using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;
    public float speed;
 //   public GameObject hitFX;

    float transitTime = 0.1f;  //allowed to leave collider, otherwise there are dependencies.

    private void Awake()
    {
        transitTime += Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D myRB = GetComponent<Rigidbody2D>();
        myRB.velocity = transform.up * speed;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transitTime > Time.time) return;

        if (other.tag == "Enemy")
        {
//            other.gameObject.GetComponent<EnemyHealthSystem>().AddDamage(Random.Range(damage, damage+damageRange));  
        } else if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().AddDamage(damage);
        }
 //       Instantiate(hitFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
