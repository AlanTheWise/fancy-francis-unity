using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    public float minX, maxX, minY, maxY, xOffset, yOffset;
    float camX, camY;

    public static CameraFollow sharedInstance;

    void Awake()
    {
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    private void LateUpdate(){
        if (target != null){
            camX = Mathf.Clamp(target.position.x + xOffset, minX, maxX);
            camY = Mathf.Clamp(target.position.y + yOffset, minY, maxY);

            transform.position = new Vector3(camX, camY, transform.position.z);
        }
    }
}
