using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Vector3 _tempPos;

    private void Start()
    {
        _target = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        _tempPos = transform.position;
        _tempPos.z = -10;
    }

    private void LateUpdate()
    {
        if (!_target)
        {
            return;
        }
        
        Follow();
    }

    private void Follow()
    {
        _tempPos = transform.position;
        _tempPos.x = _target.transform.position.x;
        
        transform.position = _tempPos;
    }
    
}
