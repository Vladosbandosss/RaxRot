using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private Material _bgMat;

    [SerializeField] private float scrollSpeed = 0.05f;

    private void Awake()
    {
        _bgMat = GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        Vector2 offset = new Vector2(scrollSpeed * Time.deltaTime, 0f);
        _bgMat.mainTextureOffset += offset;
    }
}
