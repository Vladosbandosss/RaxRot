using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject keyFx;
    [SerializeField] private Transform keyFxSpawnPosition;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            Instantiate(keyFx, keyFxSpawnPosition.position, Quaternion.identity); 
            GameObject.FindWithTag(TagManager.DOOR_TAG).GetComponent<Door>().OpenDoor();
            Destroy(gameObject);
        }
    }
}
