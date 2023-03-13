using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private float immuneDuration = 2f;
    [SerializeField] private float incapacitatedDuration = 0.75f; // cant move/jump/shoot... after receiving damage
    [SerializeField] private float flashSpeed = 0.1f;

    [HideInInspector] public bool isIncapacitated;
    [HideInInspector] public bool isImmune;

    private float incapacitatedCounter, immuneCounter, flashCounter;

    // References
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private GroundDetection gd;

    public static PlayerHealthController sharedInstance;

    private void Awake() {
        if(sharedInstance == null){
            sharedInstance = this;
        }
        InitializeHP();
    }

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        gd = GetComponentInChildren<GroundDetection>();
        
        StartCoroutine("IncapacitatedCheck");
        StartCoroutine("ImmunityCheck");
    }

    private void FixedUpdate(){
        if (currentHealth <= 0) return;
        if (isImmune){
            FlashAnimation();
        } else {
            sr.enabled = true;
        }
    }

    private void Update(){
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        anim.SetBool("isIncapacitated", isIncapacitated);
    }

     public void ReceiveDamage(int amount, bool hitFromRight){
        if (isImmune) return;
        DecreaseHP(amount); 
        if (currentHealth <= 0) return;   
        SoundManager.sharedInstance.sounds[5].Play();
        immuneCounter = Time.time;
        incapacitatedCounter = Time.time;
        StartCoroutine(Knockback(0.2f, 300f, hitFromRight));
    }

    private void DecreaseHP(int amount){
        currentHealth -= amount;

        if (currentHealth <= 0) ManageDead();
    }

    public void ManageDead(){
        SoundManager.sharedInstance.sounds[6].Stop();
        currentHealth = 0;
        sr.enabled = false;
        PlayerSpawnManager.sharedInstance.Respawn();
    }

    public void InitializeHP(){
        currentHealth = maxHealth;
    }

    // Animations

    // Used for immunity visual feedback
    private void FlashAnimation(){
        if (Time.time > flashCounter){
            flashCounter = Time.time + flashSpeed;
            sr.enabled = false;
        } else {
            sr.enabled = true;
        }
    }

    // Coroutines for Incapacitated and Immune Check

    IEnumerator IncapacitatedCheck(){
        incapacitatedCounter = Time.time - incapacitatedDuration; // starts capacitated
        while(true){
            if (currentHealth <= 0){
                incapacitatedCounter = Time.time - incapacitatedDuration;
            }
            isIncapacitated = (Time.time - incapacitatedCounter) < incapacitatedDuration;
            yield return null;
        }
    }

    IEnumerator ImmunityCheck(){
        immuneCounter = Time.time - immuneDuration; // player not immune at start
        while(true){
            if (currentHealth <= 0){
                immuneCounter = Time.time - immuneDuration;
            }
            isImmune = (Time.time - immuneCounter) < immuneDuration;
            yield return null;
        }
    }

    IEnumerator Knockback(float duration, float power, bool hitFromRight){
        float timer = 0;
        while (duration > timer){
            timer += Time.deltaTime;
            if (PlayerController.sharedInstance.isFacingRight){
                if(!hitFromRight){
                    rb.AddForce(new Vector2(power, -25));
                    
                } else{
                    rb.AddForce(new Vector2(-power, -25));
                }
                
            } else{
                if(hitFromRight){
                    rb.AddForce(new Vector2(-power, -25));
                } else{
                    rb.AddForce(new Vector2(power, -25));
                }      
            }           
        }
        yield return null;
    }
}
