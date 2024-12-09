using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;

public class MainMenuManager : MonoBehaviour
{
    private bool buttonPress = false;

    // Update is called once per frame
    private void Start()
    {
        InputSystem.onAnyButtonPress.CallOnce(currentAction =>
        {
            buttonPress = true;
            if (currentAction is ButtonControl button && buttonPress)
            {
                LoadReadyScene();
                //ONLY USE THIS DEBUG FOR TROUBLESHOOTING, CAUSES BIG LAG.
                //Debug.Log($"Key {currentAction.name} pressed! (text: {currentAction.displayName})");

                buttonPress = false;
            }
        });
    }

    private void LoadReadyScene()
    {
        SceneManager.LoadSceneAsync("ReadyUpScene");
    }
}
