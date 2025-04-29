using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLightManager : MonoBehaviour
{

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;

    public Material newMaterialRef;
    public Material oldMaterialRef;


    public float rotationAmount = 45.0f; // Amount of rotation in degrees

    public ParticleSystem explosionParticle;
    public AudioClip winSound;
    private AudioSource playerAudio;

    public bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void RotateLight(GameObject gameObject)
    {
        // Rotate the cube by the specified amount around the Y-axis
        gameObject.transform.Rotate(Vector3.up, rotationAmount);
        checkLight();
    }

    public void checkLight()
    {
        if(Mathf.Approximately(light1.transform.rotation.eulerAngles.y, 45f))
        {
            light2.transform.GetChild(0).gameObject.SetActive(true);
            light2.GetComponent<Renderer>().material = newMaterialRef;

            if (Mathf.Approximately(light2.transform.rotation.eulerAngles.y, 180f))
            {
                light3.transform.GetChild(0).gameObject.SetActive(true);
                light3.GetComponent<Renderer>().material = newMaterialRef;


                if (Mathf.Approximately(light3.transform.rotation.normalized.eulerAngles.y, 225f))
                {
                    light4.transform.GetChild(0).gameObject.SetActive(true);
                    light4.GetComponent<Renderer>().material = newMaterialRef;



                    if (Mathf.Approximately(light4.transform.rotation.normalized.eulerAngles.y, 180f))
                    {
                        explosionParticle.Play();
                        playerAudio.PlayOneShot(winSound, 1.0f);

                        Debug.Log("LETS GOO");
                        GameObject statue = GameObject.Find("Statue3");
                        GameObject door = GameObject.Find("Door3");

                        MoveToRight(statue);
                        MoveToRight(door);

                        isComplete = true;

                    }
                }
                else
                {
                    light4.transform.GetChild(0).gameObject.SetActive(false);
                    light4.GetComponent<Renderer>().material = oldMaterialRef;

                }
            }
            else
            {
                light4.transform.GetChild(0).gameObject.SetActive(false);
                light3.transform.GetChild(0).gameObject.SetActive(false);
                light4.GetComponent<Renderer>().material = oldMaterialRef;
                light3.GetComponent<Renderer>().material = oldMaterialRef;

            }
        }
        else
        {
            light4.transform.GetChild(0).gameObject.SetActive(false);
            light3.transform.GetChild(0).gameObject.SetActive(false);
            light2.transform.GetChild(0).gameObject.SetActive(false);
            light4.GetComponent<Renderer>().material = oldMaterialRef;
            light3.GetComponent<Renderer>().material = oldMaterialRef;
            light2.GetComponent<Renderer>().material = oldMaterialRef;

        }
    }

    public void RotateWin()
    {
        Debug.Log("LETS GOO");
        GameObject statue = GameObject.Find("Statue3");
        GameObject door = GameObject.Find("Door3");

        MoveToRight(statue);
        MoveToRight(door);

        isComplete = true;
    }

    private void MoveToRight(GameObject gObject)
    {
        // Get the current position of the object
        Vector3 currentPosition = gObject.transform.position;

        // Calculate the new position to move to the right
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 15);

        // Set the new position of the object
        gObject.transform.position = newPosition;
    }
}
