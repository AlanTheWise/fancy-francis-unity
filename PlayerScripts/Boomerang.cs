using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {

    [SerializeField] private float lerpTime;
    [SerializeField] private int damage;

    public GameObject freezedCollider;

    private bool returnToPlayer = false;
    private bool isVisible = true;

    private Vector3 pointA, pointB, pointC, pointD, pointAB, pointBC, pointCD, pointDA;

    private float interpolateAmount, t;

    public bool stop = false;

    private void Start(){
        pointA = GameObject.Find("PointA").transform.position;
        pointB = GameObject.Find("PointB").transform.position;
        pointC = GameObject.Find("PointC").transform.position;
        pointD = GameObject.Find("PointD").transform.position;

        pointAB = new Vector3();
        pointBC = new Vector3();
        pointCD = new Vector3();
        pointDA = new Vector3();

        PlayerController.sharedInstance.isThrowing = true;
    }

    private void Update() {
        if (PlayerHealthController.sharedInstance.currentHealth <= 0){
            BoomerangReturned();
        }

        if(stop){
            GetComponent<Animator>().speed = 0;
            freezedCollider.SetActive(true);
            PlayerController.sharedInstance.isThrowing = false;
        } else{
            GetComponent<Animator>().speed = 1;
            freezedCollider.SetActive(false);
        }

        if(PlayerInput.keyShootPressed){
            if(stop){
                stop = false;
            } else{
                stop = true;
            }
        }

        if(stop) return;

        interpolateAmount += Time.deltaTime;

        t = interpolateAmount/lerpTime;


        if (!returnToPlayer) {
            this.transform.position = QuadraticLerp(pointA, pointB, pointC, t);
        } else {
            this.transform.position = QuadraticLerp(pointC, pointD, PlayerController.sharedInstance.transform.position, t);
        }

        if (t >= 1f && !returnToPlayer){
            GetComponent<SpriteRenderer>().flipX = true;
            returnToPlayer = true;
            interpolateAmount = 0;
            t = 0;
            PlayerController.sharedInstance.isThrowing = false;
        }
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t){
        Vector3 ab = Vector3.Lerp(a, b, t); 
        Vector3 bc = Vector3.Lerp(b, c, t); 

        return Vector3.Lerp(ab, bc, t);
    }

    private void BoomerangReturned(){
        Destroy(this.gameObject);
        PlayerController.sharedInstance.canUseBoomerang = true;
        PlayerController.sharedInstance.isThrowing = false;
    }

    private void OnBecameInvisible(){
        isVisible = false;
    }

    void OnBecameVisible(){
        isVisible = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy"){
            if (isVisible){
                other.gameObject.GetComponent<EnemyController>().ReceiveDamage(damage);          
            }
        }
        if(other.tag == "Player" && returnToPlayer && !stop){
            BoomerangReturned();
        }
    }
}
