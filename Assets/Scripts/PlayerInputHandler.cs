using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour, AllInputs.IPlayerActions
{
    private PlayerInput playerInput;
    private PlayerController playerController;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerControls = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = playerControls.FirstOrDefault(m => m.GetPlayerNumber() == index);
    }

    public bool PlayerNullCheck()
    {
        if (playerController != null) return true;
        else return false;
    }

    public void OnFire(CallbackContext context)
    {
        if (!PlayerNullCheck())
        {
            return;
        }
        playerController.OnFire();
    }

    public void OnJump(CallbackContext context)
    {
        if (!PlayerNullCheck())
        {
            return;
        }
        playerController.OnJump();
    }

    public void OnMove(CallbackContext context)
    {
        if (!PlayerNullCheck())
        {
            return;
        }

        playerController.OnMove(context.ReadValue<Vector2>());
    }

    public void OnSwitchActionMap(CallbackContext context)
    {
        if (!PlayerNullCheck())
        {
            return;
        }

    }

    public void OnLook(CallbackContext context)
    {
        if (!PlayerNullCheck())
        {
            return;
        }

    }
}