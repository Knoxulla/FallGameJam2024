using UnityEngine;
using System.Collections;

public class SpringTrapController : TrapBase
{
    public GameObject springPrefab;
    public Transform spawnPoint;
    public float springForce = 10f;
    public float existenceDuration = 3f;

    private Coroutine existenceCoroutine;

    protected override void Awake()
    {
        base.Awake();
        SetState(TrapState.Inactive);
    }

    public override void TriggerTrap()
    {
        if (currentState == TrapState.Active) return;

        SetState(TrapState.Active);
        SpawnSpring();
    }

    private void SpawnSpring()
    {
        GameObject spring = Instantiate(springPrefab, spawnPoint.position, Quaternion.identity);
        Spring springScript = spring.GetComponent<Spring>();

        if (springScript != null)
        {
            springScript.SetSpringForce(springForce);
        }

        existenceCoroutine = StartCoroutine(SpringExistenceRoutine(spring));
    }

    private IEnumerator SpringExistenceRoutine(GameObject spring)
    {
        yield return new WaitForSeconds(existenceDuration);

        if (spring != null)
        {
            Destroy(spring);
            //Debug.Log("Spring Trap has expired and been destroyed.");
        }

        SetState(TrapState.Inactive);
    }

    private void OnDisable()
    {
        SetState(TrapState.Inactive);
        if (existenceCoroutine != null)
        {
            StopCoroutine(existenceCoroutine);
        }
    }
}
