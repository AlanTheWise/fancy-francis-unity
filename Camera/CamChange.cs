using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour{

    public float minX, maxX, minY, maxY, xOffset, yOffset;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            CameraFollow.sharedInstance.xOffset = xOffset;
            CameraFollow.sharedInstance.yOffset = yOffset;
            CameraFollow.sharedInstance.maxX = maxX;
            CameraFollow.sharedInstance.maxY = maxY;
            CameraFollow.sharedInstance.minX = minX;
            CameraFollow.sharedInstance.minY = minY;
        }
    }
}
