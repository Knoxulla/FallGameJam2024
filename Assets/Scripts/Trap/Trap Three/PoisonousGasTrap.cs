using System.Collections;
using UnityEngine;

public class PoisonousGasTrap : TrapBase
{
    public ParticleSystem gasParticleSystem;
    public float duration = 5f;

    protected override void Awake()
    {
        base.Awake();
        SetState(TrapState.Inactive);

        if (gasParticleSystem != null)
        {
            gasParticleSystem.gameObject.SetActive(false);
        }
    }

    public override void TriggerTrap()
    {
        if (currentState == TrapState.Active) return;

        SetState(TrapState.Active);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("GasRelease");
        }

        StartCoroutine(EmitGas());
    }

    private IEnumerator EmitGas()
    {
        //Debug.Log("Gas emitted");
        if (gasParticleSystem != null)
        {
            gasParticleSystem.gameObject.SetActive(true);
            gasParticleSystem.Play();

            Animator gasAnimator = gasParticleSystem.GetComponent<Animator>();
            if (gasAnimator != null)
            {
                gasAnimator.SetBool("IsActive", true);
            }
        }

        yield return new WaitForSeconds(duration);

        if (gasParticleSystem != null)
        {
            gasParticleSystem.Stop();
            gasParticleSystem.gameObject.SetActive(false);

            Animator gasAnimator = gasParticleSystem.GetComponent<Animator>();
            if (gasAnimator != null)
            {
                gasAnimator.SetBool("IsActive", false);
            }
        }

        //Debug.Log("Gas stopped");

        SetState(TrapState.Inactive);
    }
}
