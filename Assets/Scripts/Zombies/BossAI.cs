using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BossAI : ZombieAi
{
    private bool isPaused = false;
    private Transform playerLocation;
    private PlayerMovement player;
    private HealthBar healthUI;
    private bool facingRight = false;
    private float maxHealth;
    private GameObject bossUI;
    [SerializeField] private float lungeCharge = 1;
    [SerializeField] private float lungeCooldown = 3;
    private float cooldown = 0;

    [SerializeField] private float viewDistance = 2;
    [SerializeField] private float lungePower = 10;
    private bool isLunging = false;

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
            if (isPaused || !CheckCooldown())
            {
                rb.velocity = Vector2.zero;
            }
            else if (!isPaused && !isLunging)
            {
                //If not on cooldown, move around
                checkForPlayer();

                //Handle movement (determine direction to move)

                Vector2 moveDir = (playerLocation.position - transform.position).normalized;
                Move(moveDir);
                
            }

        }
    }

    private void Move(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        if (direction.x > 0 && !facingRight)
        {
            flipX();
        }
        else if (direction.x < 0 && facingRight)
        {
            flipX();
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

    IEnumerator startLunge()
    {
        isPaused = true;
        rb.velocity = Vector2.zero;
        //Get the direction the boss needs to face to get the player before waiting
        Vector2 dir = (playerLocation.position - transform.position).normalized;
        yield return new WaitForSeconds(lungeCharge);
        isPaused = false;
        isLunging = true;
        Debug.Log("Lunging!");
        LungeTowards(dir);
        yield return new WaitForSeconds(lungeCharge/2);
        isLunging = false;

        cooldown = lungeCooldown;

    }

    private void LungeTowards(Vector2 direction)
    {
        rb.AddForce(direction * lungePower);
    }

    private void checkForPlayer()
    {
        RaycastHit2D hit;

        Vector3 sightPosition = new Vector3(transform.position.x, transform.position.y);
        if (facingRight)
        {
            hit = Physics2D.Raycast(sightPosition, Vector2.right, viewDistance);
        }
        else
        {
            hit = Physics2D.Raycast(sightPosition, Vector2.left, viewDistance);
        }

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Spotted!");
                StartCoroutine(startLunge());
            }
        }
    }

    private bool CheckCooldown()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
