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
    public float fadeDuration = 2f;

    public List<PlayerInput> playerInputs = new List<PlayerInput>();

    private Coroutine blinkingCoroutine;

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

        End1.SetActive(false);
        End2.SetActive(false);

        DontDestroyOnLoad(gameObject);
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
        }
        else
        {
            Debug.LogWarning("Result text is not assigned in UIManager.");
        }

        HandleGameEnd(resultID);

        foreach (var playerInput in playerInputs)
        {
            playerInput.SwitchCurrentActionMap("UI");
            var clickAction = playerInput.actions.FindAction("Click");
            if (clickAction != null)
            {
                clickAction.performed += OnUIClick;
            }
            else
            {
                Debug.LogWarning("Click Action not found in the UI Action Map.");
            }
        }

        foreach (var playerInput in playerInputs)
        {
            PlayerInputHandler inputHandler = playerInput.GetComponent<PlayerInputHandler>();
            if (inputHandler != null)
            {
                inputHandler.UnsubscribeFromActions();
            }

            playerInput.SwitchCurrentActionMap("UI");
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
                End1.SetActive(true);
                Debug.Log("HandleGameEnd: Player 1 wins.");
                break;
            case 2: // Player 2 wins
                End2.SetActive(true);
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

    private void OnUIClick(InputAction.CallbackContext context)
    {
        foreach (var playerInput in playerInputs)
        {
            var clickAction = playerInput.actions.FindAction("Click");
            if (clickAction != null)
            {
                clickAction.performed -= OnUIClick;
            }
        }

        SceneManager.LoadScene("CreditScene");
    }

    private void OnDestroy()
    {
        foreach (var playerInput in playerInputs)
        {
            var clickAction = playerInput.actions.FindAction("Click");
            if (clickAction != null)
            {
                clickAction.performed -= OnUIClick;
            }
        }
    }
}
