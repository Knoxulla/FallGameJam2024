using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CreditScene")
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                //Destroy(audioManager.gameObject);
            }

            PlayerInputManager pim = FindObjectOfType<PlayerInputManager>();
            if (pim != null)
            {
                Destroy(pim.gameObject);
            }

            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                Destroy(uiManager.gameObject);
            }

            if (GameManager.Instance != null)
            {
                Destroy(GameManager.Instance.gameObject);
            }
        }
    }
}
