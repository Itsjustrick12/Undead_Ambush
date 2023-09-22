using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Used for movement
    private Vector2 moveDirection;
    public float moveSpeed;
    public Rigidbody2D rb;

    //Used for ending the game
    public bool isInfected = false;
    public GameManager gameManager;

    //Used for flipping the player
    public bool facingRight = true;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gamePaused)
        {
            CheckInputs();
            anim.SetFloat("Speed", Input.GetAxisRaw("Horizontal")*moveSpeed);
            anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical") * moveSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (!isInfected && !gameManager.gamePaused)
        {
            Move();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    void CheckInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX < 0f && facingRight)
        {
            FlipX();
        }
        else if (moveX > 0f && !facingRight)
        {
            FlipX();
        }

        moveDirection = new Vector2(moveX, moveY);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Enemy"){
            isInfected = true;
            gameManager.GameOver(true);
        }
    }

    void FlipX()
    {
        Vector3 newScale = this.transform.localScale;
        newScale.x *= -1;
        this.transform.localScale = newScale;

        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }

}
