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



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        anim.SetBool("CrouchMove", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("walk", false);
        anim.SetBool("Hang", false);
        anim.SetBool("Jump", false);

    }

    private void Update()
    {
        movex = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        //if (movex != 0 && moveY == 0)
        //{
        //    playerState = PlayerState.Move;
        //}
        //else if (movex == 0 && moveY < 0)
        //{
        //    BoxColl.enabled = false;
        //    playerState = PlayerState.Crouch;
        //}
        //else if (movex != 0 && moveY != 0)
        //{
        //    playerState = PlayerState.CrouchMove;
        //}
        //else if((moveY > 0 && isGrounded) || movex != 0)
        //{
        //    playerState = PlayerState.Jump;
        //    //playerState = PlayerState.Hang;
        //}
        //else
        //{
        //    BoxColl.enabled = true;
        //    playerState = PlayerState.Idle;
        //}

        Movement();
        crouch();

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
        transform.Translate(new Vector3(movex * speed * Time.deltaTime, 0, 0));
        if(movex == 0)
            anim.SetBool("walk", false);
        else
            anim.SetBool("walk", true);



        //if(playerState == PlayerState.Idle)
        //{
        //    anim.SetBool("CrouchMove", false);
        //    anim.SetBool("Crouch", false);
        //    anim.SetBool("walk", false);
        //    anim.SetBool("Hang", false);
        //    anim.SetBool("Jump", false);
        //}
        //else if(playerState == PlayerState.Move)
        //{
        //    anim.SetBool("walk", true);
        //    transform.Translate(new Vector3(movex * speed * Time.deltaTime, 0, 0));
        //}
        //else if (playerState == PlayerState.Crouch)
        //{
        //    anim.SetBool("CrouchMove", false);            
        //    anim.SetBool("Crouch", true);
        //}
        //else if (playerState == PlayerState.CrouchMove)
        //{
        //    anim.SetBool("CrouchMove", true);
        //    transform.Translate(new Vector3(movex * speed/2 * Time.deltaTime, 0, 0));
        //}
        //else if(playerState == PlayerState.Jump)
        //{
        //    rb.velocity = Vector2.up * jumpForce;
        //    anim.SetBool("Jump", true);
        //    isJump = true;
        //}
    }

    void crouch()
    {
        if(moveY < 0)
        {
            anim.SetBool("Crouch", true);
            BoxColl.enabled = false;
            if (movex != 0)
                anim.SetBool("CrouchMove", true);
            else
                anim.SetBool("CrouchMove", false);
        }
        else
        {
            BoxColl.enabled = true;
            anim.SetBool("Crouch", false);
        }
    }

    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJump)
        {
            if (collision.gameObject.CompareTag("platform"))
            {
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
