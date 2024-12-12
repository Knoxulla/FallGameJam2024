using System.Collections;
using UnityEngine;

public class TeslaCoilTrapController : TrapBase
{
    public GameObject teslaCoilPrefab;
    public Transform spawnPoint;
    public float activationInterval = 2f;
    public float activeDuration = 1f;
    public int maxActivations = 5;

    private int activationCount = 0;
    private Coroutine activationCoroutine;

    protected override void Awake()
    {
        base.Awake();
        SetState(TrapState.Inactive);
    }

    public override void TriggerTrap()
    {
        if (currentState == TrapState.Active) return;

        SetState(TrapState.Active);
        activationCoroutine = StartCoroutine(ActivateTeslaCoilPeriodically());
    }

    private IEnumerator ActivateTeslaCoilPeriodically()
    {
        while (currentState == TrapState.Active && activationCount < maxActivations)
        {
            ActivateTeslaCoil();
            activationCount++;
            Debug.Log($"Tesla Coil activation count: {activationCount}/{maxActivations}");

            if (activationCount >= maxActivations)
            {
                Debug.Log("Tesla Coil reached maximum activations.");
                SetState(TrapState.Inactive);
                yield break;
            }

            yield return new WaitForSeconds(activationInterval);
        }
    }

    private void ActivateTeslaCoil()
    {
        GameObject teslaCoil = Instantiate(teslaCoilPrefab, spawnPoint.position, Quaternion.identity);
        TeslaCoil teslaScript = teslaCoil.GetComponent<TeslaCoil>();

        if (teslaScript != null)
        {
            teslaScript.StartElectricity(activeDuration);
        }

        Debug.Log("Tesla Coil activated.");
    }

    protected override void SetState(TrapState newState)
    {
        base.SetState(newState);
        if (newState == TrapState.Inactive)
        {
            activationCount = 0;
            Debug.Log("Tesla Coil activation count has been reset.");
        }
    }

    private void OnDisable()
    {
        SetState(TrapState.Inactive);
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
        }
    }
}
