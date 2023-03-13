using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour{
    bool picked = false;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !picked){
            SoundManager.sharedInstance.sounds[1].Play();
            picked = true;
            PlayerScore.gemsAmount++;
            Destroy(this.gameObject);
        }
    }
}
