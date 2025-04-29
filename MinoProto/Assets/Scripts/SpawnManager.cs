using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Possible objects to be spawned:
    // Health objects [TBA]
    // Weapons [TBA]
    // Hints

    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> itemPrefabs = new List<GameObject>();

    public List<GameObject> lostSpawnPoints = new List<GameObject>();

    public GameObject lostGuidePrefab;
    public GameObject fireflyPrefab;
    public GameObject potionPrefab;

    public CameraSwitch cameraSwitch;
    public GUIManager gUIManager;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject randSpawn in spawnPoints)
        {
            int randItem = Random.Range(0, itemPrefabs.Count);
            Instantiate(itemPrefabs[randItem], randSpawn.transform.position, randSpawn.transform.rotation);

            cameraSwitch = GameObject.Find("CameraController").GetComponent<CameraSwitch>();
            gUIManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
        }

        foreach(GameObject randSpawn in lostSpawnPoints)
        {
            lostGuidePrefab.gameObject.SetActive(true);
            Instantiate(lostGuidePrefab, randSpawn.transform.position, randSpawn.transform.rotation);
            Instantiate(fireflyPrefab, randSpawn.transform.position, randSpawn.transform.rotation);
        }

        List<GameObject> tempList = lostSpawnPoints;

        for (int i = 0; i < 4; i++)
        {
            int newRand = Random.Range(0, tempList.Count);
            GameObject randSpawn = lostSpawnPoints[newRand];
            tempList.Remove(randSpawn);
            Vector3 adjusted = new Vector3 (3.0f, -3.0f, 0f);

            Instantiate(potionPrefab, randSpawn.transform.position + adjusted, randSpawn.transform.rotation);
        }

    }

    // Health potion increases health
    // Weapon increases attack power
    // Hints add more eagle eye views

    // Update is called once per frame
    void Update()
    {
        //cameraSwitch.allowSwitchCount++ -- amount of switches

    }

    public void UpdateEagleEye()
    {
        cameraSwitch.allowSwitchCount++;
        gUIManager.SetEagleEyeCount(cameraSwitch.allowSwitchCount);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Eagle") && other.CompareTag("Player"))
        {
            Debug.Log("collision!!");
        }
    }
}
