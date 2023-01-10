using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    [SerializeField] private float lifeTimer = 5f;
    
    private void Start()
    {
        Invoke(nameof(DestroyGameObj),lifeTimer);
    }

    private void DestroyGameObj()
    {
        Destroy(gameObject);
    }
}
