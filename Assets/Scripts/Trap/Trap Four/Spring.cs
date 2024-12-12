using UnityEngine;

public class Spring : MonoBehaviour
{
    public Animator animator;
    public float cooldownTime = 0.5f;
    private bool isReady = true;
    private float springForce;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError($"{gameObject.name} Need Animator Component!");
            }
        }

        isReady = true;
    }

    public void SetSpringForce(float force)
    {
        springForce = force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isReady && other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, springForce);
                Debug.Log($"{other.name} is sprung by the spring, applying the force: {springForce}");

                if (animator != null)
                {
                    animator.SetTrigger("IsActive");
                }

                StartCoroutine(CooldownRoutine());
            }
        }
    }

    private System.Collections.IEnumerator CooldownRoutine()
    {
        isReady = false;
        yield return new WaitForSeconds(cooldownTime);
        isReady = true;
    }
}
