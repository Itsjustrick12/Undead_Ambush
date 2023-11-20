using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : ZombieAi
{
    private bool isPaused = false;
    private Transform playerLocation;
    private PlayerMovement player;
    private HealthBar healthUI;
    private bool facingRight = true;
    private float maxHealth;
    private GameObject bossUI;

    void Start()
    {
        bossUI = GameObject.Find("BOSS UI");
        //Basically reveals the UI, assuming it is set to be hidden, but not inactives
        bossUI.GetComponent<Animator>().SetTrigger("Show");
        healthUI = FindObjectOfType<HealthBar>();
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
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
                //Follow the player on the x axis
                if (transform.position.x > playerLocation.position.x)
                {
                    //If player is to the left, face left and walk left
                    if (facingRight)
                    {
                        flipX();
                    }
                    rb.velocity = new Vector2(-speed, rb.velocity.y);


                }
                else if (transform.position.x < playerLocation.position.x)
                {
                    //If player is to the left, face left and walk left
                    if (!facingRight)
                    {
                        flipX();
                    }
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }

                //Follow the player on the y axis
                if (transform.position.y < playerLocation.position.y)
                {
                    //If player is above, walk upwards
                    rb.velocity = new Vector2(rb.velocity.x, speed);


                }
                else if (transform.position.y > playerLocation.position.y)
                {
                    //If player is below, walk downwards
                    rb.velocity = new Vector2(rb.velocity.x, -speed);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                }


            }
        }
        else
        {
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
            healthUI.updateBar(health, maxHealth);

        }

        if (health <= 0)
        {
           bossUI.GetComponent<Animator>().SetTrigger("Hide");

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

            //Play death animation here
            Destroy(this.gameObject);
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
