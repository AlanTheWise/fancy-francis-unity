using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour{

    private GroundDetection gd;
    private EnemyController ec;

    public HealthBar healthBar;

    private float jumpTimer, jumpDelay = 2f;

    private Animator anim;

    private int jumpPatternIndex, jumpVelocityIndex;
    private int[] jumpPattern;
    private int[][] jumpPatterns = new int[][] // 0 low, 1 high
    {
        new int[1] {1},
        new int[2] {0, 1},
        new int[3] {0, 0, 1}
    };

    Vector2 jumpVelocity;
    Vector2[] jumpVelocities;

    private void Start(){
        gd = GetComponentInChildren<GroundDetection>();
        ec = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();

        jumpVelocities = new Vector2[2];
        jumpVelocities[0] = new Vector2(1.0f * ec.rb.gravityScale, 2.0f * ec.rb.gravityScale); // low jump
        jumpVelocities[1] = new Vector2(0.75f * ec.rb.gravityScale, 4.0f * ec.rb.gravityScale); // high jump

        jumpPattern = null;
        jumpTimer = jumpDelay;
    }

    private void Update(){
        healthBar.SetHealth(GetComponent<EnemyController>().currentHealth, GetComponent<EnemyController>().maxHealth);
        if(ec.isFrozen){
            return;
        }

        if(gd.isGrounded){
            ec.rb.velocity = new Vector2(0, ec.rb.velocity.y);
            jumpTimer -= Time.deltaTime;

            if(jumpPattern == null){
                jumpPatternIndex = 0;
                jumpPattern = jumpPatterns[Random.Range(0, jumpPatterns.Length)];
            }

            if (jumpPattern != null){ 
                if(jumpPattern[jumpPatternIndex] == 1 && jumpTimer <= 1){
                    anim.Play("Jump");
                } 
                if(jumpPattern[jumpPatternIndex] == 0 && jumpTimer <= 0.5){
                    anim.Play("ShortJump");
                } 
                if(jumpTimer > 1 && jumpTimer <= 1.7){
                    anim.Play("Idle");
                }
            }

            if(jumpTimer < 0){
                jumpVelocityIndex = jumpPattern[jumpPatternIndex];
                jumpVelocity = jumpVelocities[jumpVelocityIndex];
                if (ec.player.transform.position.x <= transform.position.x){
                    jumpVelocity.x *= -1;
                }
                ec.rb.velocity = new Vector2(ec.rb.velocity.x, jumpVelocity.y);
                if (++jumpPatternIndex > jumpPattern.Length -1){
                    jumpPattern = null;
                }

                jumpTimer = jumpDelay;
            }
        } else{
            ec.rb.velocity = new Vector2(jumpVelocity.x, ec.rb.velocity.y);
        }
    }
}
