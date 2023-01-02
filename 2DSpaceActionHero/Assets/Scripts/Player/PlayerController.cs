using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float moveSpeed = 5f;
    private float _xAxis, _yAxis;

    private float allXPosition=8.5f,allYPosition = 4.5f;
    private float _xPosition, _yPosition;
    private Vector2 _movePosition;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletShootPosition;
    [SerializeField] private float timeBetweenShoot=1f;
    private float _timerBetweenShoot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown(TagManager.FIRE_MOUSE_BUTTON))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time>_timerBetweenShoot)
        {
            Instantiate(bullet, bulletShootPosition.position, Quaternion.identity);
            MusicManager.Instance.PlayPlayerShootSFX();
            _timerBetweenShoot = Time.time + timeBetweenShoot;
        }
    }

    private void MovePlayer()
    {
        _xAxis = Input.GetAxis(TagManager.HORIZONTAL_AXIS);
        _yAxis = Input.GetAxis(TagManager.VERTICAL_AXIS);
        
        _rb.velocity = new Vector2(_xAxis, _yAxis) * moveSpeed;

        _xPosition = Mathf.Clamp(transform.position.x, -allXPosition, allXPosition);
        _yPosition = Mathf.Clamp(transform.position.y, -allYPosition, allYPosition);

        transform.position = new Vector3(_xPosition, _yPosition, transform.position.z);
    }

    public void SetNewTimeBetweenShoot(float newTime)
    {
        timeBetweenShoot = newTime;
    }

    public void BackToNormalTimeBetweenShoot()
    {
        timeBetweenShoot = 1f;
    }
}
