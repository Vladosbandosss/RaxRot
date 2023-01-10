using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]public int playerHealth;
    [HideInInspector] public int playerMaxHealth = 3;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
       SetValues();
    }

    private void SetValues()
    {
        playerHealth = playerMaxHealth;
        UIManager.Instance.ChangeHealth(playerHealth);
    }

    public void PlayerDied()
    {
        playerHealth--;
            
        if (playerHealth>0)
        {
            UIManager.Instance.ChangeHealth(playerHealth);
            LevelManager.Instance.RespawnPlayer();
            print(playerHealth);
        }else
        {
            SoundManager.Instance.PlayGameOverMusic();
            UIManager.Instance.ShowLosePaneL();
        }
    }

    public void AddLive()
    {
        if (playerHealth<3)
        {
            playerHealth++;
            UIManager.Instance.ChangeHealth(playerHealth);
            SoundManager.Instance.PlayPickUpSfx();
        }
    }

    public void AddCoin()
    {
        UIManager.Instance.PickUpCoin();
        SoundManager.Instance.PlayPickUpSfx();
    }

    public void WinGame()
    {
        UIManager.Instance.ShowWinPanel();
    }
}
