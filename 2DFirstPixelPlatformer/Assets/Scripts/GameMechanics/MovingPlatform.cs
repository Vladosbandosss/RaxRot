using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject movePlatform;

    [SerializeField] private Transform leftPosition, rightPosition;
    private Vector3 _targetToMove;
    [SerializeField] private float distanceToChangeTarget = 0.1f;
    [SerializeField] private float moveSpeed = 1.5f;

    private void Start()
    {
        _targetToMove = leftPosition.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        movePlatform.transform.position = Vector3.MoveTowards
        (movePlatform.transform.position, _targetToMove, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(movePlatform.transform.position,leftPosition.position)<=distanceToChangeTarget)
        {
            _targetToMove = rightPosition.position;
        }

        if (Vector3.Distance(movePlatform.transform.position,rightPosition.position)<=distanceToChangeTarget)
        {
            _targetToMove = leftPosition.position;
        }
    }
}
