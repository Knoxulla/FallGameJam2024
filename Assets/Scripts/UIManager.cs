using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject endGamePanel;
    public TMP_Text resultText;

    public void ShowEndGameScreen(string result)
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }

        if (resultText != null)
        {
            resultText.text = result;
        }
    }
}
