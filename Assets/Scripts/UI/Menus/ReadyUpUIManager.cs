using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadyUpUIManager : MonoBehaviour
{
    [SerializeField] Toggle playerOne;
    [SerializeField] Toggle playerTwo;
    [SerializeField] GameObject playerOneNotReady;
    [SerializeField] GameObject playerTwoNotReady;
    [SerializeField] GameObject readyUpPrompt;

    private bool buttonPress = false;

    private void Update()
    {
        CheckPlayerReady();
    }

    public void PlayerJoined() 
    {
        if (!playerOne.isOn)
        {
            playerOne.isOn = true;
        }
        else
        {
            playerTwo.isOn = true;
        }
    }

    public void PlayerLeft()
    {
        if (playerTwo.isOn)
        {
            playerTwo.isOn = false;
        }
        else
        {
            playerOne.isOn = false;
        }

        readyUpPrompt.SetActive(true);
    }


    public void CheckPlayerReady()
    {
        if (playerOne.isOn && playerTwo.isOn)
        {
            readyUpPrompt.SetActive(false);
            AllowLevelStart();
        }
    }

    public void AllowLevelStart()
    {
        InputSystem.onAnyButtonPress.CallOnce(currentAction =>
        {
            buttonPress = true;
            if (currentAction is ButtonControl button && buttonPress)
            {
                SceneManager.LoadSceneAsync("GameScene");
                //ONLY USE THIS DEBUG FOR TROUBLESHOOTING, CAUSES BIG LAG.
                //Debug.Log($"Key {currentAction.name} pressed! (text: {currentAction.displayName})");

                buttonPress = false;
            }
        });
    }

    #region Display Player Not Ready
    public void SetPlayerNotReady()
    {
        if (!playerOne.isOn)
        {
            playerOneNotReady.SetActive(true);
        }
        if (!playerTwo.isOn)
        {
            playerTwoNotReady.SetActive(true);
        }
        if (!playerTwo.isOn && !playerOne.isOn)
        {
            readyUpPrompt.SetActive(true);
        }
    }

    public void TurnOffNotReady()
    {
        if (playerOne.isOn)
        {
            playerOneNotReady.SetActive(false);
            readyUpPrompt.SetActive(false);

        }
        if (playerTwo.isOn)
        {
            playerTwoNotReady.SetActive(false);
            readyUpPrompt.SetActive(false);
        }
    }
    #endregion


}
