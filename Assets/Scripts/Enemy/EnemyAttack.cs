using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float            timeFreq        = 0.5f;
    public int              attackDamage    = 10;

    private Animator        enemyAC;
    private GameObject      player;
    private PlayerHealth    playerHealth;
    private EnemyHealth     enemyHealth;
    private bool            playerInRange;
    private float           timer;


    void Awake()
    {
        player          = GameObject.FindGameObjectWithTag("Player");
        playerHealth    = player.GetComponent<PlayerHealth>();
        enemyHealth     = gameObject.GetComponent<EnemyHealth>();
        enemyAC         = gameObject.GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeFreq && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if(playerHealth.currentHealth <= 0)
        {
            enemyAC.SetTrigger("PlayerDead");
        }



    }


    private void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

}
