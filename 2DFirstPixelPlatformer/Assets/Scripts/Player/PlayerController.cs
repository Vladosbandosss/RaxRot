using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField] private float moveSpeed = 5f;
    private float _xAxis;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float distanceToGroundCheck = 0.15f;
    private bool _isGrounded;

    private Vector3 _tempLocalScale;
    public Vector3 RespawnPosition { get; set; }

    [HideInInspector]public bool isPlayerDead;

    [SerializeField] private float knockForce = 10f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        RespawnPosition = transform.position;
    }

    private void Update()
    {
        Movement();
        
        IsGrounded();
        
        AnimatePlayer();
        
        FlipPlayer();
    }
    
    private void Movement()
    {
        if (!isPlayerDead)
        {
            _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
            _rb.velocity = new Vector2(_xAxis * moveSpeed, _rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        SoundManager.Instance.PlayJumpSfx();
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void IsGrounded()
    {
        _isGrounded = Physics2D.Raycast
        (groundCheckPosition.position, Vector2.down, distanceToGroundCheck,isGroundLayer);
    }

    private void AnimatePlayer()
    {
        _anim.SetFloat(TagManager.PLAYER_MOVE_SPEED_ANIM_PARAMETR,Math.Abs(_rb.velocity.x));
        _anim.SetBool(TagManager.PLAYER_IS_GROUNDED_ANIM_PARAMETR,_isGrounded);
        _anim.SetFloat(TagManager.PLAYER_Y_VELOCITY_ANIM_PARAMETR,_rb.velocity.y);
    }

    private void FlipPlayer()
    {
        _tempLocalScale = transform.localScale;

        if (_xAxis>0)
        {
            _tempLocalScale.x = 1f;
        }else if (_xAxis<0)
        {
            _tempLocalScale.x = -1f;
        }

        transform.localScale = _tempLocalScale;
    }
    
    public void MakeKnock()
    {
       _rb.AddForce(transform.up*knockForce,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.CHECK_POINT_TAG))
        {
            RespawnPosition = col.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.MOVING_PLATFORM_TAG))
        {
            transform.parent = col.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagManager.MOVING_PLATFORM_TAG))
        {
            transform.parent = null;
        }
    }
}
