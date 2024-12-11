using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject endGamePanel;
    public TMP_Text resultText;

    public void ShowEndGameScreen(string result, int resultID)
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }

        if (resultText != null)
        {
            resultText.text = result;
        }

        HandleGameEnd(resultID);
    }

    public void HandleGameEnd(int resultID)
    {
        if (resultID == 0) // Tie, both dead
        { 
        
        }
        if (resultID == 1) // Player 1 wins (monkey)
        {

        }
        if (resultID == 2) // Player 2 wins (scientist)
        {

        }
        if (resultID == 3) // Tie, no bullets
        {

        }
    }
}
