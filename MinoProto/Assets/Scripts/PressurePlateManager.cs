using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PressurePlateManager : MonoBehaviour
{

    private AudioSource playerAudio;
    public AudioClip interactSound;
    public AudioClip winSound;

    public ParticleSystem explosionParticle;

    public bool isComplete = false;

    public Material fmat;
    public Material amat;
    public Material emat;
    public Material dmat;
    public Material cmat;

    public ParticleSystem fPart;
    public ParticleSystem aPart;
    public ParticleSystem ePart;
    public ParticleSystem dPart;
    public ParticleSystem cPart;




    public Material normalMat;




    private List<string> desiredPlateOrder = new List<string>
        {
            "Plate1", 
            "Plate2", 
            "Plate3",
            "Plate4", 
            "Plate5", 
        };

    private List<string> currentPlateOrder = new List<string> { };

    private void Start()
    {
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void pressPlate(GameObject gameObject, Renderer renderer)
    {

        if(gameObject.name == "Plate1")
        {
            gameObject.GetComponent<Renderer>().material = fmat;
            fPart.Play();

        }
        else if (gameObject.name == "Plate2")
        {
            gameObject.GetComponent<Renderer>().material = amat;
            aPart.Play();

        }
        else if (gameObject.name == "Plate3")
        {
            gameObject.GetComponent<Renderer>().material = emat;
            ePart.Play();


        }
        else if (gameObject.name == "Plate4")
        {
            gameObject.GetComponent<Renderer>().material = dmat;
            dPart.Play();

        }
        else if (gameObject.name == "Plate5")
        {
            gameObject.GetComponent<Renderer>().material = cmat;
            cPart.Play();

        }

        Debug.Log(gameObject.name);


        foreach (string item in currentPlateOrder)
        {
            Debug.Log(item);
        }

        if (!currentPlateOrder.Contains(gameObject.name))
        {
            currentPlateOrder.Add(gameObject.name);
            //playerAudio.PlayOneShot(interactSound, 1.0f);
        }
        checkPlates();

    }

    public void checkPlates()
    {
        // Check if the lists have the same length
        if (desiredPlateOrder.Count == currentPlateOrder.Count)
        {
            if (desiredPlateOrder.SequenceEqual(currentPlateOrder) && !isComplete)
            {

                playerAudio.PlayOneShot(winSound, 1.0f);
                explosionParticle.Play();

                GameObject statue = GameObject.Find("Statue2");
                GameObject door = GameObject.Find("Door2");

                MoveToRight(statue);
                MoveToRight(door);

                isComplete = true;

            }
        }

        // Compare each element to see if they are in the same order
        for (int i = 0; i < currentPlateOrder.Count; i++)
        {
            if (desiredPlateOrder[i] != currentPlateOrder[i])
            {
                ResetPlates();
            }
        }
    }

    public void ResetPlates()
    {
        for (int i = 0; i < currentPlateOrder.Count; i++)
        {
            GameObject plate = GameObject.Find(currentPlateOrder[i]);

            plate.GetComponent<Renderer>().material = normalMat;

        }

        currentPlateOrder.Clear();
    }

    private void MoveToRight(GameObject gObject)
    {
        // Get the current position of the object
        Vector3 currentPosition = gObject.transform.position;

        // Calculate the new position to move to the right
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 15);

        // Set the new position of the object
        gObject.transform.position = newPosition;
    }
}
