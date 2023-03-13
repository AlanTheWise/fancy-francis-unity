using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour{
    public int damage = 5;

    private void OnTriggerStay2D(Collider2D other) {    
        bool hitFromRight;
        if (transform.position.x > other.transform.position.x) {
             hitFromRight = true;
         } else {
             hitFromRight = false;
        }
        if (other.tag == "Player"){
            PlayerHealthController.sharedInstance.ReceiveDamage(damage, hitFromRight);
        }
    }
}
