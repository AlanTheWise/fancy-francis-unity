using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour{

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            PlayerHealthController.sharedInstance.ManageDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerHealthController.sharedInstance.ManageDead();
        }
    }
}
