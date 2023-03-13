using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour{
    public int respawnIndex = 0;
    public Transform[] respawnPoints;
    private Transform activeRespawnpoint;
    [HideInInspector] public bool spawned = false;

    public static PlayerSpawnManager sharedInstance;

    private void Awake(){
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    private void Start(){
        activeRespawnpoint = respawnPoints[0];
        MoveToRespawn();
        Invoke("Spawn", 0.5f);
    }

    private void MoveToRespawn(){
        this.transform.position = activeRespawnpoint.position;
        PlayerController.sharedInstance.ResetFacing();
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Spawn(){
        PlayerHealthController.sharedInstance.InitializeHP();
        spawned = true;
        
        if(SceneManager.GetActiveScene().name == "Level1"){
            SoundManager.sharedInstance.sounds[6].Play();
            SoundManager.sharedInstance.sounds[9].Stop();
        } else if(SceneManager.GetActiveScene().name == "Level2"){
            SoundManager.sharedInstance.sounds[8].Play();
            SoundManager.sharedInstance.sounds[9].Stop();
        }
    }

    public void Respawn(){
        // this is to reset moving platforms so they are on the startpos again
        if(SceneManager.GetActiveScene().name == "Level2" && respawnIndex == 0) {
            SceneManager.LoadScene("Level2");
            return;
        }
        if(SceneManager.GetActiveScene().name == "BossLevel") {
            SceneManager.LoadScene("BossLevel");
            return;
        }
        spawned = false;
        Invoke("MoveToRespawn", 0.5f);
        Invoke("Spawn", 1f);
    }

    public void SetRespawnIndex(int respawnIndex){
        this.respawnIndex = respawnIndex;
        activeRespawnpoint = respawnPoints[respawnIndex];
    }
}
