using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.playerIndex == 0)
            {
                GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
                //Debug.Log("Player 1 hit by Spike");
            }
            else if (playerController.playerIndex == 1)
            {
                GameManager.Instance.HandlePlayerDeath(playerController.playerIndex);
                //Debug.Log("Player 2 hit by Spike");
            }
        }
    }
}
