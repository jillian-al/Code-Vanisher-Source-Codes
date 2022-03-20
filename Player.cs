using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeKnocked;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] private LayerMask groundLayer;

    public float maxHealth = 100f;
    public float Heatlh = 0f;
    public bool isGrounded = false;
    public Transform frontCheck;
    public float wallSlidingSpeed;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    private float defaultMoveForce = 10f;
    private float movementX;
    private bool knockedBack;
    private bool facingRight = true;  //to check later if player is facing right or left
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string RUN_ANIMATION = "Run";
    private string JUMP_ANIMATION = "Jump";
    private string GROUND_TAG = "Ground";

    const float checkRadius = 0.2f;   //radius that checks if player is touching ground
    bool isTouchingFront;
    bool wallSliding;
    bool wallJumping;  

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();  
        anim = GetComponent<Animator>();     

        sr = GetComponent<SpriteRenderer>();  
    }

    void Start()
    {
        Heatlh = maxHealth;
    }

    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        isGrounded = Physics2D.OverlapCircle(groundCheckCollider.position, checkRadius, groundLayer);
        PlayerJump();
        wallSlideAndJump();
    }


    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");     //get user input if user wants to move horizontally

        myBody.velocity = new Vector2(movementX * moveForce, myBody.velocity.y);
        if (facingRight == false && movementX > 0) //flip to left if facing right and running left
        {
            flip();
        }
        else if(facingRight == true && movementX < 0)   //flip to right if facing left and running right
        {
            flip();
        }
    }

    void flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    void AnimatePlayer()
    {
        if (movementX != 0) //if player is running, animate run
        {
            anim.SetBool(RUN_ANIMATION, true);
        }
        else //if player is idle, animate idle
        {
            anim.SetBool(RUN_ANIMATION, false);
        }

        //airborne animation
        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true) 
        {
            anim.SetTrigger(JUMP_ANIMATION);
            myBody.velocity = Vector2.up * jumpForce;  
        }
    }

    void wallSlideAndJump()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);

        if (isTouchingFront == true && isGrounded == false && movementX != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, Mathf.Clamp(myBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpToFalse", wallJumpTime);
        }

        if (wallJumping == true)
        {
            anim.SetTrigger(JUMP_ANIMATION);
            if (facingRight)
            {
                myBody.velocity = new Vector2(-xWallForce, yWallForce);
                flip();
            }
            else
            {
                myBody.velocity = new Vector2(xWallForce, yWallForce);
                flip();
            }
        }
    }

    void SetWallJumpToFalse()
    {
        wallJumping = false;
    }

    public void updateHealth(float mod)
    {
        Heatlh += mod;
        if(Heatlh > maxHealth)
        {
            Heatlh = maxHealth;
        }

        if(Heatlh <= 0)
        {
            Heatlh = 0f;
        }
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwrX/*, Vector3 KnockbackDir*/)
    {
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            if (facingRight)
            {
                myBody.AddForce(new Vector2(transform.position.x - knockbackPwrX, transform.position.y));
            }
            else
            {
                myBody.AddForce(new Vector2(transform.position.x + knockbackPwrX, transform.position.y));
            }
        }
        yield return 0;
    }
    
    public void modifyMoveSpeed(float moveMod)
    {
        moveForce = moveMod;
    }

    public void modifyToDefaultMoveSpeed()
    {
        moveForce = defaultMoveForce;
    }
}
