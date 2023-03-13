using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public GameObject pauseMenu;
    [HideInInspector]
    public static bool isPaused = false;

    private void Start(){
        pauseMenu.SetActive(false);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            } else{
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        SoundManager.sharedInstance.PauseAll();
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        SoundManager.sharedInstance.UnPauseAll();
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu(){
        isPaused= false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void  QuitGame(){
        Application.Quit();
    }
}
