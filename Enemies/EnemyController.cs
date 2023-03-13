using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{

    public bool isInmortal;
    public int currentHealth;
    public int maxHealth;
    [SerializeField] int contactDamage = 5;
    public bool isFrozen = true, ignoreFrozen = false, ignoreMoveAtPlayerDeath = false, flipActivated = true;
    public int unFrozeDistance = 8;
    public bool isFacingRight = false;

    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public GameObject player;

    public bool dontDie;

    public GameObject hpDropPrefab;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        InitializeHealth();
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = this.transform.position;
    }

    private void Update() {
        if(PlayerHealthController.sharedInstance.currentHealth == 0 && !dontDie){
            Reset();
        }
        if (ignoreFrozen){
            isFrozen = false;
        } else {
            FreezeCheck();
        }
        
        Flip();
    }

    public void Reset(){
        InitializeHealth();
        if ((transform.position == startPos) || ignoreMoveAtPlayerDeath) return;
        isFrozen = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        transform.position = startPos;
    }

    public void FreezeCheck(){
        if(this.transform.position.x - player.transform.position.x <= unFrozeDistance){
            isFrozen = false;
        }
    }
    
    public void InitializeHealth(){
        currentHealth = maxHealth;
    }

    private void OnTriggerStay2D(Collider2D other) {
        bool hitFromRight;

        if (transform.position.x > other.transform.position.x) {
             hitFromRight = true;
         } else {
             hitFromRight = false;
        }
        if (other.tag == "Player"){
            PlayerHealthController.sharedInstance.ReceiveDamage(contactDamage, hitFromRight);
        }
    }

    public void ReceiveDamage(int amount){

        if (isInmortal) return;

        SoundManager.sharedInstance.sounds[0].Play();

        currentHealth -= amount;

        GetComponent<SpriteRenderer>().enabled = false;

        Invoke("FlashAnimation", 0.1f);

        if (currentHealth <= 0){
            currentHealth = 0;

            if(dontDie){
                GetComponent<SpriteRenderer>().enabled = true;
                return;
            }

            if (hpDropPrefab == null){
                Destroy(this.gameObject);
                return;
            } 

            if(Random.Range(1, 11) <= 2){ // 20% chance to drop
                Instantiate(hpDropPrefab, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }

    public void Flip(){
        if (!flipActivated) return;
        if(transform.position.x < player.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        } else{
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
    }

    public void FlashAnimation(){
        if (currentHealth <= 0) return;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
