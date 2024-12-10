using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public bool isPlayerOne = true;
    public int playerIndex;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Gun Settings")]
    public bool hasGun = true;
    public int maxAmmo = 3;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float shootingCooldown = 1f;
    private bool onCooldown = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int facingDirection = 1;

    private AllInputs controls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAmmo = maxAmmo;
        controls = new AllInputs();
        GetPlayerNumber();
    }

    private void OnEnable()
    {
        // moved to SetPlayerBindings()
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


    public int GetPlayerNumber()
    {
        return playerIndex;
    }

    //public void SetPlayerBindings()
    //{
    //    controls.Player.SetCallbacks(this);
    //    controls.Player.Enable();

    //    if (isPlayerOne)
    //    {
    //        controls.asset.bindingMask = InputBinding.MaskByGroup("PlayerOne");
    //        gameObject.GetComponent<PlayerInput>().defaultControlScheme = "PlayerOne";
    //    }
    //    else
    //    {
    //        controls.asset.bindingMask = InputBinding.MaskByGroup("PlayerTwo");
    //        gameObject.GetComponent<PlayerInput>().defaultControlScheme = "PlayerTwo";
    //    }
    //}

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnFire()
    {
        if (hasGun && currentAmmo > 0 && !onCooldown)
        {
            currentAmmo -= 1;
            onCooldown = true;
            StartCoroutine(ShootingCooldown());

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

    IEnumerator ShootingCooldown() 
    {
        yield return new WaitForSeconds(shootingCooldown);
        onCooldown = false;
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

    public void OnMove(Vector2 moveInput)
    {
        
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        if (moveInput.x > 0)
        {
            facingDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            facingDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    #region Unused Controls from Action Map

    public void OnLook(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSwitchActionMap(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    #endregion

}
