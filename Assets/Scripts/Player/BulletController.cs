using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 1;
    public int ownerPlayerIndex;

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
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.playerIndex != ownerPlayerIndex)
            {
                player.TakeDamage(damage, ownerPlayerIndex.ToString());
                Destroy(gameObject);
            }
            return;
        }
        Destroy(gameObject);
    }
}
