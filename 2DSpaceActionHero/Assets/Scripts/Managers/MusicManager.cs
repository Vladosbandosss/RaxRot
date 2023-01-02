using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource levelMusic, bossMusic, victoryMusic, gameOverMusic;

    [SerializeField] private AudioSource playerShoot, enemyShoot,enemyDead , powerUp;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        levelMusic.Play();
    }

    private void StopMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        gameOverMusic.Stop();
    }
    public void PlayGameOver()
    {
        StopMusic();
        
        gameOverMusic.Play();
    }

    public void PlayPlayerShootSFX()
    {
        playerShoot.Play();
    }

    public void PlayEnemyShootSFX()
    {
        enemyShoot.Play();
    }

    public void PlayEnemyDeadSFX()
    {
        enemyDead.Play();
    }

    public void PlayPowerPickUpSFX()
    {
        powerUp.Play();
    }

    public void InjurePlayer()
    {
        enemyDead.Play();
    }
}
