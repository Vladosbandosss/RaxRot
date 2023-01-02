using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed=2f,maxMoveSpeed=3.5f;
    private float _moveSpeed;
    
    [SerializeField] private Transform[] pointsToMove;
    private int _indexToMove;
    [SerializeField] private float distanceToChangePoint = 0.1f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint1,shootPoint2;
    [SerializeField] private float minTimeToShoot = 2f, maxTimeToShot = 4f;
    private float _timeBetweenShoots;
    private float _shootTimer;

    private bool _canShoot;

    private GameObject _bullet1, _bullet2;
    [SerializeField] private float minBulletSpeed = 3.5f, maxBulletSpeed = 5f;
    private float _bulletSpeed;
    
    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        _moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        _timeBetweenShoots = Random.Range(minTimeToShoot, maxTimeToShot);
        _shootTimer = _timeBetweenShoots;
        _bulletSpeed = Random.Range(minBulletSpeed, maxBulletSpeed);
    }

    private void Update()
    {
       MoveEnemy();

       if (Time.time>_shootTimer && _canShoot)
       {
           Shoot();
       }
    }

    private void Shoot()
    {
       _bullet1 = Instantiate(bullet, shootPoint1.position, Quaternion.identity);
       _bullet1.GetComponent<EnemyBullet>().SetSpeed(_bulletSpeed);
       
       _bullet2 = Instantiate(bullet, shootPoint2.position, Quaternion.identity);
       _bullet2.GetComponent<EnemyBullet>().SetSpeed(_bulletSpeed);
       
       MusicManager.Instance.PlayEnemyShootSFX();
       
       _shootTimer = Time.time + _timeBetweenShoots;
    }

    private void MoveEnemy()
    {
       transform.position = Vector3.MoveTowards
           (transform.position, pointsToMove[_indexToMove].position, _moveSpeed * Time.deltaTime);

       if (Vector3.Distance(transform.position,pointsToMove[_indexToMove].position)<=distanceToChangePoint)
       {
           _indexToMove++;
       }

       if (_indexToMove>=pointsToMove.Length)
       {
           Destroy(gameObject);
       }
    }

    private void OnBecameVisible()
    {
        _canShoot = true;
    }

    private void OnBecameInvisible()
    {
        _canShoot = false;
    }
}
