using UnityEngine;

public class GasParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        GameManager.Instance.HandlePlayerDeath("Player2");
        Debug.Log("Particle collided with " + other.name);
    }
}
