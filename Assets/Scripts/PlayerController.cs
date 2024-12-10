using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, WASDPlayerControls.IPlayerActions
{
    [Header("Player Settings")]
    public bool isPlayerOne = true;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Gun Settings")]
    public bool hasGun = true;
    public int maxAmmo = 3;
    private int currentAmmo;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int facingDirection = 1;

    private WASDPlayerControls controls;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAmmo = maxAmmo;
        controls = new WASDPlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

        if (isPlayerOne)
        {
            controls.asset.bindingMask = InputBinding.MaskByGroup("SchemeWASD");
        }
        else
        {
            controls.asset.bindingMask = InputBinding.MaskByGroup("SchemeArrow");
        }
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        float moveX = moveInput.x;
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX > 0)
        {
            facingDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0)
        {
            facingDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Shoot()
    {
        if (hasGun && currentAmmo > 0)
        {
            currentAmmo--;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 90f));
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = new Vector2(facingDirection * bulletSpeed, 0);
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.ownerTag = isPlayerOne ? "Player1" : "Player2";
            }
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log((isPlayerOne ? "Player1" : "Player2") + " is hit!");
        if (isPlayerOne)
        {
            Debug.Log("Player2 Wins!");
        }
        else
        {
            Debug.Log("Player1 Wins!");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }
}
