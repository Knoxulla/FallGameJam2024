using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class RocketHandler : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform rocketTransform;
    [SerializeField] Transform fireTransform;
    [SerializeField] Preset rocketPreset;
    [SerializeField] Preset firePreset;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(ChangeRocketSize());
    }

    private IEnumerator ChangeRocketSize()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        rocketPreset.ApplyTo(rocketTransform);
        firePreset.ApplyTo(fireTransform);
    }
}