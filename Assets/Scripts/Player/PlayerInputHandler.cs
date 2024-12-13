using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMoveCanceled;
        playerInput.actions["Jump"].performed += OnJumpPerformed;
        playerInput.actions["Fire"].performed += OnFirePerformed;

        playerInput.actions["Click"].performed += OnClickPerformed;
    }

    private void OnDisable()
    {
        UnsubscribeFromActions();
    }
    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (playerInput.currentActionMap.name != "Player") return;
        if (playerController == null) return;

        if (playerController.isPlayerDead)
        {
            SceneManager.LoadScene("CreditScene");
        }
    }

    public void UnsubscribeFromActions()
    {
        if (playerInput != null && playerInput.actions != null)
        {
            var moveAction = playerInput.actions["Move"];
            moveAction.performed -= OnMovePerformed;
            moveAction.canceled -= OnMoveCanceled;

            var jumpAction = playerInput.actions["Jump"];
            jumpAction.performed -= OnJumpPerformed;

            var fireAction = playerInput.actions["Fire"];
            fireAction.performed -= OnFirePerformed;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerController == null) return;

        if (playerInput.currentActionMap.name != "Player") return;

        Vector2 moveInput = context.ReadValue<Vector2>();
        if (playerController != null)
        {
            playerController.OnMove(moveInput);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerController == null) return;

        if (playerInput.currentActionMap.name != "Player") return;

        if (playerController != null)
        {
            playerController.OnJump();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (playerController == null) return;

        if (playerInput.currentActionMap.name != "Player") return;

        if (playerController != null)
        {
            playerController.OnFire();
        }
    }

    public void OnSwitchActionMap(InputAction.CallbackContext context)
    {
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        OnMove(context);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        OnMove(new InputAction.CallbackContext());
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (playerController == null) return;
        playerController.OnJump();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (playerController == null) return;
        playerController.OnFire();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        OnClick(context);
    }
}
