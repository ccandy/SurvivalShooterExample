using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int              startingHealth  = 100;
    public int              currentHealth;
    public int              scoreValue      = 10;

    public float            sinkSpeed       = 2.5f;

    public AudioClip        deathClip;


    private Animator        enemyAC;
    private AudioSource     enemyAudio;
    private ParticleSystem  hitParticle;
    private CapsuleCollider capsuleCollider;

    private bool            isDead;
    private bool            isSinking;

    void Awake()
    {
        enemyAC         = gameObject.GetComponent<Animator>();
        enemyAudio      = gameObject.GetComponent<AudioSource>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        hitParticle     = gameObject.GetComponentInChildren<ParticleSystem>();

        currentHealth   = startingHealth;
         
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

        enemyAudio.Play();
        currentHealth -= amount;

        hitParticle.transform.position = hitPoint;
        hitParticle.Play();

        if(currentHealth <= 0)
        {
            Death();
        }
    }


    private void Death()
    {
        isDead                      = true;
        capsuleCollider.isTrigger   = true;
        enemyAC.SetTrigger("die");

        enemyAudio.clip             = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
         
        gameObject.GetComponent<NavMeshAgent>().enabled     = false;
        gameObject.GetComponent<Rigidbody>().isKinematic    = true;

        isSinking = true;
        ScoreManager.score += scoreValue;

        Destroy(gameObject, 2f);
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


}
