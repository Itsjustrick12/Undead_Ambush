using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    public enum DamageType{
        PROJECTILE,
        EXPLOSIVE
    }
    public float speed = 1;
    public float health = 3;
    public int worth = 1;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected GameManager gameManager;
    protected bool isDead = false;

    [SerializeField] protected SimpleFlash flashEffect;
    
    // Start is called before the first frame update
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
                rb.velocity = new Vector2(-speed, 0f);
        }
        else
        {
            animator.SetTrigger("IsDead");
            rb.velocity = Vector2.zero;
        }
       
    }
    
    virtual public void TakeDamage (float damage, DamageType type)
    {
        if (!isDead)
        {
            flashEffect.Flash();
            health -= damage;
            
        }
        
        if (health<= 0)
        {
            isDead = true;
            AudioManager.Instance.PlaySFX("ZombieHurt2");
            if (type != DamageType.EXPLOSIVE)
            {
                FindObjectOfType<PlayerMoney>().AddCoins(worth);
            }
        } else
        {
            AudioManager.Instance.PlaySFX("ZombieHurt");
        }

    }

    public void Die()
    {
        gameManager.addToCounter();
        Destroy(gameObject);
    }

    
}
