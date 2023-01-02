using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private GameObject goPanel;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private Text scoreText;
    private int _currentScore;
    private string _scoreTxt = "Score: ";

    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        goPanel.SetActive(false);
        scoreText.text = _scoreTxt + _currentScore;
        pausePanel.SetActive(false);
    }
    
    public void ShowGameOverScreen()
    {
        goPanel.SetActive(true);
    }

    public void SetValuesForSliderHealth(int maxValue, int currentHealth)
    {
        healthSlider.maxValue = maxValue;
        healthSlider.value = currentHealth;
    }

    public void UpdateLifeFromSlider(int currentLives)
    {
        healthSlider.value = currentLives;
    }

    public void UpdateScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        scoreText.text = _scoreTxt + _currentScore;
    }

    public void PausedGame(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.LEVEL_1_SCENE_NAME);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_LEVEL_NAME);
    }
}
