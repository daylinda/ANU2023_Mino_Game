using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance; 
    public Vector3 lasterPlayerPos = new Vector3(261.4f, 0.1999999f, -87.69f); 
    public Quaternion lastPlayerRot = new Quaternion(0, 90, 0, 90);
    public static bool initialLoad = true;
    public bool[] skeletonsDead = new bool[11];
    public static bool potionPickedUp = false;
    public static int lastInteractedSkeleton = 0;
    public static float lastSavedTime;
    void Awake(){
        if (instance == null){
            instance = this;

            DontDestroyOnLoad(instance);

        } else{
            Destroy(gameObject);
        }
        if(initialLoad){
            //Stats.currentHealth = Stats.maxHealth;
            generateList();
        }
    }
 public void generateList(){
            for (int i = 0; i < skeletonsDead.Length; i++)
            {
                skeletonsDead[i] = false;
            }
 }
 public void setTrue(int index){
    lastInteractedSkeleton = index;
    skeletonsDead[index] = true; 
 }
 public void setFalse(int index){
    skeletonsDead[index] = false;
 }
}
