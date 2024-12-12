using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            GameManager.Instance.HandlePlayerDeath(other.gameObject.name);
            Debug.Log($"{other.name} hit by Spike");
        }
    }
}
