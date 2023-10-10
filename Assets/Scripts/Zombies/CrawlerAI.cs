using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAI : ZombieAi
{
    private float lungeTimer = 0f;
    public float lungeDuration = 0.5f;
    private bool isLunging = false;

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (isLunging)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                lungeTimer += Time.deltaTime;

                if (lungeTimer >= lungeDuration)
                {
                    // Lunge duration completed, stop lunging
                    isLunging = false;
                    lungeTimer = 0f;
                    rb.velocity = Vector2.zero;
                }
            }
        }
        else
        {
            animator.SetTrigger("IsDead");
            BloodTrail trail = GetComponent<BloodTrail>();
            if (trail != null)
            {
                trail.CreateBloodInstance(true);
                trail.EndTrail();
            }
            rb.velocity = Vector2.zero;
        }

    }

    public void StartLunge()
    {
        isLunging = true;
    }
}
