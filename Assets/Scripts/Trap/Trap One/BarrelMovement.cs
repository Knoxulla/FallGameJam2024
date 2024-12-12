using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;

    public void SetMovement(Vector2 direction, float moveSpeed)
    {
        moveDirection = direction.normalized;
        speed = moveSpeed;
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
            if (playerController.playerIndex == 0)
            {
                GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
                //Debug.Log("Player 1 hit by barrel");
                Destroy(gameObject);
            }
            else if (playerController.playerIndex == 1)
            {
                GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
                //Debug.Log("Player 2 hit by barrel");
                Destroy(gameObject);
            }
        }
    }
}
