using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float sprintSpeed = 20f;
    public float health = 100f;

    private Rigidbody playerRigidbody;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public FinalRoomManager finalRoomManager;
    public GameObject projectilePrefab;
    public GUIManager gUIManager;
    public bool started = false;

    public AudioClip winSound;
    public AudioClip interactSound;

    public float horizontalInput;
    public float forwardInput;
    private bool isSprinting = false;

    public Vector2 turn;
    public float sense = .5f;
    private float rotationLimit = 10.0f;
    public CameraSwitch cameraSwitch;
    public TimerController timerController;
    private Vector3 PosIn = new Vector3(261.4f, 0.1999999f, -87.69f);
    private Quaternion RotIn = new Quaternion(0, 90, 0, 90);

    // For the pause menu mouse sensitivity
    public Slider slider;

    private GameMaster gm;

    private GameObject skeletons;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameMaster>();


        playerRigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        finalRoomManager = FindObjectOfType<FinalRoomManager>();
        gUIManager = FindAnyObjectByType<GUIManager>();
        
        cameraSwitch = GameObject.Find("CameraController").GetComponent<CameraSwitch>();
        timerController = GameObject.Find("TimerController").GetComponent<TimerController>();

        slider.onValueChanged.AddListener((v) => {
            sense = v;
        });

        finalRoomManager.inText = true;
        if (GameMaster.initialLoad){
            transform.SetPositionAndRotation(PosIn, RotIn);
            Stats.currentHealth = Stats.maxHealth;
            gUIManager.startButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            started = false;
            gm.generateList();
        }

        if (!GameMaster.initialLoad){
            gUIManager.startButton.gameObject.SetActive(false);
            gUIManager.SetEagleEyeCount(cameraSwitch.allowSwitchCount);
            gUIManager.eagleEyeCounter.gameObject.SetActive(true);
            timerController.TimerStart();
            transform.SetPositionAndRotation(gm.lasterPlayerPos, gm.lastPlayerRot);
            gUIManager.introOver = true;
            started = true;
            finalRoomManager.inText = false;
            GameObject.Find("GuideStatue").SetActive(false);
            skeletons = GameObject.Find("Skeletons");
            for (int i = 0; i < skeletons.transform.childCount; i++)
            {
            if (gm.skeletonsDead[i]){
                skeletons.transform.GetChild(i).gameObject.SetActive(false);
            }
            }

        }
        



        //transform.SetPositionAndRotation(PosIn, RotIn);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.initialLoad && started == false && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            gUIManager.startButton.gameObject.SetActive(false);
            gUIManager.SetEagleEyeCount(cameraSwitch.allowSwitchCount);
            gUIManager.eagleEyeCounter.gameObject.SetActive(true);
            timerController.TimerStart();
            transform.SetPositionAndRotation(PosIn, RotIn);
            finalRoomManager.inText = false;
            started = true;
            GameMaster.initialLoad = false;
        }


        if (!cameraSwitch.inTopView)
        {
            if (finalRoomManager.inText == false && gUIManager.inTextGUI == false)
            {



                //     //dev teleport to each room
                //     if (Input.GetKeyDown(KeyCode.Alpha1))
                // {
                //     transform.position = new Vector3(553f, 0.2f, -68f);
                // }
                // else if (Input.GetKeyDown(KeyCode.Alpha2))
                // {
                //     transform.position = new Vector3(544f, 0.2f, 235f);
                // }
                // else if (Input.GetKeyDown(KeyCode.Alpha3))
                // {
                //     transform.position = new Vector3(303f, 0.2f, 125f);
                // }
                // else if (Input.GetKeyDown(KeyCode.Alpha4))
                // {
                //     transform.position = new Vector3(427f, 0.2f, 41f);
                // }

                horizontalInput = Input.GetAxis("Horizontal");
                forwardInput = Input.GetAxis("Vertical");
                turn.x += Input.GetAxis("Mouse X") * sense;
                turn.y += Input.GetAxis("Mouse Y") * sense;
                turn.y = Mathf.Clamp(turn.y, -rotationLimit, rotationLimit);


                isSprinting = Input.GetKey(KeyCode.LeftShift);

                if (Mathf.Abs(horizontalInput) > 0f || Mathf.Abs(forwardInput) > 0f)
                {
                    //Calculate the desired movement direction based on input.
                    Vector3 movement = new Vector3(horizontalInput, 0f, forwardInput).normalized;

                    //Calculate the movement speed.
                    float speed = isSprinting ? sprintSpeed : moveSpeed;

                   //Move the player using Rigidbody.velocity.
                   playerRigidbody.velocity = transform.TransformDirection(movement) * speed;


                }
                else
                {
                    //If there is no input, set the player's velocity to zero to stop movement.
                    playerRigidbody.velocity = Vector3.zero;
                    playerRigidbody.angularVelocity = Vector3.zero;

                }

             
                playerRigidbody.MoveRotation(Quaternion.Euler(0, 90 + turn.x, 0));
            

                //Update the animation.
                float movementAnim = Mathf.Max(Mathf.Abs(horizontalInput), Mathf.Abs(forwardInput));
                playerAnim.SetFloat("Speed_f", movementAnim);
                if (isSprinting)
                {
                    playerAnim.SetBool("Sprint_b", true);
                }
                else
                {
                    playerAnim.SetBool("Sprint_b", false);

                }

            }
            else
            {
                playerAnim.SetFloat("Speed_f", 0f);
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }


        if (finalRoomManager.attackStarted == true)
        {
            gUIManager.FinalRoomHealthPlayer(health);
            gUIManager.attackIcon.gameObject.SetActive(true);
            gUIManager.attackIcon.texture = gUIManager.attack;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAudio.PlayOneShot(interactSound, 1.0f);
                gUIManager.attackIcon.texture = gUIManager.attackPressed;
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
                playerAnim.Play("MeleeAttack_OneHanded");
            }
        }

        if (health <= 0)
        {
            playerAnim.Play("Death");
            gUIManager.FinalRoomHealthOff();
            playerAudio.PlayOneShot(winSound, 1.0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MinoTaur"))
        {
            Debug.Log("HIT" + health);
            playerAudio.PlayOneShot(winSound, 1.0f);
            health -= 25;
        }
    }

}
