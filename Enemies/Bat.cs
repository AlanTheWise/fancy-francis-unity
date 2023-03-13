using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour{

    public float speed;

    private EnemyController ec;

    private void Start(){
        ec = GetComponent<EnemyController>();
    }

    private void FixedUpdate(){
        if(ec.isFrozen) return;
        
        ec.rb.velocity = new Vector2(-1 * speed, ec.rb.velocity.y);

        if (transform.position.x - ec.player.transform.position.x < 3 && transform.position.x - ec.player.transform.position.x > 0){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ec.player.transform.position.y), (speed * 2) * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ec.startPos.y), speed * Time.deltaTime);
        }
    }

    private void OnBecameInvisible() {
        if (ec.GetComponent<SpriteRenderer>().enabled){
            Destroy(this.gameObject);
        }
    }
}
