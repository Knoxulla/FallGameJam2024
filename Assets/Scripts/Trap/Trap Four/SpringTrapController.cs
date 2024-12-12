using UnityEngine;

public class SpringTrapController : TrapBase
{
    [Header("Scene Springs")]
    public Spring[] springs;

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
}
