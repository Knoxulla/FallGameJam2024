using UnityEngine;
using System.Collections;   

public class SpringTrapController : TrapBase
{
    [Header("Scene Springs")]
    public Spring[] springs;

    public float trapActiveDuration = 5f;

    protected override void Awake()
    {
        base.Awake();
        SetState(TrapState.Inactive);
    }

    public override void TriggerTrap()
    {
        if (currentState == TrapState.Active) return;

        SetState(TrapState.Active);
        ActivateSprings();

        StartCoroutine(AutoDeactivateTrapAfterDelay(trapActiveDuration));
    }

    private void ActivateSprings()
    {
        foreach (Spring spring in springs)
        {
            spring.Activate();
        }

        Debug.Log("Spring Trap activated. Springs are now active.");
    }

    private void OnDisable()
    {
        SetState(TrapState.Inactive);
        
        foreach (Spring spring in springs)
        {
            spring.Deactivate();
        }
        
    }

    private IEnumerator AutoDeactivateTrapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetState(TrapState.Inactive);

        foreach (Spring spring in springs)
        {
            spring.Deactivate();
        }

        Debug.Log("Spring Trap automatically deactivated after delay.");
    }
}
