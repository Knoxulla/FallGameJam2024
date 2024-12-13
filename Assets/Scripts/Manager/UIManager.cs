using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject endGamePanel;
    public TMP_Text resultText;

    public List<PlayerInput> playerInputs = new List<PlayerInput>();

    public GameObject End1;
    public GameObject End2;

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

        DontDestroyOnLoad(gameObject);

        GameObject scenePanel = GameObject.Find("GameEnd Panel");
        GameObject sceneEnd1 = GameObject.Find("Monkey win");
        GameObject sceneEnd2 = GameObject.Find("Scientist win");
        GameObject resultextinGameScene = GameObject.Find("Result");

        if (scenePanel != null)
        {
            endGamePanel = scenePanel;
            endGamePanel.SetActive(false);
            Debug.Log("endGamePanel found and set inactive in Awake.");
        }
        else
        {
            Debug.LogWarning("No 'GameEnd Panel' found in the current scene!");
        }

        if (sceneEnd1 != null)
        {
            End1 = sceneEnd1;
            End1.SetActive(false);
            Debug.Log("End1 found and set inactive in Awake.");
        }

        if (sceneEnd2 != null)
        {
            End2 = sceneEnd2;
            End2.SetActive(false);
            Debug.Log("End2 found and set inactive in Awake.");
        }
        if (resultextinGameScene != null)
        {
            resultText = resultextinGameScene.GetComponent<TMP_Text>();
        }
    }

    public void DisplayBulletCount()
    {
        // Placeholder
    }

    public void UpdateBulletCount() { }

    private void Start()
    {
        PlayerInput[] inputs = FindObjectsOfType<PlayerInput>();
        foreach (var input in inputs)
        {
            playerInputs.Add(input);
        }
    }

    public void ShowEndGameScreen(string result, int resultID)
    {
        StartCoroutine(ShowEndGameScreenDelayed(result, resultID));
    }

    private IEnumerator ShowEndGameScreenDelayed(string result, int resultID)
    {
        yield return new WaitForSeconds(1f);

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
            Debug.Log("End game panel activated via stored scene reference.");
        }
        else
        {
            Debug.LogWarning("endGamePanel is null in UIManager, cannot activate!");
        }

        if (resultText != null)
        {
            resultText.text = result;
        }
        else
        {
            Debug.LogWarning("Result text is not assigned in UIManager.");
        }

        HandleGameEnd(resultID);

        if (endGamePanel != null)
        {
            Debug.Log($"after ShowEndGameScreen: endGamePanel activeInHierarchy={endGamePanel.activeInHierarchy}");
        }
    }

    public void HandleGameEnd(int resultID)
    {
        switch (resultID)
        {
            case 0: // Tie, both dead
                Debug.Log("HandleGameEnd: Tie, both players dead.");
                break;

            case 1: // Player 1 wins
                if (End1 != null)
                {
                    End1.SetActive(true);
                    Debug.Log("HandleGameEnd: Player 1 wins, End1 activated.");
                }
                else
                {
                    Debug.LogWarning("End1 is null, cannot activate!");
                }
                break;

            case 2: // Player 2 wins
                if (End2 != null)
                {
                    End2.SetActive(true);
                    Debug.Log("HandleGameEnd: Player 2 wins, End2 activated.");
                }
                else
                {
                    Debug.LogWarning("End2 is null, cannot activate!");
                }
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
