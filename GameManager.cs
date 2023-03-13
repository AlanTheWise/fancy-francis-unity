using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private void Awake(){
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = -1;
    }

    private void Start() {
        if(SceneManager.GetActiveScene().name == "Credits"){
            SoundManager.sharedInstance.sounds[15].Play();
        }
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            SceneManager.LoadScene("BossLevel");
        }
    }
}
