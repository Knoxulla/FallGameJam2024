using System.Collections;
using UnityEngine;

public class TeslaCoil : MonoBehaviour
{
    public Animator animator;
    public Collider2D killCollider;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError($"{gameObject.name} ȱ�� Animator �����");
            }
        }

        if (killCollider == null)
        {
            killCollider = GetComponent<Collider2D>();
            if (killCollider == null)
            {
                Debug.LogError($"{gameObject.name} ȱ�� Collider2D �����");
            }
        }

        killCollider.enabled = false;
    }

    public void StartElectricity(float duration)
    {
        StartCoroutine(ElectricityRoutine(duration));
    }

    private IEnumerator ElectricityRoutine(float duration)
    {
        if (animator != null)
        {
            animator.SetBool("IsActive", true);
        }

        killCollider.enabled = true;

        yield return new WaitForSeconds(duration);

        if (animator != null)
        {
            animator.SetBool("IsActive", false);
        }

        killCollider.enabled = false;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            GameManager.Instance.HandlePlayerDeath(other.gameObject.name);
            Debug.Log($"{other.name} hit by TeslaCoil");
        }
    }
}
