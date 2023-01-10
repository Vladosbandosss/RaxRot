using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyEnemyController : MonoBehaviour
{
    private SpriteRenderer _sr;
    
    [SerializeField] private float minXToFly, maxXToFly, minYToFly, maxYToFly;
    private float _xToFly, _yToFly;
    
    [SerializeField] private float minFlySpeed = 1.5f, maxFlySpeed = 2.5f;
    private float _flySpeed;
    
    private Vector3 _targetPosition;
    private float _previousXPosition;
    [SerializeField] private float distanceToChangePosition = 0.1f;

    private void Awake()
    {
       SetValues();
    }

    private void SetValues()
    {
        _sr = GetComponent<SpriteRenderer>();
        _targetPosition = transform.position;
        _flySpeed = Random.Range(minFlySpeed, maxFlySpeed);
    }

    private void Update()
    {
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _flySpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < distanceToChangePosition)
        {
            _xToFly = Random.Range(minXToFly, maxXToFly);
            _yToFly = Random.Range(minYToFly, maxYToFly);
            
            _targetPosition = new Vector3(_xToFly, _yToFly, 0f);
            
            _previousXPosition = transform.position.x;
        }
        
        ChangeFacingDirection();
    }

    private void ChangeFacingDirection()
    {
        if (transform.position.x>_previousXPosition)
        {
            _sr.flipX = false;
        }else if (transform.position.x<_previousXPosition)
        {
            _sr.flipX = true;
        }
    }
}
