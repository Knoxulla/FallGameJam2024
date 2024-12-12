using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject endGamePanel;
    public TMP_Text resultText;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetUIManager(this);
            Debug.Log("UIManager has been assigned to GameManager.");
        }
        else
        {
            Debug.LogError("GameManager instance is null! Cannot assign UIManager.");
        }
    }

    public void ShowEndGameScreen(string result, int resultID)
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
            Debug.Log("End game panel activated.");
        }
        else
        {
            Debug.LogWarning("End game panel is not assigned in UIManager.");
        }

        if (resultText != null)
        {
            resultText.text = result;
            Debug.Log($"Result text set to: {result}");
        }
        else
        {
            Debug.LogWarning("Result text is not assigned in UIManager.");
        }

        HandleGameEnd(resultID);
    }

    public void HandleGameEnd(int resultID)
    {
        switch (resultID)
        {
            case 0: // Tie, both dead
                Debug.Log("HandleGameEnd: Tie, both players dead.");
                break;
            case 1: // Player 1 wins (monkey)
                Debug.Log("HandleGameEnd: Player 1 wins.");
                break;
            case 2: // Player 2 wins (scientist)
                Debug.Log("HandleGameEnd: Player 2 wins.");
                break;
            case 3: // Tie, no bullets
                Debug.Log("HandleGameEnd: Tie, no bullets.");
                break;
            default:
                Debug.LogWarning($"HandleGameEnd: Unknown resultID {resultID}.");
                break;
        }
    }
}
