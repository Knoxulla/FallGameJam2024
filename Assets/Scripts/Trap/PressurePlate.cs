using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public List<TrapBase> traps;
    public float cooldownTime = 5f;

    private bool isOnCooldown = false;

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
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
