using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonController : MonoBehaviour
{
    public FightController playerController;
    public float enemyX = 2;
    public float enemyY = 1;
    private float leftBound = 0;
    private float rightBound = 4;
    private float topBound = 0;
    private float bottomBound = 2;
    private int maxHealth = 5;
    public int Health = 5;
    public int Damage = 1; 
    public int Range; 
    public int speed; 
    Animator animator;
    ParticleSystem hitParticle;



    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        InvokeRepeating("ChooseAction", 0.5f, 1f);
        animator = gameObject.GetComponent<Animator>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
        playerController = GameObject.Find("Player").GetComponent<FightController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChooseAction(){
    if (enemyX == playerController.playerX){
        if(playerController.playerY < 2){
            Attack();
        }
        else if (playerController.playerY == 2){
            PowerAttack();
        }
    }
    else{
        MoveToPlayer();
    }
}
    void MoveAttack(){
    //Take one move action, then one default attack action.
    MoveToPlayer();
    Invoke("Attack", 0.25f);

}

    void MoveWithinBounds(Vector3 direction){
        if ((direction == Vector3.left) && enemyX < rightBound){
            enemyX +=1;

            transform.Translate(direction*10);
        }
        if ((direction == Vector3.right) && enemyX > leftBound){
            enemyX -=1;
            transform.Translate(direction*10);
        }
        if ((direction == Vector3.forward) && enemyY < bottomBound ){
            enemyY +=1;
            transform.Translate(direction*10);

        }
        if ((direction == Vector3.back) && enemyY > topBound){
            enemyY -=1;
            transform.Translate(direction*10);
        }
    }
    void MoveToPlayer(){
        //Take a move action towards the player, prioritising horizontal movement

        if (enemyX < playerController.playerX){
            MoveWithinBounds(Vector3.left);

                }
        if (enemyX > playerController.playerX){
            MoveWithinBounds(Vector3.right);

                }

        else{
            if(enemyY > playerController.playerY+1){
                MoveWithinBounds(Vector3.back);

            }
            if(enemyY < playerController.playerY+1){
                MoveWithinBounds(Vector3.forward);

            }

        }
    }
void DoubleAttack(){
    Attack();
    Invoke("Attack", 0.25f);
}
    void Attack(){
        //Attack two squares ahead

        if (enemyX == playerController.playerX && enemyY > playerController.playerY){
            animator.SetTrigger("lightAttack");

    }}

    void PowerAttack(){
        animator.SetTrigger("heavyAttack");

    }

    void hitCheck(int range){
        if (enemyX == playerController.playerX && enemyY >= (playerController.playerY+ (3-range))){
            Debug.Log("Damage Taken!");
            playerController.loseHealth(Damage);
       
        }



    }
    public void loseHealth(int damage){
    Health-=damage;
    hitParticle.Play();
    if (Health<=0){
        Debug.Log("Skeleton Defeated");
        animator.SetTrigger("death");
        CancelInvoke();
        }
    }
}
