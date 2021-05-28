using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void playGame() {
       SceneManager.LoadScene("CutScene1");
   }

   public void quitGame() {
       Debug.Log("Quit");
       Application.Quit();
   }

   public void returnToLevel() {
        SceneManager.LoadScene(GameController.Instance.currentScene);
   }

   public void skip(string lvlName) {
        SceneManager.LoadScene(lvlName);
   }

   public void controls(GameObject control) {
       control.SetActive(true);
   }

   public void ExitControls(GameObject control) {
       control.SetActive(false);
   }
}
