using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerIndex;
    public int health = 1;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Climbing Settings")]
    public float climbSpeed = 3f;
    private bool canClimb = false;
    private bool isClimbing = false;
    private Collider2D ladderCollider;
    private float originalGravityScale;

    [Header("Gun Settings")]
    public bool hasGun;
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
    private Vector2 moveInput;

    bool isFlipped = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAmmo = maxAmmo;
        originalGravityScale = rb.gravityScale;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RegisterPlayer(this);
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        HandleClimbing();
    }

    private void HandleClimbing()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * climbSpeed);

            if (!canClimb)
            {
                ExitClimbingMode();
            }
        }
        else
        {
            if (rb.gravityScale == 0f)
            {
                rb.gravityScale = originalGravityScale;
            }
        }
    }

    public void OnJump()
    {
        if (!isClimbing && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnMove(Vector2 moveInput)
    {
        this.moveInput = moveInput;

        if (canClimb && moveInput.y != 0)
        {
            EnterClimbingMode();
        }
        else if (!isClimbing)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

            if (moveInput.x > 0)
            {
                facingDirection = 1;
                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    transform.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    isFlipped = false;
                }
            }
            else if (moveInput.x < 0)
            {
                facingDirection = -1;
                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                isFlipped = true;
            }
        }
    }

    private void EnterClimbingMode()
    {
        isClimbing = true;
        rb.gravityScale = 0f;
    }

    private void ExitClimbingMode()
    {
        isClimbing = false;
        rb.gravityScale = originalGravityScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = true;
            ladderCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = false;

            if (isClimbing)
            {
                ExitClimbingMode();
            }
        }
    }


    public void OnFire()
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Attempted to fire while player is inactive.");
            return;
        }

        if (hasGun && currentAmmo > 0 && !onCooldown)
        {
            currentAmmo -= 1;
            onCooldown = true;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 0f));
            if (isFlipped)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                bullet.GetComponent<SpriteRenderer>().flipX = false;
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.ownerPlayerIndex = playerIndex;
            }

            StartCoroutine(ShootingCooldown());

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = new Vector2(facingDirection * bulletSpeed, 0);
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

    public void TakeDamage(int damage, string attackerIndex)
    {
        int attackerPlayerIndex = int.Parse(attackerIndex);
        if (attackerPlayerIndex == playerIndex)
        {
            return;
        }

        Debug.Log("Player " + playerIndex + " is hit by Player " + attackerPlayerIndex);
        Destroy(gameObject);
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