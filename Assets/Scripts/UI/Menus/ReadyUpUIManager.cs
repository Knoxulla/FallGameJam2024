using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private TMP_Text promptText;

    [SerializeField] Image loadingBar;

    private bool buttonPress = false;
    bool playerOneReady = false;
    bool playerTwoReady = false;

    private void Awake()
    {
        promptText = readyUpPrompt.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (playerOneReady && playerTwoReady)
        {
            AllowLevelStart();
            playerOneReady = false;
            playerTwoReady = false;
        }

        //playerOne.isOn = playerOneReady;
        //playerTwoReady = playerTwo.isOn; // to debug without having another player, uncomment line underneath and comment out this line for proper form
        //playerTwo.isOn = playerTwoReady;
            
    }

    public void PlayerJoined() 
    {
        if (!playerOne.isOn)
        {
            playerOne.isOn = true;
            playerOneReady = true;
        }
        else
        {
            playerTwo.isOn = true;
            playerTwoReady = true;
        }
        CheckPlayerReady();
        TurnOffNotReady();
    }

    public void PlayerLeft()
    {
        if (playerTwo.isOn)
        {
            playerTwo.isOn = false;
            playerTwoReady = false;
        }
        else
        {
            playerOne.isOn = false;
            playerOneReady = false;
        }

        SetPlayerNotReady();
        ChangePrompt("Ready Up");
    }

    public void CheckPlayerReady()
    {
        if (playerOne.isOn && playerTwo.isOn)
        {
            readyUpPrompt.SetActive(false);
            ChangePrompt("Press Any Button to Begin");
        }
    }

    public void ChangePrompt(string prompt) 
    {
        promptText.text = prompt;
        readyUpPrompt.SetActive(true);
    }

    public void AllowLevelStart()
    {
        playerOneReady = playerOne.isOn;
        playerTwoReady = playerTwo.isOn;

        

        InputSystem.onAnyButtonPress.CallOnce(currentAction =>
        {
            
            buttonPress = true;
            if (currentAction is ButtonControl button && buttonPress)
            {
                LoadGameScene();
                //ONLY USE THIS DEBUG FOR TROUBLESHOOTING, CAUSES BIG LAG.
                //Debug.Log($"Key {currentAction.name} pressed! (text: {currentAction.displayName})");

                buttonPress = false;
                
            }
        });
    }

    public void LoadGameScene() 
    {
        SceneManager.LoadScene("GameScene");
    }

    


    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progressiveValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progressiveValue;
        }
        yield return null;
    }

    #region Display Player Not Ready
    public void SetPlayerNotReady()
    {
        if (!playerOneReady)
        {
            playerOneNotReady.SetActive(true);
        }
        if (!playerTwoReady)
        {
            playerTwoNotReady.SetActive(true);
        }

        if (!playerTwo.isOn || !playerOne.isOn)
        {
            readyUpPrompt.SetActive(true);
        }
    }

    public void TurnOffNotReady()
    {
        if (playerOne.isOn)
        {
            playerOneNotReady.SetActive(false);
            

        }
        if (playerTwo.isOn)
        {
            playerTwoNotReady.SetActive(false);
           
        }
    }
    #endregion


}
