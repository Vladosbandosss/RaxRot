 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;

 public class UIManager : MonoBehaviour
 {

     public static UIManager Instance;
     
     [SerializeField] private Text coinTxt;
     private int _coinCount;
     private string _coinInfo = "Coins: ";

     [SerializeField] private Image liveImage;
     [SerializeField] private Sprite fullLife, halfLife, emptyLive;

     [SerializeField] private GameObject winPanel, losePanel;

     private float _timeToStopAll = 1.5f;

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
         coinTxt.text = _coinInfo + _coinCount;
         winPanel.SetActive(false);
         losePanel.SetActive(false);
     }

     public void PickUpCoin()
     {
         _coinCount++;
         coinTxt.text = _coinInfo + _coinCount;
     }

     public void ChangeHealth(int health)
     {
         switch (health)
         {
             case 3:
                 liveImage.sprite = fullLife;
                 break;
             
             case 2:
                 liveImage.sprite = halfLife;
                 break;
             
             case 1:
                 liveImage.sprite = emptyLive;
                 break;
         }
     }

     public void ShowLosePaneL()
     {
         losePanel.SetActive(true);
         Invoke(nameof(StopTime),_timeToStopAll);
     }

     public void ShowWinPanel()
     {
         winPanel.SetActive(true);
         Invoke(nameof(StopTime),_timeToStopAll);
     }

     public void RestartBtn()
     {
         Time.timeScale = 1f;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

     public void MainMenuBtn()
     {
         Time.timeScale = 1f;
         SceneManager.LoadScene(TagManager.MAIN_MENU_SCENE_NAME);
     }

     public void ExitBtn()
     {
         Time.timeScale = 1f;
         Application.Quit();
     }

     private void StopTime()
     {
         Time.timeScale = 0f;
     }
 }
