using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObgCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.ROTATE_BOSS_OBG))
        {
            BossBatle.Instance.spawnedRotateObg++;
        }
    }
}
