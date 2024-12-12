using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInitializer : MonoBehaviour
{
    public Vector3 readyUpPosition = new Vector3(5f, -6f, 0f);

    public Vector3 gameStartPosition;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ReadyUpScene")
        {
            transform.position = readyUpPosition;
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            transform.position = gameStartPosition;
        }
    }
}
