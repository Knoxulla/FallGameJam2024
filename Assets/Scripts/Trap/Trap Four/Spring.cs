using System.Collections;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Animator animator;
    public float cooldownTime = 0.5f;
    public bool isReady = true;
    public float springForce = 10f;
    public bool isActive = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }


        if (animator != null)
        {
            animator.Play("Idle");
        }

        isReady = true;
    }

    public void SetSpringForce(float force)
    {
        springForce = force;
    }


    public void Activate()
    {
        isActive = true;
    }


    public void Deactivate()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && isReady && other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, springForce);

                if (animator != null)
                {
                    animator.SetTrigger("Activate");
                }

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX("SpringActivate");
                }

                StartCoroutine(CooldownRoutine());
            }
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isReady = false;
        yield return new WaitForSeconds(cooldownTime);
        isReady = true;
    }
}
