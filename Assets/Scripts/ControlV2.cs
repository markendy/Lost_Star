using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ControlV2 : MonoBehaviour
{
    public Action Jump;
    private Hero hero;
    private float moveInput = 0f;
    private bool facingR = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;
    private int extraJump;
    public int extraJumpValue;
    public Animator animator;
    private int isR = 0;
    private bool isJ = false;
    private float deltaMove = 0;
    // Start is called before the first frame update
    private void Start()
    {
        isR = 0;
        isJ = false;
        deltaMove = 0;
        moveInput = 0f;
        hero = GetComponent<Hero>();
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //moveInput = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(/*moveInput* */ tempSpeed, rb.velocity.y);
        if (isR == 1)//Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            RightButtonDown();
        }
        if (isR == -1)
        {
            LeftButtonDown();
        }
        if (isR == 0)
        {
            StopButtonDown();
        }
        if (!facingR && moveInput > 0)
        {
            Flip();
        }
        else if (facingR && moveInput < 0)
        {
            Flip();
        }
        if (moveInput == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        JumpControl();
    }

    private void JumpControl()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        if (isJ)
        {
            isJ = false;
            if (isGrounded)
            {
                extraJump = extraJumpValue;
                rb.velocity = Vector2.up * hero.JumpForce;
                animator.SetBool("isJumping", true);
            }
            else if (!isGrounded && extraJump > 0)
            {
                rb.velocity = Vector2.up * hero.JumpForce;
                --extraJump;
                animator.SetBool("isJumping", true);
            }
        }
        if (extraJump < 0)
        {
            extraJump = 0;
        }
    }
    private void CreateVector(int v)
    {
        moveInput = moveInput + 1.5f * v * Time.deltaTime;
        if (moveInput > 1)
        {
            moveInput = 1;
        }
        else if (moveInput < -1)
        {
            moveInput = -1;
        }
    }
    private void InVector0()
    {
        if (moveInput == 0)
        {
            deltaMove = 0;
        }
        else
        {
            if (deltaMove == 0)
            {
                deltaMove = Math.Abs(moveInput) / 3f;
            }
            if (moveInput < 0)
            {
                moveInput += deltaMove;
                if (moveInput > 0)
                {
                    moveInput = 0;
                }
            }
            else if (moveInput > 0)
            {
                moveInput -= deltaMove;
                    if (moveInput < 0)
                    {
                        moveInput = 0;
                    }
            }
            CreateVector(0);
        }
    }
    public void DoJump()
    {
        isJ = true;
    }
    public void UNChose()
    {
        isR = 0;
    }
    public void ChoseL()
    {
        isR = -1;
    }
    public void ChoseR()
    {
        isR = 1;
    }
    public void StopButtonDown()
    {
        InVector0();
        rb.velocity = new Vector2(moveInput * hero.Speed, rb.velocity.y);
    }
    public void LeftButtonDown()
    {
        CreateVector(-1);
        rb.velocity = new Vector2(moveInput * hero.Speed, rb.velocity.y);
    }
    public void RightButtonDown()
    {
        CreateVector(1);
        rb.velocity = new Vector2(moveInput * hero.Speed, rb.velocity.y);
    }
    private void Flip()
    {
        facingR = !facingR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
