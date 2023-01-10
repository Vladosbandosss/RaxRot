using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinPickUpFX;
    [SerializeField] private Transform positionToSpawnFx;

    private void Start()
    {
        LevelManager.Instance.AddCoin();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            GameManager.Instance.AddCoin();
            Instantiate(coinPickUpFX, positionToSpawnFx.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
