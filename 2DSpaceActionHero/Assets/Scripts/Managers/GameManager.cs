using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 _origCameraPosition;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _origCameraPosition = Camera.main.transform.position;
    }

    public void GameOver()
    {
        UIManager.Instance.ShowGameOverScreen();
        MusicManager.Instance.PlayGameOver();
    }

    public void InjurePlayer()
    {
        ShakeCamera();
    }

    private void ShakeCamera()
    {
        StartCoroutine(nameof(_ShakeCameraCo));
    }

    private IEnumerator _ShakeCameraCo()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * 2f;
            Camera.main.transform.position = new Vector3(randPos.x, randPos.y, Camera.main.transform.position.z);
            yield return 0.05f;
        }
        Camera.main.transform.position = _origCameraPosition;
    }
}
