using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour{

    public bool isGrounded;
    public bool inPlatform;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("GroundPlat")){
            isGrounded = true;
            if (other.gameObject.layer == LayerMask.NameToLayer("GroundPlat")){
                inPlatform = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("GroundPlat")){
            isGrounded = false;
            if (other.gameObject.layer == LayerMask.NameToLayer("GroundPlat")){
                inPlatform = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("GroundPlat")){
            isGrounded = true;
            if (other.gameObject.layer == LayerMask.NameToLayer("GroundPlat")){
                inPlatform = true;
            }
        }
    }
}
