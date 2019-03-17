using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int              damagePerShot = 20;

    public float            shootFreq = 0.15f;
    public float            range = 100f;

    private float           timer;
    private float effectDisplayTime;

    private Ray             shootRay;
    private RaycastHit      shootHit;

    private int             shootableMask;

    private ParticleSystem  gunParticles;
    private LineRenderer    gunLine;
    private AudioSource     gunAudio;
    private Light           gunLight;


    void Awake()
    {
        shootableMask   = LayerMask.GetMask("Shootable");



        gunAudio        = gameObject.GetComponent <AudioSource>();
        gunLight        = gameObject.GetComponentInChildren <Light>();
        gunParticles    = gameObject.GetComponentInChildren<ParticleSystem>();
        gunLine         = gameObject.GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= shootFreq)
        {
            Shoot();
        }
        else
        {
            DisableEffect();
        }
    }

    private void Shoot()
    {
        timer                   = 0f;
        gunAudio.Play();
        gunLight.enabled        = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled         = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin         = transform.position;
        shootRay.direction      = transform.forward;

        if(Physics.Raycast(shootRay.origin,shootRay.direction,out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }


    public void DisableEffect()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
