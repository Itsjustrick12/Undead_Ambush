using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool piercing = false;
    private int pierceCount = 0;
    private int maxPierce = 1;

    //For Screen Shake
    private float shakeAmt = .5f;
    private float shakeTime = 0.15f;

    public bool shotRight = true;

    public float speed;
    public Rigidbody2D rb;
    public float damage;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CinemachineShake.Instance.ShakeCamera(shakeAmt, shakeTime);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (shotRight)
        {
            rb.velocity = transform.right * speed;
        } else
        {
            rb.velocity = -transform.right * speed;
        }
    }

    public void updateDamage(float newDamage) {
        damage = newDamage;
    }

    public void updateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void EnablePiercing(int mP)
    {
        piercing = true;
        maxPierce = mP;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        ZombieAi zombie = hitInfo.GetComponent<ZombieAi>();
        if (zombie != null)
        {
            zombie.TakeDamage(damage, ZombieAi.DamageType.PROJECTILE);
            if (!piercing || (pierceCount >= maxPierce))
            {
                Destroy(gameObject);
            } else
            {
                pierceCount++;
            }
        }
        if (hitInfo.gameObject.tag == "Blocker")
        {
            Destroy(gameObject);
        }
    }
}
