using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBullet : MonoBehaviour
{
    private float _speed=4f;

    [SerializeField] private int damage = 1;
    
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        transform.position -= new Vector3(_speed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            PlayerHealth.Instance.HurtPlayer(damage);
            Destroy(gameObject);
        }
    }
}
