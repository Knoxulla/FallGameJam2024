using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 1;
    public string ownerTag;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            if (collision.gameObject.CompareTag(ownerTag))
            {
                return;
            }
            else
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    Debug.Log((player.isPlayerOne ? "Player1" : "Player2") + " got hit by " + ownerTag);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
