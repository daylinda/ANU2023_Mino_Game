using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public FightController playerController;
    public float enemyX = 2;
    public float enemyY = 1;
    private float leftBound = 0;
    private float rightBound = 4;
    private float topBound = 0;
    private float bottomBound = 2;
    public int maxHealth = 5;
    private int Health;
    public int Damage = 1; 
    public int Range; 
    public int speed; 
    Animator animator;
    ParticleSystem hitParticle;
    public Canvas goodEndPanel;
    public bool isMinotaur = false;
    FightLoader fightLoader; 

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<FightController>();

        if (gameObject.CompareTag("Minotaur")){
            isMinotaur =true;
        }
        Health = maxHealth;
        InvokeRepeating("ChooseAction", 0.5f, 1f);
        animator = gameObject.GetComponent<Animator>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
        fightLoader = FindAnyObjectByType<FightLoader>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChooseAction(){
    if (enemyX == playerController.playerX){
        if(playerController.playerY < enemyY){
            Attack();
        }
        else if (playerController.playerY == enemyY){
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

            animator.SetTrigger("attack");

    }

    void PowerAttack(){
        animator.SetTrigger("powerAttack");

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

        CancelInvoke();
        animator.SetTrigger("death");

        if (isMinotaur){
            Debug.Log("You have slain the Minotaur!");
            Stats.currentHealth = Stats.maxHealth;
            goodEndPanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else{
            Debug.Log("Loading Back In");
            fightLoader.LoadNextFight(0);
        }


        }
    }
}
