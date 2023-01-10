using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.ENEMY_TAG))
        {
            col.GetComponent<EnemyController>().EnemyDead();
        }
    }
}
