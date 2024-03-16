using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;
    [SerializeField] BoxCollider2D BoxColl;

    public PlayerState playerState;

    float movex;
    float moveY;
    [SerializeField] float speed;

    [Header("Ground Checking")]
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    public LayerMask layerMask;
    float groundCheckRadius = 0.1f;
    bool isGrounded;
    bool isJump;


    int jumpID;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        jumpID = Animator.StringToHash("Jump");
    }

    private void Update()
    {
        movex = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (movex != 0 && moveY == 0)
        {
            playerState = PlayerState.Move;
        }
        else if (movex == 0 && moveY < 0)
        {
            BoxColl.enabled = false;
            playerState = PlayerState.Crouch;
        }
        else if (movex != 0 && moveY != 0)
        {
            playerState = PlayerState.CrouchMove;
        }
        else if((moveY > 0 && isGrounded) || movex != 0)
        {
            playerState = PlayerState.Jump;
            //playerState = PlayerState.Hang;
        }
        else
        {
            BoxColl.enabled = true;
            playerState = PlayerState.Idle;
        }

        Movement();

        Flip();


        //if (isGrounded && Input.GetKey(KeyCode.Space))
        //{
        //    Jump();
        //}
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask);
    }
    
    void Movement()
    {
        if(playerState == PlayerState.Idle)
        {
            anim.SetBool("CrouchMove", false);
            anim.SetBool("Crouch", false);
            anim.SetBool("walk", false);
        }
        else if(playerState == PlayerState.Move)
        {
            anim.SetBool("walk", true);
            anim.SetFloat(jumpID, 0);
            transform.Translate(new Vector3(movex * speed * Time.deltaTime, 0, 0));
        }
        else if (playerState == PlayerState.Crouch)
        {
            anim.SetBool("CrouchMove", false);
            
            anim.SetBool("Crouch", true);
        }
        else if (playerState == PlayerState.CrouchMove)
        {
            anim.SetBool("CrouchMove", true);
            transform.Translate(new Vector3(movex * speed/2 * Time.deltaTime, 0, 0));
        }
        else if(playerState == PlayerState.Jump)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetFloat(jumpID, 1);
            isJump = true;
        }
    }
    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJump)
        {
            if (collision.gameObject.CompareTag("platform"))
            {
                Debug.Log("Emter 1");
                anim.SetBool("Hang", true);
            }
        }
    }

    void Flip()
    {
        if (movex > 0.1f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if(movex < -0.1f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
    void Jump()
    {
        
    }
}

public enum PlayerState
{
    Idle,
    Move,
    Crouch,
    Jump,
    Hang,
    CrouchMove
}
