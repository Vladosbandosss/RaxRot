using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
             if (SceneManager.GetActiveScene().name==TagManager.SCENE_BOSS_FIGHT)
             {
                UIManager.Instance.ShowLosePaneL();
                SoundManager.Instance.PlayGameOverMusic();
             }
            else
            {
                GameManager.Instance.PlayerDied();
            }
        }
    }
}
