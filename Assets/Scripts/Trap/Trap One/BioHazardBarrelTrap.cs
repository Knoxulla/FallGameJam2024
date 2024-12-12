using System.Collections;
using UnityEngine;

public class BioHazardBarrelTrap : TrapBase
{
    public GameObject barrelPrefab; 
    public Transform spawnPoint; 
    public float rollSpeed = 3f; 
    public float spawnInterval = 2f; 
    public Vector2 direction = Vector2.left;

    protected override void Awake()
    {
        base.Awake();
        SetState(TrapState.Inactive);
    }

    public override void TriggerTrap()
    {
        if (currentState == TrapState.Active) return;

        SetState(TrapState.Active);

        StartCoroutine(SpawnBarrels());
    }

    private IEnumerator SpawnBarrels()
    {
        SpawnSingleBarrel();
        yield return new WaitForSeconds(spawnInterval);
        SpawnSingleBarrel();

        SetState(TrapState.Inactive);
    }

    private void SpawnSingleBarrel()
    {
        GameObject barrel = Instantiate(barrelPrefab, spawnPoint.position, Quaternion.identity);
        BarrelMovement movement = barrel.GetComponent<BarrelMovement>();
        Animator barrelAnimator = barrel.GetComponent<Animator>();

        if (movement != null)
        {
            movement.SetMovement(direction, rollSpeed);
        }

        if (barrelAnimator != null)
        {
            barrelAnimator.SetBool("IsActive", true);
            Debug.Log("Barrel animation activated immediately.");
        }
    }
}
