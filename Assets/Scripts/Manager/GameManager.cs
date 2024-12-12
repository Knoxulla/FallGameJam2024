using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public PlayerController player1;
    public PlayerController player2;

    [Header("Player Start Positions")]
    public Vector3 player1StartPosition;
    public Vector3 player2StartPosition;

    private UIManager uiManager;

    private bool gameEnded = false;

    private bool playersRegistered = false;

    public PlayerSO player1SO;
    public PlayerSO player2SO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializePlayerPositions()
    {
        player1StartPosition = GameObject.FindGameObjectWithTag("Player1Spawn").GetComponent<Transform>().position;
        player2StartPosition = GameObject.FindGameObjectWithTag("Player2Spawn").GetComponent<Transform>().position;
    }

    //sets player sprites and animator controllers at runtime based on player controller index
    private void SetVisualsByPlayerID(PlayerController player)
    {
        GameObject playerObj = player.gameObject;
        SpriteRenderer playerRenderer = playerObj.AddComponent<SpriteRenderer>();
        Animator playerAnimator = playerObj.AddComponent<Animator>();

        if (player.playerIndex == 0)
        { 
            playerRenderer.sprite = player1SO.playerSprite;
            playerAnimator.runtimeAnimatorController = player1SO.animatorController;
        }

        if (player.playerIndex == 1)
        {
            playerRenderer.sprite = player2SO.playerSprite;
            playerAnimator.runtimeAnimatorController = player2SO.animatorController;
        }
    }


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

    public void SetUIManager(UIManager manager)
    {
        if (uiManager == null)
        {
            uiManager = manager;
            Debug.Log("UIManager has been set in GameManager via SetUIManager.");
        }
        else
        {
            Debug.LogWarning("UIManager is already set in GameManager.");
        }
    }


    private void Update()
    {
        if (!gameEnded && playersRegistered)
        {
            if (player1.hasGun && player2.hasGun)
            {
                CheckGameOverCondition();
            }
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
            EndGame("Tie", 0);
        }
        else if (!player1Alive)
        {
            Debug.Log("Player 2 wins!");
            EndGame("Player 2 Wins", 2);
        }
        else if (!player2Alive)
        {
            Debug.Log("Player 1 wins!");
            EndGame("Player 1 Wins", 1);
        }
        else if (!player1HasAmmo && !player2HasAmmo)
        {
            Debug.Log("Game ends in a tie (no bullets)!");
            EndGame("Tie (No Bullets)", 3);
        }
    }

    public void HandlePlayerDeath(string playerTag)
    {
        Debug.Log($"{playerTag} has died.");
        EndGame("Player 1 Wins", 1);
    }

    public void EndGame(string result, int resultID)
    {
        if (gameEnded) return;

        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            Debug.LogWarning("Attempted to end game outside of GameScene. Ignored.");
            return;
        }

        gameEnded = true;

        Debug.Log("Game Over: " + result);

        if (uiManager != null)
        {
            uiManager.ShowEndGameScreen(result, resultID);
            Debug.Log("End game screen shown.");
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            InitializePlayerPositions();

            if (uiManager == null)
            {
                uiManager = FindObjectOfType<UIManager>();
                if (uiManager == null)
                {
                    Debug.LogError("UIManager not found in GameScene!");
                }
                else
                {
                    Debug.Log("UIManager successfully found in GameScene via fallback.");
                }
            }

            if (player1 != null)
            {
                player1.transform.position = player1StartPosition;
                Debug.Log($"Player1 position set to {player1StartPosition}");
                SetVisualsByPlayerID(player1);
            }

            if (player2 != null)
            {
                player2.transform.position = player2StartPosition;
                Debug.Log($"Player2 position set to {player2StartPosition}");
                SetVisualsByPlayerID(player2);
            }
        }
        else
        {
            uiManager = null;
            Debug.Log($"Scene {scene.name} loaded. UIManager is set to null.");
        }
    }
}
