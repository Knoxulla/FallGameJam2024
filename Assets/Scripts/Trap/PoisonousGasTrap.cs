using System.Collections;
using UnityEngine;

public class PoisonousGasTrap : TrapBase
{
    public ParticleSystem gasParticleSystem;
    public float duration = 5f;

    public override void TriggerTrap()
    {
        StartCoroutine(EmitGas());
    }

    private IEnumerator EmitGas()
    {
        Debug.Log("Gas emitted");
        gasParticleSystem.gameObject.SetActive(true);
        gasParticleSystem.Play();
        yield return new WaitForSeconds(duration);
        gasParticleSystem.Stop();
        gasParticleSystem.gameObject.SetActive(false);
        Debug.Log("Gas stopped");
    }
}
