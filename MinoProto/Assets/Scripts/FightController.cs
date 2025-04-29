using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    public float leftBound = 0;
    public float rightBound = 4;
    public float topBound = 0;
    public float bottomBound = 2;
    public float playerX = 2;
    public float playerY = 1;
    public GameObject enemy;
    private EnemyController enemyController;
    public ParticleSystem hitParticle;
    public Animator anim;
    public Stats stats;
    private HealthBarManager healthBarManager;
    public Canvas badEndPanel;

    public RawImage attackIcon;
    public Texture attack;
    public Texture attackPressed;
    // Start is called before the first frame update
    void Start()
    {

        enemyController = enemy.GetComponent<EnemyController>();

        hitParticle = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
        stats = GetComponent<Stats>();
        healthBarManager = GameObject.Find("Canvas").GetComponentInChildren<HealthBarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && (playerX > leftBound)) {
            playerX -= 1;
            transform.Translate(Vector3.left * 10);

        }
        if (Input.GetKeyDown(KeyCode.D) && (playerX < rightBound)) {
            playerX += 1;
            transform.Translate(Vector3.right * 10);

        }
        if (Input.GetKeyDown(KeyCode.S) && (playerY < bottomBound)) {
            playerY += 1;
            transform.Translate(Vector3.back * 10);

        }
        if (Input.GetKeyDown(KeyCode.W) && (playerY > topBound)) {
            playerY -= 1;
            transform.Translate(Vector3.forward * 10);

        }
        if (Input.GetMouseButtonDown(0)) {
            attackIcon.texture = attackPressed;
            Invoke("changeIcon", 0.10f);
            Attack();
        }

    }
    void Attack() {
        anim.SetTrigger("Attack");
    }

    void changeIcon(){
        attackIcon.texture = attack;
    }

    void hitCheck() {
        if (playerY < enemyController.enemyY && playerX == enemyController.enemyX) {
            Debug.Log("Damage Done!");
            enemyController.loseHealth(Stats.attackDamage);
        }
    }

    public void loseHealth(int damage) {
        Stats.currentHealth -= damage;
        healthBarManager.setHealth();
        hitParticle.Play();
        anim.SetTrigger("Hit");
        if (Stats.currentHealth <= 0) {
            Debug.Log("You have died...");

            badEndPanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;

        }
    }

    //Notes: make sure to Timer.Stop() and then display time it took to defeat minotaur after defeating minotaur

}
