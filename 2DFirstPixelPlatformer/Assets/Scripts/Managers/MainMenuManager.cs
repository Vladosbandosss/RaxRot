using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene(TagManager.LEVEL_1_SCENE_NAME);
   }

   public void ExitGame()
   {
      Application.Quit();
   }
}
