using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour{
    public int respawnIndex;

    public Sprite flagOnSprite;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && PlayerSpawnManager.sharedInstance.respawnIndex != respawnIndex){
            GetComponentInChildren<SpriteRenderer>().sprite = flagOnSprite;
            SoundManager.sharedInstance.sounds[7].Play();
            if(PlayerSpawnManager.sharedInstance.respawnIndex > respawnIndex) return;
            PlayerSpawnManager.sharedInstance.SetRespawnIndex(respawnIndex);
        }
    }
}
