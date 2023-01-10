using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooter : MonoBehaviour
{
    private Animator _anim;
    
    [SerializeField] private GameObject bulletToSpawn;
    [SerializeField] private Transform bulletSpawnPosition;

    [SerializeField] private float minTimeToSpawnBullet = 2.5f, maxTimeToSpawnBullet = 5f;
    private float _timeToSpawn;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartSpawn();   
    }

    private void StartSpawn()
    {
        StartCoroutine(nameof(_SpawnBulletCo));
    }

    private IEnumerator _SpawnBulletCo()
    {
        _timeToSpawn = Random.Range(minTimeToSpawnBullet, maxTimeToSpawnBullet);
        
        yield return new WaitForSeconds(_timeToSpawn);
        
        Instantiate(bulletToSpawn, bulletSpawnPosition.position, Quaternion.identity);
        _anim.SetTrigger(TagManager.ENEMY_SHOOT_TRIGGER);

        StartCoroutine(nameof(_SpawnBulletCo));
    }
}
