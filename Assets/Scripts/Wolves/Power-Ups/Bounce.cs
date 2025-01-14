using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Bounce
    Makes a wolf constantly bounce, probably won't use this.
*/
public class Bounce : MonoBehaviour
{
    
    bool leftGround = false;
    
    float verticalVelocity = 0;

    float gravity = 9.8f;

    public CharacterController controller;

    // Causes the wolf to bounce on all movements
    void bounceMovement(){
        if(controller.isGrounded == false) {
            if(leftGround == false) {
                leftGround = true;
                verticalVelocity = 14;
            }
            verticalVelocity -= gravity * Time.deltaTime;
            Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
            controller.Move(moveVector * Time.deltaTime);
        } else {
            leftGround = false;
            verticalVelocity = 0;
        }
    }

    void Update(){
        bounceMovement();
    }
}