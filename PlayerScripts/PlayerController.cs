using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // Attributes
    [SerializeField] private float movementSpeed, jumpHeight;
    private float counterJumpHeight = 140f;

    [SerializeField] private GameObject boomerangPrefab;
    [SerializeField] private Transform boomerangShootPos;

    [HideInInspector] public bool isFacingRight = true, canUseBoomerang = true;

    // Animation State Parameters
    [HideInInspector]public bool isJumping;
    private bool isIdle, isRunning, isDancing, isThrowingJumping;
    [HideInInspector] public bool isThrowing;

    // References
    private Rigidbody2D rb;
    private GroundDetection gd;
    private Transform tr;
    private Animator anim;

    private Quaternion playerStartRotation;

    private bool collidersOn = true;

    public static PlayerController sharedInstance;

    private void Awake(){
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        gd = GetComponentInChildren<GroundDetection>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        playerStartRotation = transform.rotation;
    }

    private void Update(){
        Animation();

        if (PauseMenu.isPaused) return;

        if(!PlayerSpawnManager.sharedInstance.spawned){
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            return;
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if(PlayerHealthController.sharedInstance.isIncapacitated){
            SetVelocityX(0);
            return;
        }  

        if (isThrowing){
            SetVelocityX(0);
            return;
        }

        bool stillInY = (rb.velocity.y < 0.01) && (rb.velocity.y > -0.01);
        if (PlayerInput.keyJumpPressed && gd.isGrounded && stillInY) Jump();
        if (PlayerInput.keyShootPressed && canUseBoomerang) Shoot();
        if (PlayerInput.keyDancePressed && isIdle) isDancing = true;
        if(PlayerInput.keyDownPlatPressed && collidersOn && gd.inPlatform){
            DisableColliders();
            Invoke("EnableColliders", 0.25f);
        }

        SetVelocityX(PlayerInput.direction);
        UpdateFacing(PlayerInput.direction);
    }

    private void FixedUpdate(){                         
        if(!PlayerInput.keyJumpHeld && IsMovingUpwards()) CounterJump();
    }

    private void SetVelocityX(float direction){
        rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
    }

    private void Jump(){
        Vector2 impulseVelocity = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * (jumpHeight * rb.gravityScale)) * Vector2.up;
        rb.AddForce(impulseVelocity, ForceMode2D.Impulse);
    }

    private void CounterJump(){
        Vector2 counterJumpVelocity = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * (counterJumpHeight * rb.gravityScale)) * Vector2.down;
        rb.AddForce(counterJumpVelocity, ForceMode2D.Force);
    }

    private void Shoot(){
        SoundManager.sharedInstance.sounds[4].Play();
        canUseBoomerang = false;     
        GameObject boomerang = Instantiate(boomerangPrefab, boomerangShootPos.position, Quaternion.identity);
        boomerang.name = "Boomerang";
    }

    public void UpdateFacing(float direction){
        if (direction < 0 && isFacingRight){
            isFacingRight = false;
            tr.Rotate(0f, 180f, 0f);
        } else if (direction > 0 && !isFacingRight){
            isFacingRight = true;
            tr.Rotate(0f, 180f, 0f);
        }
    }

    private void Animation(){
        UpdateAnimationStates();
        UpdateAnimationParameters();
    }

    private void UpdateAnimationParameters(){
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isDancing", isDancing);
        anim.SetBool("isThrowing", isThrowing);
    }

    private void UpdateAnimationStates(){
        bool stillInX = (rb.velocity.x < 0.001) && (rb.velocity.x > -0.1); 
        bool stillInY = (rb.velocity.y < 0.001) && (rb.velocity.y > -0.1); 
        bool pressingMoveInputs = PlayerInput.keyLeftPressed || PlayerInput.keyRightPressed;

        if(PlayerHealthController.sharedInstance.isIncapacitated){
            isIdle = false;
            isRunning = false;
            isJumping = false;
            isThrowing = false;
            isDancing = false;
            return;
        }

        isIdle = (stillInX && stillInY) && !isJumping && gd.isGrounded && !pressingMoveInputs 
                && !isDancing && !isThrowing;
        isRunning = (!stillInX && stillInY) && gd.isGrounded;
        isJumping = (!stillInY && !gd.isGrounded) || !PlayerSpawnManager.sharedInstance.spawned;
        isDancing = isDancing && !(isRunning || isJumping || isThrowing);
    }

    public void ResetFacing(){
        isFacingRight = true;
        transform.rotation = playerStartRotation;
    }

    private bool IsMovingUpwards(){
        return ((Vector2.Dot(rb.velocity, Vector2.up)) > 0);
    }

    public void EnableColliders(){
        this.GetComponentInChildren<BoxCollider2D>().enabled = true;
        GameObject.Find("GroundZone").GetComponentInChildren<BoxCollider2D>().enabled = true;
        collidersOn = true;
    }

    public void DisableColliders(){
        this.GetComponentInChildren<BoxCollider2D>().enabled = false;
        GameObject.Find("GroundZone").GetComponentInChildren<BoxCollider2D>().enabled = false;
        collidersOn = false;
    }
}
