using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CreateBot : MonoBehaviour
{

    public GameObject Drone;

    public Transform spawn;

    [SerializeField] private int cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spawning");
        if(other.gameObject.tag == "Player")
        {
            if(HomeBase.totalOre >= cost)
            {
                GameObject tempBot;
                tempBot = (GameObject)Instantiate(Drone, spawn.position, Quaternion.identity);
                HomeBase.totalOre -= cost;
            }
            
        }
        
    }



    
}
