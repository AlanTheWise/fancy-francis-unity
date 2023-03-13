using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{
    public string sceneToLoad;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            if(SceneManager.GetActiveScene().name == "Level1"){
                bool isNinjaDead = GameObject.Find("Ninja") == null;
                if(!isNinjaDead) return;
            }
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
