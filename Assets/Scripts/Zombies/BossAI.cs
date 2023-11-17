using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : ZombieAi
{
    private bool isPaused = false;
    private Transform playerLocation;
    private PlayerMovement player;
    private bool facingRight = true;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    //Change the way the boss moves
    void FixedUpdate()
    {
        playerLocation = player.gameObject.transform;
        if (!isDead)
        {
            //If in the middle of a lunge, dont move
            if (!isPaused)
            {
                //Follow the player but always move
                if (transform.position.x > playerLocation.position.x)
                {
                    //If player is to the left, face left and walk left
                    if (facingRight)
                    {
                        flipX();
                    }
                    rb.velocity = new Vector2(-speed, 0f);


                }
                else if (transform.position.x < playerLocation.position.x)
                {
                    //If player is to the left, face left and walk left
                    if (!facingRight)
                    {
                        flipX();
                    }
                    rb.velocity = new Vector2(speed, 0f);
                }
                else
                {
                    //Do nothing
                }

            }
        }
        else
        {
            animator.SetTrigger("IsDead");
            rb.velocity = Vector2.zero;
        }

    }

    //Overload the TakeDamage to update the spawner to allow the waves to resume
    public override void TakeDamage(float damage, DamageType type)
    {
        if (!isDead)
        {
            flashEffect.Flash();
            health -= damage;

        }

        if (health <= 0)
        {

            //Update the spawner to resume the waves after the boss dies
            Spawner temp = FindObjectOfType<Spawner>();
            if (temp != null)
            {
                temp.setBossDead(true);
            }

            isDead = true;
            AudioManager.Instance.PlaySFX("ZombieHurt2");
            if (type != DamageType.EXPLOSIVE)
            {
                FindObjectOfType<PlayerMoney>().AddCoins(worth);
            }
        }
        else
        {
            AudioManager.Instance.PlaySFX("ZombieHurt");
        }

    }

    private void flipX()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
