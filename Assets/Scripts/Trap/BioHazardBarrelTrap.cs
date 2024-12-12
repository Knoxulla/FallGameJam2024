using System.Collections;
using UnityEngine;

public class BioHazardBarrelTrap : TrapBase
{
    public GameObject barrelPrefab; 
    public Transform spawnPoint; 
    public float rollSpeed = 3f; 
    public float spawnInterval = 2f; 
    public Vector2 direction = Vector2.left;

    public override void TriggerTrap()
    {
        StartCoroutine(SpawnBarrels());
    }

    private IEnumerator SpawnBarrels()
    {
        SpawnSingleBarrel();
        yield return new WaitForSeconds(spawnInterval);
        SpawnSingleBarrel();
    }

    private void SpawnSingleBarrel()
    {
        GameObject barrel = Instantiate(barrelPrefab, spawnPoint.position, Quaternion.identity);
        BarrelMovement movement = barrel.GetComponent<BarrelMovement>();
        if (movement != null)
        {
            movement.SetMovement(direction, rollSpeed);
        }
    }
}
