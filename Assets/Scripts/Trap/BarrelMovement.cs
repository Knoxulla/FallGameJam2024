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
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestructionZone")) 
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player1"))
        {
            GameManager.Instance.HandlePlayerDeath("Player2");
            Debug.Log("Player1 hit by barrel");
        }
    }
}
