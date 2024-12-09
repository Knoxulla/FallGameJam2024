using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;  //(1)
    int healthMultiplier = 1;
    int currentHealth;  //(2)
    int tempMaxHealth;

    public Image healthSlider;

    public GameObject pointsDrop;

    Animator myAnim;
    PlayerControls myPC;
    Collider2D myCol;

    public TMP_Text myMult;
    public TMP_Text myHealth;
    public GameObject damageDisplay;

    public AudioClip[] painSounds;  //(4)  Audio feedback
    AudioSource myAS;  //(4)  Audio feedback

    float timeOfDeath;
    public float deathHold;
    public float invulnerableTime;
    bool isInvulnerable = false;
 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        tempMaxHealth = maxHealth;
        myAS = GetComponentInChildren<AudioSource>();
        SetHealthSlider();
        myAnim = GetComponentInChildren<Animator>();
        myPC = GetComponentInChildren<PlayerControls>();
        myCol = GetComponent<Collider2D>();
    }

    private void Update()
    {
 
    }

    public void AddDamage(int damage)  //(3)
    {
        currentHealth -= damage;

        if (currentHealth < 0) currentHealth = 0;

        GameObject temp = Instantiate(damageDisplay, transform.position, Quaternion.identity);
        temp.GetComponent<DamageCanvasController>().SetText(damage);

        //check if dead
        if (currentHealth <= 0)
        {
            MakeDead();
        }
        if (currentHealth != maxHealth)
        {
            SetHealthSlider();
            myAS.PlayOneShot(painSounds[Random.Range(0, painSounds.Length)]);  //audio feedback
        }
    }

    public void MakeDead()  //(6)
    {
        SetHealthSlider();
        myAS.PlayOneShot(painSounds[Random.Range(0, painSounds.Length)]);  //audio feedback
        myAnim.SetTrigger("isDead");
        myPC.SetZeroVelocity();
        myPC.enabled = false;
        myCol.enabled = false;
        GameObject temp = Instantiate(pointsDrop, transform.position, Quaternion.identity);
        temp.GetComponent<SkullPickUpController>().SetPoints(healthMultiplier);
        healthMultiplier = 0;
        SetMultiplier(1);  
    }

    void SetHealthSlider()
    {
        healthSlider.fillAmount = (1 - ((float)currentHealth / (float)tempMaxHealth));
        myMult.text = healthMultiplier.ToString();
        myHealth.text = currentHealth.ToString();
    }

    public void SetMultiplier(int mult)
    {
        float tempHealth = (float)currentHealth / (float)healthMultiplier;
        healthMultiplier += mult;
        tempMaxHealth = maxHealth * healthMultiplier;
        currentHealth = (int)tempHealth * healthMultiplier;
        SetHealthSlider();
    }

    public void Respawn()
    {
        isInvulnerable = false;
        myPC.enabled = true;
        myCol.enabled = true;
        currentHealth = maxHealth;
        tempMaxHealth = maxHealth;
        SetHealthSlider();
    }

}
