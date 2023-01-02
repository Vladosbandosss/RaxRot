using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;

    private void Start()
    {
        howToPlayPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(TagManager.LEVEL_1_SCENE_NAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay(bool goToHowToPlay)
    {
        if (goToHowToPlay)
        {
            howToPlayPanel.SetActive(true);
        }
        else
        {
            howToPlayPanel.SetActive(false);
        }
    }
}
