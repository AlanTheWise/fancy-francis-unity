using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public Sprite btn_pressed, btn_unpressed;
    public GameObject tilemapDoor;
    public bool reverse;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerBullet"){
            this.GetComponent<SpriteRenderer>().sprite = btn_pressed;
            if(tilemapDoor == null) return;
            if(reverse){
                tilemapDoor.gameObject.SetActive(true);
                return;
            }
            tilemapDoor.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "PlayerBullet"){
            this.GetComponent<SpriteRenderer>().sprite = btn_unpressed;
            if(tilemapDoor == null) return;
            if(reverse){
                tilemapDoor.gameObject.SetActive(false);
                return;
            }
            tilemapDoor.gameObject.SetActive(true);
        }
    }
}
