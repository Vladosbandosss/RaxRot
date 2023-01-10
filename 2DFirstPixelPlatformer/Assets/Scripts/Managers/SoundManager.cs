using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource levelClip, goClip;
    [SerializeField] private AudioSource jumpSfx, pickUpSfx, playerHurtSfx, enemyHurtSfx;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void PlayGameOverMusic()
    {
        levelClip.Stop();
        goClip.Play();
    }

    public void PlayJumpSfx()
    {
        jumpSfx.Play();
    }

    public void PlayPickUpSfx()
    {
        pickUpSfx.Play();
    }

    public void PlayPlayerHurtSfx()
    {
        playerHurtSfx.Play();
    }

    public void PlayEnemyHurtSfx()
    {
        enemyHurtSfx.Play();
    }
}
