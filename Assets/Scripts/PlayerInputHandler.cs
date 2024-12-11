using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, AllInputs.IPlayerActions
{
    private PlayerInput playerInput;
    private PlayerController playerController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.playerIndex = playerInput.playerIndex;
        }
        else
        {
            Debug.LogError("PlayerController not found on this PlayerInput object.");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.OnMove(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.OnJump();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.OnFire();
        }
    }

    public void OnSwitchActionMap(InputAction.CallbackContext context)
    {
        // 空实现
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // 空实现
    }
}
