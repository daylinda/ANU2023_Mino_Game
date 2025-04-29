using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setHealth();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(){
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject heart = transform.GetChild(i).gameObject;
            if (i < Stats.maxHealth){
                heart.SetActive(true);
            }
            if (i >= Stats.currentHealth){
                heart.transform.GetChild(0).gameObject.SetActive(false);
            }
            else{
                heart.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }


}
