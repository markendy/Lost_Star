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
    private Hero _hero;
    private float _moveInput = 0f;
    private bool _facingR = true;
    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    private int extraJump;
    public int extraJumpValue;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    private int _isRightDirection = 0;
    private bool _isJump = false;
    private float _deltaMoveInFrame = 0;
    private void Start()
    {
        _isRightDirection = 0;
        _isJump = false;
        _deltaMoveInFrame = 0;
        _moveInput = 0f;
        _hero = GetComponent<Hero>();
        extraJump = extraJumpValue;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_isRightDirection == 1)
        {
            RightButtonDown();
        }
        if (_isRightDirection == -1)
        {
            LeftButtonDown();
        }
        if (_isRightDirection == 0)
        {
            StopButtonDown();
        }
        if (!_facingR && _moveInput > 0)
        {
            Flip();
        }
        else if (_facingR && _moveInput < 0)
        {
            Flip();
        }
        if (_moveInput == 0)
        {
            _animator.SetBool("_isRightDirectionunning", false);
        }
        else
        {
            _animator.SetBool("_isRightDirectionunning", true);
        }
    }
    private void Update()
    {
        JumpControl();
    }

    private void JumpControl()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
        if (_isGrounded)
        {
            _animator.SetBool("_isJumpumping", false);
        }
        if (_isJump)
        {
            _isJump = false;
            if (_isGrounded)
            {
                extraJump = extraJumpValue;
                _rigidbody.velocity = Vector2.up * _hero.JumpForce;
                _animator.SetBool("_isJumpumping", true);
                if (JumpHendler != null)
                    JumpHendler();
            }
            else if (!_isGrounded && extraJump > 0)
            {
                _rigidbody.velocity = Vector2.up * _hero.JumpForce;
                --extraJump;
                _animator.SetBool("_isJumpumping", true);
                if (JumpHendler != null)
                    JumpHendler();
            }
        }
        if (extraJump < 0)
        {
            extraJump = 0;
        }
    }
    private void CreateVector(int v)
    {
        _moveInput = _moveInput + 1.5f * v * Time.deltaTime;
        if (_moveInput > 1)
        {
            _moveInput = 1;
        }
        else if (_moveInput < -1)
        {
            _moveInput = -1;
        }
    }
    private void InVector0()
    {
        if (_moveInput == 0)
        {
            _deltaMoveInFrame = 0;
        }
        else
        {
            if (_deltaMoveInFrame == 0)
            {
                _deltaMoveInFrame = Math.Abs(_moveInput) / 20f;
            }
            if (_moveInput < 0)
            {
                _moveInput += _deltaMoveInFrame;
                if (_moveInput > 0)
                {
                    _moveInput = 0;
                }
            }
            else if (_moveInput > 0)
            {
                _moveInput -= _deltaMoveInFrame;
                    if (_moveInput < 0)
                    {
                        _moveInput = 0;
                    }
            }
            CreateVector(0);
        }
    }
    public void DoJump()
    {
        _isJump = true;
    }
    public void EndMove()
    {
        _isRightDirection = 0;
    }
    public void MoveLeft()
    {
        _isRightDirection = -1;
    }
    public void MoveRight()
    {
        _isRightDirection = 1;
    }
    private void StopButtonDown()
    {
        InVector0();
        _rigidbody.velocity = new Vector2(_moveInput * _hero.Speed, _rigidbody.velocity.y);
        if (MoveStopHendler != null)
            MoveStopHendler();
    }
    private void LeftButtonDown()
    {
        CreateVector(-1);
        _rigidbody.velocity = new Vector2(_moveInput * _hero.Speed, _rigidbody.velocity.y);
        if (MoveLeftHendler != null)
            MoveLeftHendler();
    }
    private void RightButtonDown()
    {
        CreateVector(1);
        _rigidbody.velocity = new Vector2(_moveInput * _hero.Speed, _rigidbody.velocity.y);
        if (MoveRightHendler != null)
            MoveRightHendler();
    }
    private void Flip()
    {
        _facingR = !_facingR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
