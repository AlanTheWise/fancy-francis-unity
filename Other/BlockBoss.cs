using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockBoss : MonoBehaviour{

    public GameObject blockBricks;
    bool ninjaMusicChanged = false;

    private void Update() {
        if (PlayerHealthController.sharedInstance.currentHealth <= 0){
            blockBricks.SetActive(false);
        }

        if (GameObject.Find("Ninja") == null && !ninjaMusicChanged && SceneManager.GetActiveScene().name == "Level1"){
            SoundManager.sharedInstance.sounds[9].Stop();
            SoundManager.sharedInstance.sounds[10].Play();
            ninjaMusicChanged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(blockBricks.gameObject.activeInHierarchy) return;
            blockBricks.SetActive(true);
            SoundManager.sharedInstance.sounds[6].Stop();
            SoundManager.sharedInstance.sounds[9].Play();
        }
    }

}
