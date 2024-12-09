using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingProjectileController : MonoBehaviour
{
    public Animator myAnim;
    public int damage = 10;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        myAnim.SetTrigger("meleeAttack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
 //           other.gameObject.GetComponent<EnemyHealthSystem>().AddDamage(Random.Range(damage, damage+damageRange));
        }
        else if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().AddDamage(damage);
        }
    }
}
