using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject openDoor, closeDoor;

    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        openDoor.SetActive(false);
        closeDoor.SetActive(true);
    }

    public void OpenDoor()
    {
        openDoor.SetActive(true);
        closeDoor.SetActive(false);
    }
    
    
}
