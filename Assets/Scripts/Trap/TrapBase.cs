using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public enum TrapState
    {
        Inactive,
        Active
    }

    protected virtual void Awake()
    {

    }

    protected TrapState currentState = TrapState.Inactive;

    public abstract void TriggerTrap();

    protected virtual void SetState(TrapState newState)
    {
        currentState = newState;
        Debug.Log($"{gameObject.name} state is switched to: {newState}");
    }
}
