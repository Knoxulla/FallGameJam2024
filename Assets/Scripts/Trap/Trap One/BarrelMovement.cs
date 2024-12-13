using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;

    public bool loopSound = false;
    private AudioSource loopSFXSource;

    public void SetMovement(Vector2 direction, float moveSpeed)
    {
        moveDirection = direction.normalized;
        speed = moveSpeed;

        if (loopSound)
        {
            if (AudioManager.Instance != null)
            {
                loopSFXSource = AudioManager.Instance.PlaySFXLoop("BarrelRoll");
            }
        }
        else
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("BarrelRoll");
            }
        }
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestructionZone"))
        {
            Destroy(gameObject);
        }

        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (loopSFXSource != null)
        {
            loopSFXSource.Stop();
        }
    }
}
