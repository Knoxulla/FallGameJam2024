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
                //Debug.LogError($"{gameObject.name} Need Animator Component!");
            }
        }

        if (killCollider == null)
        {
            killCollider = GetComponent<Collider2D>();
            if (killCollider == null)
            {
                //Debug.LogError($"{gameObject.name} Need Animator Component!");
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
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.playerIndex == 0)
            {
                GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
                Debug.Log("Player 1 hit by Tesla Coil");
                Destroy(gameObject);
            }
            else if (playerController.playerIndex == 1)
            {
                GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
                Debug.Log("Player 2 hit by Tesla Coil");
                Destroy(gameObject);
            }
        }
    }
}
