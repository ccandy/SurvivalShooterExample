using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int              startingHealth = 100;
    public int              currentHealth;
    public Slider           healthSlider;
    public Image            damageImage;
    public AudioClip        deathClip;
    public float            flashSpeed = 5f;
    public Color            flashColor = new Color(1f, 0f, 0f, 0.1f);

    private Animator        playerAC;
    private AudioSource     playerAudio;
    private PlayerMovement  playerMovement;
    private PlayerShooting  playerShooting;

    private bool            isDead;
    private bool            damage;

    void Awake()
    {
        playerAC        = gameObject.GetComponent<Animator>();
        playerAudio     = gameObject.GetComponent<AudioSource>();
        playerMovement  = gameObject.GetComponent<PlayerMovement>();
        playerShooting  = gameObject.GetComponentInChildren<PlayerShooting>();

        currentHealth   = startingHealth;

    }

    public void TakeDamage(int amount)
    {
        damage = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }


    }

    private void Death()
    {
        isDead = true;
        playerAC.SetTrigger("die");

        playerAudio.clip = deathClip;
        playerAudio.Play();


        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }




}
