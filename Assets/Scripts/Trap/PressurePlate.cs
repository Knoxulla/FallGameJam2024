using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public List<TrapBase> traps;
    public float cooldownTime = 5f;

    private bool isOnCooldown = false;


    SpriteRenderer button;

    private void Start()
    {
        button = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOnCooldown) return;

        if (other.CompareTag("Player"))
        {
            TriggerTraps();
            StartCoroutine(Cooldown());
        }

        
    }

    private void TriggerTraps()
    {
        foreach (var trap in traps)
        {
            trap.TriggerTrap();
        }
        //change sprite colour to red to show its on cooldown
        button.color = Color.red;
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;

        //change sprite colour to red to show its off cooldown
        button.color = Color.green;
    }
}
