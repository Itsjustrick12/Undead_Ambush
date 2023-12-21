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
    [SerializeField] private float lungeCooldown = 3;
    private float cooldown = 0;

    [SerializeField] private float viewDistance = 2;
    [SerializeField] private float lungePower = 10;
    public bool isLunging = false;

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
            else if (!isPaused && !isLunging && CheckCooldown())
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

        if (health <= 0 && !isDead)
        {
           BossBeatenSave();
           animator.SetTrigger("IsDead");
           rb.velocity = Vector2.zero;
           isDead = true;
           bossUI.GetComponent<Animator>().SetTrigger("Hide");

            //Update the spawner to resume the waves after the boss dies
            Spawner temp = FindObjectOfType<Spawner>();
            if (temp != null)
            {
                temp.setBossDead(true);
            }

            AudioManager.Instance.PlaySFX("ZombieHurt2");
            Debug.Log("Adding " + worth + "coins");
            FindObjectOfType<PlayerMoney>().AddCoins(worth);
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
        animator.SetTrigger("Charge");
        yield return new WaitForSeconds(0f);

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

    public void Jump()
    {
        isPaused = false;
        isPaused = false;
        isLunging = true;
        animator.SetTrigger("Jump");
        Vector2 dir = (playerLocation.position - transform.position).normalized;
        LungeTowards(dir);
    }

    public void Fall()
    {
        rb.velocity = new Vector2 (0f, -1);
        rb.velocity = Vector2.zero;
        isLunging = false;
        animator.SetTrigger("Fall");
        AudioManager.Instance.PlaySFX("GroundPound");
        CinemachineShake.Instance.ShakeCamera(2f, 0.25f);
        cooldown = lungeCooldown;
    }

    public void SetLungeFalse ()
    {
        isLunging = false;
    }

    private void BossBeatenSave()
    {
        PlayerData pd = SaveManager.Load();
        pd.beatEndless = true;
        SaveManager.Save(pd);
    }
}
