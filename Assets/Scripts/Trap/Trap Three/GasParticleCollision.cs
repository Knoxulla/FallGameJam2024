using UnityEngine;

public class GasParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
            Debug.Log($"Particle collided with Player {playerController.playerIndex}");
            Destroy(gameObject);
        }
    }
}
