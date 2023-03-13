using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public static MainMenu sharedInstance;
    public AudioSource music;

    private void Awake(){
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    private void Start() {
        if(music !=null && SceneManager.GetActiveScene().name == "MainMenu"){
            music.Play();
        }
    }

    public void PlayGame(){
        music.Stop();
        Invoke("StartGame", 0.5f);
    }

    public void StartGame(){
        PlayerScore.gemsAmount = 0;
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        music.Stop();
        Invoke("QuitGame", 1.75f);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
