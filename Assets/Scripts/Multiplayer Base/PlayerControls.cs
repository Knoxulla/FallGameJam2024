using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerControls : MonoBehaviour
{

    public float meleeWeaponAttackRate;
    public float rangedWeaponAttackRate;

    public bool facingRight = true;

    Vector2 aimDirection;
    public Animator myAnim;
    AudioSource myAS;

    public GameObject meleeAttackProjectile;
    float nextMeleeAttack;
    bool meleeAttack;

    public ParticleSystem myDust;

    //   float rangedWeapon;
    public GameObject rangedWeaponProjectile;
    public GameObject rangedWeaponFX;
    bool rangeEquiped;
    bool rangedAttack;
    float nextRangedAttack;

    Vector2 moveDirection;
    bool isMoving;
    Rigidbody2D myRB;
    public float maxSpeed;

    bool isPaused;

    CanvasGroup myControlPanel;


    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        nextMeleeAttack = Time.time;
        nextRangedAttack = Time.time;

        myRB = GetComponentInParent<Rigidbody2D>();

        myControlPanel = GameObject.FindGameObjectWithTag("ControlCanvas").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (meleeAttack) MeleeAttack();
        if (rangedAttack) RangedAttack();
        if (isMoving) PlayerMovement();
    }

    public void OnPause(InputValue pauseValue)
    {
        float paused = pauseValue.Get<float>();

        if (paused == 1)
        {
            if (!isPaused)
            {
                isPaused = true;
                myControlPanel.alpha = 1;
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                myControlPanel.alpha=0;
                Time.timeScale = 1;
            }
        }
    }

    public void OnStartGame(InputValue startValue)
    {
        float start = startValue.Get<float>();

        if (isPaused) 
        {
            isPaused = false;
            myControlPanel.alpha = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }

    public void OnQuitGame(InputValue quitValue)
    {
        float start = quitValue.Get<float>();

        if (isPaused)
        {
            Application.Quit();
        }
    }

    public void OnMove(InputValue moveValue)
    {
        moveDirection = moveValue.Get<Vector2>();

        if (moveDirection.x > 0 && !facingRight && !meleeAttack && !rangedAttack) Flip();
        else if (moveDirection.x < 0 && facingRight && !meleeAttack && !rangedAttack) Flip();

        if (Mathf.Abs(moveDirection.magnitude) > 0 && !rangeEquiped)
        {
            isMoving = true;
            if (!meleeAttack) myAnim.SetBool("isMoving", isMoving);
            else myAnim.SetBool("isMoving", false);

            myDust.Play();
        }
        else
        {
            isMoving = false;
            myRB.velocity = Vector2.zero;
            myAnim.SetBool("isMoving", isMoving);
            myDust.Stop();
        }
    }

    public void OnLook(InputValue lookValue)
    {
        aimDirection = lookValue.Get<Vector2>();

        if (aimDirection.x > 0 && !facingRight) Flip();
        else if (aimDirection.x < 0 && facingRight) Flip();

        if (Mathf.Abs(aimDirection.magnitude) > 0)
        {
            if (rangeEquiped)
            {
                rangedAttack = true;
                meleeAttack = false;
 //               nextMeleeAttack = Time.time + meleeWeaponAttackRate;
            }
            else
            {
                meleeAttack = true;
                rangedAttack = false;
 //               nextRangedAttack = Time.time + rangedWeaponAttackRate;
            }
        }
        else
        {
            rangedAttack = false;
            meleeAttack = false;
        }
    }

    public void OnRangedReady(InputValue rangedValue)
    {
        float rangedWeapon = rangedValue.Get<float>();
        if (rangedWeapon == 1)
        {
            rangeEquiped = true;

            if (meleeAttack)
            {
                meleeAttack = false;
                rangedAttack = true;
            }
        }
        else //if(rangedWeapon == 0)
        {
            rangeEquiped = false;
            if (rangedAttack)
            {
                meleeAttack = true;
                rangedAttack = false;
            }
        }

        myAnim.SetBool("rangedEquiped", rangeEquiped);
    }

    void MeleeAttack()
    {
        transform.up = new Vector3(aimDirection.x, aimDirection.y, 0).normalized;
        if (nextMeleeAttack < Time.time)
        {
            myAnim.SetTrigger("meleeAttack");

            GameObject temp = Instantiate(meleeAttackProjectile, transform.position, transform.rotation);
            temp.transform.SetParent(transform);
            nextMeleeAttack = Time.time + meleeWeaponAttackRate;
        }
    }

    void RangedAttack()
    {
        transform.up = new Vector3(aimDirection.x, aimDirection.y, 0).normalized;
 
        if (nextRangedAttack < Time.time)
        {

            myAnim.SetTrigger("rangedAttack");
            Instantiate(rangedWeaponProjectile, transform.position, gameObject.transform.rotation);
            Instantiate(rangedWeaponFX, transform.position, gameObject.transform.rotation);
 
            nextRangedAttack = Time.time + rangedWeaponAttackRate;
        }
    }

    void PlayerMovement()
    {
        myRB.velocity = new Vector2(maxSpeed * moveDirection.x, maxSpeed * moveDirection.y);
    }

    public void PlayMeleeSound()
    {
        myAS.Play();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 flipVector = transform.root.transform.localScale;
        flipVector.x *= -1;
        transform.root.transform.localScale = flipVector;
    }

    public void SetZeroVelocity()
    {
        myRB.velocity = Vector2.zero;
    }
}
