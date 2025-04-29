using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlowerOrderManager : MonoBehaviour
{
    public GameObject heldFlower;

    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;
    public GameObject flower4;
    public GameObject flower5;
    public GameObject dirt;

    public ParticleSystem explosionParticle;
    public AudioClip winSound;
    private AudioSource playerAudio;

    public bool flowerComplete = false;

    private void Start()
    {
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    private List<string> desiredFlowerOrder = new List<string>
        {
            "Purple Flower",
            "Orange Flower",
            "Yellow Flower",
            "Pink Flower",
            "Blue Flower"
        };

    private List<string> startFlowerOrder = new List<string>
        {
            "Blue Flower",
            "Purple Flower",
            "Pink Flower",
            "Orange Flower",
            "Yellow Flower"
        };


    public void PickUpFlower(GameObject gameObject)
    {

        Vector3 pos = gameObject.transform.position;
        Quaternion rot = gameObject.transform.rotation;
        string name = gameObject.name;
        string heldName = heldFlower.name;

        //get index
        int index = startFlowerOrder.IndexOf(name);
        startFlowerOrder[index] = heldName;

        Destroy(gameObject);

        if (heldName == "Dirt Pile")
        {
            pos.y = 0.23f;
        }
        else
        {
            pos.y = 0.828f;

        }

        GameObject newFlower = Instantiate(heldFlower, pos, rot);

        if (name == "Purple Flower")
        {
            heldFlower = flower1;
        }
        else if (name == "Orange Flower")
        {
            heldFlower = flower2;
        }
        else if (name == "Yellow Flower")
        {
            heldFlower = flower3;
        }
        else if (name == "Pink Flower")
        {
            heldFlower = flower4;
        }
        else if (name == "Blue Flower")
        {
            heldFlower = flower5;
        }
        else if (name == "Dirt Pile")
        {
            heldFlower = dirt;
        }

        newFlower.name = heldName;
        checkWin();
    }

    public void checkWin()
    {
        if (startFlowerOrder.SequenceEqual(desiredFlowerOrder))
        {

            explosionParticle.Play();
            playerAudio.PlayOneShot(winSound, 1.0f);

            GameObject statue = GameObject.Find("Statue1");
            GameObject door = GameObject.Find("Door1");

            MoveToRight(statue);
            MoveToRight(door);

            flowerComplete = true;
        }


    }

    private void MoveToRight(GameObject gObject)
    {
        // Get the current position of the object
        Vector3 currentPosition = gObject.transform.position;

        // Calculate the new position to move to the right
        Vector3 newPosition = new Vector3(currentPosition.x + 10, currentPosition.y, currentPosition.z);

        // Set the new position of the object
        gObject.transform.position = newPosition;
    }
}