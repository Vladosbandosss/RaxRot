using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject healFx;
    [SerializeField] private Transform pointToSpawnHealFx;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            if (GameManager.Instance.playerHealth<GameManager.Instance.playerMaxHealth)
            {
                GameManager.Instance.AddLive();
                Instantiate(healFx, pointToSpawnHealFx.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
