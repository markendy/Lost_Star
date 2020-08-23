using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MoveControl : MonoBehaviour
{
    public Action JumpHendler;
    public Action MoveRightHendler;
    public Action MoveLeftHendler;
    public Action MoveStopHendler;
    private Hero hero;
    private float moveInput = 0f;
    private bool facingR = true;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    private Rigidbody2D rigidbody;
    private int extraJump;
    public int extraJumpValue;
    [SerializeField] private Animator animator;
    private int isR = 0;
    private bool isJ = false;
    private float deltaMove = 0;
    private void Start()
    {
        isR = 0;
        isJ = false;
        deltaMove = 0;
        moveInput = 0f;
        hero = GetComponent<Hero>();
        extraJump = extraJumpValue;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isR == 1)
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
                rigidbody.velocity = Vector2.up * hero.JumpForce;
                animator.SetBool("isJumping", true);
            }
            else if (!isGrounded && extraJump > 0)
            {
                rigidbody.velocity = Vector2.up * hero.JumpForce;
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
                deltaMove = Math.Abs(moveInput) / 20f;
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
    public void EndMove()
    {
        isR = 0;
    }
    public void MoveLeft()
    {
        isR = -1;
    }
    public void MoveRight()
    {
        isR = 1;
    }
    private void StopButtonDown()
    {
        InVector0();
        rigidbody.velocity = new Vector2(moveInput * hero.Speed, rigidbody.velocity.y);
        MoveStopHendler();
    }
    private void LeftButtonDown()
    {
        CreateVector(-1);
        rigidbody.velocity = new Vector2(moveInput * hero.Speed, rigidbody.velocity.y);
        MoveLeftHendler();
    }
    private void RightButtonDown()
    {
        CreateVector(1);
        rigidbody.velocity = new Vector2(moveInput * hero.Speed, rigidbody.velocity.y);
        MoveRightHendler();
    }
    private void Flip()
    {
        facingR = !facingR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
