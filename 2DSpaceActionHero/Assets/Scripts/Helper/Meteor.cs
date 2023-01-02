using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float minRotationPower = 15f, maxRotationPower = 70f;
    private float _rotationPower;

    [SerializeField] private float minMoveSpeed = 3f, maxMoveSpeed = 5f;
    private float _moveSpeed;

    [SerializeField] private int damagePower = 1;

    [SerializeField] private GameObject meteorImpactFX;

    [SerializeField] private int scoreForDestroyMeteor = 1;
    
    [SerializeField] private GameObject powerPickUpObject;

    private void Start()
    {
       SetSpeedAndRotation();
    }

    private void SetSpeedAndRotation()
    {
        _rotationPower = Random.Range(minRotationPower, maxRotationPower);
        _moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    private void Update()
    {
       MoveMeteor();
    }

    private void MoveMeteor()
    {
        transform.Rotate(Vector3.forward * (Time.deltaTime * _rotationPower));
        transform.position -= new Vector3(_moveSpeed * Time.deltaTime, 0f, 0f);
    }

    public void MeteorImpactFX()
    {
        UIManager.Instance.UpdateScore(scoreForDestroyMeteor);
        MusicManager.Instance.PlayEnemyDeadSFX();
        Instantiate(meteorImpactFX, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            TryToInstantiatePowerPickUp();
            PlayerHealth.Instance.HurtPlayer(damagePower);
            MeteorImpactFX();
            Destroy(gameObject);
        }
    }

    private void TryToInstantiatePowerPickUp()
    {
        if (Random.Range(0,10)>8)
        {
            Instantiate(powerPickUpObject, transform.position, Quaternion.identity);
        }
    }
}
