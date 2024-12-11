using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public PlayerController player1;
    public PlayerController player2;


    [Header("UI Manager")]
    public UIManager uiManager;

    private bool gameEnded = false;

    private bool playersRegistered = false;

    public void RegisterPlayer(PlayerController newPlayer)
    {
        if (player1 == null)
        {
            player1 = newPlayer;
            Debug.Log("Player 1 registered.");
        }
        else if (player2 == null)
        {
            player2 = newPlayer;
            Debug.Log("Player 2 registered.");
        }
        else
        {
            Debug.LogError("Cannot register more than two players!");
        }

        if (player1 != null && player2 != null)
        {
            playersRegistered = true;
            Debug.Log("Both players registered. Game is now active.");
        }
    }

    private void Update()
    {
        if (!gameEnded && playersRegistered)
        {
            CheckGameOverCondition();
        }
    }

    private void CheckGameOverCondition()
    {
        bool player1Alive = player1 != null;
        bool player2Alive = player2 != null;

        bool player1HasAmmo = player1 != null && player1.hasGun && player1.maxAmmo > 0;
        bool player2HasAmmo = player2 != null && player2.hasGun && player2.maxAmmo > 0;

        if (!player1Alive && !player2Alive)
        {
            Debug.Log("Game ends in a tie!");
            EndGame("Tie");
        }
        else if (!player1Alive)
        {
            Debug.Log("Player 2 wins!");
            EndGame("Player 2 Wins");
        }
        else if (!player2Alive)
        {
            Debug.Log("Player 1 wins!");
            EndGame("Player 1 Wins");
        }
        else if (!player1HasAmmo && !player2HasAmmo)
        {
            Debug.Log("Game ends in a tie (no bullets)!");
            EndGame("Tie (No Bullets)");
        }
    }

    public void EndGame(string result)
    {
        if (gameEnded) return;

        gameEnded = true;

        Debug.Log("Game Over: " + result);

        if (uiManager != null)
        {
            uiManager.ShowEndGameScreen(result);
        }
        else
        {
            Debug.LogError("UIManager is not assigned!");
        }

        StartCoroutine(PauseGameAfterFrame());
    }

    IEnumerator PauseGameAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame button clicked.");
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game restarted.");
    }
}
