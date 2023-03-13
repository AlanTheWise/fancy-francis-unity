using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float destroyAfter;
    public bool waveMovement = false;

    private Rigidbody2D rb;
    private float startY;
    private float y;
    bool up = true;

    void Start(){
        Destroy(this.gameObject, destroyAfter);
        startY = this.transform.position.y;
    }

    public void Shoot(bool isFacingRight, Quaternion angle){
        Vector3 direction;
        if (isFacingRight){
            direction = angle * Vector3.right;
        } else {
            direction = angle * Vector3.left;
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    private void Update() {
        if(waveMovement){

            if(transform.position.y >= startY + 1.8f){
                up = false;
            } else if(transform.position.y <= startY){
                up = true;
            }
 
            if(up){
                y = transform.position.y + (Time.deltaTime) * 3.5f;
            } else{
                y = transform.position.y - (Time.deltaTime) * 3.5f;
            }

            rb.position = new Vector2(transform.position.x, y);
        }
    }

    public void setDestroyAfter(float time){
        destroyAfter = time;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        bool hitFromRight;
        if(other.tag == "Player"){
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
}
