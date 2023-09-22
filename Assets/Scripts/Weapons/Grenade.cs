using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage = 20;
    private Rigidbody2D rb;
    public float speed = 3;
    
    //For Screen Shake
    private float shakeAmt = 2.5f;
    private float shakeTime = .5f;

    
    public bool thrownRight = true;

    private Vector3 launchOffset;
    public bool thrown;

    public float explosionRadius = 4;
    public GameObject explosionEffect;

    private void FixedUpdate()
    {
        if (thrownRight)
        {
            transform.Rotate(0, 0, 360 * Time.deltaTime); //rotates 360 degrees per second around z axis

        }
        else
        {
            transform.Rotate(0, 0, -360 * Time.deltaTime); //rotates -360 degrees per second around z axis
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction;
        if (thrownRight)
        {
            direction = transform.right * speed + Vector3.up;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        } else
        {
            direction = -transform.right * speed + Vector3.up;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
        
        transform.Translate(launchOffset);
        
    }
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if (hitInfo.gameObject.tag == "Blocker")
        {
            Destroy(gameObject);
        }
        if (hitInfo.gameObject.tag == "Enemy")
        {
            AudioManager.Instance.PlaySFX("Explosion");
            Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
            CinemachineShake.Instance.ShakeCamera(shakeAmt, shakeTime);
            Collider2D[] zombies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, 1 << LayerMask.NameToLayer("EnemyLayer"));
            foreach (Collider2D en in zombies)
            {
                ZombieAi zombieAi = en.GetComponent<ZombieAi>();
                if (zombieAi != null && zombieAi.tag == "Enemy")
                {
                    zombieAi.TakeDamage(damage,ZombieAi.DamageType.EXPLOSIVE);
                    
                }
            }
            Destroy(gameObject);

        }
    }

}
