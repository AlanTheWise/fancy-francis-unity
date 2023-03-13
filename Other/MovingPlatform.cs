using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float speed;
    public int startingPoint; 
    public Transform[] points;

    public bool activateOnTouch = false;
    public bool dontReturn;

    public GameObject linkedPlat;

    private int i;

    private void Update() {
        if (activateOnTouch) return;
        
        if(dontReturn && i == points.Length) return;

        if(Vector2.Distance(transform.position, points[i].position) < 0.02f){
            i++;
            if(dontReturn && i == points.Length) return;
            if (i == points.Length){
                i = 0;
            } 
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {        
        if (other.tag == "PlayerFeet"){
            if(linkedPlat != null){
                linkedPlat.GetComponent<MovingPlatform>().activateOnTouch = false;
            }
            activateOnTouch = false;
            other.transform.parent.SetParent(this.gameObject.transform);
        } else if (other.tag == "EnemyContact"){
            other.transform.parent.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "PlayerFeet"){
            other.transform.parent.SetParent(null);
        } 
    }
}
