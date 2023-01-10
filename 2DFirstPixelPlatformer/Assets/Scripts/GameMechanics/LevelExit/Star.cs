using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private float timeToLoadLevel = 2f;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        StartCoroutine(nameof(_LoadNextLevelCo));
    }

    private IEnumerator _LoadNextLevelCo()
    {
        LevelManager.Instance.isFadingBlack = true;
        
        yield return new WaitForSeconds(timeToLoadLevel);
        
        SceneManager.LoadScene(levelToLoad);
    }
}
