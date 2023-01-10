using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    
    [SerializeField] private float moveSpeed = 2f;
    private bool _isMovingRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform switchPositionChecker;
    [SerializeField] private float distanceChecker = 1f;
    private bool _canMove;

    private RaycastHit2D _groundHit;

    private bool _canKillPlayer;

    [SerializeField] private float timeToDisappearAfterDead = 2f;

    [SerializeField] private GameObject deadFx;
    [SerializeField] private Transform pointToSpawnDeadFx;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _canKillPlayer = true;
        _canMove = true;
    }

    private void Start()
    {
        SetPositionToMove();
    }

    private void SetPositionToMove()
    {
        _isMovingRight = Random.Range(0,2)==0;
    }

    private void Update()
    {
        if (_canMove)
        {
            MoveEnemy();
        
            CheckMovePosition();
        }
    }

    private void MoveEnemy()
    {
        if (_isMovingRight)
        {
            _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void CheckMovePosition()
    {
        _groundHit = Physics2D.Raycast
            (switchPositionChecker.position, Vector2.down, distanceChecker, groundLayer);

        if (!_groundHit)
        {
            _isMovingRight = !_isMovingRight;
        }
    }

    public void EnemyDead()
    {
        _canMove = false;
        _canKillPlayer = false;
        SoundManager.Instance.PlayEnemyHurtSfx();
        Instantiate(deadFx, pointToSpawnDeadFx.position, Quaternion.identity);
        _anim.Play(TagManager.ENEMY_DEAD_ANIM_NAME);
        Invoke(nameof(DestroyEnemy),timeToDisappearAfterDead);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.PLAYER_TAG) && _canKillPlayer)
        {
            GameManager.Instance.PlayerDied();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
