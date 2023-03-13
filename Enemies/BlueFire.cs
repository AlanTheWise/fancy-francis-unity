using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFire : MonoBehaviour
{
    private Rigidbody2D rb;
    public int jumpForceY;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Launch");
    }

    void Update(){
        if(rb.velocity.y == 0){
            GetComponent<SpriteRenderer>().enabled = false;
        } else{
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    IEnumerator Launch(){
        while(true){
            yield return new WaitForSeconds(Random.Range(3f,3.5f));
            rb.AddForce(new Vector2(0, jumpForceY), ForceMode2D.Impulse);
        } 
    }
}
