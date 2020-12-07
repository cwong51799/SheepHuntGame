﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Progression system for the wolves.
public class WolfProgressionMaster : MonoBehaviour
{

    GameObject[] wolves;

    public int Level1Threshold = 5;

    public int Level2Threshold = 15;

    public int Level3Threshold = 50;

    // At level 4, the wolf can eat the farmhand. Remove the Prey script if there is one.
    public int Level4Threshold = 75;

    int sheepConsumed = 0;

    int wolfLevel = 0;

    public float levelScaleFactor = 1.25f;

    public SoundContainer sounds;

    public int getSheepConsumed() {
        return sheepConsumed;
    }

    void reachLevel1() {
        levelUpStats();
        grow();
        wolfLevel = 1;
    }
    // Every level, gain increased stamina, speed, and stamina regen.
    void reachLevel2() {
        levelUpStats();
        grow();
        wolfLevel = 2;
    }

    void reachLevel3(){
        levelUpStats();
        grow();
        wolfLevel = 3;
    }

    void reachLevel4() {
        levelUpStats();
        grow();
        wolfLevel = 4;
        shakeOffAnyPreyComponents();
    }

    void shakeOffAnyPreyComponents() {
        Prey component = this.gameObject.GetComponent<Prey>();
        if(component) {
            Destroy(this.gameObject.GetComponent<Prey>());
        }
    }

    void grow() {
        foreach(GameObject wolf in wolves) {
            WolfMovementScript movementScript = wolf.GetComponent<WolfMovementScript>();
            // Make the wolf larger and louder.
            wolf.gameObject.transform.localScale += new Vector3(.25f,.25f,.25f);
            wolf.gameObject.transform.position += new Vector3(0,-.25f,0);
            movementScript.audibleRange = movementScript.audibleRange * levelScaleFactor;
        }
    }

    // Level up stats
    void levelUpStats() {
        foreach(GameObject wolf in wolves) {
            WolfMovementScript movementScript = wolf.GetComponent<WolfMovementScript>();
            movementScript.maxStamina = movementScript.maxStamina * levelScaleFactor;
            movementScript.baseSpeed = movementScript.baseSpeed * levelScaleFactor;
            movementScript.staminaRegenRate = movementScript.staminaRegenRate * levelScaleFactor;
            sounds.levelUpSound.Play();
        }
    }


    public void checkForLevelUp() {
        if (sheepConsumed >= Level4Threshold && wolfLevel == 3){
            reachLevel4();
        }
        else if(sheepConsumed >= Level3Threshold && wolfLevel == 2){
            reachLevel3();
        } 
        else if(sheepConsumed >= Level2Threshold && wolfLevel == 1) {
            reachLevel2();
        }
        else if(sheepConsumed >= Level1Threshold && wolfLevel == 0) {
            reachLevel1();
        }
        
    }

    public int getWolfLevel() {
        return wolfLevel;
    }

    public void consumeASheep(){ 
        sheepConsumed += 1;
        checkForLevelUp();
    }

    private void Start() {
        wolves = GameObject.FindGameObjectsWithTag("Wolf");
    }

    public void Update() {
        Debug.Log(sheepConsumed);
    }
}