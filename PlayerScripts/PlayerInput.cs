using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static bool keyLeftPressed, keyRightPressed;
    private static string priorKey = "NONE"; // When pressing left and right, prior is the last key pressed
    public static float direction = 0; // 0 still, > 0 right, < 0 left

    public static bool keyShootPressed, keyPunchPressed;
    public static bool keyJumpPressed, keyJumpHeld, keyDownPlatPressed;

    public static bool keyDancePressed;

    void Update(){
        ManageInput();
        UpdateDirection();
    }

    private void UpdateDirection(){
        if (keyLeftPressed && keyRightPressed){
            if (priorKey == "Left"){
                direction = Input.GetAxisRaw("HorizontalLeft");
                return;
            } else if (priorKey == "Right"){
                direction = Input.GetAxisRaw("HorizontalRight");
                return;
            }   
        }  

        if (keyLeftPressed){
            direction = Input.GetAxisRaw("HorizontalLeft");
            return;
        } else if (keyRightPressed){
            direction = Input.GetAxisRaw("HorizontalRight");
            return;
        } else{
            // nothing pressed
            direction = 0;
        }
    }

    private void ManageInput(){
        DirectionInput();
        JumpInput();
        ShootInput();
        PunchInput();
        DanceInput();
        DownPlat();
    }

    private void DirectionInput(){
        if(Input.GetAxisRaw("HorizontalLeft") == 0 && keyLeftPressed){
            keyLeftPressed = false;
            priorKey = "NONE";
            return;
        }
        if(Input.GetAxisRaw("HorizontalRight") == 0 && keyRightPressed){
            keyRightPressed = false;
            priorKey = "NONE";
            return;
        }
        if(Input.GetAxisRaw("HorizontalLeft") < 0 && !keyLeftPressed){
            keyLeftPressed = true;
            if (keyRightPressed){
                priorKey = "Left";
            }
            return;
        }
        if(Input.GetAxisRaw("HorizontalRight") > 0 && !keyRightPressed){
            keyRightPressed = true;
            if(keyLeftPressed){
                priorKey = "Right";
            }         
            return;
        }
    }

    private void DownPlat(){
        keyDownPlatPressed = Input.GetButtonDown("DownPlat");
    }

    private void JumpInput(){
        keyJumpPressed = Input.GetButtonDown("Jump");
        keyJumpHeld = Input.GetButton("Jump");
    }

    private void ShootInput(){
        keyShootPressed = Input.GetButtonDown("Shoot");
    }

    private void PunchInput(){
        keyPunchPressed = Input.GetButtonDown("Punch");
    }

    private void DanceInput(){
        keyDancePressed = Input.GetButtonDown("Dance");
    }
}
